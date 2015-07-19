using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Geometry.DataObjects;

namespace Geometry.Models
{
    public class RectangleModel
    {
        public bool Success { get; set; }
        public string Header { get; set; }
        public string Message { get; set; }
        public RectangleDO r1 { get; set; }
        public RectangleDO r2 { get; set; }
        public Shape Diff { get; set; }
    }
}