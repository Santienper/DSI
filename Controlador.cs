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

namespace P4 {
    internal class Controlador {
        //Para manejar los mandos
        private readonly object Lock = new object();
        private List<Gamepad> Gamepads = new List<Gamepad>();
        public Gamepad mainGamepad = null;
        //Lectura y escritura de los mandos
        private GamepadReading reading;
        public Windows.Foundation.Point cursor, previousCursor;
        public bool gpInput, mouseInput, kbInput;

        public struct GPData {
            public double lx, ly, rx, ry, zoom;
            public void reset() { lx = ly = rx = ry = zoom = 0; }
        }

        public GPData data;

        public Controlador() {
            Gamepad.GamepadAdded += (object sender, Gamepad e) => {
                lock(Lock) {
                    if(!Gamepads.Contains(e)) {
                        Gamepads.Add(e);
                        if(Gamepads.Count == 1) {
                            mainGamepad = Gamepads[0];
                        }
                    }
                }
            };

            Gamepad.GamepadRemoved += (object sender, Gamepad e) => {
                lock(Lock) {
                    int i = Gamepads.IndexOf(e);
                    if(i > -1) {
                        if(mainGamepad == Gamepads[i]) {
                            Gamepads.RemoveAt(i);
                            mainGamepad = Gamepads[0];
                        } else Gamepads.RemoveAt(i);
                    }
                }
            };
        }

        //Teclado de forma continua
        private bool IsKeyDown(CoreWindow core, VirtualKey key) {
            return (core.GetKeyState(key) & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
        }

        public bool IsKeyDown(VirtualKey key) {
            return IsKeyDown(CoreWindow.GetForCurrentThread(), key);
        }

        public void Input() {
            if(mainGamepad != null) {
                reading = mainGamepad.GetCurrentReading();
            }

            data.reset(); kbInput = gpInput = mouseInput = false;
            CoreWindow core = CoreWindow.GetForCurrentThread();
            if(IsKeyDown(core, VirtualKey.W)) { data.ly -= 1; kbInput = true; }
            if(IsKeyDown(core, VirtualKey.D)) { data.lx += 1; kbInput = true; }
            if(IsKeyDown(core, VirtualKey.S)) { data.ly += 1; kbInput = true; }
            if(IsKeyDown(core, VirtualKey.A)) { data.lx -= 1; kbInput = true; }
            if(IsKeyDown(core, VirtualKey.Q)) { data.zoom -= 0.05; kbInput = true; }
            if(IsKeyDown(core, VirtualKey.E)) { data.zoom += 0.05; kbInput = true; }
            if(IsKeyDown(core, VirtualKey.GamepadRightShoulder)) { data.zoom += 0.05; gpInput = true; }
            if(IsKeyDown(core, VirtualKey.GamepadLeftShoulder)) { data.zoom -= 0.05; gpInput = true; }
            if(reading.LeftThumbstickX > 0.1 || reading.LeftThumbstickX < -0.1) {
                data.lx += reading.LeftThumbstickX; gpInput = true;
            }
            if(reading.LeftThumbstickY > 0.1 || reading.LeftThumbstickY < -0.1) {
                data.ly -= reading.LeftThumbstickY; gpInput = true;
            }
            if(reading.RightThumbstickX > 0.1 || reading.RightThumbstickX < -0.1) {
                data.rx += reading.RightThumbstickX; gpInput = true;
            }
            if(reading.RightThumbstickY > 0.1 || reading.RightThumbstickY < -0.1) {
                data.ry -= reading.RightThumbstickY; gpInput = true;
            }
            previousCursor = cursor;
            cursor = CoreWindow.GetForCurrentThread().PointerPosition;
            mouseInput = cursor != previousCursor;
        }

        public void InvalidateKeys(KeyRoutedEventArgs e) {
            switch(e.Key) {
                case VirtualKey.W:
                case VirtualKey.A:
                case VirtualKey.S:
                case VirtualKey.D:
                case VirtualKey.GamepadDPadUp:
                case VirtualKey.GamepadDPadDown:
                case VirtualKey.GamepadDPadLeft:
                case VirtualKey.GamepadDPadRight:
                case VirtualKey.GamepadLeftThumbstickUp:
                case VirtualKey.GamepadLeftThumbstickLeft:
                case VirtualKey.GamepadLeftThumbstickDown:
                case VirtualKey.GamepadLeftThumbstickRight:
                case VirtualKey.GamepadRightThumbstickUp:
                case VirtualKey.GamepadRightThumbstickLeft:
                case VirtualKey.GamepadRightThumbstickDown:
                case VirtualKey.GamepadRightThumbstickRight:
                case VirtualKey.GamepadLeftShoulder:
                case VirtualKey.GamepadRightShoulder:
                case VirtualKey.GamepadView:
                    e.Handled = true;
                    break;
            }
        }
    }
}

