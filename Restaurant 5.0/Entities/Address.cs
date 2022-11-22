using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant_5._0.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public virtual Restaurant Restaurant { get; set; }

    }
}
