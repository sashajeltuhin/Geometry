using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geometry.Models;
using Geometry.DataObjects;
using Geometry.Business;
using Geometry.Text;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = TextFactory.Instance.GetText("MSG_HEADER", "eng");
            ViewBag.Instructions = TextFactory.Instance.GetText("MSG_INSTRUCT", "eng");

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public JsonResult InitBlocks(RectangleModel model)
        {
            
            return Json(model);
        }

        [HttpPost]
        public JsonResult Analyze(RectangleModel model)
        {
            AppBusObj busObj = new AppBusObj();
            try
            {
                RectangleDO r1 = new RectangleDO(model.r1.Top, model.r1.Left,  model.r1.Width, model.r1.Height);
                RectangleDO r2 = new RectangleDO(model.r2.Top, model.r2.Left,  model.r2.Width, model.r2.Height);
                RelationshipDO rel = busObj.GetRelationship(r1, r2);
                model.Header = rel.TypeName;
                model.Message = rel.TypeDescription;
                model.Success = true;
                if (rel.CompResult != null)
                {
                    model.Diff = (Shape)rel.CompResult;
                }
            }
            catch (Exception ex)
            {
                PackageError(model, ex);
            }
            
            finally
            {
                busObj.Dispose();
                busObj = null;
            }
            return Json(model);
        }

        private void PackageError(RectangleModel model, Exception ex)
        {
            model.Success = false;
            model.Header = TextFactory.Instance.GetText("MSG_ERR_HEADER", "eng");
            model.Message = GeometryException.GetFullMessage(ex);
        }
    }
}
