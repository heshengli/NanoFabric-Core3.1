using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NanoFabric.Mediatr.Commands;
using SampleService.Kestrel.Application.CommandSide.Commands;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleService.Kestrel.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class EchoController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public EchoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEcho(
            [FromBody] EchoCommand command,
            [FromHeader(Name = "x-requestid")] string requestId
        )
        {
            if (!Guid.TryParse(requestId, out var guid))
            {
                return BadRequest();
            }

            var identifiedCommand = new IdentifiedCommand<EchoCommand, string>(
                command,
                guid
            );

            return Ok(await _mediator.Send(identifiedCommand));
        }
    }
}
