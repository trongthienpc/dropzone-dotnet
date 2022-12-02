using Microsoft.AspNetCore.Mvc;




namespace test.Controllers
{
    public class DropzoneController : Controller
    {
        private readonly ILogger<DropzoneController> _logger;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;

        public DropzoneController(ILogger<DropzoneController> logger, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpPost]
        public async Task<ActionResult> SaveUploadedFile(List<IFormFile> files)
        {
            bool SavedSuccessfully = true;
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
            List<string> urls = new List<string>();
            try
            {
                foreach (IFormFile file in files)
                {
                    string fName = file.FileName;
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/img", fName);
                    urls.Add(savePath);
                    using (var str = new FileStream(savePath, FileMode.Create))
                    {
                        file.CopyTo(str);
                    }

                }
                //loop through all the files
                //foreach (var file in files)
                //{
                //    //Save file content goes here
                //    fName = file.FileName;
                //    if (file != null && file.ContentLength > 0)
                //    {
                //        var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\", Server.MapPath(@"\")));

                //        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                //        var fileName1 = Path.GetFileName(file.FileName);

                //        bool isExists = System.IO.Directory.Exists(pathString);

                //        if (!isExists)
                //            System.IO.Directory.CreateDirectory(pathString);

                //        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                //        file.SaveAs(path);
                //    }
                //}
            }
            catch (Exception ex)
            {
                SavedSuccessfully = false;
            }

            if (SavedSuccessfully)
            {
                return Json(urls);
            }
            else
            {
                return RedirectToAction("Index", new { Message = "Error in saving file" });
            }
        }
    }
}