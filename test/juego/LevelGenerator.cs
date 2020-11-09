using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Direct2D1;

namespace DeepSpace
{
    class LevelGenerator
    {
        private static Random rnd = new Random();
        public static List<GameObject> GenerateLevel(Game game, uint planetsCount)
        {
            List<GameObject> objects = new List<GameObject>();
            List<Planeta> planets = new List<Planeta>();
            List<Route> routes = new List<Route>();
            makeCenter(game, planetsCount, planets);
            connectPlanets(game, planets, routes);
            routes.RemoveAt(rnd.Next(routes.Count));
            bool centerMode = (rnd.Next(3) > 0); // 66% chance
            if (centerMode)
            {
                createCentralPlanet(game, planets, routes);
            }
            else
            {
                addRandomRoute(game, planets, routes);
                addRandomRoute(game, planets, routes);
            }
            setPlanets(game, planets, 3);
            objects.AddRange(planets);
            objects.AddRange(routes);
            return objects;
        }

        private static void setPlanets(Game game, List<Planeta> planets, uint enemyCount)
        {
            enemyCount++;
            List<int> unusedPlanets = Enumerable.Range(0, planets.Count).ToList();
            for (int i = 0; i < enemyCount; i++)
            {
                int randomIndex = unusedPlanets[rnd.Next(unusedPlanets.Count)];
                unusedPlanets.Remove(randomIndex);
                Planeta randomPlanet = planets[randomIndex];
                randomPlanet.population = 10;
                randomPlanet.team = i + 1;
            }
        }

        private static void createCentralPlanet(Game game, List<Planeta> planets, List<Route> routes)
        {
            Vector2 position = new Vector2(400.0f, 300.0f) + rnd.NextVector2(new Vector2(-20.0f, -20.0f), new Vector2(20.0f, 20.0f));
            Planeta central = new Planeta(game, position, (uint)(20 + rnd.Next(40)), (uint)rnd.Next(10));
            int routesCount = rnd.Next(1, 4);
            List<int> indicesNotConnected = Enumerable.Range(0, planets.Count).ToList<int>();
            for (int i = 0; i < routesCount; i++)
            {
                int planetIndex = indicesNotConnected[rnd.Next(indicesNotConnected.Count)];
                indicesNotConnected.Remove(planetIndex);
                routes.Add(new Route(game, central, planets[planetIndex]));
            }
            planets.Add(central);
        }

        private static void addRandomRoute(Game game, List<Planeta> planets, List<Route> routes)
        {
            int choosen = rnd.Next(routes.Count);
            foreach (Route route in routes)
            {
                if ((route.source == planets[choosen] && route.destination == planets[(choosen + 3) % planets.Count]) ||
                    (route.destination == planets[choosen] && route.source == planets[(choosen + 3) % planets.Count]))
                {
                    return;
                }
            }
            routes.Add(new Route(game, planets[choosen], planets[(choosen + 3) % planets.Count]));
        }

        private static void connectPlanets(Game game, List<Planeta> planets, List<Route> routes)
        {
            for (int i = 0; i < planets.Count; i++)
            {
                routes.Add(new Route(game, planets[i], planets[(i + 1) % planets.Count]));
            }
        }

        private static void makeCenter(Game game, uint planetsInCenterCount, List<Planeta> planets)
        {
            for (int i = 0; i < planetsInCenterCount; i++)
            {
                float x = 400.0f + 300.0f * (float)Math.Cos(2 * i * Math.PI / planetsInCenterCount) + rnd.NextFloat(-20, 20);
                float y = 300.0f + 200.0f * (float)Math.Sin(2 * i * Math.PI / planetsInCenterCount) + rnd.NextFloat(-20, 20);
                planets.Add(new Planeta(game, new Vector2(x, y), (uint)(20 + rnd.Next(40)), (uint)rnd.Next(10)));
            }
        }
    }
}
