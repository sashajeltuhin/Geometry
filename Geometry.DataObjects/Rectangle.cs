using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometry.DataObjects
{
    [Serializable()]
    [DataContract()]
    public class RectangleDO : Shape
    {
        #region fields       
        private int right;
        private int bottom;
        #endregion

        #region Constructors
        public RectangleDO()
        {
        }

        public RectangleDO(int top, int left, int width, int height)
            : base(top, left, width, height)
        {
            recalc();
        }

        public RectangleDO(PointDO p1, PointDO p2, PointDO p3, PointDO p4)
        {
            
        }

        private void recalc()
        {
            this.right = this.Left + this.Width;
            this.bottom = this.Top + this.Height;
            this.Points = new List<PointDO>();
            this.Points.Add(new PointDO(this.Top, this.Left));
            this.Points.Add(new PointDO(this.Top, this.Right));
            this.Points.Add(new PointDO(this.Bottom, this.Left));
            this.Points.Add(new PointDO(this.Bottom, this.Right));
        }

        
        #endregion

        #region Properties
        

         [DataMember()]
        public int Right
        {
            get { return right; }
        }

         [DataMember()]
        public int Bottom
        {
            get { return bottom; }
        }
        
        public int Area
        {
            get { return this.Width * this.Height; }
        }

        
        #endregion

        #region Methods

        public PointLocation ContainsPoint(PointDO p)
        {
            PointLocation loc = PointLocation.Outside;
            if(p.Top > this.Top && p.Top < this.bottom && p.Left > this.Left && p.Left < this.right)
            {
                loc = PointLocation.Inside;
            }
            else if ((p.Top == this.Top && p.Left >= this.Left && p.Left <= this.right)
                || (p.Top == this.bottom && p.Left >= this.Left && p.Left <= this.right)
                || (p.Left == this.Left && p.Top >= this.Top && p.Top <= this.bottom) 
                || (p.Left == this.right && p.Top >= this.Top && p.Top <= this.bottom))
            {
                loc = PointLocation.Border;
            }
            return loc;
        }

        public override bool IsValid()
        {
            return this.Height > 0 && this.Width > 0;
        }
        #endregion
    }
}
