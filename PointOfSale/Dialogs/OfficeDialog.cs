using PointOfSale.Data;
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
    public partial class OfficeDialog : Form
    {
        private readonly Database db;
        private readonly AddressManager manager;
        public OfficeDialog(Database _db)
        {
            InitializeComponent();
            db = _db;
            manager = new AddressManager(db);
        }
        private void SetEnableControl(bool enable)
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox t)
                {
                    t.ReadOnly = !enable;
                }
                else
                {
                    c.Enabled = enable;
                }
            }
        }
        private async void OfficeDialog_Load(object sender, EventArgs e)
        {
            SetEnableControl(false);
            var office = await Store.GetDefault(db);
            if (office != null)
            {
                tbName.Text = office.Name;
                tbEmail.Text = office.Email;
                tbPhone.Text = office.Phone;
                tbAddress.Text = office.Address.Streetline;
                manager.InitializeProvinceComboBox(cbProvinces, await manager.GetProvincesAsync(), office.Address.Province);
                manager.InitializeCityComboBox(cbCities, await manager.GetCitiesAsync(office.Address.Province), office.Address.City);
                manager.InitializeDistrictComboBox(cbDistricts, await manager.GetDistrictsAsync(office.Address.City), office.Address.District);
                manager.InitializeVillageComboBox(cbVillages, await manager.GetVillagesAsync(office.Address.District), office.Address.Village);
                tbFooter.Text = office.FooterText;
                if (office.Logo != null) picLogo.Image = office.Logo;
                Tag = true;
            }

            cbDistricts.SelectedIndexChanged += OnDistrictChanged;
            cbCities.SelectedIndexChanged += OnCityChanged;
            cbProvinces.SelectedIndexChanged += OnProvinceChanged;

            SetEnableControl(true);
        }
        private async void OnDistrictChanged(object sender, EventArgs e)
        {
            if (cbDistricts.SelectedIndex >= 0)
            {
                if (cbDistricts.SelectedItem != null)
                {
                    var district = (District)cbDistricts.SelectedItem;
                    cbVillages.Items.Clear();
                    cbVillages.Items.AddRange((await manager.GetVillagesAsync(district.Id)).ToArray());
                }
            }
            else
            {
                cbVillages.Items.Clear();
            }
        }
        private async void OnCityChanged(object sender, EventArgs e)
        {
            if (cbCities.SelectedIndex > -1)
            {
                if (cbCities.SelectedItem != null)
                {
                    var city = (City)cbCities.SelectedItem;

                    cbDistricts.Items.Clear();
                    cbDistricts.Items.AddRange((await manager.GetDistrictsAsync(city.Id)).ToArray());

                    cbVillages.Items.Clear();
                }
            }
            else
            {
                cbDistricts.Items.Clear();
            }
        }
        private async void OnProvinceChanged(object sender, EventArgs e)
        {
            if (cbProvinces.SelectedIndex > -1)
            {
                if (cbProvinces.SelectedItem != null)
                {
                    var province = (Province)cbProvinces.SelectedItem;

                    cbCities.Items.Clear();
                    cbCities.Items.AddRange((await manager.GetCitiesAsync(province.Id)).ToArray());

                    cbDistricts.Items.Clear();
                    cbVillages.Items.Clear();
                }
            }
            else
            {
                cbCities.Items.Clear();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Trim() == "")
            {
                MessageBox.Show("Nama toko tidak boleh kosong", "Data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbName.Focus();
                return;
            }
            if (tbPhone.Text.Trim() == "")
            {
                MessageBox.Show("Nomor Telepon tidak boleh kosong", "Data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbPhone.Focus();
                return;
            }
            if (tbEmail.Text.Trim() != "")
            {
                if (!FormatHelpers.IsValidEmail(tbEmail.Text.Trim()))
                {
                    MessageBox.Show("Masukkan alamat email yang valid", "Invalid email", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbEmail.Focus();
                    return;
                }
            }
            if (tbAddress.Text.Trim() == "")
            {
                MessageBox.Show("Alamat toko tidak boleh kosong", "Alamat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbAddress.Focus();
                return;
            }
            if (cbVillages.SelectedItem is null)
            {
                MessageBox.Show("Alamat masih belum lengkap", "Alamat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SetEnableControl(false);
            var villageID = cbVillages.SelectedItem != null ? (long)((Village)cbVillages.SelectedItem).Id : 0;
            if (Tag != null)
            {
                await Store.UpdateDefault(db, tbName.Text.Trim(), tbPhone.Text.Trim(), tbEmail.Text.Trim(), tbAddress.Text.Trim(), villageID, tbFooter.Text.Trim(), picLogo.Image);
            }
            else
            {
                await Store.SetDefault(db, tbName.Text.Trim(), tbPhone.Text.Trim(), tbEmail.Text.Trim(), tbAddress.Text.Trim(), villageID, tbFooter.Text.Trim(), picLogo.Image);
            }
            
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FilterOnlyNumber(object sender, KeyPressEventArgs e)
        {
            FormatHelpers.FilterOnlyNumber(e);
        }
        private void FormatPhoneNumber(object sender, EventArgs e)
        {
            if (tbPhone.Text.Length == 0) return;
            var number = tbPhone.Text;
            if (number.StartsWith("62"))
            {
                number = "+" + number;
                if (number.Length > 10) number = number.Insert(10, " ");
                if (number.Length > 6) number = number.Insert(6, " ");
                if (number.Length > 3) number = number.Insert(3, " ");
            }
            else
            {
                if (number.Length > 9) number = number.Insert(8, " ");
                if (number.Length > 5) number = number.Insert(4, " ");
            }
            tbPhone.Text = number;
        }

        private void RemoveSpace(object sender, EventArgs e)
        {
            if (tbPhone.Text.Length == 0) return;
            tbPhone.Text = tbPhone.Text.Replace(" ", "").Replace("+", "");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Title = "Pilih Logo";
            dlg.Filter = "PNG Format(*.png)|*.png";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                picLogo.Image = Image.FromFile(dlg.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
