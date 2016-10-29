using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MojoPin.Frontend.Models;
using Newtonsoft.Json;
using umbraco.MacroEngines;

namespace MojoPin.Frontend.Helpers
{
    public static class UmbracoContentHelper
    {
        public static List<RelatedLink> GetRelatedLinks(string property, DynamicNode model)
        {
            var rlinks = new List<RelatedLink>();

            if (string.IsNullOrEmpty(model.GetPropertyValue(property)))
            {
                return rlinks;
            }

            foreach (var item in (IEnumerable<dynamic>)JsonConvert.DeserializeObject(model.GetPropertyValue(property)))
            {
                var result = new RelatedLink();
                result.Url = (bool)item.isInternal ? new DynamicNode(item["internal"]).Url : item.link;
                result.Target = (bool)item.newWindow ? "_blank" : null;
                result.Caption = item.caption;
                rlinks.Add(result);
            }

            return rlinks;
        }
    }
}