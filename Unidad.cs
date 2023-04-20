using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.UI.Xaml.Media;

namespace Trabajo_DSI
{
    public class Unidad {
        public Unidad(string source, string[] lista)
        {
            Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "\\" + source));
            Lista = lista;
        }

        public Windows.UI.Xaml.Media.Imaging.BitmapImage Source { get; set; }
        public string[] Lista { get; set; }

        public static Dictionary<string, Unidad> Unidades = new Dictionary<string, Unidad> {
            { "Casa", new Unidad("Assets\\casa.jpg", new string[] { "Oveja", "Soldado" }) },
            { "HQ", new Unidad("Assets\\superheroes hq.jpg", new string[] { "Soldado", "Funky" }) },
            { "Oveja", new Unidad("Assets\\oveja.jpg", new string[] { "Casa" }) },
            { "Soldado", new Unidad("Assets\\soldado.jpg", new string[] { "HQ" }) },
            { "Funky", new Unidad("Assets\\funky heroina.jpg", new string[] { "Casa", "HQ" }) }
        };
    }
}
