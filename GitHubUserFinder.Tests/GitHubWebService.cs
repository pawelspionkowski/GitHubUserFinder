using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GitHubUserFinder.Source;
using Octokit;
using System.Threading.Tasks;
using System.Collections.Generic;
using FluentAssertions;

namespace GitHubUserFinder.Tests
{
    [TestClass]
    public class GitHubWebService
    {
        private GitHub _gitHub = new GitHub();
        private GitHubClient _gitHubClient;

        [TestInitialize]
        public void TestInitialize()
        {
            PrivateObject privateObjectGetConnectionToRepository = new PrivateObject(_gitHub);
            _gitHubClient = (GitHubClient)privateObjectGetConnectionToRepository.Invoke("GetConnectionToRepository");

        }

        [TestMethod]
        public void CanGetExistingUser()
        {
            string gitHubUserName = "robconery";
            
            PrivateObject privateObjectGetGitHubUserData = new PrivateObject(_gitHub);
            Task<User> output = (Task<User>)privateObjectGetGitHubUserData.Invoke("GetUserData", _gitHubClient, gitHubUserName);
            
            Action a = () => output.Wait();
            a.ShouldNotThrow<Exception>();

            Assert.IsNotNull(output.Result);
            Assert.AreEqual(output.Result.Login, gitHubUserName);
        }
        
        [TestMethod]
        public void CannotGetNotExistingUser()
        {
            PrivateObject privateObjectGetGitHubUserData = new PrivateObject(_gitHub);
            Task<User> output = (Task<User>)privateObjectGetGitHubUserData.Invoke("GetUserData", _gitHubClient, "robconery_NotExists");

            Action a = () => output.Wait();

            a.ShouldThrow<Exception>();
            Assert.IsNotNull(output.Exception);
        }

        [TestMethod]
        public void CanGetAllRepositories()
        {
            PrivateObject privateObjectGetGitHubUserData = new PrivateObject(_gitHub);
            Task<IEnumerable<Repository>> output = (Task<IEnumerable<Repository>>)privateObjectGetGitHubUserData.Invoke("GetRepositoriesData", _gitHubClient, "robconery");

            Action a = () => output.Wait();

            a.ShouldNotThrow<Exception>();
            Assert.IsNotNull(output.Result);
        }
        
        [TestMethod]
        public void CannotGetAllRepositories()
        {
            PrivateObject privateObjectGetGitHubUserData = new PrivateObject(_gitHub);
            Task<IEnumerable<Repository>> output = (Task<IEnumerable<Repository>>)privateObjectGetGitHubUserData.Invoke("GetRepositoriesData", _gitHubClient, "robconery_NotExists");

            Action a = () => output.Wait();

            a.ShouldThrow<Exception>();
            Assert.IsNotNull(output.Exception);
        }
    }
}
