using System;

namespace Mojopin.Frontend.Models
{
    public class ItemPreview
    {
        public int NodeId { get; set; }
        public string NodeTypeAlias { get; set; }
        public string ParentUrl { get; set; }
        public string ParentName { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public string ThumnailVideoUrl { get; set; }
        public DateTime Date { get; set; }
        public string DateFormatted { get; set; }

    }
}