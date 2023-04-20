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
            //modos del menu principal
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
            },

            //Modos del menu de un jugador
            
            new Mode()
            {
                Id=2,
                Nombre="NUEVA PARTIDA",
                Explicacion="Empieza una nueva batalla desde el principio contra una CPU.",
                Imagen="Assets\\Images\\nueva partida iron man.jpg",
                Color="#FFFFF760",
            },
            new Mode()
            {
                Id=3,
                Nombre="CARGAR PARTIDA",
                Explicacion="Continúa con la batalla contra la CPU desde un punto de guardado.",
                Imagen="Assets\\Images\\nueva partida iron man.jpg",
                Color="#FFFFF760",
            },
            new Mode() 
            {
                Id=4,
                Nombre="MODO CAMPAÑA",
                Explicacion="Emprende una increíble aventura a través de distintas misiones y niveles.",
                Imagen="Assets\\Images\\nueva partida iron man.jpg",
                Color="#FFFFF760",
            },

            //Modos del menu multijugaodr
            new Mode()
            {
                Id=5,
                Nombre="MODO ONLINE",
                Explicacion="Empieza una batalla con jugadores de todo el mundo",
                Imagen="Assets\\Images\\nueva partida iron man.jpg",
                Color="#96ff6a",
            },
            new Mode()
            {
                Id=6,
                Nombre="MODO CON AMIGOS",
                Explicacion="Combate con tus amigos a través de una clave.",
                Imagen="Assets\\Images\\nueva partida iron man.jpg",
                Color="#96ff6a",
            },
        };

        public static IList<Mode> GetPrincipalModes() //recoge SOLO los modos del menu principal
        {
            List<Mode> principalModes = new List<Mode>();

            for(int i = 0; i < 2; i++)
            {
                principalModes.Add(Modes[i]);
            }
            return principalModes;
        }

        public static IList<Mode> OnePlayerModes() //recoge SOLO los modos del menu de un jugador
        {
            List<Mode> playerModes = new List<Mode>();

            for (int i = 2; i < 5; i++)
            {
                playerModes.Add(Modes[i]);
            }
            return playerModes;
        }
        public static IList<Mode> MultiplayerModes() //recoge SOLO los modos del menu multijugador
        {
            List<Mode> multiplayerModes = new List<Mode>();

            for (int i = 5; i < 7; i++)
            {
                multiplayerModes.Add(Modes[i]);
            }
            return multiplayerModes;
        }
    }
    
}
