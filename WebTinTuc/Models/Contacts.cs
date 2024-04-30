using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTinTuc.Models
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
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Email { get; set; }
        //public string Message { get; set; }
    }
}