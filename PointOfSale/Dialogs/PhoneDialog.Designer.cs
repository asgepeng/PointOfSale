using System.Windows.Forms;
using System.Drawing;

namespace PointOfSale.Dialogs
{
    partial class PhoneDialog
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
            cbType = new ComboBox();
            label6 = new Label();
            tbNumber = new TextBox();
            label1 = new Label();
            button2 = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // cbType
            // 
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbType.FormattingEnabled = true;
            cbType.Items.AddRange(new object[] { "Rumah", "Kantor", "Whatsapp", "Lainnya" });
            cbType.Location = new Point(12, 42);
            cbType.Name = "cbType";
            cbType.Size = new Size(260, 25);
            cbType.TabIndex = 16;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 22);
            label6.Name = "label6";
            label6.Size = new Size(33, 17);
            label6.TabIndex = 15;
            label6.Text = "Tipe";
            // 
            // tbStreetline
            // 
            tbNumber.Location = new Point(12, 95);
            tbNumber.Name = "tbStreetline";
            tbNumber.ScrollBars = ScrollBars.Vertical;
            tbNumber.Size = new Size(260, 25);
            tbNumber.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 75);
            label1.Name = "label1";
            label1.Size = new Size(50, 17);
            label1.TabIndex = 13;
            label1.Text = "Nomor";
            // 
            // button2
            // 
            button2.Location = new Point(76, 168);
            button2.Name = "button2";
            button2.Size = new Size(95, 31);
            button2.TabIndex = 18;
            button2.Text = "Batal";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(177, 168);
            button1.Name = "button1";
            button1.Size = new Size(95, 31);
            button1.TabIndex = 17;
            button1.Text = "Simpan";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // PhoneDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 211);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(cbType);
            Controls.Add(label6);
            Controls.Add(tbNumber);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "PhoneDialog";
            Text = "Telepon";
            Load += PhoneDialog_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbType;
        private Label label6;
        private TextBox tbNumber;
        private Label label1;
        private Button button2;
        private Button button1;
    }
}