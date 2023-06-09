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
    public sealed partial class PantallaCarga : Page
    {
        //Timer/ bucle para progress bar
        BarTimer barTimer = null;
        int modo = -1;

        public PantallaCarga()
        {
            double valor = 0;
            this.InitializeComponent();
            barTimer = new BarTimer(BarPartida, valor);
            barTimer.TimerBar.Start();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            modo = (int)e.Parameter;
        }
        private void BarPartida_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (BarPartida.Value == BarPartida.Maximum) { Frame.Navigate(typeof(UI_Juego), modo); }
        }
    }
}
