using Management.Models;
using System.ComponentModel.DataAnnotations;

namespace Management.ViewModels.ClassModel
{
    public class EditViewModel : Class
    {
        [DataType(DataType.DateTime)]
        public DateTime LastUpdated { get; set; }
    }
}
