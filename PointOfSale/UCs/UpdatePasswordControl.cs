using PointOfSale.Models;
using System;
using System.Windows.Forms;

namespace PointOfSale.UCs
{
    public partial class UpdatePasswordControl : UserControl
    {
        private readonly LoginManager manager;
        public UpdatePasswordControl(Database _db)
        {
            InitializeComponent();
            manager = new LoginManager(_db);
            Dock = DockStyle.Fill;
            Text = "Ubah Password";
        }
        private void SetEnableControl(bool enable)
        {
            oldPassword.Enabled = enable;
            newPassword.Enabled = enable;
            confirmNewPassword.Enabled = enable;
        }
        private async void UpdateButtonClicked(object sender, EventArgs e)
        {
            if (oldPassword.Text == "")
            {
                MessageBox.Show("Password lama tidak boleh kosong", "Password lama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                oldPassword.Focus();
                return;
            }
            if (newPassword.Text == "")
            {
                MessageBox.Show("Password baru tidak boleh kosong.", "Password Baru", MessageBoxButtons.OK, MessageBoxIcon.Information);
                newPassword.Focus();
                return;
            }
            if (confirmNewPassword.Text == "")
            {
                MessageBox.Show("Konfirmasi password tidak boleh kosong", "Konfirmasi Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                confirmNewPassword.Focus();
                return;
            }

            if (confirmNewPassword.Text != newPassword.Text)
            {
                MessageBox.Show("Konfirmasi password tidak match", "Konfirmasi Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                confirmNewPassword.Focus();
                return;
            }
            SetEnableControl(false);
            var result = await manager.UpdatePasswordAsync(this.oldPassword.Text, this.confirmNewPassword.Text);
            if (result == 1)
            {
                MessageBox.Show("Password lama tidak valid", "Password Lama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetEnableControl(true);
            }
            else if (result == 2)
            {
                MessageBox.Show("User tidak login saat ini", "User null", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetEnableControl(true);
            }
            else if (result == 0)
            {
                MessageBox.Show("Password berhasil diperbarui", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Gagal memperbarui password karena kesalahan yang tidak diketahui", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
