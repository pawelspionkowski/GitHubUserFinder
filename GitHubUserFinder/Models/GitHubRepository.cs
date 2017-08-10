using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubUserFinder.Models
{
    public class GitHubRepository
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public int StargazerCount { get; set; }
    }
}