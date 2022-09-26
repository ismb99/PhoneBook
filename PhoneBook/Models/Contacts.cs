﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class Contacts
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string? Emaill { get; set; }
    }
}
