using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crud_desafio.Models.Fornecedores;

public class UpdateRequestFornecedor
{
    public string Cnpj { get; set; }

    public string Cpf { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }

    public string Cep { get; set; }

    public int EmpresaId { get; set; }

    //Caso o fornecedor seja pessoa física (Campos opcionais)
    public string Rg { get; set; }
    public DateTime DataNascimento { get; set; }


    private string replaceEmptyWithNull(string value)
    {
        return string.IsNullOrEmpty(value) ? null : value;
    }
}