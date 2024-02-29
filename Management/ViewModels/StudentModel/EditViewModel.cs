using Management.Models;
using System.ComponentModel.DataAnnotations;

namespace Management.ViewModels.StudentModel
{
    public class EditViewModel : Student
    {
        [DataType(DataType.DateTime)]
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
