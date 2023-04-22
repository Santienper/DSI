using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace Trabajo_DSI
{
    internal class BarTimer
    {
        //Para manejar el timer
        public DispatcherTimer TimerBar;
        public ProgressBar progress = null;
        public double valor = 0;
        public float velocity = 0.7f;


        //constructor del timer con la barra de progreso
        public BarTimer(ProgressBar Bar, double value)
        {
            progress = Bar;
            valor = value;
            TimerSetup(); //Seteamos el tick del timer
        }
        //Metodo que incia el timer
        public void TimerSetup()
        {
            TimerBar = new DispatcherTimer();
            TimerBar.Tick += Timer_Tick;
            TimerBar.Interval = new TimeSpan(100000);
        }
        //Update / tick del timer que indica que debe hacer en cada 0.01 seg
        void Timer_Tick(object sender, object e)
        {
            if (progress.Value <= progress.Maximum)
            {
                valor += 1 * velocity;
                progress.Value = valor;
            }

        }
    }
}
