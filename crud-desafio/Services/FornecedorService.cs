using AutoMapper;
using crud_desafio.Helpers;
using crud_desafio.Entities;
using crud_desafio.Models.Fornecedores;
using Newtonsoft.Json;
using crud_desafio.Entities.CepInfo;

namespace crud_desafio.Services
{
    public class FornecedorService : IFornecedorService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public FornecedorService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Fornecedor> GetAll()
        {
            return _context.Fornecedores;
        }

        public Fornecedor GetById(int id)
        {
            return getFornecedor(id);
        }

        public void Create(CreateRequestFornecedor model)
        {
            // Verificar se o CNPJ ou CPF é um valor único para esta empresa
            if (_context.Fornecedores.Any(x => (x.Cnpj == model.Cnpj || (model.Cpf != null && x.Cpf == model.Cpf)) && x.EmpresaId == model.EmpresaId))
            {
                throw new AppException($"Um fornecedor com o {(model.Cnpj != null ? "CNPJ" : "CPF")} '{(model.Cnpj != null ? model.Cnpj : model.Cpf)}' já foi cadastrado para esta empresa");
            }

            // Verificar se a empresa associada ao fornecedor é do Paraná
            var empresa = _context.Empresas.FirstOrDefault(e => e.Id == model.EmpresaId && e.Estado == "PR");

            // Verificar se é do PR e se o fornecedor é menor de idade
            if (empresa != null && IsFornecedorMinor(model.DataNascimento))
            {
                throw new AppException("A empresa associada é do Paraná e o fornecedor é menor de idade, o cadastro não pode ser realizado");
            }

            // Mapear o 'model' para um novo objeto fornecedor
            var fornecedor = _mapper.Map<Fornecedor>(model);

            // Salvar fornecedor
            _context.Fornecedores.Add(fornecedor);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequestFornecedor model)
        {
            var fornecedor = getFornecedor(id);

            // Validando se o Cnpj é um valor único
            if (model.Cnpj != fornecedor.Cnpj && _context.Fornecedores.Any(x => x.Cnpj == model.Cnpj))
                throw new AppException("Um fornecedor com o Cnpj '" + model.Cnpj + "' já foi cadastrado");

            // Validando se o Cpf é um valor único
            if (model.Cpf != fornecedor.Cpf && _context.Fornecedores.Any(x => x.Cpf == model.Cpf))
                throw new AppException("Um fornecedor com o Cpf '" + model.Cpf + "' já foi cadastrado");

            // copiando o 'model' para 'fornecedor' e atualizando
            _mapper.Map(model, fornecedor);
            _context.Fornecedores.Update(fornecedor);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var fornecedor = getFornecedor(id);
            _context.Fornecedores.Remove(fornecedor);
            _context.SaveChanges();
        }


        // métodos auxiliares

        private Fornecedor getFornecedor(int id)
        {
            var fornecedor = _context.Fornecedores.Find(id);

            if (fornecedor == null)
            {
                // Tratando a exceção KeyNotFoundException
                try
                {
                    throw new KeyNotFoundException("Fornecedor não encontrado");
                }
                catch (KeyNotFoundException e)
                {

                    Console.WriteLine($"Exceção: {e.Message}");
                    return null;
                }
            }

            return fornecedor;
        }

        //Retornando a busca por nome 
        public IQueryable<Fornecedor> GetFornecedoresByName(string nome)
        {
            var query = _context.Fornecedores.AsQueryable();

            //Aplicando filtro de nome, se fornecido
            if (!string.IsNullOrWhiteSpace(nome))
            {
                query = query.Where(f => f.Nome.Contains(nome));
            }
            return query;
        }

        //Retornando a busca por Cnpj/Cpf
        public IQueryable<Fornecedor> GetFornecedoresByCnpjouCpf(string cnpjoucpf)
        {
            var query = _context.Fornecedores.AsQueryable();

            //Aplicando filtro de CNPJ/CPF, se fornecido
            if (!string.IsNullOrWhiteSpace(cnpjoucpf))
            {
                query = query.Where(f => f.Cnpj.Contains(cnpjoucpf) || f.Cpf.Contains(cnpjoucpf));
            }
            return query;
        }


        //Implementando a validação de CEP
        public CepInfo ValidateCep (string cep)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync($"http://viacep.com.br/ws/{cep}/json/").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;

                    if (!string.IsNullOrWhiteSpace(responseData))
                    {
                        var cepInfo = JsonConvert.DeserializeObject<CepInfo>(responseData);

                        // Verifique se as propriedades essenciais são preenchidas
                        if (!string.IsNullOrWhiteSpace(cepInfo.cep) &&
                            !string.IsNullOrWhiteSpace(cepInfo.localidade) &&
                            !string.IsNullOrWhiteSpace(cepInfo.logradouro))
                        {
                            return cepInfo; // Retornando as informações do CEP
                        }
                    }
                }

                return null; // Retorna null se a validação falhar ou o CEP não for encontrado
            }
        }


        //Verificando se é menor de idade
        private bool IsFornecedorMinor(DateTime? dataNascimento)
        {
            // Se a data de nascimento não estiver definida, não é possível determinar a idade
            if (!dataNascimento.HasValue)
            {
                return false; // Ou qualquer outra ação apropriada
            }

            // Definindo a idade mínima
            var idadeMinima = 18;

            // Calculando a idade do fornecedor
            var idade = DateTime.Now.Year - dataNascimento.Value.Year;

            // Verificando se o fornecedor é menor de idade
            return idade < idadeMinima;
        }
    }
}