using System.ComponentModel.DataAnnotations;

namespace TheWorld.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Phonenumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}