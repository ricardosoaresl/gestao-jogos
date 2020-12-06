using GestaoJogos.Application.ApplicationServices.Commands.Contracts;
using GestaoJogos.Application.ApplicationServices.Queries.Contracts;
using GestaoJogos.Application.ApplicationServices.ViewModel;
using GestaoJogos.Presentation.Api.Base;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GestaoJogos.Presentation.Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    public class JogoController : BaseController
    {
        private readonly IJogoCommandServiceApplication _commandService;
        private readonly IJogoQueryServiceApplication _queryService;

        public JogoController(
            IJogoCommandServiceApplication commandService,
            IJogoQueryServiceApplication queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_queryService.Index());
        }

        [HttpGet]
        [EnableQuery]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Get([FromODataUri] Guid key)
        {
            return Ok(_queryService.GetById(key));
        }

        [HttpPost]
        public IActionResult Post([FromBody] JogoViewModel jogoViewModel)
        {
            _commandService.Incluir(jogoViewModel);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] Guid id, [FromBody] JogoViewModel jogoViewModel)
        {
            _commandService.Update(id, jogoViewModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _commandService.Delete(id);
            return Ok();
        }
    }
}