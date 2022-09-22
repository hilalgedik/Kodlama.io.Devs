using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Githubs.Commands.CreateGithub;
using Application.Features.Githubs.Commands.DeleteGithub;
using Application.Features.Githubs.Commands.UpdateGithub;
using Application.Features.Githubs.Dtos;
using Application.Features.Githubs.Models;
using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Githubs.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Github, CreatedGithubDto>().ReverseMap();
            CreateMap<Github, CreateGithubCommand>().ReverseMap();

            CreateMap<Github, DeletedGithubDto>().ReverseMap();
            CreateMap<Github, DeleteGithubCommand>().ReverseMap();

            CreateMap<Github, UpdatedGithubDto>().ReverseMap();
            CreateMap<Github, UpdateGithubCommand>().ReverseMap();


            CreateMap<IPaginate<Github>, GithubListModel>().ReverseMap();
            CreateMap<Github, GithubListDto>().ReverseMap();


            CreateMap<Github, GithubListDto>().ForMember(c => c.UserId,
                opt => opt.MapFrom(c => c.User.Id)).ReverseMap();



            CreateMap<IPaginate<Github>, GithubListModel>().ReverseMap();


        }

    }
}
