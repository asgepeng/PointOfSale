using System.Windows.Forms;
using System.Drawing;

namespace PointOfSale.Dialogs
{
    partial class OfficeDialog
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
            cbVillages = new ComboBox();
            label5 = new Label();
            cbDistricts = new ComboBox();
            label4 = new Label();
            cbCities = new ComboBox();
            label3 = new Label();
            cbProvinces = new ComboBox();
            label2 = new Label();
            tbAddress = new TextBox();
            label1 = new Label();
            tbName = new TextBox();
            label6 = new Label();
            tbPhone = new TextBox();
            label7 = new Label();
            tbEmail = new TextBox();
            label8 = new Label();
            button2 = new Button();
            button1 = new Button();
            groupBox1 = new GroupBox();
            tbFooter = new TextBox();
            label10 = new Label();
            panel1 = new Panel();
            label11 = new Label();
            label9 = new Label();
            button3 = new Button();
            picLogo = new PictureBox();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // cbVillages
            // 
            cbVillages.DropDownStyle = ComboBoxStyle.DropDownList;
            cbVillages.FormattingEnabled = true;
            cbVillages.Location = new Point(130, 179);
            cbVillages.Name = "cbVillages";
            cbVillages.Size = new Size(250, 25);
            cbVillages.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(8, 182);
            label5.Name = "label5";
            label5.Size = new Size(108, 17);
            label5.TabIndex = 21;
            label5.Text = "Desa / Kelurahan";
            // 
            // cbDistricts
            // 
            cbDistricts.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDistricts.FormattingEnabled = true;
            cbDistricts.Location = new Point(130, 148);
            cbDistricts.Name = "cbDistricts";
            cbDistricts.Size = new Size(250, 25);
            cbDistricts.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 151);
            label4.Name = "label4";
            label4.Size = new Size(72, 17);
            label4.TabIndex = 19;
            label4.Text = "Kecamatan";
            // 
            // cbCities
            // 
            cbCities.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCities.FormattingEnabled = true;
            cbCities.Location = new Point(130, 117);
            cbCities.Name = "cbCities";
            cbCities.Size = new Size(250, 25);
            cbCities.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 120);
            label3.Name = "label3";
            label3.Size = new Size(74, 17);
            label3.TabIndex = 17;
            label3.Text = "Kab. / Kota";
            // 
            // cbProvinces
            // 
            cbProvinces.DropDownStyle = ComboBoxStyle.DropDownList;
            cbProvinces.FormattingEnabled = true;
            cbProvinces.Location = new Point(130, 86);
            cbProvinces.Name = "cbProvinces";
            cbProvinces.Size = new Size(250, 25);
            cbProvinces.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 89);
            label2.Name = "label2";
            label2.Size = new Size(55, 17);
            label2.TabIndex = 15;
            label2.Text = "Propinsi";
            // 
            // tbAddress
            // 
            tbAddress.Location = new Point(130, 27);
            tbAddress.MaxLength = 250;
            tbAddress.Multiline = true;
            tbAddress.Name = "tbAddress";
            tbAddress.ScrollBars = ScrollBars.Vertical;
            tbAddress.Size = new Size(250, 53);
            tbAddress.TabIndex = 3;
            // 
            // label1
            // 
            label1.Location = new Point(8, 30);
            label1.Name = "label1";
            label1.Size = new Size(116, 50);
            label1.TabIndex = 13;
            label1.Text = "Nama Jl. / Dusun / Lingkungan";
            // 
            // tbName
            // 
            tbName.Location = new Point(179, 29);
            tbName.MaxLength = 50;
            tbName.Name = "tbName";
            tbName.ScrollBars = ScrollBars.Vertical;
            tbName.Size = new Size(393, 25);
            tbName.TabIndex = 0;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(179, 9);
            label6.Name = "label6";
            label6.Size = new Size(75, 17);
            label6.TabIndex = 23;
            label6.Text = "Nama Toko";
            // 
            // tbPhone
            // 
            tbPhone.Location = new Point(179, 77);
            tbPhone.MaxLength = 15;
            tbPhone.Name = "tbPhone";
            tbPhone.ScrollBars = ScrollBars.Vertical;
            tbPhone.Size = new Size(393, 25);
            tbPhone.TabIndex = 1;
            tbPhone.Enter += RemoveSpace;
            tbPhone.KeyPress += FilterOnlyNumber;
            tbPhone.Leave += FormatPhoneNumber;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(179, 57);
            label7.Name = "label7";
            label7.Size = new Size(100, 17);
            label7.TabIndex = 25;
            label7.Text = "Nomor Telepon";
            // 
            // tbEmail
            // 
            tbEmail.Location = new Point(179, 125);
            tbEmail.MaxLength = 50;
            tbEmail.Name = "tbEmail";
            tbEmail.ScrollBars = ScrollBars.Vertical;
            tbEmail.Size = new Size(393, 25);
            tbEmail.TabIndex = 2;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(179, 105);
            label8.Name = "label8";
            label8.Size = new Size(83, 17);
            label8.TabIndex = 27;
            label8.Text = "Alamat Email";
            // 
            // button2
            // 
            button2.Location = new Point(376, 498);
            button2.Name = "button2";
            button2.Size = new Size(95, 31);
            button2.TabIndex = 30;
            button2.Text = "Batal";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(477, 498);
            button1.Name = "button1";
            button1.Size = new Size(95, 31);
            button1.TabIndex = 29;
            button1.Text = "Simpan";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(tbAddress);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(cbProvinces);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(cbCities);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(cbDistricts);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(cbVillages);
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.Location = new Point(179, 159);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(393, 225);
            groupBox1.TabIndex = 31;
            groupBox1.TabStop = false;
            groupBox1.Text = "Alamat";
            // 
            // tbFooter
            // 
            tbFooter.Location = new Point(179, 407);
            tbFooter.MaxLength = 250;
            tbFooter.Multiline = true;
            tbFooter.Name = "tbFooter";
            tbFooter.ScrollBars = ScrollBars.Vertical;
            tbFooter.Size = new Size(393, 75);
            tbFooter.TabIndex = 22;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(179, 387);
            label10.Name = "label10";
            label10.Size = new Size(272, 17);
            label10.TabIndex = 32;
            label10.Text = "Text Footer (Dicetak pada bagian bawa struk)";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.BackgroundImage = Properties.Resources.bg_2;
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(label11);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(picLogo);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(163, 541);
            panel1.TabIndex = 33;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.Transparent;
            label11.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.White;
            label11.Location = new Point(12, 9);
            label11.Name = "label11";
            label11.Size = new Size(143, 21);
            label11.TabIndex = 35;
            label11.Text = "Havas Media 2025";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Location = new Point(12, 325);
            label9.Name = "label9";
            label9.Size = new Size(38, 17);
            label9.TabIndex = 34;
            label9.Text = "Logo";
            // 
            // button3
            // 
            button3.Location = new Point(12, 498);
            button3.Name = "button3";
            button3.Size = new Size(137, 31);
            button3.TabIndex = 34;
            button3.Text = "Ubah Logo";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.White;
            picLogo.BorderStyle = BorderStyle.FixedSingle;
            picLogo.Image = Properties.Resources.budget;
            picLogo.Location = new Point(12, 345);
            picLogo.Name = "picLogo";
            picLogo.Padding = new Padding(10);
            picLogo.Size = new Size(137, 137);
            picLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            // 
            // OfficeDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 541);
            Controls.Add(panel1);
            Controls.Add(label10);
            Controls.Add(tbFooter);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(tbEmail);
            Controls.Add(label8);
            Controls.Add(tbPhone);
            Controls.Add(label7);
            Controls.Add(tbName);
            Controls.Add(label6);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "OfficeDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Data Perusahaan";
            Load += OfficeDialog_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbVillages;
        private Label label5;
        private ComboBox cbDistricts;
        private Label label4;
        private ComboBox cbCities;
        private Label label3;
        private ComboBox cbProvinces;
        private Label label2;
        private TextBox tbAddress;
        private Label label1;
        private TextBox tbName;
        private Label label6;
        private TextBox tbPhone;
        private Label label7;
        private TextBox tbEmail;
        private Label label8;
        private Button button2;
        private Button button1;
        private GroupBox groupBox1;
        private TextBox tbFooter;
        private Label label10;
        private Panel panel1;
        private Button button3;
        private PictureBox picLogo;
        private Label label9;
        private Label label11;
    }
}