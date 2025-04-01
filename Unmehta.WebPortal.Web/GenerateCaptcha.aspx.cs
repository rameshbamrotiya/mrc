using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Unmehta.WebPortal.Web
{
    public partial class GenerateCaptcha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();

            string strCaptcha="";

            //if (string.IsNullOrWhiteSpace(strCaptcha))
            {
                try
                {
                    strCaptcha = Session["captcha"].ToString();
                }
                catch (Exception ex)
                {

                }
            }
            ////if (string.IsNullOrWhiteSpace(strCaptcha))
            //{
            //    try
            //    {
            //        strCaptcha = Session["captchaEnquiry"].ToString();
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}
            ////if (string.IsNullOrWhiteSpace(strCaptcha))
            //{
            //    try
            //    {
            //        strCaptcha = Session["captchaComplaint"].ToString();
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}
            int height = 50;

            int width = 150;

            Bitmap bmp = new Bitmap(width, height);

            RectangleF rectf = new RectangleF(10, 5, 0, 0);

            Graphics g = Graphics.FromImage(bmp);

            g.Clear(Color.White);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            g.DrawString(strCaptcha, new Font("Thaoma", 20, FontStyle.Bold), Brushes.Chocolate, rectf);

            g.DrawRectangle(new Pen(Color.Blue), 1, 1, width - 2, height - 2);

            g.Flush();

            Response.ContentType = "image/jpeg";

            bmp.Save(Response.OutputStream, ImageFormat.Jpeg);

            g.Dispose();

            bmp.Dispose();
        }
    }
}