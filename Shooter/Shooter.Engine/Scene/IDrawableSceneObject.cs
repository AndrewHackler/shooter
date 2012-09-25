namespace Shooter.Engine.Scene
{
    public interface IDrawableSceneObject
    {
        bool Intersects(object bounds);
        void Draw(float dt);
    }
}