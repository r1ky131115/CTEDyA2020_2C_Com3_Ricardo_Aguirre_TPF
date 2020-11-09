using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;

namespace DeepSpace
{
    class ButtonRenderer: IRenderer
    {
        private Button button;
        private Ellipse ellipse;
        private Brush brush;
        public ButtonRenderer(Button button)
        {
            this.button = button;
            this.ellipse = new Ellipse(button.position, button.size, button.size);
            this.brush = button.game.brushes[button.team];
            
        }

        public void Draw()
        {
            button.game.target.DrawEllipse(ellipse, brush, 3.0f);
            brush.Opacity = 0.2f;
            button.game.target.FillEllipse(ellipse, brush);
            brush.Opacity = 1.0f;
        }
    }
}
