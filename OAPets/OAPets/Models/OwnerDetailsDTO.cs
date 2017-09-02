using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAPets.Models
{
    public class OwnerDetailsDTO
    {
        public string OwnerName { get; set; }
        public OwnersPetsDTO[] Pets { get; set; }
        public int Count { get; set; }
    }
}