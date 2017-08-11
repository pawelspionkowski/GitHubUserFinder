using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octokit;
using System.Threading.Tasks;
using System.Collections.Generic;
using FluentAssertions;
using GitHubUserFinder.Service.GitHub.WebServiceRepository;
using System.Configuration;

namespace GitHubUserFinder.Tests
{
    [TestClass]
    public class GitHubWebService
    {
        private GitHub _gitHub = new GitHub(ConfigurationManager.AppSettings["GitHubLoggin"], ConfigurationManager.AppSettings["GitHubPassword"]);
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
            
            output.Result.Should().NotBeNull();
            output.Result.Login.Should().Equals(gitHubUserName);
        }
        
        [TestMethod]
        public void CannotGetNotExistingUser()
        {
            PrivateObject privateObjectGetGitHubUserData = new PrivateObject(_gitHub);
            Task<User> output = (Task<User>)privateObjectGetGitHubUserData.Invoke("GetUserData", _gitHubClient, "robconery_NotExists");

            Action a = () => output.Wait();

            a.ShouldThrow<Exception>();
            output.Exception.Should().NotBeNull();
        }

        [TestMethod]
        public void CanGetAllRepositories()
        {
            PrivateObject privateObjectGetGitHubUserData = new PrivateObject(_gitHub);
            Task<IEnumerable<Repository>> output = (Task<IEnumerable<Repository>>)privateObjectGetGitHubUserData.Invoke("GetRepositoriesData", _gitHubClient, "robconery");

            Action a = () => output.Wait();

            a.ShouldNotThrow<Exception>();
            output.Result.Should().NotBeNull();
        }
        
        [TestMethod]
        public void CannotGetAllRepositories()
        {
            PrivateObject privateObjectGetGitHubUserData = new PrivateObject(_gitHub);
            Task<IEnumerable<Repository>> output = (Task<IEnumerable<Repository>>)privateObjectGetGitHubUserData.Invoke("GetRepositoriesData", _gitHubClient, "robconery_NotExists");

            Action a = () => output.Wait();

            a.ShouldThrow<Exception>();
            output.Exception.Should().NotBeNull();
        }
    }
}
