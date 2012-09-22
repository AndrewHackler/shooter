// -----------------------------------------------------------------------
// <copyright file="CameraController.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Shooter.Engine;

namespace Shooter.Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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
            throw new NotImplementedException();
        }
    }
}
