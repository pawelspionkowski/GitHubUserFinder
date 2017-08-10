using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubUserFinder.Models
{
    public class GitHubUser
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string AvataruUrl { get; set; }
        public List<GitHubRepository> Repository { get; set; }

    }
}