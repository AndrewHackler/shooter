// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILightmapGenerator.cs" company="Christopher Harris">
//   2011 Christopher Harris
// </copyright>
// <summary>
//   AssemblyInfo.cs
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Krypton.Design
{
    /// <summary>
    /// A Lightmap Generator - Can draw a lightmap with some lights and hulls
    /// </summary>
    public interface ILightmapGenerator
    {
        /// <summary>
        /// Draws all lights and hulls to the lightmap
        /// </summary>
        /// <param name="lights">The lights.</param>
        /// <param name="hulls">The hulls.</param>
        void DrawLightmap(ILightmapPass pass, IList<ILight> lights, IList<IHull> hulls);
    }
}