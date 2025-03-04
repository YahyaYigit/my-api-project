using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Basketball.Service.Services.ServiceCategoryGroups;
using Basketball.Entity.DTOs.CategoryGroups;
using Basketball.Entity.Models;

namespace Basketball.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryGroupsController : ControllerBase
    {
        private readonly ICategoryGroupsService _categoryService;

        public CategoryGroupsController(ICategoryGroupsService categoryService)
        {
            _categoryService = categoryService; // Dependency Injection ile servis alınıyor
        }

        // GET: api/Category
        [HttpGet]
        public IActionResult GetCategories(string? search = null, int page = 1, int pageSize = 10)
        {
            var categoryGroups = _categoryService.GetAllCategoryGroups();

            var totalRecords = categoryGroups.Count();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            if (page > totalPages)
            {
                return NotFound(new
                {
                    Message = "Belirtilen sayfada görüntülenecek veri yok.",
                    Page = page,
                    PageSize = pageSize,
                    TotalRecords = totalRecords
                });
            }

            var paginatedCategoryGroups = categoryGroups
           .Skip((page - 1) * pageSize)
           .Take(pageSize)
           .Select(categoryGroup => new
           {
              categoryGroup.Id,
              categoryGroup.Age,
              Users = categoryGroup.Users.Select(user => new
              {
                  user.Id,
                  user.FirstName,
                  user.LastName

              }).ToList()
           }).ToList();


            return Ok(new
            {
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize,
                Data = paginatedCategoryGroups
            });
        }



        // GET: api/category/{id}
        [HttpGet("{id}")]
        public ActionResult<CategoryGroups> GetCategory(int id)
        {
            var categoryGroups = _categoryService.GetCategoryGroupsById(id);

            if (categoryGroups == null)
            {
                return NotFound("Category Groups bulunamadı");
            }

            var result = new
            {
                categoryGroups.Id,
                categoryGroups.Age,
                Users = categoryGroups.Users.Select(user => new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName
                }).ToList() // Kullanıcıların sadece Id ve Username bilgisi
            };

            return Ok(result); // Rol bilgisi ve kullanıcı Id ve Username bilgileriyle döndür
        }

        // POST: api/category
        [HttpPost]
        public ActionResult<CategoryGroups> CreateCategory([FromBody] CategoryGroupsForInsertion categoryForInsertion)
        {
            if (categoryForInsertion == null)
                return BadRequest(); // Geçersiz istek

            var category = _categoryService.CreateCategoryGroups(categoryForInsertion);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category); // Yeni kategori oluşturulunca 201 döndür
        }

        // PUT: api/category/{id}
        [HttpPut]
        public ActionResult UpdateCategory([FromBody] CategoryGroupsForUpdate categoryForUpdate)
        {
            if (categoryForUpdate == null || categoryForUpdate.Id != categoryForUpdate.Id)
                return BadRequest(); // ID uyuşmazlığı varsa 400 döndür

            var updatedCategory = _categoryService.UpdateCategoryGroups(categoryForUpdate);
            return Ok(updatedCategory); // Güncelleme başarılıysa 204 döndür
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            _categoryService.DeleteCategoryGroups(id);
            return NoContent(); // Silme işlemi başarılıysa 204 döndür
        }
    }
}
