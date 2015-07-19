using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometry.DataObjects
{
   /// <summary>
	/// Definitions of known relationship types
	/// </summary>
    public class RelationshipType
    {
        /// <summary>
        /// Two rectangles do not share any points.
        /// </summary>
        public const int None = 0;
        /// <summary>
        /// Intersection: two rectangles have one or more intersecting lines and produce a result identifying the points of intersection.
        /// </summary>
        public const int Intersection = 1;

        /// <summary>
        /// Containment:a rectangle is wholly contained within another rectangle.
        /// </summary>
        public const int Containment = 2;

        /// <summary>
        /// Adjacency: Two rectangles are adjacent. Adjacency is defined as the sharing of a side. Side sharing may be proper or sub-line, 
        /// where a sub-line share is one where one side of rectangle A is a line that exists as a set of points wholly contained on 
        /// some other side of rectangle B.
        /// </summary>
        public const int Adjacency = 3;

        /// <summary>
        /// Rectangles are identical in size and fully overlapping
        /// </summary>
        public const int FullOverlap = 4;

        /// <summary>
        /// Rectangles are partially adjacent, each sharing a sub-line
        /// </summary>
        public const int Touching = 5;
    }
}
