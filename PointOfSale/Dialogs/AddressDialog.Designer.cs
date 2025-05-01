using System.Windows.Forms;
using System.Drawing;

namespace PointOfSale.Dialogs
{
    partial class AddressDialog
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
            label1 = new Label();
            tbStreetline = new TextBox();
            label2 = new Label();
            cbProvinces = new ComboBox();
            cbCities = new ComboBox();
            label3 = new Label();
            cbDistricts = new ComboBox();
            label4 = new Label();
            cbVillages = new ComboBox();
            label5 = new Label();
            ckPrimary = new CheckBox();
            cbType = new ComboBox();
            label6 = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 62);
            label1.Name = "label1";
            label1.Size = new Size(125, 17);
            label1.TabIndex = 0;
            label1.Text = "Dusun / Nama Jalan";
            // 
            // tbStreetline
            // 
            tbStreetline.Location = new Point(12, 82);
            tbStreetline.Multiline = true;
            tbStreetline.Name = "tbStreetline";
            tbStreetline.ScrollBars = ScrollBars.Vertical;
            tbStreetline.Size = new Size(260, 53);
            tbStreetline.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 143);
            label2.Name = "label2";
            label2.Size = new Size(55, 17);
            label2.TabIndex = 2;
            label2.Text = "Propinsi";
            // 
            // cbProvinces
            // 
            cbProvinces.DropDownStyle = ComboBoxStyle.DropDownList;
            cbProvinces.FormattingEnabled = true;
            cbProvinces.Location = new Point(12, 163);
            cbProvinces.Name = "cbProvinces";
            cbProvinces.Size = new Size(260, 25);
            cbProvinces.TabIndex = 3;
            // 
            // cbCities
            // 
            cbCities.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCities.FormattingEnabled = true;
            cbCities.Location = new Point(12, 216);
            cbCities.Name = "cbCities";
            cbCities.Size = new Size(260, 25);
            cbCities.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 196);
            label3.Name = "label3";
            label3.Size = new Size(111, 17);
            label3.TabIndex = 4;
            label3.Text = "Kabupaten / Kota";
            // 
            // cbDistricts
            // 
            cbDistricts.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDistricts.FormattingEnabled = true;
            cbDistricts.Location = new Point(12, 269);
            cbDistricts.Name = "cbDistricts";
            cbDistricts.Size = new Size(260, 25);
            cbDistricts.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 249);
            label4.Name = "label4";
            label4.Size = new Size(72, 17);
            label4.TabIndex = 6;
            label4.Text = "Kecamatan";
            // 
            // cbVillages
            // 
            cbVillages.DropDownStyle = ComboBoxStyle.DropDownList;
            cbVillages.FormattingEnabled = true;
            cbVillages.Location = new Point(12, 322);
            cbVillages.Name = "cbVillages";
            cbVillages.Size = new Size(260, 25);
            cbVillages.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 302);
            label5.Name = "label5";
            label5.Size = new Size(108, 17);
            label5.TabIndex = 8;
            label5.Text = "Desa / Kelurahan";
            // 
            // checkBox1
            // 
            ckPrimary.AutoSize = true;
            ckPrimary.Location = new Point(12, 363);
            ckPrimary.Name = "checkBox1";
            ckPrimary.Size = new Size(109, 21);
            ckPrimary.TabIndex = 10;
            ckPrimary.Text = "Alamat Utama";
            ckPrimary.UseVisualStyleBackColor = true;
            // 
            // cbType
            // 
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbType.FormattingEnabled = true;
            cbType.Items.AddRange(new object[] { "Rumah", "Kantor", "Lainnya" });
            cbType.Location = new Point(12, 29);
            cbType.Name = "cbType";
            cbType.Size = new Size(260, 25);
            cbType.TabIndex = 12;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 9);
            label6.Name = "label6";
            label6.Size = new Size(33, 17);
            label6.TabIndex = 11;
            label6.Text = "Tipe";
            // 
            // button1
            // 
            button1.Location = new Point(177, 418);
            button1.Name = "button1";
            button1.Size = new Size(95, 31);
            button1.TabIndex = 13;
            button1.Text = "Simpan";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(76, 418);
            button2.Name = "button2";
            button2.Size = new Size(95, 31);
            button2.TabIndex = 14;
            button2.Text = "Batal";
            button2.UseVisualStyleBackColor = true;
            button2.Click += CloseDialog;
            // 
            // AddressDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 461);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(cbType);
            Controls.Add(label6);
            Controls.Add(ckPrimary);
            Controls.Add(cbVillages);
            Controls.Add(label5);
            Controls.Add(cbDistricts);
            Controls.Add(label4);
            Controls.Add(cbCities);
            Controls.Add(label3);
            Controls.Add(cbProvinces);
            Controls.Add(label2);
            Controls.Add(tbStreetline);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AddressDialog";
            Text = "Alamat";
            Load += OnDialogLoad;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbStreetline;
        private Label label2;
        private ComboBox cbProvinces;
        private ComboBox cbCities;
        private Label label3;
        private ComboBox cbDistricts;
        private Label label4;
        private ComboBox cbVillages;
        private Label label5;
        private CheckBox ckPrimary;
        private ComboBox cbType;
        private Label label6;
        private Button button1;
        private Button button2;
    }
}