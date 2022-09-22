using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities
{
    public class Github:Entity
    {
        
        public int UserId { get; set; }
        public string GithubUrl { get; set; }
        public virtual User? User { get; set; }

        public Github()
        {
          
        }

        public Github(int id, int userId, string githubUrl):this()
        {
            Id = id;
            UserId = userId;
            GithubUrl = githubUrl;
        }

    }
}
