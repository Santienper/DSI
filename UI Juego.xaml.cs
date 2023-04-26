using P4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Gaming.Input;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Input;
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
    public sealed partial class UI_Juego : Page, INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        int modo = -1;
        bool Arrastra = false;
        PointerPoint PtArrastreMundo = null;
        //Mando
        Controlador control = null;
        private DispatcherTimer timer;

        public int NumCiudadanos = 0;

        public UI_Juego() {
            this.InitializeComponent();
            control =new Controlador();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(100000);
            timer.Tick += (object sender, object e) => {
                // Loop de juego
                control.Input(); // Leer input
                UpdateUI();     // Actualizar IU
            };
        }

        public InstanciaUnidad[] Unidades = { };
        public InstanciaUnidad selected { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            createEntity("Casa", 50, 100);
            //createEntity("Puntero", 50,200);
            //modo = (int)e.Parameter;
            timer.Start();
            base.OnNavigatedTo(e);
            
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            timer.Stop();
            base.OnNavigatedFrom(e);
        }

        private void PauseBottom_Click(object sender, RoutedEventArgs e) {
            Frame.Navigate(typeof(Pausa),modo);
        }

        private void Entity_KeyDown(object sender, KeyRoutedEventArgs e) {
            Button MiEntidad = sender as Button;
            CompositeTransform Transformacion = MiEntidad.RenderTransform as CompositeTransform;
            double X = 0, Y = 0;
            float zoom = 0;

            switch (e.Key)
            {
                case VirtualKey.A: //movimiento a través de las teclas
                case VirtualKey.GamepadRightThumbstickLeft:
                    //X = -10;
                    //break;
                case VirtualKey.D:
                case VirtualKey.GamepadRightThumbstickRight:
                    //X = +10;
                    //break;
                case VirtualKey.W:
                case VirtualKey.GamepadRightThumbstickUp:
                    //Y = -10;
                    //break;
                case VirtualKey.S:
                case VirtualKey.GamepadRightThumbstickDown:
                    //Y = +10;
                    //break;
                case VirtualKey.GamepadLeftShoulder:
                    //zoom -= 0.1f;
                    //e.Handled = true;
                    //break;
                case VirtualKey.GamepadRightShoulder:
                    //zoom += 0.1f;
                    //e.Handled = true;
                    //break;
                case VirtualKey.GamepadView:
                case VirtualKey.GamepadDPadUp:
                case VirtualKey.GamepadDPadDown:
                case VirtualKey.GamepadDPadLeft:
                case VirtualKey.GamepadDPadRight:
                    e.Handled = true;
                    break;
            }
            Transformacion.TranslateX += X / MiScroll.ZoomFactor;
            Transformacion.TranslateY += Y / MiScroll.ZoomFactor;
            MiScroll.ChangeView(MiScroll.HorizontalOffset, MiScroll.VerticalOffset, MiScroll.ZoomFactor + zoom);
            MiEntidad.RenderTransform = Transformacion;

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
            if (un.Nombre != "Casa") Boton.KeyDown += Entity_KeyDown; 
            CompositeTransform Transformacion = new CompositeTransform();
            Transformacion.TranslateX = 0.0; Transformacion.TranslateY = 0.0; Transformacion.Rotation = 0;
            Boton.RenderTransform = Transformacion;
            Mundo.Children.Add(Boton);
            Boton.SetValue(Canvas.LeftProperty, x);
            Boton.SetValue(Canvas.TopProperty, y);
            Boton.ManipulationMode = ManipulationModes.All;
            Boton.ManipulationDelta += Unidad_ManipulationDelta;
        }
        private void Unidad_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e) {
            Button Unidad = sender as Button;
            CompositeTransform Transformacion = Unidad.RenderTransform as CompositeTransform;
            Transformacion.TranslateX += e.Delta.Translation.X / MiScroll.ZoomFactor;
            Transformacion.TranslateY += e.Delta.Translation.Y / MiScroll.ZoomFactor;
            Transformacion.Rotation += e.Delta.Rotation;
            Unidad.RenderTransform = Transformacion;
        }
        private void Acciones_DragItemsStarting(object sender, DragItemsStartingEventArgs e) {
            Unidad un = e.Items[0] as Unidad;
            e.Data.SetText(un.id);
        }
        private void Acciones_ItemClick(object sender, ItemClickEventArgs e){
            Unidad un = e.ClickedItem as Unidad;
            createEntity(un.id, 50, 50);
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

        private void MiScroll_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var Puntero = e.GetCurrentPoint(e.OriginalSource as Image);
            if (Puntero.Properties.IsRightButtonPressed == true)
            {
                Arrastra = true;
                PtArrastreMundo = Puntero;
                Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Hand, 0);
            }
        }

        private void MiScroll_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint Pt = e.GetCurrentPoint(Mundo);
            ScrollViewer Scroll = sender as ScrollViewer;
            if (Arrastra == true)
            {
                Scroll.ChangeView(
                    Scroll.HorizontalOffset - (Pt.Position.X - PtArrastreMundo.Position.X),
                    Scroll.VerticalOffset - (Pt.Position.Y - PtArrastreMundo.Position.Y),
                    Scroll.ZoomFactor);
                e.Handled = false;
            }
        }

        private void MiScroll_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Arrastra = false;
            PtArrastreMundo = null;
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, 0);
        }

        private void MiScroll_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Arrastra = false;
            PtArrastreMundo = null;
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, 0);
        }
        private void UpdateUI() {
            Button Entidad = FocusManager.GetFocusedElement() as Button;

            if(Entidad?.Parent == Mundo) {
                CompositeTransform Transformacion = Entidad.RenderTransform as CompositeTransform;
                Transformacion.TranslateX += control.data.lx * 10 / MiScroll.ZoomFactor;
                Transformacion.TranslateY += control.data.ly * 10 / MiScroll.ZoomFactor;
                if(control.gpInput) {
                    Point cursor = control.cursor;
                    cursor.X += control.data.rx * 10 / MiScroll.ZoomFactor;
                    cursor.Y += control.data.ry * 10 / MiScroll.ZoomFactor;
                    CoreWindow.GetForCurrentThread().PointerPosition = cursor;
                }
                Entidad.RenderTransform = Transformacion;
                MiScroll.ChangeView(MiScroll.HorizontalOffset, MiScroll.VerticalOffset, MiScroll.ZoomFactor + (float)control.data.zoom);
            }
        }
    }
}
