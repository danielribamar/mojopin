using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojoPin.Frontend.Helpers
{
    public static class MobileHelper
    {
        private static readonly string[] MobileDevices = new string[] {"iphone","ppc",
                                                      "windows ce","blackberry",
                                                      "opera mini","mobile","palm",
                                                      "portable","opera mobi" };

        public static bool IsMobileDevice(string userAgent)
        {
            // TODO: null check
            userAgent = userAgent.ToLower();
            return MobileDevices.Any(x => userAgent.Contains(x));
        }
    }
}