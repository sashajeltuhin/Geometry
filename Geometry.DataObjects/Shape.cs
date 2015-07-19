using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Geometry.DataObjects
{
    [Serializable()]
    [DataContract()]
    public class Shape :IShape
    {
        private int top;
        private int left;
        
        private int width;
        private int height;

        private List<PointDO> points;

        public Shape()
        {
        }

        public Shape(int top, int left, int width, int height)
        {
            this.top = top;
            this.left = left;
            this.height = height;
            this.width = width;
        }

        [DataMember()]
        public int Top
        {
            get { return top; }
            set { top = value; }
        }
        

        [DataMember()]
        public int Left
        {
            get { return left; }
            set { left = value; }
        }

        [DataMember()]
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        [DataMember()]
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        [DataMember()]
        public List<PointDO> Points
        {
            get { return points; }
            set { points = value; }
        }

        public virtual bool IsValid()
        {
            return true;
        }
    }
}
