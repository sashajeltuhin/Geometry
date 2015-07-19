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
        #region Fields       
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

        /// <summary>
        /// Determins how this instance interects with the passed in rectangle
        /// </summary>
        /// <param name="insidePoints">Points of the passed in rectangle</param>
        /// <param name="other">The other rectangle</param>
        /// <returns>Shape representing the Interection</returns>
        public IShape FindIntersect(List<PointDO> insidePoints, RectangleDO other)
        {
            IShape intersect = null;
            //4 point scenario: 1-dim overlapp
            if (insidePoints.Count == 4)
            {
                intersect = FromPoints(insidePoints);
            }
            else 
            {
                List<PointDO> complete = FindMissingPoints(insidePoints, other);
                intersect = FromPoints(complete);
            }
            
            //intersect

            return intersect;
        }

        /// <summary>
        /// Given vertices of the passed in rectangle that are within the bound of the current instance, determines
        /// the missing vertices of the intersection
        /// </summary>
        /// <param name="insidePoints">Vertices of the passed in rectangle that are within bounds of the current instance</param>
        /// <param name="other">The other rectangle</param>
        /// <returns>List of all vertices of the intersect region</returns>
        private List<PointDO> FindMissingPoints(List<PointDO> insidePoints, RectangleDO other)
        {
            List<PointDO> complete = new List<PointDO>();
            complete.AddRange(insidePoints);
            if (insidePoints.Count == 2 && insidePoints[0].Left != insidePoints[1].Left && insidePoints[0].Top != insidePoints[1].Top)
            {
                complete.Add(new PointDO(insidePoints[1].Top, insidePoints[0].Left));
                complete.Add(new PointDO(insidePoints[0].Top, insidePoints[1].Left));
            }
            else
            {
                if (other.Right > this.Right)
                {
                    foreach(PointDO p in insidePoints)
                    {
                        if(p.Left < this.Right)
                        {
                            PointDO missing = new PointDO(p.Top, this.Right);
                            if (!complete.Contains(missing))
                            {
                                complete.Add(missing);
                            }
                        }
                    }
                }
                else if (other.Left < this.Left)
                {
                    foreach(PointDO p in insidePoints)
                    {
                        if(p.Left > this.Left)
                        {
                            PointDO missing = new PointDO(p.Top, this.Left);
                            if (!complete.Contains(missing))
                            {
                                complete.Add(missing);
                            }
                        }
                    }
                }
                else if (other.Top < this.Top)
                {
                    foreach(PointDO p in insidePoints)
                    {
                        if(p.Top > this.Top)
                        {
                            PointDO missing = new PointDO(this.Top, p.Left);
                            if (!complete.Contains(missing))
                            {
                                complete.Add(missing);
                            }
                        }
                    }
                }
                else if (other.Bottom > this.Bottom)
                {
                    foreach(PointDO p in insidePoints)
                    {
                        if(p.Top < this.Bottom)
                        {
                            PointDO missing = new PointDO(this.Bottom, p.Left);
                            if (!complete.Contains(missing))
                            {
                                complete.Add(missing);
                            }
                        }
                    }
                }
            }
            return complete;
        }

        /// <summary>
        /// Builds a rectangle given an unordered list of its vertices
        /// </summary>
        /// <param name="points">List of vertices</param>
        /// <returns>RectangleDO object</returns>
        private IShape FromPoints(List<PointDO> points)
        {
            //find bottom right
            int bottom = 0; 
            int right = 0;
            foreach (PointDO p in points)
            {
                if(p.Left > right)
                {
                    right = p.Left;
                }
                if(p.Top > bottom)
                {
                    bottom = p.Top;
                }
            }

            //find bottom right
            int top = bottom; 
            int left = right;

            foreach (PointDO p in points)
            {
                if(p.Left < left)
                {
                    left = p.Left;
                }
                if(p.Top < top)
                {
                    top = p.Top;
                }
            }

            return new RectangleDO(top, left, right-left, bottom - top);
 
        }

        public override bool IsValid()
        {
            return this.Height > 0 && this.Width > 0;
        }
        #endregion
    }
}
