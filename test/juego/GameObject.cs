using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepSpace
{
    class GameObject
    {
        public Game game;

        public GameObject(Game game)
        {
            this.game = game;
        }

        public virtual void Update(float delta)
        {

        }

        public virtual void Draw()
        {
            
        }
    }
}
