using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct2D1;

namespace DeepSpace
{
    class RouteRenderer: IRenderer, IDisposable
    {
        private Route route;
        private Brush brush;
        private StrokeStyle strokeStyle;
        private int team;
        private Vector2 center, perpendicular;
        public RouteRenderer(Route route)
        {
            this.route = route;
            this.team = 0;
            this.brush = route.game.brushes[team];
            this.center = (route.start + route.end) / 2.0f;
            Vector2 direction = route.start - route.end;
            this.perpendicular = new Vector2(direction.Y, -direction.X);
            this.perpendicular.Normalize();

            StrokeStyleProperties props = new StrokeStyleProperties();
            props.DashOffset = 0.1f;
            props.DashStyle = DashStyle.Dash;
            props.StartCap = CapStyle.Flat;
            props.EndCap = CapStyle.Flat;
            this.strokeStyle = new StrokeStyle(route.game.target.Factory, props);
        }

        public void Draw()
        {
            if (route.source.team == route.destination.team)
            {
                team = route.source.team;
            }
            else
            {
                team = 0;
            }
            brush = route.game.brushes[team];
            
            brush.Opacity = 0.7f;
            route.game.target.DrawLine(route.start, route.end, brush, 1.0f, strokeStyle);
            if (route.autoTransfer)
            {
                Vector2 direction = route.end - route.start;
                direction.Normalize();
                Vector2 head = center + 15.0f * direction;
                Vector2 first = center - 10.0f * direction + 10.0f * perpendicular;
                Vector2 second = center - 10.0f * direction - 10.0f * perpendicular;
                route.game.target.DrawLine(head, first, brush, 1.0f, strokeStyle);
                route.game.target.DrawLine(head, second, brush, 1.0f, strokeStyle);
            }
            brush.Opacity = 1.0f;

        }

        public void Dispose()
        {
            strokeStyle.Dispose();
        }
    }
}
