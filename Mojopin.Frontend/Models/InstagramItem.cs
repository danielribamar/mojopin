using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojoPin.Frontend.Models
{

    public class InstragramMediaModel
    {

        public Images images { get; set; }

        public string link { get; set; }
    }

    public class Images
    {
        public Image standard_resolution { get; set; }
        public Image thumbnail { get; set; }
    }

    public class Image
    {
        public string url { get; set; }

        public int width { get; set; }

        public int height { get; set; }
    }
}
