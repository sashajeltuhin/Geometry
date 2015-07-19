using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometry.DataObjects
{
    public class RelationshipDO
    {
        private string typeName = string.Empty;
        private string typeDescription = string.Empty;
        private int type;
        private IShape compResult;


        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }
        
        public string TypeDescription
        {
            get { return typeDescription; }
            set { typeDescription = value; }
        }
        
        public IShape CompResult
        {
            get { return compResult; }
            set { compResult = value; }
        }

        public int Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
