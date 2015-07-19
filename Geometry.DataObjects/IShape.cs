using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometry.DataObjects
{
    /// <summary>
    /// Contract definition for all shapes
    /// </summary>
    public interface IShape
    {
        List<PointDO> Points { get; }
        bool IsValid();
    }
}
