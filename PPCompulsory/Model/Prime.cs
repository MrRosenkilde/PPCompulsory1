using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PPCompulsory.Model
{
    public class Prime
    {
        [Display(Name = "Minimum value")]
        public string MinimumValue { get; set; }
        [Display(Name = "Maximum value")]
        public string MaximumValue { get; set; }

       
    }
}
