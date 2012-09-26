// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILightmapProvider.cs" company="Christopher Harris">
//   2011 Christopher Harris
// </copyright>
// <summary>
//   A Lightmap Provider
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Xna.Framework.Graphics;

namespace Krypton.Design
{
    /// <summary>
    /// A Lightmap Provider
    /// </summary>
    public interface ILightmapProvider
    {
        /// <summary>
        /// Gets the lightmap.
        /// </summary>
        Texture2D Lightmap { get; }
    }
}