using Microsoft.AspNetCore.Mvc;
using MultiShopBB202.Context;
using MultiShopBB202.Dtos.SizeDtos;
using MultiShopBB202.Dtos;
using MultiShopBB202.Models;
using MultiShopBB202.Dtos.CategoryDtos;
using MultiShopBB202.Extentions;
using Microsoft.AspNetCore.Authorization;

namespace MultiShopBB202.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CategoryController : Controller
    {
        private readonly MultiDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(MultiDbContext context, IWebHostEnvironment env = null)
        {
            _context = context;
            _env = env;
        }
       

        public IActionResult Index()
        {
            IQueryable<Category> categories = _context.Categories.Where(x => !x.IsDeleted);
            GetAllDto<CategoryGetDto> getAllDto = new();
            getAllDto.Items = categories.Select(x => new CategoryGetDto { Id = x.Id, Name=x.Name,IMage=x.Image}).ToList();

            return View(getAllDto.Items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryPostDto postDto)
        {
            _context.Categories.Add(new Category { IsDeleted = false, Name = postDto.Name,
                Image=postDto.formFile.FileCreate(_env.WebRootPath,"assets/img") });
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            Category? category = _context.Categories.Find(id);

            if (category == null)
                return NotFound();


            CategoryUpdateDto categoryUpdateDto = new CategoryUpdateDto
            {
                categoryGetDto = new CategoryGetDto { Id = category.Id, Name = category.Name,IMage=category.Image}
            };

            return View(categoryUpdateDto);
        }

        [HttpPost]
        public IActionResult Update(CategoryUpdateDto updateDto)
        {

            Category? category = _context.Categories.Find(updateDto.categoryGetDto.Id);

            if (category == null)
                return NotFound();


            category.Name = updateDto.categoryPostDto.Name;

            if (updateDto.categoryPostDto.formFile != null)
            {
                category.Image = updateDto.categoryPostDto.formFile.FileCreate(_env.WebRootPath, "assets/img");
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
