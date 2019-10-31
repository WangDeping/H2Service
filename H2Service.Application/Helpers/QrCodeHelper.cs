using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.IO;
using System.Drawing;
using System.Web;
using System.Drawing.Imaging;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;

namespace H2Service.Helpers
{
    public static class QrCodeHelper
    {
        /// <summary>
        /// 生成并保存Qrcode,带logo
        /// </summary>
        /// <param name="content">内容文字或地址</param>
        /// <param name="iconPath">中间logo</param>
        /// <param name="qrpath">保存位置</param>
        /// <param name="moduleSize"></param>
        /// <returns></returns>
        public static void GetQrCode(string content, string iconPath, string qrpath, int moduleSize = 9)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode = qrEncoder.Encode(content);
            GraphicsRenderer render = new GraphicsRenderer(new FixedModuleSize(moduleSize, QuietZoneModules.Two), Brushes.Black, Brushes.White);

            DrawingSize dSize = render.SizeCalculator.GetSize(qrCode.Matrix.Width);
            Bitmap map = new Bitmap(dSize.CodeWidth, dSize.CodeWidth);
            Graphics g = Graphics.FromImage(map);
            render.Draw(g, qrCode.Matrix);

            //追加Logo图片 ,注意控制Logo图片大小和二维码大小的比例
            //PS:追加的图片过大超过二维码的容错率会导致信息丢失,无法被识别
            Image img = Image.FromFile(iconPath);

            Point imgPoint = new Point((map.Width - img.Width) / 2, (map.Height - img.Height) / 2);
            g.DrawImage(img, imgPoint.X, imgPoint.Y, img.Width, img.Height);

            MemoryStream memoryStream = new MemoryStream();
            map.Save(memoryStream, ImageFormat.Jpeg);
            map.Save(HttpContext.Current.Server.MapPath(qrpath), ImageFormat.Jpeg);//fileName为存放的图片路径          

        }
        /// <summary>
        /// 生成文件流
        /// </summary>
        /// <param name="content"></param>
        /// <param name="moduleSize"></param>
        /// <returns></returns>
        public static MemoryStream CommonQrCode(string content, int moduleSize = 9)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode = qrEncoder.Encode(content);
            GraphicsRenderer render = new GraphicsRenderer(new FixedModuleSize(moduleSize, QuietZoneModules.Two), Brushes.DeepSkyBlue, Brushes.White);

            DrawingSize dSize = render.SizeCalculator.GetSize(qrCode.Matrix.Width);
            Bitmap map = new Bitmap(dSize.CodeWidth, dSize.CodeWidth);            
            Graphics g = Graphics.FromImage(map);
            render.Draw(g, qrCode.Matrix);
            //Point imgPoint = new Point((map.Width - img.Width) / 2, (map.Height - img.Height) / 2);
            // g.DrawImage(img, imgPoint.X, imgPoint.Y, img.Width, img.Height);

            MemoryStream memoryStream = new MemoryStream();
            map.Save(memoryStream, ImageFormat.Jpeg);

            g.Dispose();
            map.Dispose();
            return memoryStream;
        }

        public static MemoryStream EAN_13_Barcode(string content, int width, int height)
        {
            BarcodeWriter writer = new BarcodeWriter();
            //使用ITF 格式，不能被现在常用的支付宝、微信扫出来
            //如果想生成可识别的可以使用 CODE_128 格式
            //writer.Format = BarcodeFormat.ITF;
            writer.Format = BarcodeFormat.EAN_13;
            EncodingOptions options = new EncodingOptions()
            {
                Width = width,
                Height = height,
                Margin = 5
            };
            writer.Options = options;
            Bitmap map = writer.Write(content);            
            MemoryStream memoryStream = new MemoryStream();
            map.Save(memoryStream, ImageFormat.Jpeg);            
            map.Dispose();
            return memoryStream;


        }
    }
}
