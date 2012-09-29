// -----------------------------------------------------------------------
// <copyright file="CameraController.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Shooter.Engine;
using Shooter.Engine.Cameras;
using Shooter.Engine.Core;

namespace Shooter.Gameplay
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CameraController : ICanUpdate
    {
        private readonly Camera camera;

        public CameraController(Camera camera)
        {
            this.camera = camera;
        }

        public void Update(float dt)
        {
            var state = Keyboard.GetState();

            float linearSpeed = 10f * this.camera.ZoomFactor;
            float zoomSpeed = 1f;
            float angularSpeed = 1f;

            Vector2 linearVelocity = Vector2.Zero;
            float zoomVelocity = 0f;
            float angularVelocity = 0f;

            if (state.IsKeyDown(Keys.Up))
            {
                linearVelocity += Vector2.UnitY;
            }

            if (state.IsKeyDown(Keys.Down))
            {
                linearVelocity -= Vector2.UnitY;
            }

            if (state.IsKeyDown(Keys.Left))
            {
                linearVelocity -= Vector2.UnitX;
            }

            if (state.IsKeyDown(Keys.Right))
            {
                linearVelocity += Vector2.UnitX;
            }

            if (state.IsKeyDown(Keys.Z))
            {
                zoomVelocity += zoomSpeed;
            }

            if (state.IsKeyDown(Keys.X))
            {
                zoomVelocity-= zoomSpeed;
            }

            if (state.IsKeyDown(Keys.OemComma))
            {
                angularVelocity += angularSpeed;
            }

            if (state.IsKeyDown(Keys.OemPeriod))
            {
                angularVelocity -= angularSpeed;
            }

            this.camera.Zoom += zoomVelocity * dt;
            this.camera.Rotation += angularVelocity * dt;
            this.camera.Position += linearVelocity * linearSpeed * dt;
        }
    }
}
