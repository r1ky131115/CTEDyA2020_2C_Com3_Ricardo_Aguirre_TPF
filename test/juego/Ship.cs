using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace DeepSpace
{
    class Ship: GameObject, IDisposable
    {
        public uint population;
        public int team;
        private Route route;
        public Planeta source;
        public Planeta destination;
        public Vector2 position;
        public Vector2 direction;
        private float speed;
        private ShipRenderer shipRenderer;
        public Ship(Game game, Route route, Planeta source, Planeta destination, uint population, int team)
            : base(game)
        {
            this.population = population;
            this.team = team;
            this.route = route;
            this.source = source;
            this.destination = destination;
            this.direction = destination.position - source.position;
            this.direction.Normalize();
            this.position = calculateStartPosition();
            this.speed = 30.0f;
            this.shipRenderer = new ShipRenderer(this);
        }
        
        public override void Update(float delta)
        {
            position += delta * speed * direction;
        }

        private Vector2 calculateStartPosition()
        {
            float distanceToStart, distanceToEnd;
            distanceToStart = (route.start.X - source.position.X) * (route.start.X - source.position.X) + (route.start.Y - source.position.Y) * (route.start.Y - source.position.Y);
            distanceToEnd = (route.end.X - source.position.X) * (route.end.X - source.position.X) + (route.end.Y - source.position.Y) * (route.end.Y - source.position.Y);
            if (distanceToStart < distanceToEnd)
            {
                return route.start;
            }
            else
            {
                return route.end;
            }
        }

        public override void Draw()
        {
            shipRenderer.Draw();
        }

        public void Dispose()
        {
            shipRenderer.Dispose();
        }

    }
}
