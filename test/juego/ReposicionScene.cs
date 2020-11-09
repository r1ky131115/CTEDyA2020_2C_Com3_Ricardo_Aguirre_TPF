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
    class ReposicionScene: Scene
    {
        public ReposicionScene(Game game): base(game)
        {
            Button menuButton = new Button(game, new Vector2(625.0f, 450.0f), 40.0f, 2);
            menuButton.buttonClickedEvent += delegate{
                game.scene.Dispose();
                game.scene = new MenuScene(game);
            };
            this.objects = new List<GameObject>() {menuButton, 
                new Text(game, new Vector2(100.0f, 100.0f), "Planeta Rojo y Azul Reposicionados!", 26.0f), new Text(game, new Vector2(600.0f, 440.0f), "Menu")};
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
