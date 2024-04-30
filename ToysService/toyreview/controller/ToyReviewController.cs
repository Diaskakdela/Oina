using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToysService.toyreview.model;
using ToysService.toyreview.service;

namespace ToysService.toyreview.controller;

[ApiController]
[Route("toys")]
public class ToyReviewController(IToyReviewService toyReviewService) : ControllerBase
{
    [HttpGet("{id}/review")]
    [AllowAnonymous]
    public IActionResult FindAll(string id)
    {
        return Ok(toyReviewService.FindAllByToyId(Guid.Parse(id)));
    }

    [HttpPost("{id}/review")]
    [Authorize(Roles = "USER")]
    public IActionResult Create(string id, [FromBody] ToyReviewCreationRequest toyReviewCreationRequest)
    {
        try
        {
            return Ok(toyReviewService.Create(new ToyReviewCreationParams(toyReviewCreationRequest.Review,
                Guid.Parse(id))));
        }
        catch (Exception e)
        {
            return StatusCode(500, "An error occurred while creating review for toy.");
        }
    }
}