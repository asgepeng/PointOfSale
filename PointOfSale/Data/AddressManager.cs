using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using PointOfSale.Models;
using System.Windows.Forms;

namespace PointOfSale.Data
{
    public class AddressManager
    {
        private readonly IDatabase db;
        public AddressManager(Database _db) { db = _db; }
        public async Task<List<Province>> GetProvincesAsync()
        {
            var list = new List<Province>();
            var commandText = "SELECT [id], [name] FROM provinces ORDER BY [name]";
            using (var reader = await db.ExecuteReaderAsync(commandText))
            {
                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new Province()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
            return list;
        }
        public async Task<List<City>> GetCitiesAsync(int provinceId)
        {
            var list = new List<City>();
            var commandText = "SELECT [id], [name] FROM cities WHERE province = @province ORDER BY [name]";
            using (var reader = await db.ExecuteReaderAsync(commandText, new SqlParameter("@province", provinceId)))
            {
                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new City()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
            return list;
        }
        public async Task<List<District>> GetDistrictsAsync(int cityId)
        {
            var list = new List<District>();
            var commandText = "SELECT [id], [name] FROM districts WHERE city = @city ORDER BY [name]";
            using (var reader = await db.ExecuteReaderAsync(commandText, new SqlParameter("@city", cityId)))
            {
                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new District()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
            return list;
        }
        public async Task<List<Village>> GetVillagesAsync(int districtId)
        {
            var list = new List<Village>();
            var commandText = "SELECT [id], [name] FROM villages WHERE district = @district ORDER BY [name]";
            using (var reader = await db.ExecuteReaderAsync(commandText, new SqlParameter("@district", districtId)))
            {
                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new Village()
                        {
                            Id = reader.GetInt64(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
            return list;
        }

        public void InitializeProvinceComboBox(ComboBox cb, List<Province> provinces, int selectedId)
        {
            for (int i = 0; i < provinces.Count; i++)
            {
                cb.Items.Add(provinces[i]);
                if (provinces[i].Id == selectedId) cb.SelectedIndex = i;
            }
        }
        public void InitializeCityComboBox(ComboBox cb, List<City> cities, int selectedId)
        {
            for (int i = 0; i < cities.Count; i++)
            {
                cb.Items.Add(cities[i]);
                if (cities[i].Id == selectedId) cb.SelectedIndex = i;
            }
        }
        public void InitializeDistrictComboBox(ComboBox cb, List<District> districts, int selectedId)
        {
            for (int i = 0; i < districts.Count; i++)
            {
                cb.Items.Add(districts[i]);
                if (districts[i].Id == selectedId) cb.SelectedIndex = i;
            }
        }
        public void InitializeVillageComboBox(ComboBox cb, List<Village> villages, Int64 selectedId)
        {
            for (int i = 0; i < villages.Count; i++)
            {
                cb.Items.Add(villages[i]);
                if (villages[i].Id == selectedId) cb.SelectedIndex = i;
            }
        }
    }
}
