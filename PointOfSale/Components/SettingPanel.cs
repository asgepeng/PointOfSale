using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PointOfSale.Components
{
    public class SettingPanel : Control
    {
        private XButton backButton;
        private XButton contactButton;
        private XButton purchaseButton;
        private XButton productButton;
        private XButton accountButton;
        private XButton settingButton;
        private readonly Database db;
        internal ButtonCollection Buttons { get; } = new ButtonCollection();
        public MainPanel MainPanel { get; set; } 
        public SettingPanel(MainPanel mainPanel, Database _db)
        {
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            MainPanel = mainPanel;
            db = _db;
            this.BackColor = Color.WhiteSmoke;
            this.backButton = new XButton()
            {
                Icon = global::PointOfSale.Properties.Resources.back,
                Size = new Size(32, 32),
                Location = new Point(8, 5),
                Text = "Back"
            };
            this.contactButton = new XButton()
            {
                Icon = global::PointOfSale.Properties.Resources.contact,
                Size = new Size(32, 32),
                Location = new Point(8, 45),
                Text = "Kontak"
            };
            this.productButton = new XButton()
            {
                Icon = global::PointOfSale.Properties.Resources.history,
                Size = new Size(32, 32),
                Location = new Point(8, 85),
                Text = "Produk"
            };
            this.purchaseButton = new XButton()
            {
                Icon = global::PointOfSale.Properties.Resources.budget,
                Size = new Size(32, 32),
                Location = new Point(8, 125)
            };
            accountButton = new XButton()
            {
                Icon = global::PointOfSale.Properties.Resources.access_control,
                Size = new Size(32, 32),
                Location = new Point(0, 0)
            };
            settingButton = new XButton()
            {
                Icon = global::PointOfSale.Properties.Resources.support,
                Size = new Size(32, 32),
                Location = new Point(0, 0),
                Text = "Setting"
            };
            this.Width = 48;
            this.Dock = DockStyle.Left;
            Buttons.Add(backButton);
            Buttons.Add(contactButton);
            Buttons.Add(productButton);
            Buttons.Add(purchaseButton);
            Buttons.Add(accountButton);
            Buttons.Add(settingButton);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(220, 220, 220))), e.ClipRectangle);
            if (HasControls())
            {
                backButton.Draw(g);
                accountButton.Draw(g);
                settingButton.Draw(g);
            }
            else
            {
                for (int i = 1; i < Buttons.Count; i++)
                {
                    Buttons[i].Draw(g);
                }
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            var loc = e.Location;
            foreach (var item in this.Buttons)
            {
                item.IsHover = item.GetRectangle().Contains(loc);
            }
            Invalidate();
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (HasControls())
            {
                if (new Rectangle(backButton.Location, backButton.Size).Contains(e.Location))
                {
                    this.MainPanel.RemoveLastControl();
                    return;
                }
            }
            else
            {
                if (new Rectangle(contactButton.Location, contactButton.Size).Contains(e.Location))
                {
                    this.MainPanel.Controls.Add(new ListingControl(db, Data.RepositoryType.Contact));
                    return;
                }
                if (new Rectangle(productButton.Location, productButton.Size).Contains(e.Location))
                {
                    this.MainPanel.Controls.Add(new ListingControl(db, Data.RepositoryType.Product));
                    return;
                }
                if (this.purchaseButton.GetRectangle().Contains(e.Location))
                {
                    this.MainPanel.Controls.Add(new Panels.PurchasePanel(db));
                    return;
                }
            }
            if (settingButton.GetRectangle().Contains(e.Location))
            {
                var form = (MainForm)this.FindForm();
                if (form != null)
                {
                    if (form.Controls.Find("settingPanel", false).Length > 0)
                    {
                        var removedPanel = form.Controls.Find("settingPanel", false)[0];
                        form.Controls.Remove(removedPanel);
                        removedPanel.Dispose();
                        removedPanel = null;
                        return;
                    }
                    var panel = new XSettingPanel();
                    panel.Location = new Point(this.Location.X + this.Width, this.Location.Y);
                    panel.Size = new Size(300, this.Height);
                    panel.BackColor = SystemColors.Window;
                    panel.MouseLeave += (sender, evt) =>
                    {
                        form.Controls.Remove(panel);
                        panel.Dispose();
                        panel = null;
                    };
                    form.Controls.Add(panel);
                    panel.BringToFront();
                }
            }
            if (accountButton.GetRectangle().Contains(e.Location))
            {
                var mainForm = ((MainForm)this.FindForm());
                var panels = mainForm.Controls.Find("accountPanel", false);
                if (panels.Length == 0)
                {
                    AccountPanel accountPanel = new AccountPanel(mainForm.Database);
                    accountPanel.Name = "accountPanel";
                    accountPanel.Size = new Size(300, mainForm.ClientSize.Height - 48);
                    accountPanel.Location = new Point(48, 48);
                    if (mainForm.Controls.Find("accountPanel", false).Length == 0)
                    {
                        mainForm.Controls.Add(accountPanel);
                        accountPanel.BringToFront();
                        accountPanel.MouseLeave += (s, evt) =>
                        {
                            mainForm.Controls.Remove(accountPanel);
                            accountPanel.Dispose();
                            accountPanel = null;
                            this.Invalidate();
                        };
                    }
                }
                else
                {
                    var accountPanel = panels[0];
                    mainForm.Controls.Remove(accountPanel);
                    accountPanel.Dispose();
                    accountPanel = null;
                }
            }
        }
        protected override void OnLeave(EventArgs e)
        {
            Invalidate();
        }
        protected override void OnResize(EventArgs e)
        {
            var y = this.ClientSize.Height - 48;
            this.accountButton.Location = new Point(8, y - 40);
            this.settingButton.Location = new Point(8, y);
            this.Invalidate();
        }
        public bool HasControls() { return MainPanel.Controls.Count > 0; }
        internal class XButton
        {
            internal string Text { get; set; } = "";
            internal Point Location { get; set; }
            internal Size Size { get; set; }
            internal Image Icon { get; set; } = null;
            internal bool IsHover { get; set; } = false;
            internal Rectangle GetRectangle()
            {
                return new Rectangle(this.Location, this.Size);
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
            internal void Draw(Graphics g)
            {
                if (this.IsHover)
                {
                    DrawRoundedRectangle(g, GetRectangle(), 8, Brushes.Gray);
                }
                if (this.Icon != null)
                {
                    var imageRectangle = new Rectangle(this.Location.X + 4, this.Location.Y + 4, 24, 24);
                    g.DrawImage(this.Icon, imageRectangle);
                }
                //g.DrawString(Text, new Font("Segoe UI", 8.75F, FontStyle.Regular), Brushes.Black, this.Location.X + 5, this.Location.Y + 32);
            }
        }
        internal class ButtonCollection : List<XButton>
        {
            internal void Add(string text, Image img)
            {

            }
        }
    }
}
