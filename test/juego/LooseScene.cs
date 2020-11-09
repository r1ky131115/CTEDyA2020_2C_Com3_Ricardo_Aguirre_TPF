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
    class LooseScene: Scene
    {
        public LooseScene(Game game): base(game)
        {
            Button menuButton = new Button(game, new Vector2(400.0f, 300.0f), 50.0f, 2);
            menuButton.buttonClickedEvent += delegate{
                if (game.level != 0) game.level++;
                game.scene.Dispose();
                LevelLoader.LoadLevel(game);
                game.scene = new MenuScene(game);
            };
            this.objects = new List<GameObject>() {menuButton, 
                new Text(game, new Vector2(310.0f, 100.0f), "Has perdido!", 30.0f), new Text(game, new Vector2(375.0f, 290.0f), "Menu")};
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
