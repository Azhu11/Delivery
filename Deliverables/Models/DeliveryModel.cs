using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deliverables.Models
{
    public class DeliveryModel
    {
        public string IDNumber { get; set; }
        public string FirstName{ get; set; }
        public string Surname{ get; set; }
        public string DOB { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

    }
}