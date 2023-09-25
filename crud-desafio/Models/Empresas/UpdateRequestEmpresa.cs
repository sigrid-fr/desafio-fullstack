namespace crud_desafio.Models.Empresas;

public class UpdateRequestEmpresa
{
    public string Cnpj { get; set; }

    public string NomeFantasia { get; set; }

    public string Cep { get; set; }

    public string Estado { get; set; }


    private string replaceEmptyWithNull(string value)
    {
        return string.IsNullOrEmpty(value) ? null : value;
    }
}