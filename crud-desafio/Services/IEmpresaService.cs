namespace crud_desafio.Services;

using crud_desafio.Entities;
using crud_desafio.Models.Empresas;

public interface IEmpresaService
{
    IEnumerable<Empresa> GetAll();
    Empresa GetById(int id);
    void Create(CreateRequestEmpresa model);
    void Update(int id, UpdateRequestEmpresa model);
    void Delete(int id);
}