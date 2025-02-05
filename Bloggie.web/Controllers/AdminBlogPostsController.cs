using Bloggie.web.Models.ViewModel;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bloggie.web.Models.Domain;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Identity.Client;


namespace Bloggie.web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository,
            IBlogPostRepository blogPostRepository )
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
           var tags = await tagRepository.GetAllAsync();
            //mapp the tag to blog vie model

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
            return View(model);
        }
        [HttpPost]  
        public async Task<IActionResult>Add(AddBlogPostRequest addBlogPostRequest)
        {
            //need to map  add blog post viewmodel to blog domain model
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
            };
            //map the tags
            var selectedTags = new List<Tag>();
            //loop through to get the tags from the database
            foreach (var selectedTagId  in addBlogPostRequest.SelectedTags)
            {
             //need to parse the guid to a string
                var selectedTagasGuid=Guid.Parse(selectedTagId);
                //get the selected tags from database
               var existingTags= await tagRepository.GetAsync(selectedTagasGuid);
                if (existingTags != null)
                {
                    selectedTags.Add(existingTags);  
                }
            }
          blogPost.tags = selectedTags;
            await blogPostRepository.AddAsync(blogPost);
            return RedirectToAction("Add");
        }
        [HttpGet]
        public async Task<IActionResult>List()
        {
         var blogPost=  await blogPostRepository.GetAllAsync();

            return View(blogPost);
        }
        [HttpGet]
        public async Task<ActionResult>Edit(Guid id)
        {
            //retrieve the result from the repository
           var blogs= await blogPostRepository.GetAsync(id);
            var tagsDomain=await tagRepository.GetAllAsync();
            //map the domain model to view
            if (blogs != null)
            {
                var editblogRequest = new EditBlogPostRequest
                {
                    Id = blogs.Id,
                    Heading = blogs.Heading,
                    PageTitle = blogs.PageTitle,
                    Content = blogs.Content,
                    ShortDescription = blogs.ShortDescription,
                    FeaturedImageUrl = blogs.FeaturedImageUrl,
                    UrlHandle = blogs.UrlHandle,
                    PublishedDate = blogs.PublishedDate,
                    Author = blogs.Author,
                    Visible = blogs.Visible,
                    Tags = tagsDomain.Select(x => new SelectListItem
                    {
                        Text = x.Name, Value = x.Id.ToString()

                    }),
                    SelectedTags = blogs.tags.Select(x => x.Id.ToString()).ToArray(),
                };
                return View(editblogRequest);
            }
             return View(null);
        }
        [HttpPost]
        public async Task<IActionResult>Edit(EditBlogPostRequest editBlogPostRequest)
        {
            //map the viewmodel to the domain
            var blogPostDomain = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                Author = editBlogPostRequest.Author,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                PublishedDate = editBlogPostRequest.PublishedDate,
                UrlHandle = editBlogPostRequest.UrlHandle,
                Visible = editBlogPostRequest.Visible,

            };
            var selectedTags=new List<Tag>();
            foreach (var selectedid in editBlogPostRequest.SelectedTags)
            {
                var selectedidasGuid = Guid.Parse(selectedid);
                var found = await tagRepository.GetAsync(selectedidasGuid);
                if (found != null)
                {
                    selectedTags.Add(found);
                }
            }
                blogPostDomain.tags = selectedTags;
            //submit the info to the repository
            var result=await blogPostRepository.UpdateAsync(blogPostDomain);
            if (result != null) 
                {
                    //return redirect
                    return RedirectToAction("Edit");
                }
             else
                {
                 return RedirectToAction("Edit");
            }

           
           
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            var blogfound=await blogPostRepository.DeleteAsync(editBlogPostRequest.Id);
            if (blogfound != null)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Edit" ,new {id= editBlogPostRequest.Id });   
            }
        }
    }
}
