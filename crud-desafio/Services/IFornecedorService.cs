namespace crud_desafio.Services;

using crud_desafio.Entities;
using crud_desafio.Entities.CepInfo;
using crud_desafio.Models.Fornecedores;

public interface IFornecedorService
{
    IEnumerable<Fornecedor> GetAll();
    Fornecedor GetById(int id);
    IQueryable<Fornecedor> GetFornecedoresByName(String nome);
    IQueryable<Fornecedor>GetFornecedoresByCnpjouCpf(String cnpjoucpf);
    CepInfo ValidateCep(string cep);
    void Create(CreateRequestFornecedor model);
    void Update(int id, UpdateRequestFornecedor model);
    void Delete(int id);
}