using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Title { get; set; } = "";
        public string Organization { get; set; } = "";
        public int Type { get; set; } = 0;
        public List<Address> Addresses { get; } = new List<Address>();
        public List<int> DeletedAddresses { get; } = new List<int>();
        public List<Phone> Phones { get; } = new List<Phone>();
        public List<int> DeletedPhones { get; } = new List<int>();
    }
    public class Phone
    {
        public int Type { get; set; } = 0;
        public string Number { get; set; } = "";
        public bool IsPrimary { get; set; } = false;
        public static string GetPhoneTypeName(int type)
        {
            if (type < 0 || type > 3) return "-";
            return (new string[] { "Rumah", "Seluler", "Kantor", "Whatsapp" })[type];
        }
    }
    public class Address
    {
        public int Id { get; set; } = 0;
        public int Type { get; set; } = 0;
        public string Streetline { get; set; } = "";
        public Int64 Village { get; set; } = 0;
        public int District { get; set; } = 0;
        public int City { get; set; } = 0;
        public int Province { get; set; } = 0;
        public string Description { get; set; } = "";
        public bool IsPrimary { get; set; } = false;
    }

    internal class ContactManager
    {
        internal async Task<DataTable> GetDataTable()
        {
            return await Task.FromResult<DataTable>(new DataTable());
        }
        internal async Task<Contact> CreateAsync(Contact contact)
        {
            return contact;
        }
    }
}
