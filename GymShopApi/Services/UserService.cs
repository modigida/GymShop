using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services;
public class UserService(IUnitOfWork unitOfWork) : GenericService<User>(unitOfWork)
{
}
