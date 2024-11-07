using System.ComponentModel.DataAnnotations;

namespace CommerceApp.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = string.Empty;

        public string? ContactNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public DateTime LastCheckupDate { get; set; } = DateTime.Now;

        [Required]
        public string VitalSigns { get; set; } = "Not recorded";

        public int Age { get; set; }

        public bool IsCheckupOverdue { get; set; }
        // Add this new property
        public DateTime? LastReminderSent { get; set; }
    }
}
