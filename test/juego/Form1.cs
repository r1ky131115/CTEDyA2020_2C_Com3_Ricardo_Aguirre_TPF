using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.WIC;
using SharpDX.IO;

namespace DeepSpace
{

    public partial class Form1 : Form
    {
        Game game;

        public Form1()
        {
            InitializeComponent();
        }
        //SharpDX.Direct2D1.Device device;

        //target of rendering
        WindowRenderTarget target;

        //factory for creating 2D elements
        SharpDX.Direct2D1.Factory factory = new SharpDX.Direct2D1.Factory();

        DateTime lastTick = DateTime.Now;

        private void Form1_Load(object sender, EventArgs e)
        {
            //device = new SharpDX.Direct2D1.Device(factory.);
            //Init Direct Draw
            //Set Rendering properties
            RenderTargetProperties renderProp = new RenderTargetProperties()
            {
                DpiX = 0,
                DpiY = 0,
                MinLevel = FeatureLevel.Level_10,
                PixelFormat = new SharpDX.Direct2D1.PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied),
                Type = RenderTargetType.Default,
                Usage = RenderTargetUsage.None
            };

            //set hwnd target properties (permit to attach Direct2D to window)
            HwndRenderTargetProperties winProp = new HwndRenderTargetProperties()
            {
                Hwnd = this.Handle,
                PixelSize = new Size2(this.ClientSize.Width, this.ClientSize.Height),
                PresentOptions = PresentOptions.Immediately
            };

            //target creation
            target = new WindowRenderTarget(factory, renderProp, winProp);

            game = new Game(target);

            //avoid artifacts
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);

            //minimal size
            this.MinimumSize = new Size(800, 600);
            this.Size = new Size(800, 600);
            this.MaximumSize = new Size(800, 600);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //draw elements
            Draw();
            //force refresh
            this.Invalidate();
        }

        private void Draw()
        {
            //begin rendering
            target.BeginDraw();
            //clear target
            target.Clear(SharpDX.Color.Black);

            float delta = (DateTime.Now.Ticks - lastTick.Ticks) / 10000000f;
            lastTick = DateTime.Now;
            game.Update(delta);
            game.Draw();

            //end drawing
            target.EndDraw();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //resize target
            target.Resize(new Size2(this.ClientSize.Width, this.ClientSize.Height));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            target.Dispose();
            factory.Dispose();

            game.Closing();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            game.OnMouseClick(e.X, e.Y, e.Button);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (game != null)
            {
                game.OnMouseMove(e.X, e.Y);
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            game.OnKeyPress(e);
        }
    }
}
