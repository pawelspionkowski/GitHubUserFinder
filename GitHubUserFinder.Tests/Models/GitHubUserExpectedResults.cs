using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUserFinder.Tests.Models
{
    public class GitHubUserExpectedResults
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string AvataruUrl { get; set; }
        public int RepositoryCount { get; set; }
    }
}
