using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonData
{
    public static class Common
    {
        public const int DEFAULT_ALIGN = 30;
        public static readonly StringFormat STRFMT_CENTER;
        public static readonly Font CELLFONT;
        public static readonly Font XFONT;
        public static readonly Font BIGXFONT;
        public static readonly Brush TRANSBRUSH;
        static Common()
        {
            STRFMT_CENTER = new StringFormat();
            STRFMT_CENTER.LineAlignment = StringAlignment.Center;
            STRFMT_CENTER.Alignment = StringAlignment.Center;
            CELLFONT = new Font("微软雅黑", 18);
            XFONT = new Font("微软雅黑", 9);
            BIGXFONT = new Font("微软雅黑", 24);
            TRANSBRUSH = new SolidBrush(Color.FromArgb(30, 0, 0, 0));
        }
    }
}
