using Bloggie.web.Models.Domain;

namespace Bloggie.web.Models.ViewModel
{
	public class HomeViewModel
	{
        public IEnumerable<BlogPost> BlogPost { get; set; }
        public IEnumerable<Tag> Tag{ get; set; }

    }
}
