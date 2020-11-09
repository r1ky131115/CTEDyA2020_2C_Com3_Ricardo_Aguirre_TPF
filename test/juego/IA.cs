using System;
namespace DeepSpace
{
    class IA: GameObject
    {
        private Estrategia est;
        private float acc;
        public IA(Game game)
            : base(game)
        {
            this.est = new Estrategia();
        }

        public override void Update(float delta)
        {
            acc += delta;
            if (acc > 5)
            {
                GameScene gamescene = (GameScene)game.scene;
                Movimiento ataque = this.est.CalcularMovimiento(game.scene.arbolDePlanetas);
                if(ataque!=null)
                    gamescene.SendFleet(ataque.origen, ataque.destino);
                acc -= 5;
            }
        }

        
    }
}

