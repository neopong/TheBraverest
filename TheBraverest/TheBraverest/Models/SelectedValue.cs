using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBraverest.Models
{
    /// <summary>
    /// Standard information for data
    /// </summary>
    public class SelectedValue
    {
        /// <summary>
        /// The Id of the data element
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Name of the data element
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The URL of the image for the visual representation of the data element
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
