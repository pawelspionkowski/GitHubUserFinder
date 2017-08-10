using GitHubUserFinder.Models;
using Octokit;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace GitHubUserFinder.Source
{
    public interface IRepository
    {
        Task<GitHubUser> GetUser(string userName);
    }
}