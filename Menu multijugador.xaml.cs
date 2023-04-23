using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace Trabajo_DSI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Menu_multijugador : Page
    {
        public ObservableCollection<Mode> ListaModos { get; } = new ObservableCollection<Mode>();
        bool seleccionado;
        int modoSel = -1; //-1 Ningun modo seleccionado
        public Menu_multijugador()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Cosntruye las listas de ModelView a partir de la lista Modelo 
            if (ListaModos != null)
                foreach (Mode modo in ModesInformation.MultiplayerModes())
                {
                    ViewMode VMode = new ViewMode(modo);
                    ListaModos.Add(VMode);
                }
            base.OnNavigatedTo(e);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            App.TryGoBack();
        }

        private async void  PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (modoSel == 5) Frame.Navigate(typeof(PantallaCarga),modoSel);
            else if (modoSel == 6) //mostramos contentDialog para que el jugador introduzca clave de amigo/contraseña
            {
                TextBox input = new TextBox()
                {
                    Height = (double)Application.Current.Resources["TextControlThemeMinHeight"],
                    PlaceholderText = "Codigo Amigo",
                    
                };
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "Introduce el codigo de amigo.",
                    MaxWidth = this.ActualWidth,
                    PrimaryButtonText = "OK",
                    SecondaryButtonText = "Cancel",
                    Content = input,
                };
                ContentDialogResult result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    input = (TextBox)dialog.Content;
                    Frame.Navigate(typeof(PantallaCarga), modoSel);
                }
            }
        }
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Mode Sel = e.ClickedItem as Mode;
            Description.Text = Sel.Explicacion;
            modoSel = Sel.Id;
            seleccionado = true;
            BotonJugar.IsEnabled = true;
        }
    }
}
