using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Geometry.DataObjects
{
    /// <summary>
    /// A simple 2 point line
    /// </summary>
    [Serializable()]
    [DataContract()]
    public class LineDO : Shape
    {
        public LineDO(PointDO p1, PointDO p2)
        {
            this.Points = new List<PointDO>();
            this.Points.Add(p1);
            this.Points.Add(p2);
            if (p1.Left <= p2.Left)
            {
                this.Left = p1.Left;
            }
            else
            {
                this.Left = p2.Left;
            }

            if (p1.Top <= p2.Top)
            {
                this.Top = p1.Top;
            }
            else
            {
                this.Top = p2.Top;
            }

            this.Height = Math.Abs(p2.Top - p1.Top);
            this.Width = Math.Abs(p2.Left - p1.Left);
        }


        public double Length
        {
            get
            {
                if (this.Points.Count != 2)
                {
                    return 0;
                }
                return Math.Sqrt(Math.Abs(this.Points[0].Top - this.Points[1].Top) * Math.Abs(this.Points[0].Top - this.Points[1].Top) + Math.Abs(this.Points[0].Left - this.Points[1].Left) * Math.Abs(this.Points[0].Left - this.Points[1].Left));
            }
        }

        public override bool IsValid()
        {
            return this.Length > 0;
        }
    }
}
