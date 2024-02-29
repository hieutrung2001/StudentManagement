using AutoMapper;
using Management.Models;

namespace Management.AutoMapper
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<ViewModels.ClassModel.CreateViewModel, Class>();
        }
    }
}
