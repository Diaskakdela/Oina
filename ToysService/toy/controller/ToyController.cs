using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToysService.core.filter;
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
    [ValidateGuid("id")]
    public IActionResult FindById(string id)
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
    [ValidateGuid("id")]
    public IActionResult Update(string id, [FromBody] ToyUpdateRequest toyUpdateRequest)
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
    [ValidateGuid("id")]
    public IActionResult Delete(string id)
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