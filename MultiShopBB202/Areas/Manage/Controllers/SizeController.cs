using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShopBB202.Context;
using MultiShopBB202.Dtos;
using MultiShopBB202.Dtos.DiscountDtos;
using MultiShopBB202.Dtos.SizeDtos;
using MultiShopBB202.Models;

namespace MultiShopBB202.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class SizeController:Controller
    {
        private readonly MultiDbContext _context;

        public SizeController(MultiDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IQueryable<Size> sizes = _context.Sizes.Where(x => !x.IsDeleted);
            GetAllDto<SizeGetDto> getAllDto = new();
            getAllDto.Items = sizes.Select(x => new SizeGetDto { Id = x.Id, Value = x.Value }).ToList();

            return View(getAllDto.Items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SizePostDto postDto)
        {
            _context.Sizes.Add(new Size { IsDeleted = false,Value =postDto.Value });
            _context.SaveChanges();
            return RedirectToAction(nameof(Index)); 
        }

        public IActionResult Update(int id)
        {
            Size? size = _context.Sizes.Find(id);

            if (size == null)
                return NotFound();


            SizeUpdateDto sizeUpdateDto = new SizeUpdateDto
            {
                sizeGetDto = new SizeGetDto { Id = size.Id, Value = size.Value}
            };

            return View(sizeUpdateDto);
        }

        [HttpPost]
        public IActionResult Update(SizeUpdateDto updateDto)
        {

            Size? size = _context.Sizes.Find(updateDto.sizeGetDto.Id);

            if (size == null)
                return NotFound();


            size.Value = updateDto.sizePostDto.Value;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult GetSize()
        {
           List<Size> sizes= _context.Sizes.ToList();

            return Json(sizes);
        }
    }
}
