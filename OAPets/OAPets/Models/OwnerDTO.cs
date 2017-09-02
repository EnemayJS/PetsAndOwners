using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAPets.Models
{
    public class OwnerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PetsCount { get; set; }
    }
}