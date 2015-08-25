using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBraverest.Models
{
    /// <summary>
    /// Standard information for an item that can be purchased
    /// </summary>
    public class SelectedItem : SelectedValue
    {
        /// <summary>
        /// The total cost of the item
        /// </summary>
        public int Cost { get; set; }
    }
}
