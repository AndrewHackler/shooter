using System.Collections;
using System.Collections.Generic;

namespace Shooter.Engine.Viewports
{
    public class PerspectiveManager : IEnumerable<Perspective>
    {
        public List<Perspective> Perspectives { get; private set; }

        public Perspective CurrentPerspective { get; set; }

        public PerspectiveManager()
        {
            this.Perspectives = new List<Perspective>();
        }

        public IEnumerator<Perspective> GetEnumerator()
        {
            return this.Perspectives.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}