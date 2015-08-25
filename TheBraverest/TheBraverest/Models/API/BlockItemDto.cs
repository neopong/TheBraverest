using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBraverest.Models.API
{
    /// <summary>
    /// Represents an item that is displayed in a block of an item set.
    /// </summary>
    public class BlockItemDto
    {
        /// <summary>
        /// The Id of the item to be displayed
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The number of items to buy
        /// </summary>
        public int Count { get; set; }
    }
}