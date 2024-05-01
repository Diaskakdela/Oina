using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToysService.core.filter;
using ToysService.toyreview.model;
using ToysService.toyreview.service;

namespace ToysService.toyreview.controller;

[ApiController]
[Route("toys")]
public class ToyReviewController(IToyReviewService toyReviewService) : ControllerBase
{
    [HttpGet("{id}/review")]
    [AllowAnonymous]
    [ValidateGuid("id")]
    public IActionResult FindAll(string id)
    {
        return Ok(toyReviewService.FindAllByToyId(Guid.Parse(id)));
    }

    [HttpPost("{id}/review")]
    [Authorize(Roles = "USER")]
    [ValidateGuid("id")]
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