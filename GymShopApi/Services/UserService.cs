using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services;
public class UserService(IUnitOfWork unitOfWork) : GenericService<User>(unitOfWork)
{
    // TODO
    public override Task<User> AddAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public override Task<User> Update(int id, User entity)
    {
        throw new NotImplementedException();
    }
}
