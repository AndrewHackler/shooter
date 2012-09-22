using System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Engine;

namespace Shooter.Game
{
    public class AbsoluteRobotController : ICanUpdate
    {
        private readonly Body body;
        private readonly IMatrixProvider wvpMatrixProvider;
        private readonly Camera camera;
        private readonly Robot robot;

        public AbsoluteRobotController(Robot robot, Body body, IMatrixProvider wvpMatrixProvider)
        {
            this.robot = robot;
            this.body = body;
            this.wvpMatrixProvider = wvpMatrixProvider;
            this.camera = camera;
        }

        public void Update(float dt)
        {
            var state = Keyboard.GetState();

            float speed = 30f;

            if (state.IsKeyDown(Keys.LeftShift)) { speed = 10f; }

            Vector2 linearVelocity = Vector2.Zero;

            if (state.IsKeyDown(Keys.A)) { linearVelocity -= Vector2.UnitX; }
            if (state.IsKeyDown(Keys.D)) { linearVelocity += Vector2.UnitX; }
            if (state.IsKeyDown(Keys.W)) { linearVelocity += Vector2.UnitY; }
            if (state.IsKeyDown(Keys.S)) { linearVelocity -= Vector2.UnitY; }

            if (linearVelocity.LengthSquared() > 0)
            {
                linearVelocity.Normalize();
            }

            var mousePosition = Mouse.GetState().PositionToVector2();
            var wvpMatrix = wvpMatrixProvider.GetMatrix();
            var invMatrix = Matrix.Invert(wvpMatrix);
            var cursorPosition = Vector2.Transform(mousePosition, invMatrix);

            var targetPosition = cursorPosition - this.body.Position;

            this.robot.TurretRotation = (float)Math.Atan2(targetPosition.Y, targetPosition.X);

            this.body.LinearVelocity += linearVelocity * speed * dt;
            // this.body.Position = targetPosition;
        }
    }

    //public interface IPositionTransformer
    //{
    //    public IPositionTransformer()
    //    {
            
    //    }

    //    public Vector2 ConvertScreenToWorld(Vector2 v)
    //    {
            
    //    }
    //}
}