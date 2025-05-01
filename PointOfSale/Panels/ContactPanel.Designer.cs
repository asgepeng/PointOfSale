using System.Windows.Forms;
using System.Drawing;

namespace PointOfSale.Panels
{
    partial class ContactPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContactPanel));
            label1 = new Label();
            tbName = new TextBox();
            tbTitle = new TextBox();
            label2 = new Label();
            lvPhone = new ListView();
            colNumber = new ColumnHeader();
            colType = new ColumnHeader();
            cmsPhone = new ContextMenuStrip(components);
            aturSebagaiNomorUtamaToolStripMenuItem = new ToolStripMenuItem();
            imageList2 = new ImageList(components);
            label3 = new Label();
            label4 = new Label();
            lvAddress = new ListView();
            addressDescription = new ColumnHeader();
            addressType = new ColumnHeader();
            addressPrimary = new ColumnHeader();
            cmsAddress = new ContextMenuStrip(components);
            toolStripMenuItem1 = new ToolStripMenuItem();
            imageList1 = new ImageList(components);
            button1 = new Button();
            btnDeletePhone = new Button();
            btnDeleteAddress = new Button();
            button4 = new Button();
            tbOrganization = new TextBox();
            label5 = new Label();
            cbType = new ComboBox();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            cmsPhone.SuspendLayout();
            cmsAddress.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 31);
            label1.Name = "label1";
            label1.Size = new Size(43, 17);
            label1.TabIndex = 0;
            label1.Text = "Nama";
            // 
            // tbName
            // 
            tbName.Location = new Point(113, 28);
            tbName.Name = "tbName";
            tbName.Size = new Size(390, 25);
            tbName.TabIndex = 1;
            // 
            // tbTitle
            // 
            tbTitle.Location = new Point(113, 89);
            tbTitle.Name = "tbTitle";
            tbTitle.Size = new Size(390, 25);
            tbTitle.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 91);
            label2.Name = "label2";
            label2.Size = new Size(32, 17);
            label2.TabIndex = 2;
            label2.Text = "Title";
            // 
            // lvPhone
            // 
            lvPhone.Columns.AddRange(new ColumnHeader[] { colNumber, colType });
            lvPhone.ContextMenuStrip = cmsPhone;
            lvPhone.LargeImageList = imageList2;
            lvPhone.Location = new Point(113, 151);
            lvPhone.Name = "lvPhone";
            lvPhone.Size = new Size(390, 171);
            lvPhone.TabIndex = 4;
            lvPhone.TileSize = new Size(160, 36);
            lvPhone.UseCompatibleStateImageBehavior = false;
            lvPhone.View = View.Tile;
            lvPhone.ItemActivate += OpenPhoneDialog;
            lvPhone.SelectedIndexChanged += HandlerSelectedItemChanged;
            // 
            // cmsPhone
            // 
            cmsPhone.Items.AddRange(new ToolStripItem[] { aturSebagaiNomorUtamaToolStripMenuItem });
            cmsPhone.Name = "cmsPhone";
            cmsPhone.Size = new Size(221, 26);
            cmsPhone.Opening += cmsPhone_Opening;
            // 
            // aturSebagaiNomorUtamaToolStripMenuItem
            // 
            aturSebagaiNomorUtamaToolStripMenuItem.Name = "aturSebagaiNomorUtamaToolStripMenuItem";
            aturSebagaiNomorUtamaToolStripMenuItem.Size = new Size(220, 22);
            aturSebagaiNomorUtamaToolStripMenuItem.Text = "Atur Sebagai Nomor Utama";
            aturSebagaiNomorUtamaToolStripMenuItem.Click += aturSebagaiNomorUtamaToolStripMenuItem_Click;
            // 
            // imageList2
            // 
            imageList2.ColorDepth = ColorDepth.Depth32Bit;
            imageList2.ImageStream = (ImageListStreamer)resources.GetObject("imageList2.ImageStream");
            imageList2.TransparentColor = Color.Transparent;
            imageList2.Images.SetKeyName(0, "money-bills.png");
            imageList2.Images.SetKeyName(1, "remove.png");
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 154);
            label3.Name = "label3";
            label3.Size = new Size(54, 17);
            label3.TabIndex = 5;
            label3.Text = "Telepon";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(25, 332);
            label4.Name = "label4";
            label4.Size = new Size(48, 17);
            label4.TabIndex = 7;
            label4.Text = "Alamat";
            // 
            // lvAddress
            // 
            lvAddress.Columns.AddRange(new ColumnHeader[] { addressDescription, addressType, addressPrimary });
            lvAddress.ContextMenuStrip = cmsAddress;
            lvAddress.LargeImageList = imageList1;
            lvAddress.Location = new Point(113, 328);
            lvAddress.Name = "lvAddress";
            lvAddress.Size = new Size(390, 171);
            lvAddress.TabIndex = 6;
            lvAddress.TileSize = new Size(260, 36);
            lvAddress.UseCompatibleStateImageBehavior = false;
            lvAddress.View = View.Tile;
            lvAddress.ItemActivate += lvAddress_ItemActivate;
            lvAddress.SelectedIndexChanged += lvAddress_SelectedIndexChanged;
            // 
            // addressDescription
            // 
            addressDescription.Text = "Alamat";
            // 
            // addressType
            // 
            addressType.Text = "Tipe";
            // 
            // addressPrimary
            // 
            addressPrimary.Text = "Utama";
            // 
            // cmsAddress
            // 
            cmsAddress.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
            cmsAddress.Name = "cmsPhone";
            cmsAddress.Size = new Size(217, 26);
            cmsAddress.Opening += cmsAddress_Opening;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(216, 22);
            toolStripMenuItem1.Text = "Atur sebagai alamat utama";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "atm-card.png");
            imageList1.Images.SetKeyName(1, "import.png");
            imageList1.Images.SetKeyName(2, "checklist (1).png");
            // 
            // button1
            // 
            button1.BackColor = Color.Blue;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(509, 151);
            button1.Name = "button1";
            button1.Size = new Size(75, 25);
            button1.TabIndex = 8;
            button1.Text = "Tambah";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // btnDeletePhone
            // 
            btnDeletePhone.BackColor = Color.Blue;
            btnDeletePhone.Enabled = false;
            btnDeletePhone.FlatAppearance.BorderSize = 0;
            btnDeletePhone.FlatStyle = FlatStyle.Flat;
            btnDeletePhone.ForeColor = Color.White;
            btnDeletePhone.Location = new Point(509, 182);
            btnDeletePhone.Name = "btnDeletePhone";
            btnDeletePhone.Size = new Size(75, 25);
            btnDeletePhone.TabIndex = 9;
            btnDeletePhone.Text = "Hapus";
            btnDeletePhone.UseVisualStyleBackColor = false;
            btnDeletePhone.Click += btnDeletePhone_Click;
            // 
            // btnDeleteAddress
            // 
            btnDeleteAddress.BackColor = Color.Blue;
            btnDeleteAddress.Enabled = false;
            btnDeleteAddress.FlatAppearance.BorderSize = 0;
            btnDeleteAddress.FlatStyle = FlatStyle.Flat;
            btnDeleteAddress.ForeColor = Color.White;
            btnDeleteAddress.Location = new Point(509, 359);
            btnDeleteAddress.Name = "btnDeleteAddress";
            btnDeleteAddress.Size = new Size(75, 25);
            btnDeleteAddress.TabIndex = 11;
            btnDeleteAddress.Text = "Hapus";
            btnDeleteAddress.UseVisualStyleBackColor = false;
            btnDeleteAddress.Click += btnDeleteAddress_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.Blue;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.ForeColor = Color.White;
            button4.Location = new Point(509, 328);
            button4.Name = "button4";
            button4.Size = new Size(75, 25);
            button4.TabIndex = 10;
            button4.Text = "Tambah";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // tbOrganization
            // 
            tbOrganization.Location = new Point(113, 120);
            tbOrganization.Name = "tbOrganization";
            tbOrganization.Size = new Size(390, 25);
            tbOrganization.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(25, 122);
            label5.Name = "label5";
            label5.Size = new Size(70, 17);
            label5.TabIndex = 12;
            label5.Text = "Organisasi";
            // 
            // cbType
            // 
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbType.Items.AddRange(new object[] { "Pelanggan", "Supplier" });
            cbType.Location = new Point(113, 58);
            cbType.Name = "cbType";
            cbType.Size = new Size(121, 25);
            cbType.TabIndex = 14;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(25, 61);
            label6.Name = "label6";
            label6.Size = new Size(33, 17);
            label6.TabIndex = 15;
            label6.Text = "Tipe";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9.75F);
            label7.ForeColor = Color.Red;
            label7.Location = new Point(66, 31);
            label7.Name = "label7";
            label7.Size = new Size(13, 17);
            label7.TabIndex = 16;
            label7.Text = "*";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9.75F);
            label8.ForeColor = Color.Red;
            label8.Location = new Point(56, 61);
            label8.Name = "label8";
            label8.Size = new Size(13, 17);
            label8.TabIndex = 17;
            label8.Text = "*";
            // 
            // ContactPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(tbOrganization);
            Controls.Add(label5);
            Controls.Add(btnDeleteAddress);
            Controls.Add(button4);
            Controls.Add(btnDeletePhone);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(lvAddress);
            Controls.Add(label3);
            Controls.Add(lvPhone);
            Controls.Add(tbTitle);
            Controls.Add(label2);
            Controls.Add(tbName);
            Controls.Add(label1);
            Controls.Add(cbType);
            Font = new Font("Segoe UI", 9.75F);
            Name = "ContactPanel";
            Size = new Size(748, 549);
            Load += OnPanelLoaded;
            cmsPhone.ResumeLayout(false);
            cmsAddress.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbName;
        private TextBox tbTitle;
        private Label label2;
        private ListView lvPhone;
        private ColumnHeader colNumber;
        private ColumnHeader colType;
        private Label label3;
        private Label label4;
        private ListView lvAddress;
        private ColumnHeader addressDescription;
        private ColumnHeader addressType;
        private Button button1;
        private Button btnDeletePhone;
        private Button btnDeleteAddress;
        private Button button4;
        private ImageList imageList1;
        private ColumnHeader addressPrimary;
        private TextBox tbOrganization;
        private Label label5;
        private ImageList imageList2;
        private ComboBox cbType;
        private Label label6;
        private Label label7;
        private Label label8;
        private ContextMenuStrip cmsPhone;
        private ToolStripMenuItem aturSebagaiNomorUtamaToolStripMenuItem;
        private ContextMenuStrip cmsAddress;
        private ToolStripMenuItem toolStripMenuItem1;
    }
}
