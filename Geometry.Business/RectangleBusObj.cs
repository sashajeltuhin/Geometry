using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geometry.DataObjects;
using Geometry.Text;

namespace Geometry.Business
{
    internal class RectangleBusObj : IDisposable
    {
        public RelationshipDO GetRelationship(RectangleDO r1, RectangleDO r2)
        {
            if (r1 == null || !r1.IsValid())
            {
                throw new GeometryException("MSG_ERR_INVALID RECT", "eng");
            }
            if (r2 == null || !r2.IsValid())
            {
                throw new GeometryException("MSG_ERR_INVALID RECT", "eng");
            }
            RelationshipDO result = null;
            if (r1.Area > r2.Area)
            {
                result = GetDiff(r1, r2, false);
            }
            else
            {
                result = GetDiff(r2, r1, r1.Area == r2.Area);
            }
            return result;           
        }
        
        /// <summary>
        /// Builds the complete <see cref="RelationshipDO"/> object
        /// </summary>
        /// <param name="r1">Larger rectangle, if different</param>
        /// <param name="r2">Smaller rectangle, if different</param>
        /// <returns></returns>
        private RelationshipDO GetDiff(RectangleDO r1, RectangleDO r2, bool equalSize)
        {
            RelationshipDO result = new RelationshipDO();
            List<PointDO> insidePoints = new List<PointDO>();
            List<PointDO> borderPoints = new List<PointDO>();;
            try
            {
                //see which points of the hypothetically smaller rectangle
                //fit in the larger one
                AnalyzeIntersect(r1, r2, insidePoints, borderPoints);
                //second rectangle is fully within the first one
                if (insidePoints.Count + borderPoints.Count == 4)
                {
                    if (equalSize)
                    {
                        result.Type = RelationshipType.FullOverlap;
                    }
                    else
                    {
                        result.Type = RelationshipType.Containment;
                        result.CompResult = r2;
                    }
                }
                //smaller r2 intersecting with r1
                else if (insidePoints.Count == 2 && borderPoints.Count == 0 || insidePoints.Count == 1 && borderPoints.Count == 1)
                {
                    result.Type = RelationshipType.Intersection;
                    result.CompResult = GetIntersect(r1, r2, insidePoints, borderPoints);
                }
                //if there are no border points nor inside points => the rectangles do not touch
                else if (insidePoints.Count == 0 && borderPoints.Count == 0)
                {
                    result.Type = RelationshipType.None;
                    result.CompResult = null;
                }
                
                //touching, but not adjacent
                else if (insidePoints.Count == 0 && borderPoints.Count == 1)
                {
                    result.Type = RelationshipType.Touching;
                }
                else //need to find which points of r1 fall within r2
                {
                    AnalyzeIntersect(r2, r1, insidePoints, borderPoints);
                    if (borderPoints.Count == 2)
                    {
                        result.Type = RelationshipType.Adjacency;
                        result.CompResult = new LineDO(borderPoints[0], borderPoints[1]);
                    }
                    else if (insidePoints.Count == 2)
                    {
                        result.Type = RelationshipType.Intersection;
                        result.CompResult = GetIntersect(r1, r2, insidePoints, borderPoints);
                    }
                    else if (borderPoints.Count == 4)
                    {
                        result.Type = RelationshipType.Intersection;
                        result.CompResult = GetIntersect(r1, r2, insidePoints, borderPoints);
                    } 
                }
            }
            finally
            {
                if (insidePoints != null)
                {
                    insidePoints.Clear();
                    insidePoints = null;
                }
                if (borderPoints != null)
                {
                    borderPoints.Clear();
                    borderPoints = null;
                }
            }
            return result;
        }

        /// <summary>
        /// Helper method to check intersect points
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <param name="insidePoints"></param>
        /// <param name="borderPoints"></param>
        private static void AnalyzeIntersect(RectangleDO r1, RectangleDO r2, List<PointDO> insidePoints, List<PointDO> borderPoints)
        {

            foreach (PointDO p in r2.Points)
            {
                PointLocation loc = r1.ContainsPoint(p);
                switch (loc)
                {
                    case PointLocation.Inside:
                        if (!insidePoints.Contains(p))
                        {
                            insidePoints.Add(p);
                        }
                        break;
                    case PointLocation.Border:
                        if (!borderPoints.Contains(p))
                        {
                            borderPoints.Add(p);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Helper method to retrieve the area of the intersection provided by <see cref="RectangleDO"/>
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <param name="insidePoints"></param>
        /// <param name="borderPoints"></param>
        /// <returns></returns>
        private IShape GetIntersect(RectangleDO r1, RectangleDO r2, List<PointDO> insidePoints, List<PointDO> borderPoints)
        {
            List<PointDO> combined = new List<PointDO>();
            combined.AddRange(insidePoints);
            combined.AddRange(borderPoints);
            return r1.FindIntersect(combined, r2);
        }


        public void Dispose()
        {
            //clean up
        }
    }
}
