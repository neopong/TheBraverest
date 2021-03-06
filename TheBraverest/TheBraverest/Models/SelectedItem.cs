﻿using System;
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
        /// <summary>
        /// True if is a jungle item. 
        /// This is used as a flag in the item set JSON to only show the block if our BraveChampion has selected smite as they were supposed to.
        /// </summary>
        public bool JungleItem { get; set; }
        /// <summary>
        /// List of Item Id's that this item builds from
        /// </summary>
        public List<int> From { get; set; }
    }
}
