using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octokit;
using System.Collections.Generic;
using System.Linq;
using GitHubUserFinder.Tests.Models;
using FluentAssertions;
using GitHubUserFinder.Domain.Models;
using GitHubUserFinder.Service.GitHub.WebServiceRepository;
using System.Configuration;

namespace GitHubUserFinder.Tests
{
    [TestClass]
    public class GitHubIntegration
    {
        private GitHub _gitHub = new GitHub(ConfigurationManager.AppSettings["GitHubLoggin"], ConfigurationManager.AppSettings["GitHubPassword"]);

        [TestMethod]
        public void CanGetRepositoriesWithLimit()
        {
            int maxItems = 5;
            int expectedItems = maxItems;
            PrivateObject privateObjectGetRepositoriesWithLimit = new PrivateObject(_gitHub);

            IEnumerable<Repository> repositoryList = new Repository[] {
                new Repository(),
                new Repository(),
                new Repository(),
                new Repository(),
                new Repository(),
                new Repository(),
                new Repository()
            };
            
            IEnumerable<Repository> selectedRepository = (IEnumerable<Repository>)privateObjectGetRepositoriesWithLimit.Invoke("GetRepositoriesWithLimit", repositoryList, maxItems);
            
            selectedRepository.Count().Should().Equals(expectedItems);
        }

        [TestMethod]
        public void CanGetRepositoriesWithLimitAndWithLimitedItems()
        {
             int maxItems = 5;
            int expectedItems = 3;
            PrivateObject privateObjectGetRepositoriesWithLimit = new PrivateObject(_gitHub);

            IEnumerable<Repository> repositoryList = new Repository[] {
                new Repository(),
                new Repository(),
                new Repository()
            };
            
            IEnumerable<Repository> selectedRepository = (IEnumerable<Repository>)privateObjectGetRepositoriesWithLimit.Invoke("GetRepositoriesWithLimit", repositoryList, maxItems);
            
            selectedRepository.Count().Should().Equals(expectedItems);
        }

        [TestMethod]
        public void CanGetRepositoriesWithLimniteBelowZero()
        {
            int maxItems = -10;
            int expectedItems = 0;
            PrivateObject privateObjectGetRepositoriesWithLimit = new PrivateObject(_gitHub);

            IEnumerable<Repository> repositoryList = new Repository[] {
                new Repository(),
                new Repository(),
                new Repository()
            };
            
            IEnumerable<Repository> selectedRepository = (IEnumerable<Repository>)privateObjectGetRepositoriesWithLimit.Invoke("GetRepositoriesWithLimit", repositoryList, maxItems);
            
            selectedRepository.Count().Should().Equals(expectedItems);
        }

        [TestMethod]
        public void CanGetRepositoriesWithEmptyRepositoryModelAndLimit()
        {
            int maxItems = 5;
            int expectedItems = 0;
            PrivateObject privateObjectGetRepositoriesWithLimit = new PrivateObject(_gitHub);

            IEnumerable<Repository> repositoryList = new Repository[] {
            };
            
            IEnumerable<Repository> selectedRepository = (IEnumerable<Repository>)privateObjectGetRepositoriesWithLimit.Invoke("GetRepositoriesWithLimit", repositoryList, maxItems);
            
            selectedRepository.Count().Should().Equals(expectedItems);
        }

        [TestMethod]
        public void CanGetGitHubUserForExistingUser()
        {
            int expectedResultRepositoryItems = 4;

            User user = new User(avatarUrl: "/Content/img/UserNotExist.jpg", name: "Heniu", blog: "", collaborators: 0, email: "", createdAt: DateTime.Now, diskUsage: 0, bio: "", company: "", followers: 0, following: 0, hireable: null, htmlUrl: "", id: 0, ldapDistinguishedName: "", location: "Warszaw", login: "", url: "", ownedPrivateRepos: 0, permissions: null, plan: null, privateGists: 0, publicGists: 0, publicRepos: 0, siteAdmin: false, suspendedAt: null, totalPrivateRepos: 0);

            List<Repository> repositoryList = new List<Repository>();
            for (int i = 0; i < expectedResultRepositoryItems; i++)
                repositoryList.Add(new Repository(url: "", gitUrl: "", allowMergeCommit: false, fork: false, forksCount: 0, fullName: "", hasDownloads: false, hasIssues: false, allowRebaseMerge: false, cloneUrl: "", createdAt: DateTime.Now, defaultBranch: "", hasWiki: false, allowSquashMerge: false, description: "", hasPages: false, homepage: "", htmlUrl: "", id: 0, language: "", mirrorUrl: "", name: "", openIssuesCount: 0, owner: null, parent: null, permissions: null, @private: false, pushedAt: DateTime.Now, size: 0, source: null, sshUrl: "", stargazersCount: 0, subscribersCount: 0, svnUrl: "", updatedAt: DateTime.Now));

            GitHubAccount gitHubAccount = new GitHubAccount()
            {
                Repository = repositoryList,
                User = user
            };
            
            PrivateObject privateObjectGetGitHubUser = new PrivateObject(_gitHub);
            GitHubUser gitHubUser = (GitHubUser)privateObjectGetGitHubUser.Invoke("GetGitHubUser", gitHubAccount);

            CheckGitHubUser(new GitHubUserExpectedResults { Name = "Heniu", Location = "Warszaw", AvataruUrl = "/Content/img/UserNotExist.jpg", RepositoryCount = expectedResultRepositoryItems } , gitHubUser);
        }

        [TestMethod]
        public void CanGetGitHubUserForNotExistingUser()
        {
            int expectedRepositoryItems = 0;

            User user = new User();

            List<Repository> repositoryList = new List<Repository>();
            GitHubAccount gitHubAccount = new GitHubAccount()
            {
                Repository = repositoryList,
                User = user
            };
            
            PrivateObject privateObjectGetGitHubUser = new PrivateObject(_gitHub);
            GitHubUser gitHubUser = (GitHubUser)privateObjectGetGitHubUser.Invoke("GetGitHubUser", gitHubAccount);

            CheckGitHubUser(new GitHubUserExpectedResults { Name = "None", Location = "None", AvataruUrl = "None", RepositoryCount = expectedRepositoryItems }, gitHubUser);
        }

        private static void CheckGitHubUser(GitHubUserExpectedResults gitHubUserExpectedResults, GitHubUser gitHubUser)
        {
            gitHubUser.Should().NotBeNull();
            gitHubUser.Name.Should().Equals(gitHubUserExpectedResults.Name);
            gitHubUser.Location.Should().Equals(gitHubUserExpectedResults.Location);
            gitHubUser.AvataruUrl.Should().Equals(gitHubUserExpectedResults.AvataruUrl);
            gitHubUser.Repository.Count().Should().Equals(gitHubUserExpectedResults.RepositoryCount);
        }
    }
}
