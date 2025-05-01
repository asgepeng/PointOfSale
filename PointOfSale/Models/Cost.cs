using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class Cost
    {
        public string Name { get; set; } = "";
        public decimal Amount { get; set; } = 0;
    }
    public class CostCollection : List<Cost>
    {
        public decimal GetTotal()
        {
            decimal total = 0;
            foreach (var cost in this)
            {
                total += cost.Amount;
            }
            return total;
        }
    }
}
