using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geometry.DataObjects;
using Geometry.Text;

namespace Geometry.Business
{
    public class AppBusObj : IDisposable
    {
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
