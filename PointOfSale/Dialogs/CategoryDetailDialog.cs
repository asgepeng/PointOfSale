using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PointOfSale.Models;

namespace PointOfSale.Dialogs
{
    public partial class CategoryDetailDialog : Form
    {
        public CategoryDetailDialog()
        {
            InitializeComponent();
        }

        private void CategoryDetailDialog_Load(object sender, EventArgs e)
        {
            if (Tag != null)
            {
                var category = (Category)Tag;
                textBox1.Text = category.Name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Nama kategori produk tidak boleh kosong", "Data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Focus();
                return;
            }
            var category = Tag != null ? (Category)Tag : new Category();
            category.Name = textBox1.Text;
            Tag = category;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
