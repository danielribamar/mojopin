using Mojopin.Frontend.Services;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Web.WebApi;

namespace Mojopin.Frontend.Controllers
{
    public class SearchController : UmbracoApiController
    {
        private SearchService searchService;
        private const int pageSize = 8;
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
            var results = searchService.FeedSearch(id, pageSize, cid);
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        [HttpGet]
        public IHttpActionResult GetFeed(string keyword,int page = 1)
        {
            var results = searchService.Search(keyword);
            results = results.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            return Ok(results);
        }
    }
}
