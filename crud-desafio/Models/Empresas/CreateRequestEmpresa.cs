namespace crud_desafio.Models.Empresas;

using System.ComponentModel.DataAnnotations;

public class CreateRequestEmpresa
{

    [Required]
    [MaxLength(14)]
    public string Cnpj { get; set; }

    [Required]
    public string NomeFantasia { get; set; }

    [Required]
    [MaxLength(8)]
    public string Cep { get; set; }

    //Caso a empresa seja do Paraná
    public string Estado { get; set; }

}