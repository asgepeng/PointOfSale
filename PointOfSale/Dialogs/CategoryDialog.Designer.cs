using System.Windows.Forms;
using System.Drawing;

namespace PointOfSale.Dialogs
{
    partial class CategoryDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grid = new DataGridView();
            button1 = new Button();
            btnDelete = new Button();
            tbFilter = new TextBox();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            SuspendLayout();
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.Fixed3D;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.ColumnHeadersHeight = 25;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.GridColor = Color.FromArgb(230, 230, 230);
            grid.Location = new Point(12, 44);
            grid.MultiSelect = false;
            grid.Name = "grid";
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(310, 393);
            grid.TabIndex = 1;
            grid.CellDoubleClick += EditCategory;
            // 
            // button1
            // 
            button1.Location = new Point(12, 443);
            button1.Name = "button1";
            button1.Size = new Size(75, 26);
            button1.TabIndex = 2;
            button1.Text = "Baru";
            button1.UseVisualStyleBackColor = true;
            button1.Click += CreateNewCategory;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(93, 443);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 26);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Hapus";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += DeleteCategory;
            // 
            // tbFilter
            // 
            tbFilter.BackColor = Color.FromArgb(255, 255, 192);
            tbFilter.Location = new Point(12, 12);
            tbFilter.Name = "tbFilter";
            tbFilter.Size = new Size(310, 25);
            tbFilter.TabIndex = 5;
            // 
            // CategoryDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 481);
            Controls.Add(tbFilter);
            Controls.Add(btnDelete);
            Controls.Add(button1);
            Controls.Add(grid);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CategoryDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Daftar Kategori Produk";
            Load += OnDialogLoaded;
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView grid;
        private Button button1;
        private Button btnDelete;
        private TextBox tbFilter;
    }
}