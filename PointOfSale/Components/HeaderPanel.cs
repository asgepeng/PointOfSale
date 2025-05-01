using System;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using PointOfSale.Dialogs;

namespace PointOfSale.Components
{
    public class HeaderPanel : Control
    {
        private HButton home;
        public HeaderPanel(MainPanel mainPanel)
        {
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.Dock = DockStyle.Top;
            this.BackColor = SystemColors.ControlDarkDark;
            this.home = new HButton();
            this.Text = "Point of Sale Application";
            MainPanel = mainPanel;
            this.Height = 48;
        }
        public MainPanel MainPanel { get; }
        protected override void OnPaint(PaintEventArgs e)
        {
            var textRectangle = new Rectangle(52, 0, 500, 48);
            e.Graphics.DrawString(this.Text, new Font("Segoe UI", 16.75F, FontStyle.Bold), Brushes.White, textRectangle, new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
            var color = home.IsHover ? Color.DimGray : Color.Gray;
            e.Graphics.FillRectangle(new SolidBrush(color), home.ClientRectangle);
            e.Graphics.DrawImage(global::PointOfSale.Properties.Resources.dashboard, new Rectangle(12, 12, 24, 24));
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            home.IsHover = home.ClientRectangle.Contains(e.Location);
            this.Invalidate();
        }
        protected override void OnLeave(EventArgs e)
        {
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            Invalidate();
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (home.ClientRectangle.Contains(e.Location))
            {
                OfficeDialog dlg = new OfficeDialog(((MainForm)this.FindForm()).Database);
                dlg.ShowDialog();
            }
        }
        internal class HButton
        {
            internal HButton()
            {
                ClientRectangle = new RectangleF(0, 0, 48, 48);
            }
            internal RectangleF ClientRectangle { get; set; }
            internal bool IsHover { get; set; } = false;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // HeaderPanel
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ResumeLayout(false);

        }
    }
}
