using Application.Features.Githubs.Commands.CreateGithub;
using Application.Features.Githubs.Commands.DeleteGithub;
using Application.Features.Githubs.Commands.UpdateGithub;
using Application.Features.Githubs.Dtos;
using Application.Features.Githubs.Models;
using Application.Features.Githubs.Queries.GetListGithub;
using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries.GetListTechnology;
using Core.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListGithubQuery getListGithubQuery = new GetListGithubQuery { PageRequest = pageRequest };
            GithubListModel result = await Mediator.Send(getListGithubQuery);
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateGithubCommand createGithubCommand)
        {
            CreatedGithubDto result = await Mediator.Send(createGithubCommand);
            return Created("", result);
        }

        [HttpPost("Delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteGithubCommand deleteGithubCommand)
        {
            DeletedGithubDto result = await Mediator.Send(deleteGithubCommand);
            return Created("", result);
        }

        [HttpPost("Update")]
        public async Task<ActionResult> Update([FromBody] UpdateGithubCommand updateGithubCommand, CancellationToken cancellationToken)
        {
            var updatedGithub = await Mediator.Send(updateGithubCommand);
            return Ok(updatedGithub);
        }


    }
}
