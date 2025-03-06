using GymShopApi.Entities;
using GymShopApi.Services;
using GymShopApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymShopApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CampaignsController(IGenericService<Campaign> campaignService) : ControllerBase
{
    private readonly IGenericService<Campaign> _campaignService = campaignService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var campaigns = await _campaignService.GetAllAsync();

        if (!campaigns.Any())
        {
            return NotFound("No campaigns found");
        }
        return Ok(campaigns);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var campaign = await _campaignService.GetByIdAsync(id);

        if (campaign == null)
        {
            return NotFound($"No campaign found with ID: {id}");
        }

        return Ok(campaign);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Campaign campaign)
    {
        try
        {
            var newCampaign = await _campaignService.AddAsync(campaign);
            return CreatedAtAction(nameof(Get), new { id = newCampaign.Id }, newCampaign);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Campaign updatedCampaign)
    {
        try
        {
            var campaign = await _campaignService.Update(updatedCampaign, id);
            return Ok(campaign);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Campaign not found");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _campaignService.Delete(id);
            return Ok("Campaign deleted");
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Campaign not found");
        }
    }
}
