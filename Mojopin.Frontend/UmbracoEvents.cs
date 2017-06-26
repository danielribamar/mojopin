using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;

namespace Mojopin.Frontend
{
    public class UmbracoEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            Umbraco.Core.Services.ContentService.Published += ContentService_Published;
        }

        private void ContentService_Published(IPublishingStrategy sender, PublishEventArgs<IContent> args)
        {
            foreach (var node in args.PublishedEntities.Where(p => p.HasProperty("shareUrl")))
            {
                node.SetValue("shareUrl", $"http://mojopin.pt/FacebookShareGateway.aspx?pid={node.Id}");
                ApplicationContext.Current.Services.ContentService.Publish(node);
            }
        }
    }
}