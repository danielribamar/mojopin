using Examine;
using Examine.LuceneEngine.SearchCriteria;
using Mojopin.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Mojopin.Frontend.Services
{
    public class SearchService
    {
        private UmbracoHelper umbracoHelper = new Umbraco.Web.UmbracoHelper(Umbraco.Web.UmbracoContext.Current);

        public List<ItemPreview> FeedSearch(int page, int numberOfItems)
        {
            var results = new List<ItemPreview>();

            //Fetching our SearchProvider by giving it the name of our searchprovider
            var Searcher = Examine.ExamineManager.Instance.SearchProviderCollection["MySearchSearcher"];

            var searchCriteria = Searcher.CreateSearchCriteria(Examine.SearchCriteria.BooleanOperation.Or);
            var query = searchCriteria
                .Field("nodeTypeAlias", "Article".Boost(2)).Or()
                .Field("nodeTypeAlias", "Article".Fuzzy(0.5f)).Or()
                .Field("nodeTypeAlias", "Note".Boost(2)).Or()
                .Field("nodeTypeAlias", "Note".Fuzzy(0.5f));

            //Searching and ordering the result by score, and we only want to get the results that has a minimum of 0.05(scale is up to 1.)
            var searchResults = umbracoHelper.TypedSearch(query.Compile()).OrderByDescending(x => x.CreateDate);
            //Printing the results
            foreach (var item in searchResults.Skip(numberOfItems * (page - 1)).Take(numberOfItems).ToList())
            {
                switch (item.DocumentTypeAlias)
                {
                    case "Note":
                    case "Article":
                        results.Add(ConvertToItem(item));
                        break;
                }
            }
            return results;
        }

        private ItemPreview ConvertToItem(IPublishedContent result)
        {
            var item = new ItemPreview();
            item.NodeId = result.Id;
            item.NodeTypeAlias = result.DocumentTypeAlias;
            item.ParentName = result.Parent.Name;
            item.ParentUrl = result.Parent.Url;
            item.Url = result.Url;
            if (result.HasProperty("contentTitle") && result.HasValue("contentTitle"))
            {
                item.Title = result.GetPropertyValue<string>("contentTitle");
            }
            if (result.HasProperty("contentSubtitle") && result.HasValue("contentSubtitle"))
            {
                item.Subtitle = result.GetPropertyValue<string>("contentSubtitle");
            }
            if (result.HasProperty("contentDescription") && result.HasValue("contentDescription"))
            {
                item.Description = result.GetPropertyValue<string>("contentDescription");
            }
            if (result.HasProperty("contentThumbnail") && result.HasValue("contentThumbnail"))
            {
                item.ThumbnailImageUrl = umbracoHelper.TypedMedia(result.GetPropertyValue("contentThumbnail")).Url;
            }
            if (result.HasProperty("contentThumbnailVideo") && result.HasValue("contentThumbnailVideo"))
            {
                item.ThumnailVideoUrl = result.GetPropertyValue<string>("contentThumbnailVideo");
            }
            if (result.HasProperty("contentDate") && result.HasValue("contentDate"))
            {
                item.Date = result.GetPropertyValue<DateTime>("contentDate");
                item.DateFormatted = item.Date.ToString("dd MMMMMMMMM, yyyy", CultureInfo.GetCultureInfoByIetfLanguageTag("pt"));
            }

            return item;
        }
    }
}