using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUserFinder.Domain.Models
{
    public class GitHubRepository
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public int StargazerCount { get; set; }
    }
}
