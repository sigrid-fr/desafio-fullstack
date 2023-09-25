namespace crud_desafio.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Fornecedor
{
    public int Id { get; set; }

    [MaxLength(14)]
    public string Cnpj { get; set; }

    [MaxLength(11)]
    public string Cpf { get; set; }

    [Required]
    public string Nome { get; set; }

    public string Email { get; set; }

    [Required]
    [MaxLength(8)]
    public string Cep { get; set; }

    public int EmpresaId { get; set; }

    //Caso o fornecedor seja pessoa física (Campos opcionais)
    public string Rg { get; set; }
    public DateTime DataNascimento { get; set; }

}