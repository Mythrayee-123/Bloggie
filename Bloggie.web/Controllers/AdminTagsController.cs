using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModel;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
     
        public async Task<IActionResult> AddAsync(AddTagRequest addTagRequest)
        {//mapping.Repository does not have a definition of ass tag view model
            //we are mapping add tag to Tag
            //Db context have the definition of Tags Model.not the view Model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,

            };
           await tagRepository.AddAsync(tag);
            return RedirectToAction("List");
        }
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> ListAsync()
        {
         var result =await tagRepository.GetAllAsync();
            return View(result);
        }
		[HttpGet]
		public async Task<IActionResult> EditAsync(Guid id)
		{
            var tag=await tagRepository.GetAsync(id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };

                return View(editTagRequest);
        }
           
            return View(null);
		}
        [HttpPost]
        public async Task<IActionResult> EditAsync(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };
            var result=await tagRepository.UpdateAsync(tag);
            if (result != null)
            {
                //Show success notification
            }
            else
            {
                //error notification
            }
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
        [HttpPost]
           
        public async Task<IActionResult> DeleteAsync(EditTagRequest editTagRequest)
        {
         var deletedtag=  await tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedtag != null)
            {
                //success notification
                return RedirectToAction("List");
            }
            //Show error
            return RedirectToAction("Edit",new { id = editTagRequest.Id });
        }
    }
}
