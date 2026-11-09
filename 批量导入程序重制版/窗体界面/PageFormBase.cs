using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    public class PageFormBase : Form
    {
        public PageFormBase()
        {
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.White;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            UpdateRoundedRegion();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var path = CreateRoundedPath(ClientRectangle, 10))
            using (var pen = new Pen(Color.FromArgb(226, 233, 238), 1F))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void UpdateRoundedRegion()
        {
            if (Width <= 0 || Height <= 0) return;
            using (var path = CreateRoundedPath(new Rectangle(0, 0, Width, Height), 10))
                Region = new Region(path);
        }

        private static GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            var diameter = radius * 2;
            var rect = new Rectangle(bounds.X, bounds.Y, Math.Max(1, bounds.Width - 1), Math.Max(1, bounds.Height - 1));
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
