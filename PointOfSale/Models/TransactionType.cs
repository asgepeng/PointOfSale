using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public enum TransactionType
    {
        NotSet = 0,
        Purchase = 1,
        Sales = 2,
        AccountPayable = 3,
        AccountReceivable = 4
    }
}
