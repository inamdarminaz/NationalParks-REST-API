﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Models.DTOs
{
    public class NationalParkDTO
    { 
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string  State { get; set; }

        public DateTime Created1 { get; set; }

        public DateTime Established { get; set; }
    }
}
