<%@ WebHandler Language="C#" Class="FileUploadHandler" %>

using System;
using System.Web;
using System.IO;

public class FileUploadHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            HttpFileCollection files = context.Request.Files;
            string fname = "";
            string newFname = "";

            DateTime dt = DateTime.Now;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                var fileextension = new FileInfo(file.FileName).Extension;
                string origFname = file.FileName.Substring(0, file.FileName.IndexOf("."));
                newFname = origFname + "_" + dt.ToString("yyyyMMddHHmmss") + fileextension;
                fname = context.Server.MapPath("~/uploads/HCMUploads/" + newFname);
                file.SaveAs(fname);
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(newFname + "");
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}  