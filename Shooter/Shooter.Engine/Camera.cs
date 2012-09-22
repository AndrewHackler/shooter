// -----------------------------------------------------------------------
// <copyright file="Camera.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shooter.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Camera : IMatrixProvider
    {
        private readonly GraphicsDevice device;

        public Vector2 Position { get; set; }

        public float VerticalUnits { get; set; }

        public Camera(GraphicsDevice device)
        {
            this.device = device;
            this.VerticalUnits = 15f;
        }

        public Matrix GetMatrix()
        {
            var aspectRatio = device.Viewport.Width / (float)device.Viewport.Height;

            var undoMatrix = Matrix.CreateScale(1f, -1f, 1f) *
                             Matrix.CreateTranslation(0.5f, 0.5f, 0.0f) *
                             Matrix.CreateScale(device.Viewport.Width, device.Viewport.Height, 1f);

            var viewMatrix = Matrix.CreateOrthographic(aspectRatio * this.VerticalUnits, this.VerticalUnits, 0, 1f);

            var matrix = viewMatrix * undoMatrix;

            return matrix;
        }
    }

    public interface IMatrixProvider
    {
        Matrix GetMatrix();
    }
}