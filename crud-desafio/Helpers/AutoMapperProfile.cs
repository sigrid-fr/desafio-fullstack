namespace crud_desafio.Helpers;

using AutoMapper;
using crud_desafio.Entities;
using crud_desafio.Models.Empresas;
using crud_desafio.Models.Fornecedores;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateRequest -> Empresa
        CreateMap<CreateRequestEmpresa, Empresa>();

        // UpdateRequest -> Empresa
        CreateMap<UpdateRequestEmpresa, Empresa>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore both null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        // CreateRequest -> Fornecedor
        CreateMap<CreateRequestFornecedor, Fornecedor>();

        // UpdateRequest -> Fornecedor
        CreateMap<UpdateRequestFornecedor, Fornecedor>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore both null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));
    }
}