using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GitHubUserFinder.Source;
using Octokit;
using System.Collections.Generic;
using System.Linq;
using GitHubUserFinder.Models;

namespace GitHubUserFinder.Tests
{
    [TestClass]
    public class GitHubIntegration
    {
        [TestMethod]
        public void CanGetRepositoriesWithLimit()
        {
            GitHub gitHub = new GitHub();
            int maxItems = 5;
            PrivateObject privateObjectGetRepositoriesWithLimit = new PrivateObject(gitHub);

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

            Assert.AreEqual(selectedRepository.Count(), maxItems);
        }

        [TestMethod]
        public void CanGetRepositoriesWithLimitAndWithLimitedItems()
        {
            GitHub gitHub = new GitHub();
            int maxItems = 5;
            int resultItems = 3;
            PrivateObject privateObjectGetRepositoriesWithLimit = new PrivateObject(gitHub);

            IEnumerable<Repository> repositoryList = new Repository[] {
                new Repository(),
                new Repository(),
                new Repository()
            };


            IEnumerable<Repository> selectedRepository = (IEnumerable<Repository>)privateObjectGetRepositoriesWithLimit.Invoke("GetRepositoriesWithLimit", repositoryList, maxItems);

            Assert.AreEqual(selectedRepository.Count(), resultItems);
        }

        [TestMethod]
        public void CanGetRepositoriesWithLimniteBelowZero()
        {
            GitHub gitHub = new GitHub();
            int maxItems = -10;
            int resultItems = 0;
            PrivateObject privateObjectGetRepositoriesWithLimit = new PrivateObject(gitHub);

            IEnumerable<Repository> repositoryList = new Repository[] {
                new Repository(),
                new Repository(),
                new Repository()
            };


            IEnumerable<Repository> selectedRepository = (IEnumerable<Repository>)privateObjectGetRepositoriesWithLimit.Invoke("GetRepositoriesWithLimit", repositoryList, maxItems);

            Assert.AreEqual(selectedRepository.Count(), resultItems);
        }

        [TestMethod]
        public void CanGetRepositoriesWithEmptyRepositoryModelAndLimit()
        {
            GitHub gitHub = new GitHub();
            int maxItems = 5;
            int resultItems = 0;
            PrivateObject privateObjectGetRepositoriesWithLimit = new PrivateObject(gitHub);

            IEnumerable<Repository> repositoryList = new Repository[] {
            };


            IEnumerable<Repository> selectedRepository = (IEnumerable<Repository>)privateObjectGetRepositoriesWithLimit.Invoke("GetRepositoriesWithLimit", repositoryList, maxItems);

            Assert.AreEqual(selectedRepository.Count(), resultItems);
        }

        [TestMethod]
        public void CanGetGitHubUserForExistingUser()
        {
            GitHub gitHub = new GitHub();
            int resultRepositoryItems = 4;

            User user = new User(avatarUrl: "/Content/img/UserNotExist.jpg", name: "Heniu", blog: "", collaborators: 0, email: "", createdAt: DateTime.Now, diskUsage: 0, bio: "", company: "", followers: 0, following: 0, hireable: null, htmlUrl: "", id: 0, ldapDistinguishedName: "", location: "Warszaw", login: "", url: "", ownedPrivateRepos: 0, permissions: null, plan: null,  privateGists: 0, publicGists: 0, publicRepos: 0, siteAdmin: false, suspendedAt: null, totalPrivateRepos: 0);

            List<Repository> repositoryList = new List<Repository>();
            repositoryList.Add( new Repository(url: "", gitUrl: "", allowMergeCommit: false, fork: false, forksCount: 0, fullName: "", hasDownloads: false, hasIssues: false, allowRebaseMerge: false, cloneUrl: "", createdAt: DateTime.Now, defaultBranch: "", hasWiki: false, allowSquashMerge: false, description: "", hasPages: false, homepage: "", htmlUrl: "", id: 0, language: "", mirrorUrl: "",  name: "", openIssuesCount: 0, owner: null, parent: null, permissions: null, @private: false, pushedAt: DateTime.Now, size: 0, source: null, sshUrl: "", stargazersCount: 0, subscribersCount: 0, svnUrl: "", updatedAt: DateTime.Now));
            repositoryList.Add( new Repository(url: "", gitUrl: "", allowMergeCommit: false, fork: false, forksCount: 0, fullName: "", hasDownloads: false, hasIssues: false, allowRebaseMerge: false, cloneUrl: "", createdAt: DateTime.Now, defaultBranch: "", hasWiki: false, allowSquashMerge: false, description: "", hasPages: false, homepage: "", htmlUrl: "", id: 0, language: "", mirrorUrl: "", name: "", openIssuesCount: 0, owner: null, parent: null, permissions: null, @private: false, pushedAt: DateTime.Now, size: 0, source: null, sshUrl: "", stargazersCount: 0, subscribersCount: 0, svnUrl: "", updatedAt: DateTime.Now));
            repositoryList.Add( new Repository(url: "", gitUrl: "", allowMergeCommit: false, fork: false, forksCount: 0, fullName: "", hasDownloads: false, hasIssues: false, allowRebaseMerge: false, cloneUrl: "", createdAt: DateTime.Now, defaultBranch: "", hasWiki: false, allowSquashMerge: false, description: "", hasPages: false, homepage: "", htmlUrl: "", id: 0, language: "", mirrorUrl: "", name: "", openIssuesCount: 0, owner: null, parent: null, permissions: null, @private: false, pushedAt: DateTime.Now, size: 0, source: null, sshUrl: "", stargazersCount: 0, subscribersCount: 0, svnUrl: "", updatedAt: DateTime.Now));
            repositoryList.Add( new Repository(url: "", gitUrl: "", allowMergeCommit: false, fork: false, forksCount: 0, fullName: "", hasDownloads: false, hasIssues: false, allowRebaseMerge: false, cloneUrl: "", createdAt: DateTime.Now, defaultBranch: "", hasWiki: false, allowSquashMerge: false, description: "", hasPages: false, homepage: "", htmlUrl: "", id: 0, language: "", mirrorUrl: "", name: "", openIssuesCount: 0, owner: null, parent: null, permissions: null, @private: false, pushedAt: DateTime.Now, size: 0, source: null, sshUrl: "", stargazersCount: 0, subscribersCount: 0, svnUrl: "", updatedAt: DateTime.Now));

            GitHubAccount gitHubAccount = new GitHubAccount()
            {
                Repository = repositoryList,
                User = user
            };


            PrivateObject privateObjectGetGitHubUser = new PrivateObject(gitHub);
            GitHubUser gitHubUser = (GitHubUser)privateObjectGetGitHubUser.Invoke("GetGitHubUser", gitHubAccount);

            Assert.IsNotNull(gitHubUser);
            Assert.AreEqual(gitHubUser.Name, "Heniu");
            Assert.AreEqual(gitHubUser.Location , "Warszaw");
            Assert.AreEqual(gitHubUser.AvataruUrl, "/Content/img/UserNotExist.jpg");
            Assert.AreEqual(gitHubUser.Repository.Count(), resultRepositoryItems);
        }

        [TestMethod]
        public void CanGetGitHubUserForNotExistingUser()
        {
            GitHub gitHub = new GitHub();
            int resultRepositoryItems = 0;

            User user = new User();

            List<Repository> repositoryList = new List<Repository>();
            GitHubAccount gitHubAccount = new GitHubAccount()
            {
                Repository = repositoryList,
                User = user
            };


            PrivateObject privateObjectGetGitHubUser = new PrivateObject(gitHub);
            GitHubUser gitHubUser = (GitHubUser)privateObjectGetGitHubUser.Invoke("GetGitHubUser", gitHubAccount);

            Assert.IsNotNull(gitHubUser);
            Assert.AreEqual(gitHubUser.Name, "None");
            Assert.AreEqual(gitHubUser.Location, "None");
            Assert.AreEqual(gitHubUser.AvataruUrl, "None");
            Assert.AreEqual(gitHubUser.Repository.Count(), resultRepositoryItems);
        }
    }
}
