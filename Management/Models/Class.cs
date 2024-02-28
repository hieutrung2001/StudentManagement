using System.ComponentModel.DataAnnotations;

namespace Management.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public byte State { get; set; } = 1;

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastUpdated { get; set; }

        public List<Student> Students { get; set; }
    }
}
