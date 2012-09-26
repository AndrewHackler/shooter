// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBoundingCircle.cs" company="Christopher Harris">
//   2011 Christopher Harris
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Xna.Framework;

namespace Krypton.Design
{
    /// <summary>
    /// An object with a circular boundry
    /// </summary>
    public interface IBoundingCircle
    {
        /// <summary>
        /// Gets RadiusSquared.
        /// </summary>
        float RadiusSquared { get; }

        /// <summary>
        /// Gets Position.
        /// </summary>
        Vector2 Position { get; }
    }
}