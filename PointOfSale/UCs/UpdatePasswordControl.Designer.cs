using System.Windows.Forms;
using System.Drawing;

namespace PointOfSale.UCs
{
    partial class UpdatePasswordControl
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
            button1 = new Button();
            newPassword = new TextBox();
            label2 = new Label();
            label1 = new Label();
            oldPassword = new TextBox();
            confirmNewPassword = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.Location = new Point(30, 261);
            button1.Name = "button1";
            button1.Size = new Size(256, 29);
            button1.TabIndex = 3;
            button1.Text = "Perbarui Password";
            button1.UseVisualStyleBackColor = true;
            button1.Click += UpdateButtonClicked;
            // 
            // newPassword
            // 
            newPassword.Anchor = AnchorStyles.None;
            newPassword.Location = new Point(30, 168);
            newPassword.Name = "newPassword";
            newPassword.PasswordChar = '*';
            newPassword.Size = new Size(256, 25);
            newPassword.TabIndex = 1;
            newPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(30, 148);
            label2.Name = "label2";
            label2.Size = new Size(156, 17);
            label2.TabIndex = 7;
            label2.Text = "Masukkan Password Baru";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(30, 100);
            label1.Name = "label1";
            label1.Size = new Size(172, 17);
            label1.TabIndex = 5;
            label1.Text = "Masukkan Password Saat Ini";
            // 
            // oldPassword
            // 
            oldPassword.Anchor = AnchorStyles.None;
            oldPassword.Location = new Point(30, 120);
            oldPassword.Name = "oldPassword";
            oldPassword.PasswordChar = '*';
            oldPassword.Size = new Size(256, 25);
            oldPassword.TabIndex = 0;
            oldPassword.UseSystemPasswordChar = true;
            // 
            // confirmNewPassword
            // 
            confirmNewPassword.Anchor = AnchorStyles.None;
            confirmNewPassword.Location = new Point(30, 216);
            confirmNewPassword.Name = "confirmNewPassword";
            confirmNewPassword.PasswordChar = '*';
            confirmNewPassword.Size = new Size(256, 25);
            confirmNewPassword.TabIndex = 2;
            confirmNewPassword.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point(30, 196);
            label3.Name = "label3";
            label3.Size = new Size(160, 17);
            label3.TabIndex = 11;
            label3.Text = "Konfirmasi Password Baru";
            // 
            // UpdatePasswordControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(confirmNewPassword);
            Controls.Add(label3);
            Controls.Add(oldPassword);
            Controls.Add(button1);
            Controls.Add(newPassword);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "UpdatePasswordControl";
            Size = new Size(316, 349);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox newPassword;
        private Label label2;
        private Label label1;
        private TextBox oldPassword;
        private TextBox confirmNewPassword;
        private Label label3;
    }
}
