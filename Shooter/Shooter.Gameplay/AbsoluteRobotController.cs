using System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Shooter.Engine;

namespace Shooter.Gameplay
{
    public class AbsoluteRobotController // : ICanUpdate
    {
        private readonly Body body;
        private readonly ShooterEngine engine;
        private readonly Robot robot;

        public AbsoluteRobotController(ShooterEngine engine, Robot robot, Body body)
        {
            this.engine = engine;
            this.robot = robot;
            this.body = body;
        }

        public void Update(float dt)
        {
            var state = Keyboard.GetState();

            float speed = 30f;

            if (state.IsKeyDown(Keys.LeftShift)) { speed = 60f; }

            Vector2 linearVelocity = Vector2.Zero;

            if (state.IsKeyDown(Keys.A)) { linearVelocity -= Vector2.UnitX; }
            if (state.IsKeyDown(Keys.D)) { linearVelocity += Vector2.UnitX; }
            if (state.IsKeyDown(Keys.W)) { linearVelocity += Vector2.UnitY; }
            if (state.IsKeyDown(Keys.S)) { linearVelocity -= Vector2.UnitY; }

            if (linearVelocity.LengthSquared() > 0)
            {
                linearVelocity.Normalize();
            }

            this.body.LinearVelocity += linearVelocity * speed * dt;


            // Turret Rotation
            var cursorPosition = engine.InputManager.GetMouseTarget();

            if (cursorPosition.HasValue)
            {
                var targetPosition = cursorPosition.Value - this.body.Position;

                this.robot.TurretRotation = (float)Math.Atan2(targetPosition.Y, targetPosition.X);
            }
        }
    }
}