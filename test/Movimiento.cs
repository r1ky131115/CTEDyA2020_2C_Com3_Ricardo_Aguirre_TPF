using System;

namespace DeepSpace
{

	class Movimiento
	{
		public Movimiento(Planeta o, Planeta d)
		{
			this.origen=o;
			this.destino=d;
		}
		
		
		public Planeta origen { get; set; }
		public Planeta destino { get; set; }
	}
}
