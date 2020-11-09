using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct2D1;

namespace DeepSpace
{
    class Route: GameObject, IDisposable
    {
        public Planeta source, destination;
        public Vector2 start, end;
        public List<Ship> ships;
        public bool autoTransfer;
        private float acc;
        private RouteRenderer routeRenderer;

        public Route(Game game, Planeta source, Planeta destination): base(game)
        {
            this.source = source;
            this.destination = destination;
            this.ships = new List<Ship>();
            this.autoTransfer = false;
            this.acc = 0.0f;
            double angle = Math.Atan2(source.position.Y-destination.position.Y, source.position.X-destination.position.X);
            this.start = source.position - source.size * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            this.end = destination.position + destination.size * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            this.routeRenderer = new RouteRenderer(this);
        }

        public override void Update(float delta){
            updateShips(delta);
            updateTransfer(delta);
        }

        private void updateTransfer(float delta)
        {
            if (source.team != ((GameScene)game.scene).playerTeam)
            {
                autoTransfer = false;
            }
            if (autoTransfer)
            {
                acc += delta;
                if (acc > 5)
                {
                    GameScene gameScene = (GameScene)game.scene;
                    gameScene.SendFleet(source, destination);
                    acc = acc - 5;
                }
            }
            else
            {
                acc = 0.0f;
            }
        }

        private void updateShips(float delta)
        {
            for (int i = 0; i < ships.Count; i++)
            {
                ships[i].Update(delta);
                if (checkPlanetCollision(ships[i]))
                {
                    ships[i].destination.Invade(ships[i]);
                    game.scene.objects.Remove(ships[i]);
                    ships[i].Dispose();
                    ships.Remove(ships[i]);
                }
            }
            checkShipCollision();
        }

        public void AddShip(Ship ship)
        {
            ships.Add(ship);
        }

        private bool checkPlanetCollision(Ship ship)
        {
            float distanceFromPlanet = (ship.destination.position.X - ship.position.X) * (ship.destination.position.X - ship.position.X) +
                (ship.destination.position.Y - ship.position.Y) * (ship.destination.position.Y - ship.position.Y);
            return (distanceFromPlanet <= ship.destination.size * ship.destination.size);
        }


        private void checkShipCollision()
        {
            for (int i = 0; i < ships.Count; i++)
            {
                for (int j = i + 1; j < ships.Count; j++)
                {
                    if (CollideShips(ships[i], ships[j])){
                        if (ships[i].population > ships[j].population)
                        {
                            ships[i].population -= ships[j].population;
                            ships[j].population = 0;
                        }
                        else if (ships[i].population < ships[j].population)
                        {
                            ships[j].population -= ships[i].population;
                            ships[i].population = 0;
                        }
                        else
                        {
                            ships[i].population = 0;
                            ships[j].population = 0;
                        }
                    }
                }
            }
            int lenght = ships.Count;
            for (int i = 0; i < lenght; i++)
            {
                if (ships[i].population == 0)
                {
                    game.scene.objects.Remove(ships[i]);
                    ships[i].Dispose();
                    ships.RemoveAt(i);
                    lenght--;
                }
            }
        }

        private bool CollideShips(Ship first, Ship second)
        {
            float distance = (first.position.X - second.position.X) * (first.position.X - second.position.X) + (first.position.Y - second.position.Y) * (first.position.Y - second.position.Y);
            return (distance < 10.0f * 10.0f) && (first.team != second.team);
        }

        public override void Draw()
        {
            routeRenderer.Draw();
            foreach (Ship ship in ships)
            {
                ship.Draw();
            }
        }

        public void Dispose()
        {
            routeRenderer.Dispose();
        }
    }
}
