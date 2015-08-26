using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBraverest.Models
{
    /// <summary>
    /// Standard information for a champion skill
    /// </summary>
    public class SelectedSkill : SelectedValue
    {
        /// <summary>
        /// The default letter on the keyboard the skill is bound to
        /// </summary>
        public string Letter { get; set; }
    }
}
