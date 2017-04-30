using System;
using System.Web.UI.HtmlControls;
using umbraco.MacroEngines;
using Umbraco.Web;

namespace Mojopin.Frontend
{
    public partial class FacebookShareGateway : System.Web.UI.Page
    {
        protected string RedirectUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

            var pageId = Request.QueryString["pid"];

            if (string.IsNullOrEmpty(pageId))
            {
                return;
            }

            var node = new DynamicNode(pageId);

            var title = node.GetPropertyValue("seoTitle");
            HtmlMeta meta_title = new HtmlMeta();
            meta_title.Name = "og:title";
            meta_title.Content = title;
            MetaPlaceHolder.Controls.Add(meta_title);

            var description = node.GetPropertyValue("seoDescription");
            HtmlMeta meta_description = new HtmlMeta();
            meta_description.Name = "og:description";
            meta_description.Content = description;
            MetaPlaceHolder.Controls.Add(meta_description);

            var imageUrl = node.GetPropertyValue("shareImage");
            HtmlMeta meta_imageUrl = new HtmlMeta();
            meta_imageUrl.Name = "og:image";
            meta_imageUrl.Content = $"http://mojopin.pt/{imageUrl}";
            MetaPlaceHolder.Controls.Add(meta_imageUrl);

            HtmlMeta meta_sitename = new HtmlMeta();
            meta_sitename.Name = "og:site_name";
            meta_sitename.Content = "mojopin.pt";
            MetaPlaceHolder.Controls.Add(meta_sitename);

            HtmlMeta meta_siteurl = new HtmlMeta();
            meta_siteurl.Name = "og:site_name";
            meta_siteurl.Content = "http://mojopin.pt";
            MetaPlaceHolder.Controls.Add(meta_siteurl);

            RedirectUrl = meta_siteurl.Content;
        }
    }
}