using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometry.DataObjects
{
    public interface IShape
    {
        List<PointDO> Points { get; }
        bool IsValid();
    }
}
