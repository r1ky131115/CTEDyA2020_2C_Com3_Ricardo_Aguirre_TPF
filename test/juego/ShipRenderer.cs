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
    class ShipRenderer: IRenderer, IDisposable
    {
        private Ship ship;
        private Vector2 nose, firstWing, secondWing, middle, normal;
        private Brush brush;
        private TextFormat textFormat;
        private TextLayout textLayout;
        public ShipRenderer(Ship ship)
        {
            this.ship = ship;
            this.brush = ship.game.brushes[ship.team];
            this.normal = new Vector2(ship.direction.Y, -ship.direction.X);
            normal.Normalize();
            this.textFormat = new TextFormat(ship.game.factoryWrite, "Arial", 14);
            this.textLayout = new TextLayout(ship.game.factoryWrite, ship.population.ToString(), textFormat, 36.0f, 24.0f);
        }

        public void Draw()
        {
            brush = ship.game.brushes[ship.team];
            nose = ship.position + 15*ship.direction;
            middle = ship.position;
            firstWing = ship.position - 7*ship.direction - 7*normal;
            secondWing = ship.position - 7*ship.direction + 7*normal;
            ship.game.target.DrawLine(nose, firstWing, brush);
            ship.game.target.DrawLine(nose, secondWing, brush);
            ship.game.target.DrawLine(middle, firstWing, brush);
            ship.game.target.DrawLine(middle, secondWing, brush);
            // text
            textLayout.Dispose();
            textLayout = new TextLayout(ship.game.factoryWrite, ship.population.ToString(), textFormat, 36.0f, 24.0f);
            ship.game.target.DrawTextLayout(ship.position + new Vector2(5.0f, -20.0f), textLayout, brush);
        }


        public void Dispose()
        {
            textFormat.Dispose();
            textLayout.Dispose();
        }
    }
}
