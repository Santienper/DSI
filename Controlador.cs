using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Text;
using Windows.Gaming.Input;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace P4
{
    public struct DeltaTeclado {public double X, Y, Z;}
    
    internal class Controlador
    {
        //Para manejar los mandos
        private readonly object myLock = new object();
        private List<Gamepad> myGamepads = new List<Gamepad>();
        public Gamepad mainGamepad = null;
        //Lectura y escritura de los mandos
        public GamepadReading reading, prereading;
        private GamepadVibration vibration;

        //Cambios por teclado
        
        DeltaTeclado delta;

        // Teclado por defecto
        VirtualKey Izquierda = VirtualKey.A;
        VirtualKey Izquierda1 = VirtualKey.GamepadDPadLeft;
        VirtualKey Derecha = VirtualKey.D;
        VirtualKey Derecha1 = VirtualKey.GamepadDPadRight;
        VirtualKey Arriba = VirtualKey.W;
        VirtualKey Arriba1 = VirtualKey.GamepadDPadUp;
        VirtualKey Abajo = VirtualKey.S;
        VirtualKey Abajo1 = VirtualKey.GamepadDPadDown;
        VirtualKey ZoomMas = VirtualKey.C;
        VirtualKey ZoomMas1 = VirtualKey.GamepadRightShoulder;
        VirtualKey ZoomMen = VirtualKey.Z;
        VirtualKey ZoomMen1 = VirtualKey.GamepadLeftShoulder;


        //Varibles Gesto
        public bool Gesto = false;
        float TiempoGesto = 0;
        int EstadoGesto = 0;



        //Teclado de forma continua
        public bool KeyIsDown(VirtualKey key)
        {
            var keystate = CoreWindow.GetForCurrentThread().GetKeyState(key);
            return (keystate & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
        }

        public DeltaTeclado KeyContinuo()
        { 
            delta.X = 0.0; delta.Y = 0.0; delta.Z = 0.0;
            
            if (KeyIsDown(Izquierda) | KeyIsDown(Izquierda1)) delta.X = -1;
            if (KeyIsDown(Derecha) | KeyIsDown(Derecha1)) delta.X = +1;
            if (KeyIsDown(Arriba) | KeyIsDown(Arriba1)) delta.Y = -1;
            if (KeyIsDown(Abajo) | KeyIsDown(Abajo1)) delta.Y = +1;
            if (KeyIsDown(ZoomMas) | KeyIsDown(ZoomMas1)) delta.Z = +0.1;
            if (KeyIsDown(ZoomMen) | KeyIsDown(ZoomMen1)) delta.Z = -0.1;
            return delta;
        }

        //Keyboard por eventos
        public DeltaTeclado KeyDown(KeyRoutedEventArgs e)
        {
            delta.X = 0.0;delta.Y = 0.0;delta.Z= 0.0;
            if ((e.Key==Izquierda)| (e.Key == Izquierda1)) delta.X = -10;
            if ((e.Key == Derecha) | (e.Key == Derecha1)) delta.X = +10;
            if ((e.Key == Arriba) | (e.Key == Arriba1)) delta.Y = -10;
            if ((e.Key == Abajo) | (e.Key == Abajo1)) delta.Y= +10;
            if ((e.Key == ZoomMas) | (e.Key == ZoomMas1)) delta.Z = +0.1;
            if ((e.Key == ZoomMen) | (e.Key == ZoomMen1)) delta.Z = -0.1;
            return delta;    
            
        }
        public Controlador ()
        {
            Gamepad.GamepadAdded += (object sender, Gamepad e) =>
            {
                // Check if the just-added gamepad is already in myGamepads;
                // if it isn't, add it.
                lock (myLock)
                {
                    bool gamepadInList = myGamepads.Contains(e);
                    if (!gamepadInList)
                    {
                        myGamepads.Add(e);
                        mainGamepad = myGamepads[0];
                    }
                }
            };

            Gamepad.GamepadRemoved += (object sender, Gamepad e) =>
            {
                lock (myLock)
                {
                    int indexRemoved = myGamepads.IndexOf(e);
                    if (indexRemoved > -1)
                    {
                        if (mainGamepad == myGamepads[indexRemoved])
                        {
                            mainGamepad = null;
                        }
                        myGamepads.RemoveAt(indexRemoved);
                    }
                }
            };
        }
        public void LeeMando()
        {
            if (mainGamepad != null)
            {
                prereading = reading;
                reading = mainGamepad.GetCurrentReading();
            }
        }
        public bool DetectaGestosMando()
        {
            TiempoGesto = TiempoGesto * 0.9f;
            if (TiempoGesto < 0.3)
            { TiempoGesto = 0.0f; EstadoGesto = 0; Gesto = false; }

            if ((reading.RightThumbstickX > 0.0) & (reading.RightThumbstickY > 0.0))
            { EstadoGesto = 1; TiempoGesto = 1.0f; }

            if ((EstadoGesto == 1) & (TiempoGesto > .3f) & (reading.RightThumbstickX > 0.0) & (reading.RightThumbstickY < 0.0))
            { EstadoGesto = 2; TiempoGesto = 1.0f; }

            if ((EstadoGesto == 2) & (TiempoGesto > .3f) & (reading.RightThumbstickX < 0.0) & (reading.RightThumbstickY < 0.0))
            { EstadoGesto = 3; TiempoGesto = 1.0f; }

            if ((EstadoGesto == 3) & (TiempoGesto > .3f) & (reading.RightThumbstickX < 0.0) & (reading.RightThumbstickY > 0.0))
            { EstadoGesto = 0; TiempoGesto = 0.0f; Gesto = true; }

            return Gesto;
        }

        public void ZMMando()
        {
            if (reading.RightThumbstickX < -0.1) reading.RightThumbstickX += 0.1;
            else if (reading.RightThumbstickX > 0.1) reading.RightThumbstickX -= 0.1;
            else reading.RightThumbstickX = 0;

            if (reading.RightThumbstickY < -0.1) reading.RightThumbstickY += 0.1;
            else if (reading.RightThumbstickY > 0.1) reading.RightThumbstickY -= 0.1;
            else reading.RightThumbstickY = 0;
        }

        public void FeedBack()
        {
            if (mainGamepad != null)
            {
                // ... set vibration levels on vibration struct here
                if ((reading.RightThumbstickX != 0) | (reading.RightThumbstickY != 0))
                    if (reading.RightThumbstickX * reading.RightThumbstickX > reading.RightThumbstickY * reading.RightThumbstickY)
                        vibration.RightMotor = reading.RightThumbstickX * reading.RightThumbstickX;
                    else
                        vibration.RightMotor = reading.RightThumbstickY * reading.RightThumbstickY;
                else
                    vibration.RightMotor = 0;

                if ((reading.LeftTrigger != 0) | (reading.RightTrigger != 0))
                    if (reading.LeftTrigger > reading.RightTrigger)
                        vibration.LeftMotor = reading.LeftTrigger;
                    else
                        vibration.LeftMotor = reading.RightTrigger;
                else
                    vibration.LeftMotor = 0;
                // copy the GamepadVibration struct to the gamepad
                mainGamepad.Vibration = vibration;
            }
        }
    }
}

