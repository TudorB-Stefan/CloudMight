using CloudMight.Core.Interfaces.Repositories;
using CloudMight.Core.Interfaces.Services;

namespace CloudMight.Infrastructure.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    
}