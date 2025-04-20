using System;
using System.ComponentModel.DataAnnotations;

namespace Web_1.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
        public string TK { get; set; }
        public string Mk { get; set; }

        [Required]
        public HocVien hocVien { get; set; }

        public bool ValidatePassword(string mk)
        {
            return Mk == mk;
        }
    }
}