using GitHubUserFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUserFinder.Service.GitHub.AbstractWebServiceRepository
{
    public interface IRepository
    {
        Task<GitHubUser> GetUser(string userName);
    }
}
