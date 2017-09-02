using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAPets.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Owner Owner { get; set; }
        public int OwnerId { get; set; }
    }
}