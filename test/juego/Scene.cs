using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SharpDX;
using SharpDX.Direct2D1;

namespace DeepSpace
{
    class Scene: IDisposable
    {
        public List<GameObject> objects;
        public ArbolGeneral<Planeta> arbolDePlanetas;
        public Game game;

        public Scene(Game game)
        {
            this.game = game;
            this.objects = new List<GameObject>();
        }

        public virtual void OnMouseClick(int x, int y, MouseButtons mouseButtons){
        }

        public virtual void OnMouseMove(int x, int y)
        {

        }

        public virtual void OnKeyPress(KeyPressEventArgs e)
        {
            
        }

        public void Dispose()
        {
            foreach (GameObject obj in objects.Where(obj => obj is IDisposable))
            {
                ((IDisposable)obj).Dispose();
            }
        }
    }

}
