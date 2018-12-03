using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace H2Service.Web.Helpers
{
    public static class PictureHelper
    {
        /// <summary>  
        /// 图片转换（裁剪并缩放）  
        /// </summary>  
        /// <param name="ASrcFileName">源文件名称</param>  
        /// <param name="ADestFileName">目标文件名称</param>  
        /// <param name="AWidth">转换后的宽度（像素）</param>  
        /// <param name="AHeight">转换后的高度（像素）</param>  
        /// <param name="AQuality">保存质量（取值在1-100之间）</param>  
        public static void DoConvert(string ASrcFileName, string ADestFileName, int AWidth, int AHeight, int AQuality)
        {
            Image ASrcImg = Image.FromFile(ASrcFileName);
            //if (ASrcImg.Width <= AWidth && ASrcImg.Height <= AHeight)
            //{//图片的高宽均小于目标高宽，直接保存  
            //    ASrcImg.Save(ADestFileName);
            //    return;
            //}
            double ADestRate = AWidth * 1.0 / AHeight;
            double ASrcRate = ASrcImg.Width * 1.0 / ASrcImg.Height;
            //裁剪后的宽度  
            double ACutWidth = ASrcRate > ADestRate ? (ASrcImg.Height * ADestRate) : ASrcImg.Width;
            //裁剪后的高度  
            double ACutHeight = ASrcRate > ADestRate ? ASrcImg.Height : (ASrcImg.Width / ADestRate);
            //待裁剪的矩形区域，根据原图片的中心进行裁剪  
            Rectangle AFromRect = new Rectangle(Convert.ToInt32((ASrcImg.Width - ACutWidth) / 2), Convert.ToInt32((ASrcImg.Height - ACutHeight) / 2), (int)ACutWidth, (int)ACutHeight);
            //目标矩形区域  
            Rectangle AToRect = new Rectangle(0, 0, AWidth, AHeight);

            Image ADestImg = new Bitmap(AWidth, AHeight);
            Graphics ADestGraph = Graphics.FromImage(ADestImg);
            ADestGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            ADestGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            ADestGraph.DrawImage(ASrcImg, AToRect, AFromRect, GraphicsUnit.Pixel);

            //获取系统image/jpeg编码信息  
            ImageCodecInfo[] AInfos = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo AInfo = null;
            foreach (ImageCodecInfo i in AInfos)
            {
                if (i.MimeType == "image/jpeg")
                {
                    AInfo = i;
                    break;
                }
            }
            //设置转换后图片质量参数  
            EncoderParameters AParams = new EncoderParameters(1);
            AParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)AQuality);
            //保存  
            ADestImg.Save(ADestFileName, AInfo, AParams);
        }
    }
}