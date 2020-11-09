using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Direct2D1;

namespace DeepSpace
{
    public delegate void ButtonClickedEvent(object sender, EventArgs e);
    class Button: GameObject
    {
        public Vector2 position;
        public float size;
        public int team;
        private ButtonRenderer buttonRenderer;
        public event ButtonClickedEvent buttonClickedEvent;
        public Button(Game game, Vector2 position, float size, int team): base(game)
        {
            this.position = position;
            this.size = size;
            this.team = team;
            this.buttonRenderer = new ButtonRenderer(this);
            this.buttonClickedEvent = null;
        }

        public override void Update(float delta)
        {
            
        }

        public bool IsClicked(int x, int y)
        {
            return (position.X - x) * (position.X - x) + (position.Y - y) * (position.Y - y) <= size * size;
        }

        public void OnClicked()
        {
            if (buttonClickedEvent != null)
            {
                buttonClickedEvent(this, EventArgs.Empty);
            }
        }

        public override void Draw()
        {
            buttonRenderer.Draw();
        }
    }
}
