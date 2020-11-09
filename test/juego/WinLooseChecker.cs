using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepSpace
{
    class WinLooseChecker: GameObject
    {
        public WinLooseChecker(Game game): base(game)
        {

        }

        public override void Update(float delta)
        {
            bool enemyStillAlive = checkForEnemy(game);
            bool playerStillAlive = checkForPlayer(game);
            if (playerStillAlive && !enemyStillAlive)
            {
                game.scene.Dispose();
                game.scene = new WinScene(game);
            }
            else if (!playerStillAlive && enemyStillAlive)
            {
                game.scene.Dispose();
                game.scene = new LooseScene(game);
            }
        }

        private bool checkForPlayer(Game game)
        {
            foreach (Planeta p in game.scene.objects.Where(obj => obj is Planeta))
            {
                if (p.team == 1) return true;
            }
            foreach (Route r in game.scene.objects.Where(obj => obj is Route))
            {
                foreach (Ship s in r.ships)
                {
                    if (s.team == 1) return true;
                }
            }
            return false;
        }

        private bool checkForEnemy(Game game)
        {
            foreach (Planeta p in game.scene.objects.Where(obj => obj is Planeta))
            {
                if (p.team > 1) return true;
            }
            foreach (Route r in game.scene.objects.Where(obj => obj is Route))
            {
                foreach (Ship s in r.ships)
                {
                    if (s.team > 1) return true;
                }
            }
            return false;
        }
    }
}
