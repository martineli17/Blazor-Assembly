﻿using Dominio.Entidades;
using Dominio.Interfaces.Repositorio;
using Dominio.Interfaces.Service;
using FluentValidation;
using Service.Validators;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BaseService<TEntidade> : IBaseService<TEntidade> where TEntidade : Base
    {
        protected readonly IBaseRepositorio<TEntidade> Repositorio;
        protected readonly IValidacaoFluent Validator;

        public BaseService(IBaseRepositorio<TEntidade> repositorio, IValidacaoFluent validator)
        {
            Repositorio = repositorio;
            Validator = validator;
        }
        public async Task<TEntidade> AddAsync(TEntidade entidade, AbstractValidator<TEntidade> validation)
        {
            if (Validator.Executar(validation, entidade))
            {
                await Repositorio.AddAsync(entidade);
            }
            return entidade;
        }

        public async Task<IQueryable<TEntidade>> GetAsync(Func<TEntidade, bool> query = null) => await Repositorio.GetAsync(query);
        

        public async Task<TEntidade> GetByIdAsync(Guid id) => await Repositorio.GetByIdAsync(id);

        public async Task<bool> RemoveAsync(Guid id)
        {
            await Repositorio.RemoveAsync(id);
            return true;
        }

        public async Task<TEntidade> UpdateAsync(TEntidade entidade, AbstractValidator<TEntidade> validation)
        {
            if (Validator.Executar(validation, entidade))
            {
                await Repositorio.UpdateAsync(entidade);
            }
            return entidade;
        }
    }
}
