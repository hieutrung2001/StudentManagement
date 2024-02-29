using Management.Models;
using System.ComponentModel.DataAnnotations;

namespace Management.ViewModels.StudentModel
{
    public class CreateViewModel : Student
    {
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
