using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repositories
{
	public class TagRepository : ITagRepository
	{
		private readonly BloggieDbContext bloggieDbContext;

		public TagRepository(BloggieDbContext bloggieDbContext)
        {
			this.bloggieDbContext = bloggieDbContext;
		}
        public async Task<Tag> AddAsync(Tag tag)
		{

			await bloggieDbContext.Tags.AddAsync(tag);
			await bloggieDbContext.SaveChangesAsync();
			return tag;

        }

		public async Task<Tag?> DeleteAsync(Guid id)
		{
			var existing=await bloggieDbContext.Tags.FirstOrDefaultAsync(x=>x.Id==id);
			if (existing!=null)
			{
                bloggieDbContext.Tags.Remove(existing);

				await bloggieDbContext.SaveChangesAsync();
				return existing;	
            }
			return null;
		}

		public async Task<IEnumerable<Tag>> GetAllAsync()
		{
			return await bloggieDbContext.Tags.ToListAsync();
		}

		public async Task<Tag?> GetAsync(Guid id)
		{
			return await bloggieDbContext.Tags.FirstOrDefaultAsync(y=>y.Id==id);	
			
		}

		public async Task<Tag?> UpdateAsync(Tag tag)
		{
			var existing=await bloggieDbContext.Tags.FirstOrDefaultAsync(x=>x.Id==tag.Id);
			if (existing != null)
			{ 
				  existing.Name = tag.Name;	
				existing.DisplayName = tag.DisplayName;
				await bloggieDbContext.SaveChangesAsync();

                return existing;
            }
			else
			{
				return null;
			}
		}
	}
}
