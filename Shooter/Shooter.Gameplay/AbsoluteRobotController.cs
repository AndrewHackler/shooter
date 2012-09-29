using System;
using FarseerPhysics.Dynamics;
using Krypton.Lights;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Shooter.Engine;
using Shooter.Engine.Core;

namespace Shooter.Gameplay
{
    public class AbsoluteRobotController : ICanUpdate
    {
        private readonly ShooterEngine engine;
        private readonly Body body;
        private readonly PointLight light;
        private readonly RobotModel robotModel;

        public AbsoluteRobotController(ShooterEngine engine, RobotModel robotModel, Body body, PointLight light)
        {
            this.engine = engine;
            this.robotModel = robotModel;
            this.body = body;
            this.light = light;
        }

        public void Update(float dt)
        {
            var state = Keyboard.GetState();

            float speed = 30f;

            if (state.IsKeyDown(Keys.LeftShift))
            {
                speed = 60f;
            }

            Vector2 linearVelocity = Vector2.Zero;

            if (state.IsKeyDown(Keys.A))
            {
                linearVelocity -= Vector2.UnitX;
            }
            if (state.IsKeyDown(Keys.D))
            {
                linearVelocity += Vector2.UnitX;
            }
            if (state.IsKeyDown(Keys.W))
            {
                linearVelocity += Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.S))
            {
                linearVelocity -= Vector2.UnitY;
            }

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

                this.robotModel.TurretRotation = (float)Math.Atan2(targetPosition.Y, targetPosition.X);
            }

            //var offset = GetAngularOffset(this.RobotModel.LowerBodyRotation, this.light.Rotation);

            //this.light.Rotation = this.RobotModel.TurretRotation;
            //this.light.Rotation += offset * 10 * dt;
        }

        private static float GetAngularOffset(float a, float b)
        {
            var angle = (a - b) % MathHelper.TwoPi;

            if (angle > MathHelper.Pi)
            {
                angle -= MathHelper.TwoPi;
            }

            if (angle < -MathHelper.Pi)
            {
                angle += MathHelper.TwoPi;
            }

            return angle;
        }
    }
}