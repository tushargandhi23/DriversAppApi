using System;
using Microsoft.AspNetCore.Mvc;
using DriversAppApi.Services;
using DriversAppApi.Models;

namespace DriversAppApi.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class DriversController : ControllerBase
{
    private readonly DriverService _driverService;

    public DriversController(DriverService driverService) => _driverService = driverService;

    [HttpGet(template: "{id:length(24)}")]
    public async Task<ActionResult> Get(string id)
    {
        var existingDriver = await _driverService.GetAsync(id);

        if (existingDriver is null)
            return NotFound();

        return Ok(existingDriver);
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var allDrivers = await _driverService.GetAsync();

        if( allDrivers.Any())
            return Ok(allDrivers);

        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Post(Driver driver)
    {
        await _driverService.CreateAsync(driver);
        return CreatedAtAction(nameof(Get), routeValues: new { id = driver.Id }, value: driver);
    }

    [HttpPut(template:"{id:length(24)}")]
    public async Task<ActionResult> Update(string id, Driver driver)
    {
        var existingDriver = await _driverService.GetAsync(id);
        if (existingDriver is null)
            return BadRequest();

        driver.Id = existingDriver.Id;

        await _driverService.UpdateAsync(driver);
        return NoContent();
    }

    [HttpDelete(template:"{id:length(24)}")]
    public async Task<ActionResult> Delete(string id)
    {
        var existingDriver = await _driverService.GetAsync(id);

        if (existingDriver is null)
            return NotFound();

        await _driverService.RemoveAsync(id);

        return NoContent();
    }
}
