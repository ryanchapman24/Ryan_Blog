﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ryan_Blog.Models
{
    public class Contacts
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public DateTime MessageSent { get; set; }
    }
}