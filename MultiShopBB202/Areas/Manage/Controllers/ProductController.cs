using Microsoft.AspNetCore.Mvc;
using MultiShopBB202.Context;
using MultiShopBB202.Dtos.CategoryDtos;
using MultiShopBB202.Dtos;
using MultiShopBB202.Models;
using MultiShopBB202.Dtos.ProductDtos;
using MultiShopBB202.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MultiShopBB202.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductController : Controller
    {
        private readonly MultiDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(MultiDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            IQueryable<Product> categories = _context.Products.Where(x => !x.IsDeleted);
            GetAllDto<ProductGetDto> getAllDto = new();
            getAllDto.Items = categories.Select(x => new ProductGetDto
            {
                Id = x.Id,
                Name = x.Name,
                InStock = x.InStock,
                Price = x.Price,
                Title = x.Title,
            }).ToList();
            return View(getAllDto.Items);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Discounts = _context.Discounts.ToList();
            ViewBag.Sizes = _context.Sizes.ToList();
            ViewBag.Colors = _context.Colors.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductPostDto postDto)
        {

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Discounts = _context.Discounts.ToList();
            ViewBag.Sizes = _context.Sizes.ToList();
            ViewBag.Colors = _context.Colors.ToList();

            if (postDto.Counts.Count != postDto.SizeIds.Count)
            {
                ModelState.AddModelError("", "Please add valid Count or Size");
                return View();
            }

            if (postDto.FormFile.Count == 0)
            {
                ModelState.AddModelError("", "Images can not be null");
                return View();
            }


            Product product = new Product()
            {
                CategoryId = postDto.CategoryId,
                Description = postDto.Description,
                Information = postDto.Information,
                InStock = postDto.InStock,
                IsDeleted = false,
                Price = postDto.Price,
                Title = postDto.Title,
                Name = postDto.Name,
            };
            product.ProductImages = new List<ProductImage>();
            foreach (var item in postDto.FormFile)
            {
                ProductImage productImage = new ProductImage();

                productImage.Image = item.FileCreate(_env.WebRootPath, "assets/img");
                    productImage.IsDeleted = false;
                    productImage.Product = product;
                productImage.IsMain = false;

                
                if (product.ProductImages.Count == 0)
                    productImage.IsMain = true;


                product.ProductImages.Add(productImage);
            }


            for (int i = 0; i < postDto.SizeIds.Count; i++)
            {
                ProductSize productSize = new ProductSize
                {
                    SizeId = postDto.SizeIds[i],
                    IsDeleted = false,
                    Product = product,
                    Count = postDto.Counts[i],
                };
                _context.ProductSizes.Add(productSize);
            }



            for (int i = 0; i < postDto.SizeIds.Count; i++)
            {
                ProductColor productColor = new ProductColor
                {
                    ColorId = postDto.ColorIds[i],
                    IsDeleted = false,
                    Product = product,
                    Count = postDto.Counts[i],
                };
                _context.ProductColors.Add(productColor);
            }


            if (postDto.DiscountId != null)
                product.DiscountId = postDto.DiscountId;


            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Discounts = _context.Discounts.ToList();
            ViewBag.Sizes = _context.Sizes.ToList();
            ViewBag.Colors = _context.Colors.ToList();

            Product? product = _context.Products.Include(x=>x.ProductImages).Include(X=>X.Category)
                .Include(X=>X.Discount).Include(x=>x.ProductSizes).ThenInclude(x=>x.Size).Include(x=>x.ProductColors)
                .ThenInclude(x=>x.Color)
                .FirstOrDefault(x=>x.Id==id);

            if (product == null)
                return NotFound();


            ProductUpdateDto productUpdateDto = new ProductUpdateDto
            {
                productGetDto = new ProductGetDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    ProductImages = product.ProductImages.Select(x => x.Image).ToList(),
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    InStock = product.InStock,
                    Information = product.Information,
                    Price = product.Price,
                    Title = product.Title,
                    ColorIds = product.ProductColors.Select(x => x.ColorId).ToList(),
                    SizeIds = product.ProductSizes.Select(x => x.SizeId).ToList(),
                    Counts = product.ProductSizes.Select(x=>x.Count).ToList(),
                }
            };

            return View(productUpdateDto);
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
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
