using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToysService.toy.model;
using ToysService.toy.model.request;
using ToysService.toy.service;

namespace ToysService.toy.controller;

[ApiController]
[Route("toys")]
public class ToyController(IToyService toyService, ToyMapper toyMapper) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult FindAll()
    {
        return Ok(toyService.FindAll());
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public IActionResult FindById(String id)
    {
        var toy = toyService.FindById(Guid.Parse(id));
        if (toy == null)
        {
            return NotFound($"No toy found with ID {id}");
        }

        return Ok(toy);
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Save([FromBody] ToyCreationRequest toyCreationRequest)
    {
        try
        {
            var creationParams = toyMapper.MapToCreationParams(toyCreationRequest);
            var createdToy = toyService.Create(creationParams);
            return Ok(new { Toy = createdToy });
        }
        catch (Exception e)
        {
            return StatusCode(500, "An error occurred while creating toy.");
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Update(String id, [FromBody] ToyUpdateRequest toyUpdateRequest)
    {
        try
        {
            var updateParams = toyMapper.MapToUpdateParams(toyUpdateRequest);
            var updatedToy = toyService.UpdateById(Guid.Parse(id), updateParams);
            return Ok(new { Toy = updatedToy });
        }
        catch (Exception e)
        {
            return StatusCode(500, "An error occurred while updating toy.");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Delete(String id)
    {
        try
        {
            toyService.DeleteById(Guid.Parse(id));
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, "An error occurred while updating toy.");
        }
    }
}