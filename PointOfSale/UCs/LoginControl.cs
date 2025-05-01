using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSale.UCs
{
    public partial class LoginControl : UserControl
    {
        private readonly Database db;
        internal LoginControl(Database _db)
        {
            InitializeComponent();
            this.db = _db;
        }

        private void LoginControl_Load(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Username tidak boleh kosong", "Username", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.textBox2.Text == "")
            {
                MessageBox.Show("Password tidak boleh kosong", "Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            this.textBox1.Enabled = false;
            this.textBox2.Enabled = false;
            this.button1.Enabled = false;

            LoginManager manager = new LoginManager(this.db);
            if (await manager.SignInAsync(this.textBox1.Text, this.textBox2.Text))
            {
                if (this.Parent != null)
                {
                    ((MainForm)this.Parent).LoginSuccess();
                }
            }
            else
            {
                MessageBox.Show("Kombinasi username dan password tidak sesuai", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Enabled = true;
                this.textBox2.Enabled = true;
                this.button1.Enabled = true;
            }
        }

        private void LoginControl_Load_1(object sender, EventArgs e)
        {
            this.textBox1.Focus();
            this.textBox1.KeyDown += (s, evt) =>
            {
                if (evt.KeyData == Keys.Enter)
                {
                    button1.PerformClick();
                }
            };
            this.textBox2.KeyDown += (s, evt) =>
            {
                if (evt.KeyData == Keys.Enter)
                {
                    button1.PerformClick();
                }
            };
        }
    }
}
