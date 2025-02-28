using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services;
public class RoleService(IUnitOfWork unitOfWork) : GenericService<Role>(unitOfWork)
{
}
