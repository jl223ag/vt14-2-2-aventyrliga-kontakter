using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AventyrligaKontakter.Model
{
    public class Contact
    {
        // behövs ingen validering på pkn
        public int ContactID { get; set; }

        [StringLength(50, ErrorMessage = "För många tecken. Max 50!")]
        [Required(ErrorMessage="Det måste finnas ett förnamn")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "För många tecken. Max 50!")]
        [Required(ErrorMessage="Det måste finnas ett efternamn")]
        public string LastName { get; set; }

        [Required(ErrorMessage="Det måste finnas en email")]
        [StringLength(50, ErrorMessage="För många tecken. Max 50!")]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}