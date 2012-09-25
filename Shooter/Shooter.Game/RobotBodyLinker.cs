using System;
using FarseerPhysics.Dynamics;
using Shooter.Engine;
using Shooter.Engine.Core;

namespace Shooter.Gameplay
{
    public class RobotBodyLinker : ICanUpdate
    {
        private readonly Robot robot;
        private readonly Body body;

        public RobotBodyLinker(Robot robot, Body body)
        {
            this.robot = robot;
            this.body = body;
        }

        public void Update(float dt)
        {
            this.robot.Position = this.body.Position;

            if (this.body.LinearVelocity.LengthSquared() > 0)
            {
                this.robot.LowerBodyRotation = (float)Math.Atan2(this.body.LinearVelocity.Y, this.body.LinearVelocity.X);
            }
        }
    }
}