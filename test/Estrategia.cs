using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
					if (!arbolAux.esVacia()) {
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
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
			int contador = 0;

			c.encolar(arbol);

			while (!c.esVacia())
			{
				arbol = c.desencolar();

				if (arbol.getDatoRaiz().Poblacion() > 10)
				{
					contador++;
				}
				Console.Write(arbol.getDatoRaiz() + "  ");

				foreach (var hijo in arbol.getHijos())
					c.encolar(hijo);
			}
			
			//calcular cantidad de planetas con población > 10 en cada nivel del arbol (recorrido por niveles,con limitante de población)
			return "La cantidad de planetas con población mayor a 10 son: " + contador + "  ";
		}


		public String Consulta3( ArbolGeneral<Planeta> arbol)
		{
			// calcular  promedio poblacion por nivel de arbol (recorrido por niveles, sumar en total y cantidad y dividir por cantidad para sacar promedio)
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
			
			long cont = 0;
			long canti = 0;

			c.encolar(arbol);

			while (!c.esVacia())
			{
				arbol = c.desencolar();
				
				foreach (var hijo in arbol.getHijos()){
					c.encolar(hijo);
				}
				cont += arbol.getDatoRaiz().population;
				canti++;
			}
			
			Double promedio = cont / canti;
			
			return "El promedio de población por nivel del arbol es de: " + promedio;
		}
		
		
		public Movimiento CalcularMovimiento(ArbolGeneral<Planeta> arbol)
		{	
			List<Planeta> caminoRaiz = caminoConPreOrden(arbol);
			caminoConPreOrden(arbol);
			
			List<Planeta> caminoHumano = caminoAtaqueJugador(arbol);
			
			if (!arbol.getDatoRaiz().EsPlanetaDeLaIA()) {				
				Movimiento movRaiz = new Movimiento(caminoRaiz[caminoRaiz.Count - 1], caminoRaiz[caminoRaiz.Count - 2]);
				
				return movRaiz;
			}
			else{
				
				for(int index=0; index< caminoHumano.Count(); index++)
				{
					if (caminoHumano[index].EsPlanetaDeLaIA() && 
							(caminoHumano[index+1].EsPlanetaNeutral()|| 
								caminoHumano[index+1].EsPlanetaDelJugador()))
					{
						Movimiento movAhumano = new Movimiento(caminoHumano[index], caminoHumano[index+1]);
						return movAhumano;
					}
				}
			}
			
			return null;
		}
		
		private List<Planeta> _caminoConPreOrden(ArbolGeneral<Planeta> arbol, List<Planeta> camino){
			//Procesamos la raiz primero
			camino.Add(arbol.getDatoRaiz());
				
			//Si encontramos camino...
			if (arbol.getDatoRaiz().EsPlanetaDeLaIA()) {
				return camino;
			}
			else{
				//luego procesamos los hijos recursivamente
				foreach (var hijo in arbol.getHijos()){
					List<Planeta> caminoAux = _caminoConPreOrden(hijo, camino);
					if (caminoAux != null) {
						return caminoAux;
					}
				}
				//saco ultimo planeta del camino
				camino.RemoveAt(camino.Count - 1);
			}
			return null;
		}
		
		public List<Planeta> caminoConPreOrden(ArbolGeneral<Planeta> buscado){
			List<Planeta> camino = new List<Planeta>();
			return _caminoConPreOrden(buscado, camino);
		}
		
		private List<Planeta> _caminoAtaqueJugador(ArbolGeneral<Planeta> arbol, List<Planeta> caminoRaizHumano){
			
			// Procesamos primero la raiz
			caminoRaizHumano.Add(arbol.getDatoRaiz());
			
			// si se encontra camino a jugador
			if (arbol.getDatoRaiz().EsPlanetaDelJugador()){
				return caminoRaizHumano;
			}
			else{
			
				foreach (var hijo in arbol.getHijos()){
					List<Planeta> caminoAuxJugador = _caminoAtaqueJugador(hijo, caminoRaizHumano);
					
					if (caminoAuxJugador != null){
						return caminoAuxJugador;
					}
					
					caminoRaizHumano.RemoveAt(caminoRaizHumano.Count - 1);
				}
			}
			
			return null;
		}
		
		public List<Planeta> caminoAtaqueJugador(ArbolGeneral<Planeta> buscado){
			List<Planeta> caminoDeAtaque = new List<Planeta>();
			return _caminoConPreOrden(buscado, caminoDeAtaque);
		}		
	}
}
