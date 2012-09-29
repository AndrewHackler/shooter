using System;
using FarseerPhysics.Dynamics;
using Krypton.Lights;
using Shooter.Engine;
using Shooter.Engine.Core;

namespace Shooter.Gameplay
{
    public class RobotLinker : ICanUpdate
    {
        private readonly RobotModel robotModel;
        private readonly PointLight light;
        private readonly Body body;

        public RobotLinker(Body body, RobotModel robotModel, PointLight light)
        {
            this.body = body;
            this.robotModel = robotModel;
            this.light = light;
        }

        public void Update(float dt)
        {
            this.robotModel.Position = this.body.Position;

            if (this.body.LinearVelocity.LengthSquared() > 0)
            {
                this.robotModel.LowerBodyRotation = (float)Math.Atan2(this.body.LinearVelocity.Y, this.body.LinearVelocity.X);
            }

            // this.light.Position = this.RobotModel.Position;
        }
    }
}