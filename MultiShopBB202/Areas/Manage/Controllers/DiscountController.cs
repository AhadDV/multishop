using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShopBB202.Context;
using MultiShopBB202.Dtos;
using MultiShopBB202.Dtos.DiscountDtos;
using MultiShopBB202.Models;

namespace MultiShopBB202.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DiscountController:Controller
    {
        private readonly MultiDbContext _context;

        public DiscountController(MultiDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IQueryable<Discount> discounts = _context.Discounts.Where(x => !x.IsDeleted);
            GetAllDto<DiscountGetDto> getAllDto = new();
            getAllDto.Items = discounts.Select(x => new DiscountGetDto { Id = x.Id, Percent = x.Percent }).ToList();

            return View(getAllDto.Items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DiscountPostDto postDto)
        {
            _context.Discounts.Add(new Discount { IsDeleted = false,Percent=postDto.Percent });
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            Discount? discount = _context.Discounts.Find(id);

            if (discount == null)
                return NotFound();


            DiscountUpdateDto discountUpdateDto = new DiscountUpdateDto
            {
                discountGetDto = new DiscountGetDto { Id = discount.Id, Percent = discount.Percent }
            };

            return View(discountUpdateDto);
        }

        [HttpPost]
        public IActionResult Update(DiscountUpdateDto updateDto)
        {

            Discount? discount = _context.Discounts.Find(updateDto.discountGetDto.Id);

            if (discount == null)
                return NotFound();

            discount.Percent = updateDto.discountPostDto.Percent;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
