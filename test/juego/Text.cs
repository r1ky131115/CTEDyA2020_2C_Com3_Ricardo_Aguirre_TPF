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
    class Text: GameObject, IDisposable
    {
        private Vector2 position;
        private Brush brush;
        private TextFormat textFormat;
        private TextLayout textLayout;
        public Text(Game game, Vector2 position, string text): base(game)
        {
            this.position = position;
            this.brush = game.brushes[0];
            this.textFormat = new TextFormat(game.factoryWrite, "Arial", 20);
            this.textLayout = new TextLayout(game.factoryWrite, text, textFormat, 16.0f * text.Length, 24.0f);
        }

        public Text(Game game, Vector2 position, string text, float size)
            : base(game)
        {
            this.position = position;
            this.brush = game.brushes[0];
            this.textFormat = new TextFormat(game.factoryWrite, "Arial", size);
            this.textLayout = new TextLayout(game.factoryWrite, text, textFormat, 16.0f * text.Length, size + 4.0f);
        }
        
         public Text(Game game, Vector2 position, string text, float size, int brush)
            : base(game)
        {
            this.position = position;
            this.brush = game.brushes[brush];
            this.textFormat = new TextFormat(game.factoryWrite, "Arial", size);
            this.textLayout = new TextLayout(game.factoryWrite, text, textFormat, 16.0f * text.Length, size + 4.0f);
        }

        public override void Update(float delta)
        {

        }

        public override void Draw()
        {
            brush.Opacity = 0.8f;
            game.target.DrawTextLayout(position, textLayout, brush);
            brush.Opacity = 1.0f;
        }

        public void Dispose()
        {
            textFormat.Dispose();
            textLayout.Dispose();
        }
    }
}
