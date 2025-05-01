using PointOfSale.Data;
using PointOfSale.Dialogs;
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

namespace PointOfSale.Panels
{
    public partial class ContactPanel : PanelBase
    {
        private List<int> DeletedAddresses { get; } = new List<int>();
        private readonly Database db;
        public ContactPanel(Database _db)
        {
            InitializeComponent();
            db = _db;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddressDialog dlg = new AddressDialog(db);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.Tag is null) return;
                var address = (Address)dlg.Tag;
                var lvi = lvAddress.Items.Add(address.Streetline, address.Type);
                lvi.Tag = address;
                lvi.SubItems.Add(address.Description);
                if (address.Type == 0) lvi.SubItems.Add("Rumah").Font = new Font("Segoe UI", 8.25F, FontStyle.Regular);
                else if (address.Type == 1) lvi.SubItems.Add("Kantor").Font = new Font("Segoe UI", 8.25F, FontStyle.Regular);
                if (address.IsPrimary) lvi.SubItems.Add("Utama").Font = new Font("Segoe UI", 8.25F, FontStyle.Regular);
            }
        }

        public override async Task<bool> Save(IRepository repository)
        {
            if (this.tbName.Text.Trim() == "")
            {
                MessageBox.Show("Nama tidak boleh kosong", "data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbName.Focus();
                return false;
            }
            if (cbType.SelectedIndex == -1)
            {
                MessageBox.Show("Tipe kontak belum dipilih, field ini tidak boleh kosong", "Data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbType.Focus();
                return false;
            }

            var contact = this.Tag is null ? new Contact() : (Contact)Tag;
            contact.Name = tbName.Text.Trim();
            contact.Type = cbType.SelectedIndex + 1;
            contact.Title = tbTitle.Text.Trim();
            contact.Organization = tbOrganization.Text.Trim();
            contact.Addresses.Clear();
            bool havePrimaryAddress = false;
            foreach (ListViewItem item in lvAddress.Items)
            {
                if (item.Tag != null)
                {
                    var address = (Address)item.Tag;
                    if (!havePrimaryAddress) havePrimaryAddress = address.IsPrimary == true;
                    contact.Addresses.Add(address);
                }
            }
            if ((contact.Addresses.Count == 1 && !havePrimaryAddress) || (contact.Addresses.Count > 1 && !havePrimaryAddress)) contact.Addresses[0].IsPrimary = true;
            contact.Phones.Clear();
            bool havePrimaryPhone = false;
            foreach (ListViewItem item in lvPhone.Items)
            {
                if (item.Tag != null)
                {
                    var phone = (Phone)item.Tag;
                    contact.Phones.Add(phone);
                    if (!havePrimaryPhone) havePrimaryPhone = phone.IsPrimary == true;
                }
            }
            if ((contact.Phones.Count == 1 && !havePrimaryPhone) || (contact.Phones.Count > 1 && !havePrimaryPhone)) contact.Phones[0].IsPrimary = true;
            if (contact.Id > 0)
            {
                if (DeletedAddresses.Count > 0) contact.DeletedAddresses.AddRange(DeletedAddresses.ToArray());
                contact = (Contact)await repository.UpdateAsync(contact);
            }
            else
            {
                contact = (Contact)await repository.CreateAsync(contact);
            }
            return contact.Id > 0;
        }

        private void OnPanelLoaded(object sender, EventArgs e)
        {
            if (Tag != null)
            {
                var contact = (Contact)Tag;
                tbName.Text = contact.Name;
                cbType.SelectedIndex = contact.Type - 1;
                tbTitle.Text = contact.Title;
                tbOrganization.Text = contact.Organization;
                foreach (var phone in contact.Phones)
                {
                    var lvi = lvPhone.Items.Add(phone.Number, phone.Type - 1);
                    lvi.Tag = phone;
                    lvi.SubItems.Add(Phone.GetPhoneTypeName(phone.Type));

                }
                foreach (var add in contact.Addresses)
                {
                    var lvi = lvAddress.Items.Add(add.Streetline, add.Type);
                    lvi.SubItems.Add(add.Description);
                    if (add.IsPrimary) lvi.SubItems.Add("Utama");
                    lvi.Tag = add;
                }
            }
        }

        private void lvAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDeleteAddress.Enabled = lvAddress.SelectedItems.Count > 0;
        }

        private void btnDeleteAddress_Click(object sender, EventArgs e)
        {
            if (lvAddress.SelectedItems.Count == 0) return;
            if (MessageBox.Show("Yakin Menghapus alamat", "Hapus", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                var obj = lvAddress.SelectedItems[0].Tag;
                if (obj != null)
                {
                    var address = (Address)obj;
                    if (address.Id > 0) DeletedAddresses.Add(address.Id);
                    lvAddress.Items.Remove(lvAddress.SelectedItems[0]);
                }
            }
        }

        private void lvAddress_ItemActivate(object sender, EventArgs e)
        {
            if (lvAddress.SelectedItems.Count > 0)
            {
                var obj = lvAddress.SelectedItems[0].Tag;
                if (obj is null) return;

                var address = (Address)obj;
                var dlg = new AddressDialog(db);
                dlg.Tag = address;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.Tag is null) return;
                    address = (Address)dlg.Tag;
                    var lvi = lvAddress.SelectedItems[0];
                    lvi.Tag = address;
                    lvi.SubItems[1].Text = address.Description;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dlg = new PhoneDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.Tag is null) return;
                var phone = (Phone)dlg.Tag;
                if (lvPhone.Items.Count == 0) phone.IsPrimary = true;
                var lvi = lvPhone.Items.Add(phone.Number, phone.Type);
                lvi.SubItems.Add(Phone.GetPhoneTypeName(phone.Type));
                lvi.Tag = phone;
            }
        }

        private void HandlerSelectedItemChanged(object sender, EventArgs e)
        {
            btnDeletePhone.Enabled = lvPhone.SelectedItems.Count > 0;
        }

        private void OpenPhoneDialog(object sender, EventArgs e)
        {
            if (lvPhone.SelectedItems.Count > 0)
            {
                var obj = lvPhone.SelectedItems[0].Tag;
                if (obj != null)
                {
                    var phone = (Phone)obj;
                    var dlg = new PhoneDialog();
                    dlg.Tag = phone;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg.Tag != null)
                        {
                            phone = (Phone)dlg.Tag;
                            lvPhone.SelectedItems[0].Tag = phone;
                            lvPhone.SelectedItems[0].Text = phone.Number;
                            lvPhone.SelectedItems[0].SubItems[1].Text = Phone.GetPhoneTypeName(phone.Type);
                        }
                    }
                }
            }
        }

        private void cmsPhone_Opening(object sender, CancelEventArgs e)
        {
            if (lvPhone.SelectedItems.Count == 0)
            {
                e.Cancel = true;
            }
            else
            {
                var obj = lvPhone.SelectedItems[0].Tag;
                if (obj != null)
                {
                    var phone = (Phone)obj;
                    if (phone.IsPrimary) e.Cancel = true;
                }
            }
        }

        private void aturSebagaiNomorUtamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItem = lvPhone.SelectedItems[0];
            foreach (ListViewItem item in lvPhone.Items)
            {
                var obj = item.Tag;
                if (obj != null)
                {
                    var phone = (Phone)obj;
                    phone.IsPrimary = (item == selectedItem);
                }
            }
        }

        private void cmsAddress_Opening(object sender, CancelEventArgs e)
        {
            if (lvAddress.SelectedItems.Count == 0)
            {
                e.Cancel = true;
            }
            else
            {
                var obj = lvAddress.SelectedItems[0].Tag;
                if (obj != null)
                {
                    var address = (Address)obj;
                    if (address.IsPrimary) e.Cancel = true;
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var selectedItem = lvAddress.SelectedItems[0];
            foreach (ListViewItem item in lvAddress.Items)
            {
                var obj = item.Tag;
                if (obj != null)
                {
                    var address = (Address)obj;
                    address.IsPrimary = (item == selectedItem);
                }
            }
        }

        private void btnDeletePhone_Click(object sender, EventArgs e)
        {
            lvPhone.Items.Remove(lvPhone.SelectedItems[0]);
        }
    }
}
