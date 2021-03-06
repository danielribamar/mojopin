﻿using Examine.LuceneEngine.SearchCriteria;
using Mojopin.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Mojopin.Frontend.Services
{
    public class SearchService
    {
        private UmbracoHelper umbracoHelper = new Umbraco.Web.UmbracoHelper(Umbraco.Web.UmbracoContext.Current);

        public List<ItemPreview> FeedSearch(int page, int numberOfItems, int categoryId = 0, string term = "")
        {
            var results = new List<ItemPreview>();

            //Fetching our SearchProvider by giving it the name of our searchprovider
            var Searcher = Examine.ExamineManager.Instance.SearchProviderCollection["MySearchSearcher"];

            var searchCriteria = Searcher.CreateSearchCriteria();
            var searchFields = new[] { "nodeTypeAlias" };
            if (!string.IsNullOrEmpty(term))
            {
                searchFields = new[] { "contentTitle", "contentSubtitle", "contentDescription" };
            }
            var searchTerms = new[] { "article", "note" };
            if (!string.IsNullOrEmpty(term))
            {
                searchTerms = new[] { term };
            }

            var query = searchCriteria.GroupedOr(searchFields, searchTerms.ToArray()).Compile();
            var searchResults = Searcher.Search(query);
            //Searching and ordering the result by score, and we only want to get the results that has a minimum of 0.05(scale is up to 1.)
            //Printing the results
            foreach (var item in searchResults.Select(p => umbracoHelper.TypedContent(p.Id)).OrderByDescending(p => p.CreateDate).ToList())
            {
                switch (item.DocumentTypeAlias)
                {
                    case "Note":
                    case "Article":
                        var itemPreview = ConvertToItem(item);
                        if (categoryId == 0 || itemPreview.ParentId == categoryId)
                        {
                            results.Add(itemPreview);
                        }
                        break;
                }
            }
            if (numberOfItems != 0)
            {
                return results.Skip(numberOfItems * (page - 1)).Take(numberOfItems).ToList();
            }
            return results;
        }

        internal List<ItemPreview> Search(string keyword)
        {

            var results = new List<ItemPreview>();

            //Fetching our SearchProvider by giving it the name of our searchprovider
            var Searcher = Examine.ExamineManager.Instance.SearchProviderCollection["MySearchSearcher"];

            var searchCriteria = Searcher.CreateSearchCriteria();
            var searchFields = new[] { "nodeTypeAlias", };
            var searchTerms = new[] { "article", "note" };
            var query = searchCriteria.GroupedOr(searchFields, searchTerms.ToArray()).Compile();
            var searchResults = Searcher.Search(query);
            //Searching and ordering the result by score, and we only want to get the results that has a minimum of 0.05(scale is up to 1.)
            //Printing the results
            foreach (var item in searchResults.Select(p => umbracoHelper.TypedContent(p.Id)).OrderByDescending(p => p.CreateDate).ToList())
            {
                var itemPreview = ConvertToItem(item);

                results.Add(itemPreview);
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
            item.ParentId = result.Parent.Id;
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
                var contentThumbnail = umbracoHelper.TypedMedia(result.GetPropertyValue("contentThumbnail"));

                if (contentThumbnail == null)
                {
                    item.ThumbnailImageUrl = "";
                }
                else
                {
                    item.ThumbnailImageUrl = string.Format("{0}&bgcolor=FFF", contentThumbnail
               .GetCropUrl(1280, 768,
               quality: 30,
               imageCropAnchor: Umbraco.Web.Models.ImageCropAnchor.Center,
               imageCropMode: Umbraco.Web.Models.ImageCropMode.Crop
               ));
                }
            }
            if (result.HasProperty("contentThumbnailVideo") && result.HasValue("contentThumbnailVideo"))
            {
                item.ThumbnailVideoUrl = result.GetPropertyValue<string>("contentThumbnailVideo");
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