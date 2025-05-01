using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace PointOfSale.Components
{
    public class MainPanel : ScrollableControl
    {
        private readonly Database db;
        internal MainPanel(Database _db)
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            db = _db;
        }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl == e.Control)
                {
                    ctrl.BringToFront();
                }
                else { ctrl.SendToBack(); }
            }
            if (Header != null)
            {
                Header.Text = this.Controls[this.Controls.Count - 1].Text;
                Header.Invalidate();
            }
        }
        private void DrawRoundedRectangle(Graphics g, Rectangle rect, int cornerRadius, Brush fillColor)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(rect.Left, rect.Top, cornerRadius, cornerRadius, 180, 90);
                path.AddArc(rect.Right - cornerRadius, rect.Top, cornerRadius, cornerRadius, 270, 90);
                path.AddArc(rect.Right - cornerRadius, rect.Bottom - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                path.AddArc(rect.Left, rect.Bottom - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                path.CloseFigure();

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillPath(fillColor, path);
            }
        }
        protected override async void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            if (this.Controls.Count > 0)
            {
                this.Controls[this.Controls.Count - 1].BringToFront();
                if (Header != null)
                {
                    Header.Text = this.Controls[this.Controls.Count - 1].Text;
                    Header.Invalidate();
                }
            }
            else
            {
                if (Header != null)
                {
                    Header.Text = "Point Of Sale Application";
                    Header.Invalidate();
                }
                await LoadDataDasboard();
            }
        }
        public HeaderPanel Header { get; set; }
        public void RemoveLastControl()
        {
            if (Controls.Count > 0)
            {
                var control = Controls[Controls.Count - 1];
                if (control is ListingControl listing && listing.IsDetailView())
                {
                    listing.BackToList();
                }
                else
                {
                    Controls.Remove(control);
                    control.Dispose();
                    control = null;
                }
            }
        }
        private string[] labels = new string[]
        {
            "Pembelian", "Penjualan", "Pemasukan", "Pengeluaran"
        };
        private decimal[] totals = new decimal[4];
        private Color[] colors = new Color[4]
        {
            Color.FromArgb(178, 255, 0, 0), Color.FromArgb(178, 255, 165, 0), Color.FromArgb(178, 0, 0, 255), Color.FromArgb(178, 0, 128, 0)
        };
        protected override void OnPaint(PaintEventArgs e)
        {
            var x = 20;
            var y = 40;
            var width = (int)((double)(this.ClientSize.Width - 55) / 4);
            var r = new Rectangle(x, y, width, 120);
            for (int i = 0; i < totals.Length; i++)
            {
                var value = totals[i];
                DrawRoundedRectangle(e.Graphics, r, 10, new SolidBrush(colors[i]));
                var textRect = new Rectangle(r.X + 10, r.Y + 5, 260, 25);
                e.Graphics.DrawString(labels[i], new Font("Segoe UI", 11.75F, FontStyle.Bold), Brushes.White, textRect);
                textRect.Height = 40;
                textRect.Y = r.Y + r.Height - 45;
                e.Graphics.DrawString(value.ToString("N0"), new Font("Segoe UI", 18.75F, FontStyle.Bold), Brushes.White, textRect, new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
                r.X += r.Width + 5;
            }
            
        }
        protected override async void OnCreateControl()
        {
            await LoadDataDasboard();
            base.OnCreateControl();
        }
        private async Task LoadDataDasboard()
        {
            var commandText = "SELECT ISNULL((SELECT SUM(ambp) FROM purchases), 0) AS purchase, ISNULL((SELECT SUM(ambc) FROM sales), 0) AS sales, (SELECT SUM(ap) FROM purchases WHERE tp < ambp) AS ap";
            using (var reader = await db.ExecuteReaderAsync(commandText, new System.Data.SqlClient.SqlParameter("@date", DateTime.Today)))
            {
                if (reader != null)
                {
                    if (await reader.ReadAsync())
                    {
                        totals[0] = reader.GetDecimal(0);
                        totals[2] = reader.GetDecimal(2);
                    }
                }
            }
        }
        protected override void OnResize(EventArgs e)
        {
            Invalidate();
        }
        internal class MenuItem
        {

        
        }
        internal class MenuItemCollection : List<MenuItem>
        {

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPanel));
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.MainPanel_Layout);
            this.ResumeLayout(false);

        }

        private void MainPanel_Layout(object sender, LayoutEventArgs e)
        {

        
        }
    }

}
