using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JudgmentWriter1.Models
{
    public class Judgment
    {
        [Key]
        public int Id { get; set; } 
        public string Court { get; set; }
        public string Judge { get; set; }
        public string SideA { get; set; }
        public string SideB { get; set; }
        public string facts { get; set; }
        public string Decision { get; set; }
        public string Thejudgment { get; set; }
        public string Notes { get; set; }

        public override string ToString()
        {
            return $"Court:{Court}" +
                $"Judge:{Judge}" +
                $"SideA:{SideA}";
        }
    }
}