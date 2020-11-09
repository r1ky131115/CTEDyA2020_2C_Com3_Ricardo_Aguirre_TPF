using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct2D1;

namespace DeepSpace
{
    class Brushes: IDisposable
    {
        Brush[] brushes;
        public Brushes(Game game)
        {
            this.brushes = new Brush[]{new SolidColorBrush(game.target, Color.White), new SolidColorBrush(game.target, Color.Red), 
                                       new SolidColorBrush(game.target, Color.Blue), new SolidColorBrush(game.target, Color.LimeGreen),
                                       new SolidColorBrush(game.target, Color.Yellow), new SolidColorBrush(game.target, Color.Magenta), new SolidColorBrush(game.target, Color.LightGreen)};
        }
        public Brush this[int id]
        {
            get
            {
                return brushes[id];
            }
        }

        public void Dispose()
        {
            foreach (Brush brush in brushes)
            {
                brush.Dispose();
            }
        }
        
    }
}
