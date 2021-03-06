﻿using Crosscuting.Notificacao;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorio;
using Microsoft.EntityFrameworkCore;
using Repositorio.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class AlunoDisciplinaRepositorio : BaseRepositorio<AlunoDisciplina>, IAlunoDisciplinaRepositorio
    {
        public AlunoDisciplinaRepositorio(Context context, INotificador notificador) : base(context, notificador)
        {

        }

        public async override Task<IQueryable<AlunoDisciplina>> GetAsync(Func<AlunoDisciplina, bool> query = null)
        {
            await Task.Yield();
            return query is null ?
              Context.AlunoDisciplina.Include(x => x.Aluno).Include(x => x.Disciplina).ThenInclude(x => x.Curso).AsQueryable() :
              Context.AlunoDisciplina.Include(x => x.Aluno).Include(x => x.Disciplina).ThenInclude(x => x.Curso).Where(query).AsQueryable();
        }
    }
}
