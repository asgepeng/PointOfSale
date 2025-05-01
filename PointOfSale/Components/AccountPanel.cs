using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSale.Components
{
    public partial class AccountPanel : UserControl
    {
        private Rectangle changePasswordButton;
        private Rectangle logoutButton;
        private Point mouseLoc;
        private readonly Database db;
        public AccountPanel(Database _db)
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            changePasswordButton = new Rectangle(10, 0, 280, 30);
            logoutButton = new Rectangle(10, 0, 280, 30);
            db = _db;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var userRectangle = new Rectangle(0, 0, 300, 48);
            var nameRectangle = new Rectangle(5, 0, 280, 48);
            e.Graphics.FillRectangle(Brushes.Gainsboro, userRectangle);
            e.Graphics.DrawString(My.Application.User.Name, new Font("Segoe UI", 16.26F, FontStyle.Bold), Brushes.Black, nameRectangle, new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
            if (changePasswordButton.Contains(mouseLoc))
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.AliceBlue), changePasswordButton);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.ButtonFace), changePasswordButton);
            }
            e.Graphics.DrawRectangle(Pens.Silver, changePasswordButton);
            e.Graphics.DrawString("Ubah Password", this.Font, Brushes.Black, changePasswordButton, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            if (logoutButton.Contains(mouseLoc))
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.AliceBlue), logoutButton);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.ButtonFace), logoutButton);
            }
            e.Graphics.DrawRectangle(Pens.Silver, logoutButton);
            e.Graphics.DrawString("Logout", this.Font, Brushes.Black, logoutButton, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            mouseLoc = e.Location;
            Invalidate();
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (changePasswordButton.Contains(e.Location))
            {
                var updatePasswordControl = new UCs.UpdatePasswordControl(db);
                var mainForm = ((MainForm)this.FindForm());
                mainForm.MainPanel.Controls.Add(updatePasswordControl);
                mainForm.Controls.Remove(this);
                this.Dispose();
            }
        }
        protected override void OnResize(EventArgs e)
        {
            changePasswordButton.Y = this.ClientSize.Height - 75;
            logoutButton.Y = this.ClientSize.Height - 40;
            Invalidate();
        }
    }
}
