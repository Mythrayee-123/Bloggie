using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
           await bloggieDbContext.AddAsync(blogPost);
            await bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
           var existing=await bloggieDbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (existing != null)
            {
                bloggieDbContext.BlogPosts.Remove(existing);
                await bloggieDbContext.SaveChangesAsync();
                return existing;    
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDbContext.BlogPosts.Include(x=>x.tags).ToListAsync(); 
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
           return await bloggieDbContext.BlogPosts.Include(x=>x.tags).FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<BlogPost?> GetByurlHandleAsync(string UrlHandle)
        {
            return await bloggieDbContext.BlogPosts
                .Include(x => x.tags)
                .FirstOrDefaultAsync(x=> x.UrlHandle == UrlHandle); 
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
           var existingBlog= await bloggieDbContext.BlogPosts
                 .Include(x => x.tags)
                 .FirstOrDefaultAsync(x => x.Id ==blogPost.Id);
            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;  
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;    
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.PublishedDate=blogPost.PublishedDate;
                existingBlog.Author = blogPost.Author;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.tags = blogPost.tags;
                await bloggieDbContext.SaveChangesAsync();
                return existingBlog;
            }
            else
            {
                return null;
            }
        }
    }
}
