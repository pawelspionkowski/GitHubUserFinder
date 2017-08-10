using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubUserFinder.Models
{
    public class GitHubAccount
    {
        public User User { get; set; }
        public IEnumerable<Repository> Repository { get; set; }
    }
}