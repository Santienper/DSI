using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace Trabajo_DSI {
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class UI_Juego : Page, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        int modo = -1;

        public UI_Juego() {
            this.InitializeComponent();
        }

        public InstanciaUnidad[] Unidades = { };
        public InstanciaUnidad selected { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            createEntity("Casa", 50, 100);
            modo = (int)e.Parameter;
            base.OnNavigatedTo(e);
        }

        private void PauseBottom_Click(object sender, RoutedEventArgs e) {
            Frame.Navigate(typeof(Pausa),modo);
        }

        private void Entity_KeyDown(object sender, KeyRoutedEventArgs e) {

        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button a = sender as Button;
            selected = a.Tag as InstanciaUnidad;
            List<Unidad> accs = new List<Unidad>();
            foreach(string id in Unidad.Unidades[selected.id].Lista) {
                accs.Add(Unidad.Unidades[id]);
            }
            Acciones.ItemsSource = accs;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selected)));
        }

        private void createEntity(string id, double x, double y) {
            Unidad un = Unidad.Unidades[id];
            Image Imagen = new Image(); Imagen.Source = un.Source;
            Button Boton = new Button(); Imagen.Width = Imagen.Height = 30;
            Boton.Content = Imagen; Boton.Click += Button_Click;
            Boton.Tag = new InstanciaUnidad(id);
            ContentControl ControladorContenido = new ContentControl();
            ControladorContenido.Content = Boton; ControladorContenido.IsTabStop = true; ControladorContenido.UseSystemFocusVisuals = true;
            ControladorContenido.KeyDown += Entity_KeyDown;
            CompositeTransform Transformacion = new CompositeTransform();
            Transformacion.TranslateX = 0.0; Transformacion.TranslateY = 0.0; Transformacion.Rotation = 0;
            ControladorContenido.RenderTransform = Transformacion;
            Mundo.Children.Add(ControladorContenido);
            ControladorContenido.SetValue(Canvas.LeftProperty, x);
            ControladorContenido.SetValue(Canvas.TopProperty, y);
        }

        private void Acciones_DragItemsStarting(object sender, DragItemsStartingEventArgs e) {
            Unidad un = e.Items[0] as Unidad;
            e.Data.SetText(un.id);
        }

        private void Map_DragOver(object sender, DragEventArgs e) {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private async void Map_Drop(object sender, DragEventArgs e) {
            if(e.DataView.Contains(StandardDataFormats.Text)) {
                Point punto = e.GetPosition(Mundo);
                createEntity(await e.DataView.GetTextAsync(), punto.X, punto.Y);
            }
        }
    }
}
