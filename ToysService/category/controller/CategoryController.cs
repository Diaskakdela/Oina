using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToysService.category.model;
using ToysService.category.service;

namespace ToysService.category.controller;

[ApiController]
[Route("category")]
public class CategoryController(ICategoryService categoryService, CategoryMapper categoryMapper) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult FindAll(string id) => Ok(categoryService.FindAll().Select(categoryMapper.MapToDto));

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Create([FromBody] CategoryCreationRequest categoryCreationRequest)
    {
        try
        {
            var category = categoryService.Create(categoryCreationRequest.CategoryName);
            return Ok(categoryMapper.MapToDto(category));
        }
        catch (Exception e)
        {
            return StatusCode(500, "An error occurred while creating category.");
        }
    }
}