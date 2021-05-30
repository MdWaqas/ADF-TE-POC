using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADF_TE_POC.DTOs
{
    public class TokenRequest
    {
        [Required(ErrorMessage = "The {0} field is mandatory")]
        [EmailAddress(ErrorMessage = "The field {0} is in an invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        public string Password { get; set; }
    }
}
