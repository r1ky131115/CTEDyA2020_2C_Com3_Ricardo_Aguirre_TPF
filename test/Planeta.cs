using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Direct2D1;

namespace DeepSpace
{
    class Planeta : GameObject, IDisposable
    {
        public Vector2 position;
        public uint population, size;
        public int team;
        private float acc;
        private PlanetRenderer planetRenderer;

        public Planeta(Game game, Vector2 position, uint size, uint population)
            : base(game)
        {
            this.position = position;
            this.size = size;
            this.population = population;
            this.acc = 0;
            this.planetRenderer = new PlanetRenderer(this);
        }

        public Planeta(Game game, Vector2 position, uint size, int team, uint population)
            : base(game)
        {
            this.position = position;
            this.size = size;
            this.population = population;
            this.team = team;
            this.acc = 0;
            this.planetRenderer = new PlanetRenderer(this);
        }

        public override void Update(float delta)
        {
            acc += delta;
            if (acc > 1 - size/100.0f)
            {
                if (team != 0)
                {
                    population += 1;
                }
                acc = acc - 1 + size/100.0f;
            }
        }

        public void Invade(Ship invader)
        {
            if (invader.team == team)
            {
                population += invader.population;
            }
            else
            {
                if (population < invader.population)
                {
                    invader.population -= population;
                    population = invader.population;
                    team = invader.team;
                }
                else
                {
                    population -= invader.population;
                }
            }
        }

        public bool IsClicked(int x, int y)
        {
            return (position.X - x) * (position.X - x) + (position.Y - y) * (position.Y - y) <= size*size;
        }

        public override void Draw()
        {
            planetRenderer.Draw();
        }

        public void Dispose()
        {
            planetRenderer.Dispose();
        }
        
        public bool EsPlanetaDeLaIA()
        {
        	return team==2;
        }
        
        public bool EsPlanetaDelJugador()
        {
        	return team==1;
        }
        
        public bool EsPlanetaNeutral()
        {
        	return team==0;
        }
        
        public int Poblacion()
        {
        	return (int) this.population;
        }
    }
}

