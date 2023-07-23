using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;
        public TarefaRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext)
        {
            _dbContext = sistemaTarefasDBContext;
        }
        public async Task<TarefaModel> BuscarPorId(int id)
        {
            return await _dbContext.Tarefas
                .Include(t=> t.Usuario)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<TarefaModel>> BuscarTodasTarefas()
        {
            return await _dbContext.Tarefas
                .Include (t=> t.Usuario)
                .ToListAsync();
        }
        public async Task<TarefaModel> Adicionar(TarefaModel Tarefa)
        {
            await _dbContext.Tarefas.AddAsync(Tarefa);
            await _dbContext.SaveChangesAsync();
            return Tarefa;
        }
        public async Task<TarefaModel> Atualizar(TarefaModel Tarefa, int id)
        {
            TarefaModel TarefaPorId = await BuscarPorId(id);
            if (TarefaPorId == null)
            {
                throw new Exception($"Tarefa para o Id: {id} não foi encontrado no banco de dados.");
            }
            TarefaPorId.Nome = Tarefa.Nome;
            TarefaPorId.Descricao = Tarefa.Descricao;
            TarefaPorId.Status = Tarefa.Status;
            TarefaPorId.UsuarioId = Tarefa.UsuarioId;

            _dbContext.Tarefas.Update(TarefaPorId);
            await _dbContext.SaveChangesAsync();

            return TarefaPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            TarefaModel TarefaPorId = await BuscarPorId(id);
            if (TarefaPorId == null)
            {
                throw new Exception($"Tarefa para o Id: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Tarefas.Remove(TarefaPorId);
            await _dbContext.SaveChangesAsync();
            return true;
        }


    }
}
