using Microsoft.AspNetCore.Mvc;
using TravelAgency.Exceptions;
using TravelAgency.Models.DTOs;
using TravelAgency.Services;

namespace TravelAgency.Controllers;

[ApiController]
[Route("api")]
public class TravelAgencyController(IDbService dbService) : ControllerBase
{
    /// <summary>
    /// zwraca informacje o wszystkich wycieczkach
    /// </summary>
    
    [HttpGet("trips")]
    public async Task<IActionResult> GetAllTrips()
    {
        return Ok(await dbService.GetTripsAsync());
    }
    /// <summary>
    /// zwraca wszystkie informacje o wycieczkach dla klienta o podanym id
    /// </summary>
    /// <param name="id">Id kienta</param>
    [HttpGet("clients/{id}/trips")]
    public async Task<IActionResult> GetTripsByClientId(
        [FromRoute] int id
    )
    {
        try
        {
            return Ok(await dbService.GetTripsByClientIdAsync(id));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    /// <summary>
    /// Tworzy nowego klienta
    /// </summary>
    /// <param name="body">Dane do utworzenia klienta</param>
    /// <returns>Informacje o nowym kliencie</returns>
    [HttpPost("clients")]
    public async Task<IActionResult> CreateClient(
        [FromBody] ClientCreateDTO body
    )
    {
        var client = await dbService.CreateClientAsync(body);
        return Created($"clients/{client.IdClient}", client);
    }
    
    /// <summary>
    /// Rejestruje klienta na wycieczkę o podanym tripId
    /// </summary>
    /// <param name="id">Id klienta</param>
    /// <param name="tripId">Id wycieczki</param>
    /// <returns>kod wykonania zadania</returns>
    [HttpPut("clients/{id}/trips/{tripId}")]
    public async Task<IActionResult> RegisterClientTrip(
        [FromRoute] int id,
        [FromRoute] int tripId
    )
    {
        try
        {
            await dbService.RegisterClientTripAsync(id, tripId);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
    }
    /// <summary>
    /// Usuwa klienta z wycieczki o podanym id klienta i tripId wycieczki
    /// </summary>
    /// <param name="id">Id klienta</param>
    /// <param name="tripId">Id wycieczki</param>
    /// <returns>kod wykonania zadania</returns>
    [HttpDelete("clients/{id}/trips/{tripId}")]
    public async Task<IActionResult> DeleteClientTrip(
        [FromRoute] int id,
        [FromRoute] int tripId
    )
    {
        try
        {
            await dbService.DeleteClientTripAsync(id, tripId);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
}