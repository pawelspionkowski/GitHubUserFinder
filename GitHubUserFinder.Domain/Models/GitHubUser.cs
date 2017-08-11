using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUserFinder.Domain.Models
{
    public class GitHubUser
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string AvataruUrl { get; set; }
        public List<GitHubRepository> Repository { get; set; }
    }
}
