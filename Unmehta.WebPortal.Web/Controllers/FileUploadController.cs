using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Unmehta.WebPortal.Common;
using System.Web.Script.Serialization;

namespace Unmehta.WebPortal.Web.Controllers
{

    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }
        
        [Route("upload_ckeditor")]
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            string _mianpath = "";
            if (file.ContentLength > 0)
            {
                string filePath = ConfigDetailsValue.AddBlogCategoryFileUploadPath;

                if (!filePath.Contains("|"))
                {
                    string ImageName = DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(file.FileName);
                    string ImagePath = filePath + "/" + ImageName;
                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));


                    // Create the path and file name to check for duplicates.
                    var pathToCheck1 = "/" + filePath + ImageName;

                    // Create a temporary file name to use for checking duplicates.
                    //var tempfileName1 = "";
                    string fileName = Path.GetFileName(ImageName);
                    FileInfo fi = new FileInfo(fileName);
                    string ext = fi.Extension.ToUpper();

                    string _path = Server.MapPath(filePath) + "/" + ImageName;

                    _mianpath = (filePath) + "/" + ImageName;

                    file.SaveAs(_path);
                }
            }


            return Json(_mianpath, JsonRequestBehavior.AllowGet);

        }

        [Route("fileBrowse")]
        [HttpPost]
        public ActionResult FileBrowse(HttpPostedFileBase file)
        {

            return View("fileBrowse");
        }

    }
}