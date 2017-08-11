# GitHub User Finder
GitHub User Finder is a simple to use data search application from GitHub. The application is written in 

### Used programming languages:
- C#
- JavaScript

### Used technologies and frameworks:
- Ninject
- jQuery
- Octokit
- FluentAssertions
- jQuery.validate
- .Net MVC

### How to use:
To use this, enter GitHub username in the text box and press button receive user and his repository data

### How to setUp application:
Provide your GitHub Login and Password to app.config in GitHubUserFinder.Tests project:
    <add key="GitHubLoggin" value="" />
    <add key="GitHubPassword" value="" />

Identically for GitHubUserFinder in Web.config file:
    <add key="GitHubLoggin" value="" />
    <add key="GitHubPassword" value="" />

When we do not enter a login and password will result in a limit to the GitHUB web service.
More informations on: https://developer.github.com/v3/rate_limit/
