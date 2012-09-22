using System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Shooter.Engine;

namespace Shooter.Game
{
    public class RelativeRobotController : ICanUpdate
    {
        private readonly Body body;

        public RelativeRobotController(Body body)
        {
            this.body = body;
        }

        public void Update(float dt)
        {
            var state = Keyboard.GetState();

            var linearVelocity = 0f;
            var angularVelocity = 0f;

            const float linearSpeed = 10f;
            const float angularSpeed = 6f;

            if (state.IsKeyDown(Keys.Left))
            {
                angularVelocity += angularSpeed;
            }

            if (state.IsKeyDown(Keys.Right))
            {
                angularVelocity -= angularSpeed;
            }

            if (state.IsKeyDown(Keys.Up))
            {
                linearVelocity += linearSpeed;
            }

            if (state.IsKeyDown(Keys.Down))
            {
                linearVelocity -= linearSpeed;
            }

            this.body.AngularVelocity += angularVelocity * dt;

            var x = (float)Math.Cos(this.body.Rotation);
            var y = (float)Math.Sin(this.body.Rotation);

            this.body.LinearVelocity += new Vector2(x, y) * linearVelocity * dt;
        }
    }
}