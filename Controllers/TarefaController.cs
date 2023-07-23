using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepositorio _tarefaRepositorio;
        public TarefaController(ITarefaRepositorio TarefaRepositorio)
        {
            _tarefaRepositorio = TarefaRepositorio;
        }
        [HttpGet]
        public async Task<ActionResult<List<TarefaModel>>> BuscarTodasTarefas()
        {
            List<TarefaModel> Tarefas = await _tarefaRepositorio.BuscarTodasTarefas();
            return Ok(Tarefas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<TarefaModel>>> BuscarPorId(int id)
        {
            TarefaModel Tarefa = await _tarefaRepositorio.BuscarPorId(id);
            return Ok(Tarefa);
        }

        [HttpPost]
        public async Task<ActionResult<TarefaModel>> Cadastrar([FromBody] TarefaModel TarefaModel)
        {
            TarefaModel Tarefa = await _tarefaRepositorio.Adicionar(TarefaModel);
            return Ok(Tarefa);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TarefaModel>> Atualizar([FromBody] TarefaModel TarefaModel, int id)
        {
            TarefaModel.Id = id;
            TarefaModel Tarefa = await _tarefaRepositorio.Atualizar(TarefaModel, id);
            return Ok(Tarefa);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TarefaModel>> Apagar(int id)
        {
            bool Tarefa = await _tarefaRepositorio.Apagar(id);
            return Ok(Tarefa);
        }
    }
}
