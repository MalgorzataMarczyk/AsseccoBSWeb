using AsseccoBSWeb.Models;
using AsseccoBSWeb.Models.XmlDokument;
using AsseccoBSWeb.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System.Diagnostics;
using System.Xml.Serialization;

namespace AsseccoBSWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPageService _pageService;
        private readonly ITokenService _tokenService;

        public HomeController(ILogger<HomeController> logger, IPageService pageService, ITokenService tokenService)
        {
            _logger = logger;
            _pageService = pageService;
            _tokenService = tokenService;
        }

        public IActionResult Index(int page=1)
        {
            var homeView = new HomeModel
            {
                CurrentPage = page,
                Page = _pageService.GetPage(page),
                PageCount = _pageService.numberOfPages

            };
            return View(homeView);
        }

        public IActionResult GetPaths()
        {
            var client = new RestClient("https://portalcloudapi-test.assecobs.pl/?wadl&DBC=rest")
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(_tokenService.GetToken(), "Bearer"),
            };

            var request = new RestRequest();
            request.AddHeader("Cookie", "ROUTEID=.1");

            var response = client.Get(request);

            XmlApplication result;

            XmlSerializer serializer = new XmlSerializer(typeof(XmlApplication));

            using (TextReader reader = new StringReader(response.Content))
            {
                result = (XmlApplication)serializer.Deserialize(reader);
            }

            var pathList = result.BaseResource.Resources.Select(x => x.ResourcePath.Path).ToList();

            _pageService.AssignList(pathList);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}