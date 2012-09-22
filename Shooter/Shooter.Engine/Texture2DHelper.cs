using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shooter.Engine
{
    public static class Texture2DHelper
    {
        public static Vector2 Size(this Texture2D tex)
        {
            return new Vector2(tex.Width, tex.Height);
        }
    }

    public static class MouseStateHelper
    {
        public static Vector2 PositionToVector2(this MouseState state)
        {
            return new Vector2(state.X, state.Y);
        }
    }
}