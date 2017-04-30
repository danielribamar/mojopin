using Mojopin.Frontend.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Umbraco.Web.WebApi;

namespace Mojopin.Frontend.Controllers
{
    public class SearchController : UmbracoApiController
    {
        private SearchService searchService;

        public SearchController()
        {
            searchService = new SearchService();
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }

        [HttpGet]
        public HttpResponseMessage GetFeed(int id, int cid)
        {
            var results = searchService.FeedSearch(id, 5, cid);
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }
    }
}
