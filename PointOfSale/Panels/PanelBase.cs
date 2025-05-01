using PointOfSale.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSale.Panels
{
    public class PanelBase : UserControl
    {
        public virtual async Task<bool> Save(IRepository repository) { return await Task.FromResult(true); }
    }
}
