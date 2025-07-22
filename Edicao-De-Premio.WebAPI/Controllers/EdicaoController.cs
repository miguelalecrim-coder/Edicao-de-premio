


using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/edicao")]
[ApiController]

public class EdicaoController : ControllerBase
{
    private readonly IEdicaoService _edicaoService;
    private readonly IEdicaoTemporaryService _edicaoTemporaryService;


    public EdicaoController(IEdicaoService edicaoService, IEdicaoTemporaryService edicaoTemporaryService)
    {
        _edicaoService = edicaoService;
        _edicaoTemporaryService = edicaoTemporaryService;
    }

    [HttpPost]
    public async Task<ActionResult<CreatedEdicaoDTO>> Create([FromBody] CreateEdicaoDTO createEdicaoDTO)
    {
        var createEdicaoDto = new CreateEdicaoDTO(createEdicaoDTO.UserId, createEdicaoDTO.Date, createEdicaoDTO.TipoId);
        var edicaoCreated = await _edicaoService.Create(createEdicaoDto);

        return edicaoCreated.ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CreatedEdicaoDTO>>> GetAll()
    {
        var result = await _edicaoService.GetAllAsync();
        return Ok(result);
    }

    

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<CreatedEdicaoDTO>>> GetByUser(Guid userId)
    {
        var result = await _edicaoService.GetByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<CreatedEdicaoDTO>>> Search([FromQuery] Guid? userId,
    [FromQuery] DateOnly? date,
    [FromQuery] Guid? tipoId)
    {
        var result = await _edicaoService.SearchAsync(userId, date, tipoId);
        return Ok(result);
    }

    [HttpPost("with-tipo")]
    public async Task<IActionResult> CreateWithTipo([FromBody] CreatedEdicaoTipoDTO dto)
    {
        await _edicaoTemporaryService.PublishCreateReqEdicaoSaga(dto);
        return Accepted();
    }

    


}