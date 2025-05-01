using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PointOfSale.Models
{
    public class ColumnFilter
    {
        public ColumnFilter(string columnName) { ColumnName = columnName; }
        public string ColumnName { get; }
        public string Value { get; set; } = "";
        public override string ToString()
        {
            return "[" + ColumnName + "] LIKE '%" + Value + "%'";
        }
    }

    public class ColumnFilterCollection : List<ColumnFilter>
    {
        public void Add(string columnName)
        {
            var filter = new ColumnFilter(columnName);
            base.Add(filter);
        }
        public void ApplyFilter(BindingSource bs, string textFilter)
        {
            if (textFilter.Trim() == "" || this.Count == 0)
            {
                bs.RemoveFilter();
                return;
            }
            textFilter = textFilter.Replace("'", "''");
            foreach (var filter in this)
            {
                filter.Value = textFilter;
            }
            bs.Filter = string.Join(" OR ", this);
        }
    }
}
