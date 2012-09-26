using System;
using FarseerPhysics.Dynamics;
using Krypton.Lights;
using Shooter.Engine;
using Shooter.Engine.Core;

namespace Shooter.Gameplay
{
    public class RobotLinker : ICanUpdate
    {
        private readonly Robot robot;
        private readonly PointLight light;
        private readonly Body body;

        public RobotLinker(Body body, Robot robot, PointLight light)
        {
            this.body = body;
            this.robot = robot;
            this.light = light;
        }

        public void Update(float dt)
        {
            this.robot.Position = this.body.Position;

            if (this.body.LinearVelocity.LengthSquared() > 0)
            {
                this.robot.LowerBodyRotation = (float)Math.Atan2(this.body.LinearVelocity.Y, this.body.LinearVelocity.X);
            }

            this.light.Position = this.robot.Position;
        }
    }
}