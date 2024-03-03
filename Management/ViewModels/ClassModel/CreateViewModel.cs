using Management.Models;
using System.ComponentModel.DataAnnotations;

namespace Management.ViewModels.ClassModel
{
    public class CreateViewModel : Class
    {
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; } = DateTime.Now;
        public List<int> StudentSelectList { get; set; }
    }
}
