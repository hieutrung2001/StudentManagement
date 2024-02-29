using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Management.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date of birth")]
        public DateTime Dob { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public int PhoneNumber { get; set; }

        public string Address { get; set; }

        public byte State { get; set; } = 1;

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastUpdated { get; set; }

        public List<Class>? Classes { get; set; }
    }
}
