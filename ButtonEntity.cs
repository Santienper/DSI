using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Trabajo_DSI
{
    public class ButtonEntity {
        public ButtonEntity(string source)
        {
            Source = source;
        }

        public string Source { get; set; }
    }
}
