using Bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Bloggie.web.Repositories
{
	public interface ITagRepository
	{
		Task<IEnumerable<Tag>> GetAllAsync();
		Task<Tag?>GetAsync(Guid id);
		Task<Tag> AddAsync(Tag tag);
		Task<Tag?> UpdateAsync(Tag tag);
		Task<Tag?> DeleteAsync(Guid id);
	}
}
