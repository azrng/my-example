using AutoMapper;
using NetByDocker.Domain;
using NetByDocker.Model.UserManage;

namespace NetByDocker.ProfileConfig;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AddUserVm, User>();
    }
}