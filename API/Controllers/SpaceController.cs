using Application.DTOs;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace API.Controllers;

[ApiController]
[Route("api/v1/parking-spaces")]
public class SpaceController : ControllerBase
{
    private readonly CreateParkingSpaceUseCase _createUseCase;
    private readonly GetAllSpacesUseCase _getAllSpacesUseCase;
    private readonly GetSpaceByIdUseCase _getSpaceByIdUseCase;
    private readonly UpdateSpaceUseCase _updateSpaceUseCase;
    private readonly OccupySpaceUseCase _occupeSpaceUseCase;
    private readonly ReleaseSpaceUseCase _releaseSpaceUseCase;

    public SpaceController(CreateParkingSpaceUseCase createUseCase, GetAllSpacesUseCase getAllSpacesUseCase, GetSpaceByIdUseCase getSpaceByIdUseCase,
        UpdateSpaceUseCase updateSpaceUseCase, OccupySpaceUseCase occupySpaceUseCase, ReleaseSpaceUseCase releaseSpaceUseCase)
    {
        _createUseCase = createUseCase;
        _getAllSpacesUseCase = getAllSpacesUseCase;
        _getSpaceByIdUseCase = getSpaceByIdUseCase;
        _updateSpaceUseCase = updateSpaceUseCase;
        _occupeSpaceUseCase = occupySpaceUseCase;
        _releaseSpaceUseCase = releaseSpaceUseCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ParkingSpaceResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateParkingSpaceRequest request)
    {
        try
        {
            var response = await _createUseCase.ExecuteAsync(request);

            return CreatedAtAction(nameof(GetSpaceById), new { id = response.Id }, response);
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ParkingSpaceResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSpaces()
    {
        var response = await _getAllSpacesUseCase.ExecuteAsync();
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ParkingSpaceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSpaceById(Guid id)
    {
        var response = await _getSpaceByIdUseCase.ExecuteAsync(id);

        if (response == null)
            return NotFound(new { error = $"Vaga não encontrada com ID: {id}." });

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ParkingSpaceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSpaceRequest request)
    {
        try
        {
            var response = await _updateSpaceUseCase.ExecuteAsync(id, request);

            if (response is null)
                return NotFound(new { error = $"Vaga com ID '{id}' não encontrada." });

            return Ok(response);
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPatch("{id:guid}/occupy")]
    [ProducesResponseType(typeof(ParkingSpaceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Occupy(Guid id, [FromBody] OccupySpaceRequest request)
    {
        try
        {
            var response = await _occupeSpaceUseCase.ExecuteAsync(id, request);

            if (response is null)
                return NotFound(new { error = $"Vaga com ID '{id}' não encontrada." });

            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPatch("{id:guid}/release")]
    [ProducesResponseType(typeof(ParkingSpaceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Release(Guid id)
    {
        var response = await _releaseSpaceUseCase.ExecuteAsync(id);

        if (response is null)
            return NotFound(new { error = $"Vaga com ID '{id}' não encontrada." });

        return Ok(response);
    }
}
