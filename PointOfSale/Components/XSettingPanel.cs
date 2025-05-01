using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace PointOfSale.Components
{
    public class XSettingPanel : Control
    {
        private SettingButtonCollection Buttons { get; } = new SettingButtonCollection();
        public XSettingPanel()
        {
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            Name = "settingPanel";
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var bigFont = new Font("Segoe UI", 18.25F, FontStyle.Bold);
            var textRectangle = new Rectangle(5, 5, this.Width - 10, 40);
            e.Graphics.DrawString("Pengaturan", bigFont, Brushes.Black, textRectangle, new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        internal class SettingButton
        {
            internal string Text { get; set; } = "";
            internal Rectangle Rectangle { get; set; }
            internal bool IsHover { get; set; } = false;
            internal Image Icon { get; set; }
        }
        internal class SettingButtonCollection: List<SettingButton>
        {
            public void Add(string text, Rectangle rectangle)
            {

            }
        }
    }
}
