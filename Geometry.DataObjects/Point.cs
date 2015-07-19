using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometry.DataObjects
{
    [Serializable()]
    [DataContract()]
    public class PointDO : IEquatable<PointDO>
    {
        private int top;
        private int left;



        public PointDO(int top, int left)
        {
            this.top = top;
            this.left = left;
        }

        [DataMember()]
        public int Top
        {
          get { return top; }
        }

        [DataMember()]
        public int Left
        {
          get { return left; }
        }

        public bool Equals(PointDO other)
        {
            if (other == null) return false;
            return (this.Left == other.Left && this.Top == other.Top);
        }
    }
}
