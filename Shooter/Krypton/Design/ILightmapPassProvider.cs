using System.Collections.Generic;

namespace Krypton.Design
{
    /// <summary>
    /// A Lightmap Pass Provider
    /// </summary>
    public interface ILightmapPassProvider
    {
        /// <summary>
        /// Gets Passes.
        /// </summary>
        IEnumerable<ILightmapPass> Passes { get; }
    }

}