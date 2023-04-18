using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Trabajo_DSI
{
    public class Mode
    {
        public int Id { get; set; }
        public string Explicacion { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }

        public string Color { get; set; }


        public Mode() { }
    }
    public class ModesInformation
    {
        public static List<Mode> Modes = new List<Mode>()
        {
            new Mode()
            {
                Id=0,
                Nombre="UN JUGADOR (vs CPU)",
                Explicacion="MODO DE UN JUGADOR\nEnfrentante en solitario al modo Campaña.",
                Imagen="Assets\\Images\\nueva partida iron man.jpg",
                Color="#FFFFF760",
            },
            new Mode()
            {
                Id=1,
                Nombre="MULTIJUGADOR",
                Explicacion="MODO MULTIJUGADOR\nEnfrentante a jugadores de todo el mundo o con amigos.",
                Imagen="Assets\\Images\\vengadores multijugador.jpg",
                Color="#96ff6a",
            }
        };

        public static IList<Mode> GetAllModes()
        {
            return Modes;
        }
    }
    
}
