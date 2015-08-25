using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBraverest.Models
{
    /// <summary>
    /// An overview how many mastery points go into each mastery tree
    /// </summary>
    public class MasterySummary
    {
        /// <summary>
        /// The amount of points to be placed in the Offense Mastery tree
        /// </summary>
        public int Offense { get; set; }
        /// <summary>
        /// The amount of points to be placed in the Defense Mastery tree
        /// </summary>
        public int Defense { get; set; }
        /// <summary>
        /// The amount of points to be placed in the Utility Mastery tree
        /// </summary>
        public int Utility { get; set; }
    }
}
