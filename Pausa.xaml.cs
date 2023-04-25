using System;
using System.Collections.Generic;
using System.Drawing;
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
    public sealed partial class Pausa : Page
    {
        int modo = -1;
        public Pausa()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            modo = (int)e.Parameter;
            if (modo == 5 || modo == 6) //si se ha elegido una modalidad del menu multijugador se desactivan los botones propios del singleplayer
            {
                SaveButton.IsEnabled = false;
                RetryButton.IsEnabled = false;
            }
        }
        private void ReanudeButton_Click(object sender, RoutedEventArgs e)
        {
            App.TryGoBack();
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Configuracion));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "¡Partida Guadada con éxito!",
                MaxWidth = this.ActualWidth,
                PrimaryButtonText = "Listo",
            };
            dialog.ShowAsync();
        }

        private void RetryButton_Click(object sender, RoutedEventArgs e)
        {
            App.TryGoBack();
        }

        private void GiveUpButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "¡Te has rendido!",
                MaxWidth = this.ActualWidth,
                PrimaryButtonText = "Listo",
            };
            dialog.ShowAsync();
        }
    }
}
