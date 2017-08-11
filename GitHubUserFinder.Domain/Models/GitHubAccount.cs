using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUserFinder.Domain.Models
{
    public class GitHubAccount
    {
        public User User { get; set; }
        public IEnumerable<Repository> Repository { get; set; }
    }
}
