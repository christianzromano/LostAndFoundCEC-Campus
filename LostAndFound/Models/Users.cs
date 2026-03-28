using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Student ID is required.")]
        public string StudentId { get; set; } = string.Empty;
        [Required(ErrorMessage = "FullName field is required.")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Course field is required.")]

        public string Course { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;
        public string? ContactNum { get; set; }
        public string? SocialMedContact { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
