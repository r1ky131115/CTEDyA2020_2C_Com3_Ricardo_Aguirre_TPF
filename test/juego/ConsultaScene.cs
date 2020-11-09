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
    class ConsultaScene: Scene
    {
        public ConsultaScene(Game game): base(game)
        {
            Button menuButton = new Button(game, new Vector2(625.0f, 450.0f), 40.0f, 2);
            menuButton.buttonClickedEvent += delegate{
                game.scene.Dispose();
                game.scene = new MenuScene(game);
            };
            game.scene.arbolDePlanetas = LevelLoader.arbolDePlanetas;
            this.objects = new List<GameObject>() {menuButton, 
            	new Text(game, new Vector2(300.0f, 10.0f), "Consultas!", 30.0f),
            	new Text(game, new Vector2(20.0f, 70.0f), (new Estrategia()).Consulta1(game.scene.arbolDePlanetas)), 
            	new Text(game, new Vector2(20.0f, 100.0f), (new Estrategia()).Consulta2(game.scene.arbolDePlanetas)), 
            	new Text(game, new Vector2(20.0f, 130.0f), (new Estrategia()).Consulta3(game.scene.arbolDePlanetas)), 
            	new Text(game, new Vector2(600.0f, 440.0f), "Menu")};
        }

        public override void OnMouseClick(int x, int y, MouseButtons mouseButtons)
        {
            foreach (Button button in objects.Where(obj => obj is Button))
            {
                if (button.IsClicked(x, y))
                {
                    button.OnClicked();
                }
            }
        }
    }
}