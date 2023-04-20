using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Trabajo_DSI
{
    internal class ViewMode: Mode
    {
        public Image Img;

        public ViewMode(Mode mode)
        {
            Id = mode.Id;
            Nombre = mode.Nombre;
            Explicacion = mode.Explicacion;
            Imagen = mode.Imagen;
            Img = new Image();
            string s = System.IO.Directory.GetCurrentDirectory() + "\\" + mode.Imagen;
            Img.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(s));
            Color = mode.Color;
        }
    }
}
