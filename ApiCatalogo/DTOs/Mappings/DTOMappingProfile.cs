using ApiCatalogo.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.DTOs.Mappings
{
    public class DTOMappingProfile: Profile
    {
        public DTOMappingProfile() 
        {
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDTOUpdateRequest>().ReverseMap();
            CreateMap<Produto, ProdutoDTOUpdateResponse>().ReverseMap();

        }
    }
}
