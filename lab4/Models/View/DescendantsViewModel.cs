using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.ComponentModel.DataAnnotations;

namespace lab4.Models.View
{
    public class DescendantsViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Descendant's First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Descendant's Last Name")]
        public string LastName { get; set; }
        public int UserId { get; set; }
    }
}