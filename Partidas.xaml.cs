﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace Trabajo_DSI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Partidas : Page
    {
        int modo = -1; 
        public Partidas()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            modo = (int)e.Parameter;
            if (modo == 3 || modo == 4) TextPartidas.Text = "SELECCIONA UNA PARTIDA";
            else TextPartidas.Text = "ELIGE UN ARCHIVO VACÍO";
            
        }
        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            App.TryGoBack();
        }

        private void ButtonStack1_Click(object sender, RoutedEventArgs e)
        {
            if (modo == 3 || modo == 4) Frame.Navigate(typeof(UI_Juego), modo); //pasamos el modo de juego a la UI del Juego, que despues se enviara al menu de pausa
        }
        //archivos vacios
        private void ButtonStack2_Click(object sender, RoutedEventArgs e)
        {
            if (modo == 2) Frame.Navigate(typeof(UI_Juego), modo); 
        }

        private void ButtonStack3_Click(object sender, RoutedEventArgs e)
        {
            if (modo == 2) Frame.Navigate(typeof(UI_Juego), modo);
        }
    }
}
