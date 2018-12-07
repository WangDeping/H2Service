using Abp.Dependency;
using Castle.Core.Logging;
using H2Service.WeChatWork.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace H2Service.WeChatWork
{
 public   class WxFileManager: ITransientDependency
    {
        private WxTokenManager _tokenManager;
        private ILogger _logger;
        public WxFileManager(WxTokenManager tokenManager,
            ILogger logger )
        {
            _tokenManager = tokenManager;
            _logger = logger;
        }

        private const string UPLOADFILE_URL = "https://qyapi.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}";
        private const string UPLOADIMG_URL = "https://qyapi.weixin.qq.com/cgi-bin/media/uploadimg?access_token={0}";
        private const string DOWNLOAD_URL = "https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}";
        /// <summary>
        /// 临时素材上传
        /// </summary>
        /// <param name="path">HttpPostedFileBase.FileName</param>
        /// <param name="bf">HttpPostedFileBase.InputStream.Read</param>
        /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video），普通文件（file）</param>
        /// <returns></returns>
        public WxRetTempFile UploadTempFile(string path, byte[] bf,string type)
        {
            var concatId = WebConfigurationManager.AppSettings["contactsAppid"];
            var accessToken =_tokenManager.GetWxToken(concatId);
            string url = string.Format(UPLOADFILE_URL, accessToken,type);
            var responseJson = HttpUpload(url, path, bf);
            _logger.Error("上传临时素材返回" + responseJson);
            return JsonConvert.DeserializeObject<WxRetTempFile>(responseJson);
        }
        /// <summary>
        /// 永久图片上传
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bf"></param>
        /// <returns></returns>
        public WxRetImg UploadImg(string path, byte[] bf)
        {
            var concatId = WebConfigurationManager.AppSettings["contactsAppid"];
            var accessToken = _tokenManager.GetWxToken(concatId);
            string url = string.Format(UPLOADIMG_URL, accessToken);
           var responseJson = HttpUpload(url, path, bf);
            _logger.Error("上传永久图片返回"+responseJson);
            return JsonConvert.DeserializeObject<WxRetImg>(responseJson);
        }

        private string HttpUpload(string postUrl,string filePath,byte[] bf)
        {
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
            request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            int pos = filePath.LastIndexOf("\\");
            string fileName = filePath.Substring(pos + 1);
            //请求头部信息
            StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"media\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());
            Stream postStream = request.GetRequestStream();
            postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            postStream.Write(bf, 0, bf.Length);
            postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            postStream.Close();
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            return sr.ReadToEnd();

        }

        /// <summary>
        /// 根据媒体Id下载保存文件
        /// </summary>
        /// <param name="media_id"></param>
        /// <returns></returns>
        public string DownLoadWxTempFile(string media_id, string path)
        {
            var concatId = WebConfigurationManager.AppSettings["contactsAppid"];
            string token = _tokenManager.GetWxToken(concatId);
            string wxUrl = string.Format(DOWNLOAD_URL, token, media_id);
            HttpWebRequest request = HttpWebRequest.Create(wxUrl) as HttpWebRequest;
            request.Method = "GET";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            string fileName = response.Headers["Content-disposition"].Split('"')[1];//文件名
            string filePath = path + fileName;//物理路径
            Stream stream = response.GetResponseStream();         
            FileStream fs = new FileStream(filePath, FileMode.Create);
            byte[] bArr = new byte[1024];
            int size = stream.Read(bArr, 0, bArr.Length);
            while (size > 0)
            {
                fs.Write(bArr, 0, size);
                size = stream.Read(bArr, 0, bArr.Length);
            }
            fs.Close();
            stream.Close();
            return fileName;
        }
    }
}
