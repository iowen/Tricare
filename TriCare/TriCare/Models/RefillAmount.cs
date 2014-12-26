using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Models
{
    public class RefillAmount
    {
        [PrimaryKey]
        public int RefillAmountId { get; set; }
        public int Amount { get; set; }
    }
}
