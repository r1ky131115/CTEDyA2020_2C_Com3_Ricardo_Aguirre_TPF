using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
