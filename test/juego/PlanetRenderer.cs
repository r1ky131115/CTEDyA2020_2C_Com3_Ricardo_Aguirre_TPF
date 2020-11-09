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
    class PlanetRenderer: IRenderer, IDisposable
    {
        private Planeta planet;
        private Ellipse ellipse;
        private Brush brush;
        private TextFormat textFormat;
        private TextLayout textLayout;
        private Vector2 position;
        public PlanetRenderer(Planeta planet)
        {
            this.planet = planet;
            this.ellipse = new Ellipse(planet.position, planet.size, planet.size);
            this.brush = planet.game.brushes[planet.team];
            this.textFormat = new TextFormat(planet.game.factoryWrite, "Arial", 20);
            this.textLayout = new TextLayout(planet.game.factoryWrite, planet.population.ToString(), textFormat, 36.0f, 24.0f);
        }

        public void Draw()
        {
            DrawCircle();
            DrawValueInCircle();
        }

        public void DrawCircle()
        {
            brush = planet.game.brushes[planet.team];
            float thickness = 1.0f;
            GameScene scene = (GameScene)planet.game.scene;
            if (scene.selectedPlanet == planet)
            {
                thickness = 3.0f;
            }
            planet.game.target.DrawEllipse(ellipse, brush, thickness);
        }


        public void DrawValueInCircle()
        {
            float offset = calculateOffset(planet.population);
            Vector2 temp = new Vector2(offset, 12.0f);
            position = planet.position - temp;
            textLayout.Dispose();
            textLayout = new TextLayout(planet.game.factoryWrite, planet.population.ToString(), textFormat, 36.0f, 24.0f);
            planet.game.target.DrawTextLayout(position, textLayout, brush);
            //planet.game.target.DrawRectangle(new RectangleF(position.X, position.Y, 36.0f, 24.0f), brush);
        }

        private static float calculateOffset(uint value)
        {
            if ((0 <= value) && (value <= 9))
            {
                return 6.0f;
            }
            else if ((10 <= value) && (value <= 99))
            {
                return 12.0f;
            }
            else
            {
                return 16.0f;
            }
        }

        public void Dispose()
        {
            textFormat.Dispose();
            textLayout.Dispose();
        }
    }
}
