using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DeepSpace
{

	class Estrategia
	{
		public String Consulta1(ArbolGeneral<Planeta> arbol)
		{
			//instancio la clase cola
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();

			//encolo el arbol
			c.encolar(arbol);
			//encolo el separador de niveles
			c.encolar(null);

			int contNivel = 0;

			//mientras la cola no esta vacia 
			while (!c.esVacia())
			{
				ArbolGeneral<Planeta> arbolAux = c.desencolar();
				if (arbolAux != null){
					if (arbolAux.getDatoRaiz().EsPlanetaDeLaIA()) {
						return "La distancia es de: " + contNivel;
					}
					else {
						foreach (var hijo in arbolAux.getHijos()) {
							c.encolar(hijo);
						}
					}
				}
				else{
					contNivel++;
					if (!c.esVacia()) {
						c.encolar(null);
					}
				}
					
			}
			return "No se encontro ningun planeta";
		}


		public String Consulta2( ArbolGeneral<Planeta> arbol)
		{
			//instancio la clase cola
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
			int contador = 0;
			
			//encolamos raiz
			c.encolar(arbol);
			
			//Mientras la cola no se vacie ira iterando
			while (!c.esVacia())
			{
				arbol = c.desencolar();
				
				//Si la poblacion es mayor a 10 el contador aumenta
				if (arbol.getDatoRaiz().Poblacion() > 10)
				{
					contador++;
				}
				
				Console.Write(arbol.getDatoRaiz() + "  ");
				
				//Encola los hijos del arbol pasado por parametro
				foreach (var hijo in arbol.getHijos()){
					c.encolar(hijo);
				}
			}
			
			//calcular cantidad de planetas con población > 10 en cada nivel del arbol (recorrido por niveles, con limitante de población)
			return "La cantidad de planetas con población mayor a 10 son: " + contador + ".";
		}


		public String Consulta3( ArbolGeneral<Planeta> arbol)
		{
			// calcular  promedio poblacion por nivel de arbol (recorrido por niveles, sumar en total y cantidad y dividir por cantidad para sacar promedio)
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
			
			//Arbol auxiliar
			ArbolGeneral<Planeta> arbolAux;
			
			//encolo raiz
			c.encolar(arbol);
			
			//encolo null para separar niveles
			c.encolar(null);			
				
			int contadorPlanetas = 0;
			int cantidadPoblacionPorNivel = 0;
			int nivel = 0;
			float promedioPorNivel = 0;
			
			string anuncio = "";

			while (!c.esVacia())
			{
				arbolAux = c.desencolar();
				
				if (arbolAux != null) {
					
					//Se incrementa la cantidad de planetas por nivel
					contadorPlanetas++;
					
					//Se suma la poblacion de cada planeta
					cantidadPoblacionPorNivel += arbolAux.getDatoRaiz().Poblacion();
					
					//Se divide la cantidad de poblacion por nivel con la cantidad de planetas
					promedioPorNivel = cantidadPoblacionPorNivel / contadorPlanetas;
					
					foreach (var hijo in arbolAux.getHijos()){
						c.encolar(hijo);
					}					
				}
				else{
					anuncio += "El promedio de poblacion en el nivel " + nivel + " es de: " + promedioPorNivel + "\n";
					contadorPlanetas = 0;
					cantidadPoblacionPorNivel = 0;
					nivel++;
					promedioPorNivel = 0;
					
					if (!c.esVacia()) {
						c.encolar(null);
					}
				}
				
			}
			
			return "Promedio de poblacion por nivel:\n" + anuncio;
		}
		
		public Movimiento CalcularMovimiento(ArbolGeneral<Planeta> arbol)
		{
			//Creo e instancio el camino de ataque hacia la raiz
			List<Planeta> caminoHaciaRaiz = CaminoAtaqueARaiz(arbol);

			
			//Si la raiz no le pertenece a la IA se la ataca
			if (!arbol.getDatoRaiz().EsPlanetaDeLaIA())
			{
				Movimiento movARaiz = new Movimiento(caminoHaciaRaiz[caminoHaciaRaiz.Count - 1], caminoHaciaRaiz[caminoHaciaRaiz.Count - 2]);
				return movARaiz;
			}
			// Despues de capturar la raiz ataco al humano
			else
			{
				List<Planeta> caminoHaciaHumano = CaminoAtaqueAHumano(arbol);
				int pos = 0;
				
				while (!caminoHaciaHumano[caminoHaciaHumano.Count-1].EsPlanetaDeLaIA()) {
					if (caminoHaciaHumano[pos].EsPlanetaDeLaIA() && 
							(caminoHaciaHumano[pos+1].EsPlanetaNeutral()|| 
								caminoHaciaHumano[pos+1].EsPlanetaDelJugador()))
					{
						Movimiento movAhumano = new Movimiento(caminoHaciaHumano[pos], caminoHaciaHumano[pos+1]);
						
						return movAhumano;
					}
					pos++;
				}
			}
			return null;
		}

		private List<Planeta> _caminoAtaqueARaiz(ArbolGeneral<Planeta> arbol, List<Planeta> caminoDeLaIA){
			//Primero se agrega la raiz
			caminoDeLaIA.Add(arbol.getDatoRaiz());
			
			//Si encontramos el camino a la raiz
			if (arbol.getDatoRaiz().EsPlanetaDeLaIA())
			{
				return caminoDeLaIA;
			}
			else
			{
				//Se procesan los hijos recursivamente
				foreach(var hijo in arbol.getHijos())
				{
					//Camino auxiliar que va descartando planetas
					List<Planeta> caminoAux = _caminoAtaqueRaiz(hijo, caminoDeLaIA);
					if (caminoAux != null)
					{
						return caminoAux;
					}
					
				}
				//Se elimina el ultimo elemento de la lista de planetas
				caminoDeLaIA.RemoveAt(caminoDeLaIA.Count()-1);
			}
			return null;
		}
		
		public List<Planeta> CaminoAtaqueARaiz(ArbolGeneral<Planeta> arbol){
			List<Planeta> caminoRaiz = new List<Planeta>();
			return _caminoAtaqueARaiz(arbol, caminoRaiz);
		}

		private List<Planeta> _caminoAtaqueAHumano(ArbolGeneral<Planeta> arbol, List<Planeta> caminoDeRaizAHumano)
		{
			//Se procesa primero la raiz
			caminoDeRaizAHumano.Add(arbol.getDatoRaiz());
			
			//Si se encontro camino..
			if (arbol.getDatoRaiz().EsPlanetaDelJugador())
			{
				return caminoDeRaizAHumano;
			}
			else
			{
				//Se procesan los hijos recursivamente
				foreach (var hijo in arbol.getHijos())
				{
					
					List<Planeta> caminoAux = _caminoAtaqueAHumano(hijo, caminoDeRaizAHumano);
					if (caminoAux != null)
					{
						return caminoAux;
					}

				}
				//saco ultimo planeta del dominio
				caminoDeRaizAHumano.RemoveAt(caminoDeRaizAHumano.Count() - 1);

			}
			return null;
		}
		
		public List<Planeta> CaminoAtaqueAHumano(ArbolGeneral<Planeta> arbol){
			List<Planeta> caminoHaciaHumano = new List<Planeta>();
			return _caminoAtaqueAHumano(arbol, caminoHaciaHumano);
		}
	}
}
