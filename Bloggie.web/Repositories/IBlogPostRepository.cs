using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModel;

namespace Bloggie.web.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?>GetAsync(Guid id);
        Task<BlogPost?> GetByurlHandleAsync(string UrlHandle);
        Task<BlogPost> AddAsync(BlogPost blogPost);
        Task<BlogPost?>UpdateAsync(BlogPost blogPost); 
        Task<BlogPost?> DeleteAsync(Guid id);

    }
}
