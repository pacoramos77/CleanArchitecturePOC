using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Core.ToDoListAggregate.Commands;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    private readonly ISender _sender;

    public ToDoController(ISender sender) => _sender = sender;

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("get");
    }

    [HttpPost]
    public async Task<IActionResult> CreateToDoList([FromBody] CreateToDoListRequest request)
    {
        var response = await _sender.Send(request);
        return Ok(response);
    }
}
