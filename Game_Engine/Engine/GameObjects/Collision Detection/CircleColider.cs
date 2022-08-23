using Microsoft.Xna.Framework;

namespace Game_Engine.Engine.GameObjects.Collision_Detection
{
    public class CircleColider
    {
        public Vector2 Center { get; set; }

        public float Radius { get; set; }

        public CircleColider(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public bool Intersects(Rectangle rectangle)
        {
            Vector2 v = new Vector2(MathHelper.Clamp(Center.X, rectangle.Left, rectangle.Right),
                MathHelper.Clamp(Center.Y, rectangle.Top, rectangle.Bottom));

            Vector2 direction = Center - v;
            float distanceSquared = direction.LengthSquared();

            return ((distanceSquared >= 0) && distanceSquared < Radius * Radius);
        }
    }
}
