using Bloggie.web.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.web.Models.ViewModel
{
    public class AddBlogPostRequest
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        //Display the collections
        public IEnumerable<SelectListItem>Tags { get; set; }
        //collect tags
        public string [] SelectedTags { get; set; }=Array.Empty<string>();
    }
}
