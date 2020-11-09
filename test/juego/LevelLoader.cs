using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Direct2D1;

namespace DeepSpace
{
	class LevelLoader
	{
		public static ArbolGeneral<Planeta> arbolDePlanetas = null;
		public static List<GameObject> objects = new List<GameObject>();
		public static List<GameObject> LoadLevel(Game game)
		{
			List<Planeta> planets = new List<Planeta>();
			List<Route> routes = new List<Route>();
			objects = new List<GameObject>();
  
			float x = 400.0f;
			float y = 250.0f;
			float radio = 100.0f;
			int limite = 5;
			double cuadrante = 0;
			var rnd = new Random();
			planets = new List<Planeta> { new Planeta(game, new Vector2(x, y), 50, 20) };
			List<ArbolGeneral<Planeta>> arboles = new List<ArbolGeneral<Planeta>>{ new ArbolGeneral<Planeta>(planets[0]) };
			routes = new List<Route>();
			double porcion = 2 * Math.PI / limite;
			for (int i = 0; i <= 5; i++) {
				for (uint j = 1; j <= limite; j++) {
					x = (float)(400.0f + radio * Math.Cos(j * porcion + cuadrante));
					y = (float)(250.0f - radio * Math.Sin(j * porcion + cuadrante));
					planets.Add(new Planeta(game, new Vector2(x, y), ((uint)rnd.Next(20, 30)), ((uint)rnd.Next(5, 15))));
					arboles.Add(new ArbolGeneral<Planeta>(planets.Last()));
					routes.Add(new Route(game, planets[i], planets.Last()));
					arboles[i].agregarHijo(arboles.Last());
                    		
				}
				radio = 180.0f;
				limite = 3;
				porcion = (2 * Math.PI / 5) / limite;
				cuadrante = i * limite * porcion;
			}
			radio = 240;
			for (int i = 0; i <= 14; i++) {
				x = (float)(400.0f + radio * Math.Cos(i * 2 * Math.PI / 15));
				y = (float)(250.0f - radio * Math.Sin(i * 2 * Math.PI / 15));
				planets.Add(new Planeta(game, new Vector2(x, y), ((uint)rnd.Next(10, 20)), ((uint)rnd.Next(1, 5))));
				arboles.Add(new ArbolGeneral<Planeta>(planets.Last()));
				routes.Add(new Route(game, planets[6 + i], planets.Last()));
				arboles[6 + i].agregarHijo(arboles.Last());
			}
                  
			int index = rnd.Next(0, planets.Count);
			planets[index].team = 1;
			planets[index].population = 30;
			index = rnd.Next(0, planets.Count);
			planets[index].team = 2;
			planets[index].population = 30;
			objects = new List<GameObject> { new Text(game, new Vector2(10.0f, 530.0f), "Tip: Para ganar debes destruir al enemigo.", 20.0f, 6) };

			LevelLoader.arbolDePlanetas = arboles[0];
			objects.AddRange(planets);
			objects.AddRange(routes);
			return objects;
		}
	}
}
