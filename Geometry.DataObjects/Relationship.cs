using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometry.DataObjects
{
    /// <summary>
    /// Describes the relationship of two rectangles in a 2-d space
    /// </summary>
    public class RelationshipDO
    {
        private string typeName = string.Empty;
        private string typeDescription = string.Empty;
        private int type;
        private IShape compResult;

        /// <summary>
        /// String literal of the relationship name. Derived from Type
        /// </summary>
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }
        
        /// <summary>
        /// String literal. Description of the relationship.
        /// </summary>
        public string TypeDescription
        {
            get { return typeDescription; }
            set { typeDescription = value; }
        }
        
        /// <summary>
        /// Optional result of the interaction - a border or an intersect
        /// </summary>
        public IShape CompResult
        {
            get { return compResult; }
            set { compResult = value; }
        }

        /// <summary>
        /// Unique identifier of the relationship. Relies on static definitions in <see cref="RelationshipType"/>
        /// </summary>
        public int Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
