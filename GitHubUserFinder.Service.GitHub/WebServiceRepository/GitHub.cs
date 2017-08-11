using GitHubUserFinder.Domain.Models;
using GitHubUserFinder.Service.GitHub.AbstractWebServiceRepository;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUserFinder.Service.GitHub.WebServiceRepository
{
    public class GitHub : IRepository
    {
        public GitHub() : this("", "")
        {
        }

        public GitHub(string _gitHubLoggin, string _gitHubPassword)
        {
            this._gitHubLoggin = _gitHubLoggin;
            this._gitHubPassword = _gitHubPassword;
        }

        private string _gitHubLoggin;
        private string _gitHubPassword;

        private async Task<bool> IsUserExist(GitHubClient gitHubClient, string userName)
        {
            var request = new SearchUsersRequest(userName)
            {
                In = new[] { UserInQualifier.Username },
            };
            var searchResult = await gitHubClient.Search.SearchUsers(request);

            return searchResult.Items.Any(x => x.Login == userName);
        }

        public async Task<GitHubUser> GetUser(string userName)
        {
            GitHubClient gitHubClient = GetConnectionToRepository();


            GitHubUser gitHubUser = await SetUser(userName, gitHubClient);

            return gitHubUser;
        }

        private async Task<GitHubUser> SetUser(string userName, GitHubClient gitHubClient)
        {
            GitHubUser gitHubUser;

            if (await IsUserExist(gitHubClient, userName))
            {
                User user = await GetUserData(gitHubClient, userName);
                IEnumerable<Repository> allRepositories = await GetRepositoriesData(gitHubClient, userName);
                IEnumerable<Repository> selectedRepositories = GetRepositoriesWithLimit(allRepositories, 5);

                GitHubAccount account = new GitHubAccount()
                {
                    Repository = selectedRepositories,
                    User = user
                };

                gitHubUser = GetGitHubUser(account);
            }
            else
                gitHubUser = AnonymusUser();

            return gitHubUser;
        }

        private GitHubUser AnonymusUser()
        {
            return new GitHubUser()
            {
                AvataruUrl = "/Content/img/UserNotExist.jpg",
                Location = "None",
                Name = "None",
                Repository = new List<GitHubRepository>()
            };
        }

        private async Task<IEnumerable<Repository>> GetRepositoriesData(GitHubClient gitHubClient, string userName)
        {
            return await gitHubClient.Repository.GetAllForUser(userName);
        }

        private async Task<User> GetUserData(GitHubClient gitHubClient, string userName)
        {
            return await gitHubClient.User.Get(userName); ;
        }

        private IEnumerable<Repository> GetRepositoriesWithLimit(IEnumerable<Repository> allRepositories, int limit)
        {
            return allRepositories.OrderByDescending(x => x.StargazersCount).Take(limit);
        }

        private GitHubUser GetGitHubUser(GitHubAccount account)
        {
            return new GitHubUser()
            {
                Name = account.User.Name ?? "None",
                Location = account.User.Location ?? "None",
                AvataruUrl = account.User.AvatarUrl ?? "None",
                Repository = account.Repository.Select(x => new GitHubRepository() { Name = x.Name ?? "None", Language = x.Language ?? "None", StargazerCount = x.StargazersCount }).ToList()
            };
        }

        private GitHubClient GetConnectionToRepository()
        {
            GitHubClient github = new GitHubClient(new ProductHeaderValue("GitHubUserFinder"));

            if (_gitHubLoggin != "" && _gitHubPassword != "")
            {
                Credentials basicAuth = new Credentials(_gitHubLoggin, _gitHubPassword);
                github.Credentials = basicAuth;
            }
            return github;
        }
    }
}
