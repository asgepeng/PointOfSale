using PointOfSale.Data;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSale.Dialogs
{
    public partial class AddressDialog : Form
    {
        private readonly AddressManager manager;
        private readonly Database db;
        public AddressDialog(Database _db)
        {
            InitializeComponent();
            db = _db;
            manager = new AddressManager(db);
        }

        private void CloseDialog(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void OnDialogLoad(object sender, EventArgs e)
        {
            Address address;
            if (Tag != null) address = (Address)Tag;
            else
            {
                var store = await Store.GetDefault(db);
                if (store != null) { address = store.Address; address.Streetline = ""; }
                else address = new Address();
            }
            cbType.SelectedIndex = address.Type;
            tbStreetline.Text = address.Streetline;
            var provinces = await manager.GetProvincesAsync();
            var cities = await manager.GetCitiesAsync(address.Province);
            var districts = await manager.GetDistrictsAsync(address.City);
            var villages = await manager.GetVillagesAsync(address.District);

            manager.InitializeProvinceComboBox(cbProvinces, provinces, address.Province);
            manager.InitializeCityComboBox(cbCities, cities, address.City);
            manager.InitializeDistrictComboBox(cbDistricts, districts, address.District);
            manager.InitializeVillageComboBox(cbVillages, villages, address.Village);

            ckPrimary.Checked = address.IsPrimary;

            cbDistricts.SelectedIndexChanged += OnDistrictChanged;
            cbCities.SelectedIndexChanged += OnCityChanged;
            cbProvinces.SelectedIndexChanged += OnProvinceChanged;

            button1.Click += OnSubmit;
        }

        private void OnSubmit(object sender, EventArgs e)
        {
            if (cbType.SelectedIndex == -1)
            {
                MessageBox.Show("Tipe alamat belum dipilih, field ini tidak boleh kosong", "Tipe alamat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbType.Focus();
                return;
            }

            if (tbStreetline.Text.Trim() == "")
            {
                MessageBox.Show("Dusun / Nama jalan tidak boleh kosong", "Data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbStreetline.Focus();
                return;
            }
            if (cbProvinces.SelectedItem is null)
            {
                MessageBox.Show("Propinsi belum dipilih, field ini tidak boleh kosong", "Data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbProvinces.Focus();
                return;
            }
            if (cbCities.SelectedItem is null)
            {
                MessageBox.Show("Kabupaten / kota belum dipilih, field ini tidak boleh kosong", "Data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCities.Focus();
                return;
            }
            if (cbDistricts.SelectedItem is null)
            {
                MessageBox.Show("Kecamatan belum dipilih, field ini tidak boleh kosong", "Data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbDistricts.Focus();
                return;
            }
            if (cbVillages.SelectedItem is null)
            {
                MessageBox.Show("Desa / Kelurahan belum dipilih, field ini tidak boleh kosong", "Data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbVillages.Focus();
                return;
            }
            var address = Tag != null ? (Address)Tag : new Address();
            address.Type = cbType.SelectedIndex;
            address.Streetline = tbStreetline.Text.Trim();
            address.Province = ((Province)cbProvinces.SelectedItem).Id;
            address.City = ((City)cbCities.SelectedItem).Id;
            address.District = ((District)cbDistricts.SelectedItem).Id;
            address.Village = ((Village)cbVillages.SelectedItem).Id;
            address.IsPrimary = ckPrimary.Checked;
            address.Description = cbCities.Text + " - " + cbProvinces.Text; 
            Tag = address;
            DialogResult = DialogResult.OK;
            Close();
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
    }
}
