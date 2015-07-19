using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geometry.DataObjects;
using Geometry.Text;

namespace Geometry.Business
{
    /// <summary>
    /// Entry point into the Business Logic tier of the app
    /// </summary>
    public class AppBusObj : IDisposable
    {
        /// <summary>
        /// Determines the type of the relationship between the rectangles and provides a human readable description of the relationship
        /// </summary>
        /// <param name="r1">First Rectangle</param>
        /// <param name="r2">Second Rectangle</param>
        /// <returns>An instance of <see cref="RelationshipDO"/> that represents the current relationship</returns>
        public RelationshipDO GetRelationship(RectangleDO r1, RectangleDO r2)
        {
            RelationshipDO rel = null;
            RectangleBusObj busObj = null;
            try
            {
                busObj = new RectangleBusObj();
                rel = busObj.GetRelationship(r1, r2);
                if (rel == null)
                {
                    throw new GeometryException("MSG_ERR_LOGIC_COMPARE", "eng");
                }
                rel.TypeName = TextFactory.Instance.GetText(rel.Type.ToString(), "eng");
                rel.TypeDescription = TextFactory.Instance.GetLongText(rel.Type.ToString(), "eng");
            }
            catch (Exception ex)
            {
                throw new GeometryException("MSG_ERR_RECT_COMPARE", "eng",  ex);
            }
            finally
            {
                if (busObj != null)
                {
                    busObj.Dispose();
                }
            }
            return rel;
        }

        public void Dispose()
        {
            
        }
    }
}
