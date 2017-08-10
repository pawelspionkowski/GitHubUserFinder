using GitHubUserFinder.Models;
using GitHubUserFinder.Source;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GitHubUserFinder.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _gitHub = null;

        public HomeController(IRepository _gitHub)
        {
            this._gitHub = _gitHub;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetUserFromGitHubAsync(string userName)
        {
            GitHubUser gitHubUser = await _gitHub.GetUser(userName);

            return Json(gitHubUser);
        }
    }
}