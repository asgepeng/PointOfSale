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

namespace PointOfSale.Dialogs
{
    public partial class PhoneDialog : Form
    {
        public PhoneDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbType.SelectedIndex < 0)
            {
                MessageBox.Show("Tipe telepon masih kosong, field ini tidak boleh kosong", "Data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbType.Focus();
                return;
            }
            if (tbNumber.Text.Trim() == "")
            {
                MessageBox.Show("Nomor telepon tidak boleh kosong", "Data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbNumber.Focus();
                return;
            }
            var phone = Tag is null ? new Phone() : (Phone)Tag;
            phone.Type = cbType.SelectedIndex + 1;
            phone.Number = tbNumber.Text.Trim();
            Tag = phone;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void PhoneDialog_Load(object sender, EventArgs e)
        {
            if (Tag != null)
            {
                var phone = (Phone)Tag;
                cbType.SelectedIndex = phone.Type - 1;
                tbNumber.Text = phone.Number;
            }
        }
    }
}
