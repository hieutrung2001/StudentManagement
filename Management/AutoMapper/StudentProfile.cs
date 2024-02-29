using AutoMapper;
using Management.Models;

namespace Management.AutoMapper
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, ViewModels.StudentModel.CreateViewModel>();
            CreateMap<ViewModels.StudentModel.CreateViewModel, Student>();

            CreateMap<ViewModels.StudentModel.EditViewModel, Student>();
        }
    }
}
