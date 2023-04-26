using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.UI.Xaml.Media;

namespace Trabajo_DSI {
    public class Unidad {
        public Unidad(string ID, string nombre, int maxHealth, string source, string[] lista) {
            Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "\\" + source));
            Lista = lista;
            id = ID;
            Nombre = nombre;
            MaxHealth = maxHealth;
        }

        public Windows.UI.Xaml.Media.Imaging.BitmapImage Source { get; set; }
        public string[] Lista { get; set; }
        public string id { get; set; }
        public string Nombre { get; set; }
        public int MaxHealth { get; set; }

        public static Dictionary<string, Unidad> Unidades = new Dictionary<string, Unidad> {
            { "Casa", new Unidad("Casa", "Casa", 300, "Assets\\casa.jpg", new string[] { "Oveja", "Soldado" }) },
            { "HQ", new Unidad("HQ", "HQ", 500, "Assets\\superheroes hq.jpg", new string[] { "Soldado", "Funky", "SpiderMan", "IronMan"}) },
            { "Oveja", new Unidad("Oveja", "Oveja", 50, "Assets\\oveja.jpg", new string[] { "Casa" }) },
            { "Soldado", new Unidad("Soldado", "Soldado", 100, "Assets\\soldado.jpg", new string[] { "HQ" }) },
            { "Funky", new Unidad("Funky", "Heroina", 200, "Assets\\funky heroina.jpg", new string[] { "Casa", "HQ" }) },
            {"SpiderMan",new Unidad("SpiderMan","Spider-Man",400,"Assets\\SpiderManPeque.jpg",new string[]{"Oveja","Funky"}) },
            {"IronMan",new Unidad("IronMan","Iron Man",500,"Assets\\IronManPeque.jpg",new string[]{"Oveja","Funky", "BlackPanther"}) },
            {"CapitanA",new Unidad("CapitanA","Capitan América",400,"Assets\\CapAmericaPeque.jpg",new string[]{"Oveja","Funky", "AntMan"}) },
            {"AntMan",new Unidad("AntMan","Ant-Man",350,"Assets\\AntMan.jpg",new string[]{"Oveja","Funky","Casa"}) },
            {"BlackPanther",new Unidad("BlackPanther","Black-Panther",420,"Assets\\BlackPantherPeque.jpg",new string[]{"Oveja","Funky","SpiderMan"}) },
        };
    }

    public class InstanciaUnidad {
        public InstanciaUnidad(string ID) {
            id = ID;
            health = Unidad.Unidades[id].MaxHealth;
            unidad = Unidad.Unidades[id];
            healthString = health + "/" + unidad.MaxHealth;
        }

        public string id { get; set; }
        public int health { get; set; }
        public Unidad unidad { get; }
        public string healthString { get; set; }
    }
}
