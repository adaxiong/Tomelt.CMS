using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Tomelt.Mvc.AntiForgery;
using UEditor.Models;

namespace UEditor.Controllers
{
    
    public class UEditorController : Controller
    {
        /// <summary>
        /// 获取配置参数
        /// </summary>
        /// <returns></returns>
        public ActionResult Config()
        {
            string callback = Request["callback"];
            return Content(WriteJson(callback, UEditorConfig.Items), string.IsNullOrWhiteSpace(callback) ? "text/plain" : "application/javascript");
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult UploadFile(string fileType)
        {
            string callback = Request["callback"];
            UploadResult result = new UploadResult() { State = UploadState.Unknown };
            UEditorModel uploadConfig = new UEditorModel();
            if (fileType == "uploadimage")
            {
                uploadConfig = new UEditorModel()
                {
                    AllowExtensions = UEditorConfig.GetStringList("imageAllowFiles"),
                    PathFormat = UEditorConfig.GetString("imagePathFormat"),
                    SizeLimit = UEditorConfig.GetInt("imageMaxSize"),
                    UploadFieldName = UEditorConfig.GetString("imageFieldName")
                };
            }
            if (fileType == "uploadscrawl")
            {
                uploadConfig = new UEditorModel()
                {
                    AllowExtensions = new string[] { ".png" },
                    PathFormat = UEditorConfig.GetString("scrawlPathFormat"),
                    SizeLimit = UEditorConfig.GetInt("scrawlMaxSize"),
                    UploadFieldName = UEditorConfig.GetString("scrawlFieldName"),
                    Base64 = true,
                    Base64Filename = "scrawl.png"
                };
            }
            if (fileType == "uploadvideo")
            {
                uploadConfig = new UEditorModel()
                {
                    AllowExtensions = UEditorConfig.GetStringList("videoAllowFiles"),
                    PathFormat = UEditorConfig.GetString("videoPathFormat"),
                    SizeLimit = UEditorConfig.GetInt("videoMaxSize"),
                    UploadFieldName = UEditorConfig.GetString("videoFieldName")
                };
            }
            if (fileType == "uploadfile")
            {
                uploadConfig = new UEditorModel()
                {
                    AllowExtensions = UEditorConfig.GetStringList("fileAllowFiles"),
                    PathFormat = UEditorConfig.GetString("filePathFormat"),
                    SizeLimit = UEditorConfig.GetInt("fileMaxSize"),
                    UploadFieldName = UEditorConfig.GetString("fileFieldName")
                };
            }
            byte[] uploadFileBytes;
            string uploadFileName;
            if (uploadConfig.Base64)
            {
                uploadFileName = uploadConfig.Base64Filename;
                uploadFileBytes = Convert.FromBase64String(Request[uploadConfig.UploadFieldName]);
            }
            else
            {
                var file = Request.Files[uploadConfig.UploadFieldName];
                uploadFileName = file.FileName;
                var fileExtension = Path.GetExtension(uploadFileName).ToLower();

                if (!uploadConfig.AllowExtensions.Select(x => x.ToLower()).Contains(fileExtension))
                {
                    result.State = UploadState.TypeNotAllow;
                    return Content(WriteUploadResult(callback, result), string.IsNullOrWhiteSpace(callback) ? "text/plain" : "application/javascript");

                }
                if (!(file.ContentLength < uploadConfig.SizeLimit))
                {
                    result.State = UploadState.SizeLimitExceed;
                    return Content(WriteUploadResult(callback, result), string.IsNullOrWhiteSpace(callback) ? "text/plain" : "application/javascript");
                }
                uploadFileBytes = new byte[file.ContentLength];
                try
                {
                    file.InputStream.Read(uploadFileBytes, 0, file.ContentLength);
                }
                catch (Exception)
                {
                    result.State = UploadState.NetworkError;
                    return Content(WriteUploadResult(callback, result), string.IsNullOrWhiteSpace(callback) ? "text/plain" : "application/javascript");
                }

            }
            result.OriginFileName = uploadFileName;

            var savePath = PathFormatter.Format(uploadFileName, uploadConfig.PathFormat);
            var localPath = Server.MapPath(savePath);
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(localPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(localPath));
                }
                System.IO.File.WriteAllBytes(localPath, uploadFileBytes);
                result.Url = savePath;
                result.State = UploadState.Success;
            }
            catch (Exception e)
            {
                result.State = UploadState.FileAccessError;
                result.ErrorMessage = e.Message;
            }
            return Content(WriteUploadResult(callback, result), string.IsNullOrWhiteSpace(callback) ? "text/plain" : "application/javascript");
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public ActionResult GetFileList(string fileType)
        {
            string callback = Request["callback"];
            ListFile listFile = new ListFile();
            if (fileType == "listimage")
            {
                listFile.SearchExtensions = UEditorConfig.GetStringList("imageManagerAllowFiles");
                listFile.PathToList = UEditorConfig.GetString("imageManagerListPath");
            }
            if (fileType == "listfile")
            {
                listFile.SearchExtensions = UEditorConfig.GetStringList("fileManagerAllowFiles");
                listFile.PathToList = UEditorConfig.GetString("fileManagerListPath");
            }
            try
            {
                listFile.Start = String.IsNullOrEmpty(Request["start"]) ? 0 : Convert.ToInt32(Request["start"]);
                listFile.Size = String.IsNullOrEmpty(Request["size"]) ? UEditorConfig.GetInt("imageManagerListSize") : Convert.ToInt32(Request["size"]);
            }
            catch (FormatException)
            {
                listFile.State = ResultState.InvalidParam;
                return Content(WriteFileResult(callback, listFile), string.IsNullOrWhiteSpace(callback) ? "text/plain" : "application/javascript");

            }
            string result = string.Empty;
            var buildingList = new List<String>();
            try
            {
                var localPath = Server.MapPath(listFile.PathToList);
                buildingList.AddRange(Directory.GetFiles(localPath, "*", SearchOption.AllDirectories)
                    .Where(x => listFile.SearchExtensions.Contains(Path.GetExtension(x).ToLower()))
                    .Select(x => listFile.PathToList + x.Substring(localPath.Length).Replace("\\", "/")));
                listFile.Total = buildingList.Count;
                listFile.FileList = buildingList.OrderBy(x => x).Skip(listFile.Start).Take(listFile.Size).ToArray();
            }
            catch (UnauthorizedAccessException)
            {
                listFile.State = ResultState.AuthorizError;
            }
            catch (DirectoryNotFoundException)
            {
                listFile.State = ResultState.PathNotFound;
            }
            catch (IOException)
            {
                listFile.State = ResultState.IOError;
            }
            finally
            {
                result = WriteFileResult(callback, listFile);
            }

            return Content(result, string.IsNullOrWhiteSpace(callback) ? "text/plain" : "application/javascript");
        }

        /// <summary>
        /// 远程抓图
        /// </summary>
        /// <returns></returns>
        public ActionResult CatchImage()
        {
            string callback = Request["callback"];
            var sources = Request.Form.GetValues("source[]");
            if (sources == null || sources.Length == 0)
            {
                return Content(WriteJson(callback, new
                {
                    state = "参数错误：没有指定抓取源"
                }), string.IsNullOrWhiteSpace(callback) ? "text/plain" : "application/javascript");
            }

            var crawlers = sources.Select(x => new Crawler(x).Fetch()).ToArray();
            return Content(WriteJson(callback, new
            {
                state = "SUCCESS",
                list = crawlers.Select(x => new
                {
                    state = x.State,
                    source = x.SourceUrl,
                    url = x.ServerUrl
                })
            }), string.IsNullOrWhiteSpace(callback) ? "text/plain" : "application/javascript");

        }

        public ActionResult Test()
        {
            string callback = Request["callback"];
            return Content(WriteJson(callback, new
            {
                state = "看到此信息，说明百度编辑器后端服务调用成功！"
            }), string.IsNullOrWhiteSpace(callback) ? "text/plain" : "application/javascript");
        }

















        private string WriteFileResult(string callback, ListFile listFile)
        {
            return WriteJson(callback, new
            {
                state = GetStateString(listFile.State),
                list = listFile.FileList == null ? null : listFile.FileList.Select(x => new { url = x }),
                start = listFile.Start,
                size = listFile.Size,
                total = listFile.Total
            });
        }

        private string GetStateString(ResultState state)
        {
            switch (state)
            {
                case ResultState.Success:
                    return "SUCCESS";
                case ResultState.InvalidParam:
                    return "参数不正确";
                case ResultState.PathNotFound:
                    return "路径不存在";
                case ResultState.AuthorizError:
                    return "文件系统权限不足";
                case ResultState.IOError:
                    return "文件系统读取错误";
            }
            return "未知错误";
        }


        private string WriteUploadResult(string callback, UploadResult result)
        {
            return WriteJson(callback, new
            {
                state = GetStateMessage(result.State),
                url = result.Url,
                title = result.OriginFileName,
                original = result.OriginFileName,
                error = result.ErrorMessage
            });
        }

        private string WriteJson(string callback, object json)
        {
            if (string.IsNullOrWhiteSpace(callback))
            {
                return JsonConvert.SerializeObject(json);
            }
            return string.Format("{0}({1});", callback, json);
        }
        private string GetStateMessage(UploadState state)
        {
            switch (state)
            {
                case UploadState.Success:
                    return "SUCCESS";
                case UploadState.FileAccessError:
                    return "文件访问出错，请检查写入权限";
                case UploadState.SizeLimitExceed:
                    return "文件大小超出服务器限制";
                case UploadState.TypeNotAllow:
                    return "不允许的文件格式";
                case UploadState.NetworkError:
                    return "网络错误";
            }
            return "未知错误";
        }
    }
}