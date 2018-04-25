using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_Sharp_Project.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // public List<Ideas> Ideas { get; set; }

        // public List<Likes> Likes { get; set; }
 
        // public User()
        // {
        //     Likes = new List<Likes>();
        //     Ideas = new List<Ideas>();
        // }
    }
}