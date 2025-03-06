using GymShopApi.Entities;
using GymShopApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymShopApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CampaignProductsController(IGenericService<CampaignProduct> campaignProductService) : ControllerBase
{
    private readonly IGenericService<CampaignProduct> _campaignProductService = campaignProductService;
}
