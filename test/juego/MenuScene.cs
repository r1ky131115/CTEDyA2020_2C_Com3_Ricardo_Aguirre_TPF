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
    class MenuScene: Scene
    {
        public MenuScene(Game game): base(game)
        {
            if (LevelLoader.objects.Count==0) {
            	 LevelLoader.LoadLevel(game);
            }
            
        	Button consultaMode = new Button(game, new Vector2(200.0f, 150.0f), 65.0f, 1);
            consultaMode.buttonClickedEvent += delegate{
                game.scene.Dispose();
                game.scene = new ConsultaScene(game);
        	};
            Button juegoMode = new Button(game, new Vector2(510.0f, 250.0f), 75.0f, 3);
            juegoMode.buttonClickedEvent += delegate{
                game.scene.Dispose();
                game.scene = new GameScene(game);
            };
            Button newPositionsButton = new Button(game, new Vector2(550.0f, 450.0f), 70.0f, 4);
            newPositionsButton.buttonClickedEvent += delegate{
                game.scene.Dispose();
                LevelLoader.LoadLevel(game);
                game.scene = new ReposicionScene(game);
            };
            
            Button exitButton = new Button(game, new Vector2(270.0f, 350.0f), 45.0f, 2);
            exitButton.buttonClickedEvent += delegate{
                game.scene.Dispose();
                game.brushes.Dispose();
                Application.Exit();
            };
            this.objects = new List<GameObject>() {consultaMode, juegoMode, newPositionsButton, exitButton,
                new Text(game, new Vector2(155.0f, 140.0f), "Consultas"),
                new Text(game, new Vector2(455.0f, 240.0f), "Iniciar juego"),
                new Text(game, new Vector2(490.0f, 440.0f), "Reposicionar"),
                new Text(game, new Vector2(250.0f, 340.0f), "Salir")};
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
