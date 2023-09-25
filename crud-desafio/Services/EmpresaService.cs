using AutoMapper;
using crud_desafio.Helpers;
using crud_desafio.Entities;
using crud_desafio.Models.Empresas;
using Microsoft.AspNetCore.Mvc;

namespace crud_desafio.Services
{
    public class EmpresaService : IEmpresaService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public EmpresaService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Empresa> GetAll()
        {
            return _context.Empresas;
        }

        public Empresa GetById(int id)
        {
            return getEmpresa(id);
        }

        public void Create(CreateRequestEmpresa model)
        {
            // Verificando se o Cnpj é um valor único
            if (_context.Empresas.Any(x => x.Cnpj == model.Cnpj))
                throw new AppException("Uma empresa com o Cnpj '" + model.Cnpj + "' já foi cadastrada");

            // mapeando o 'model' para um novo objeto empresa
            var empresa = _mapper.Map<Empresa>(model);

            //salvando empresa
            _context.Empresas.Add(empresa);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequestEmpresa model)
        {
            var empresa = getEmpresa(id);

            // validate
            if (model.Cnpj != empresa.Cnpj && _context.Empresas.Any(x => x.Cnpj == model.Cnpj))
                throw new AppException("Uma empresa com o Cnpj '" + model.Cnpj + "' já foi cadastrada");

            // copiando o 'model' para 'empresa' e atualizando
            _mapper.Map(model, empresa);
            _context.Empresas.Update(empresa);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var empresa = getEmpresa(id);
            _context.Empresas.Remove(empresa);
            _context.SaveChanges();
        }

        // métodos auxiliares

        private Empresa getEmpresa(int id)
        {
            var empresa = _context.Empresas.Find(id);

            if (empresa == null)
            {
                // Tratando a exceção KeyNotFoundException
                try
                {
                    throw new KeyNotFoundException("Empresa não encontrada");
                }
                catch (KeyNotFoundException e)
                {
                    
                    Console.WriteLine($"Exceção: {e.Message}");
                    return null;
                }
            }

            return empresa;
        }
    }
}