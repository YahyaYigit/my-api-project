using Basketball.Entity.DTOs.Dues;
using Basketball.Entity.Models;
using Basketball.Service.Services.ServiceDues;
using Microsoft.AspNetCore.Mvc;

namespace Basketball.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuesController : ControllerBase
    {
        private readonly IDuesService _duesService;

        public DuesController(IDuesService duesService)
        {
            _duesService = duesService; // Dependency Injection ile servis alınıyor
        }

        // GET: api/Dues
        [HttpGet]
        public IActionResult GetDues(string? search = null, int page = 1, int pageSize = 10)
        {
            var duesGroups = _duesService.GetAllDues();

            var totalRecords = duesGroups.Count();
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

            var paginatedDues = duesGroups
           .Skip((page - 1) * pageSize)
           .Take(pageSize)
           .Select(duesGroups => new
           {
               duesGroups.Id,
              duesGroups.Fee,
              duesGroups.PaymentType,
              duesGroups.Month,
              duesGroups.Year,
               Users = duesGroups.Users.Select(user => new
               {
                   user.Id,
                   user.FirstName,
                   user.LastName,
                   CategoryGroup = user.CategoryGroups?.Age, // Kullanıcının ait olduğu kategori grubu ismi
                   CategoryGroupId = user.CategoryGroups?.Id // Kullanıcının ait olduğu kategori grubu ismi

               }).ToList()
           }).ToList();


            return Ok(new
            {
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize,
                Data = paginatedDues
            });
        }



        // GET: api/dues/{id}
        [HttpGet("{id}")]
        public ActionResult<Dues> GetDues(int id)
        {
            var dues = _duesService.GetDuesById(id);

            if (dues == null)
            {
                return NotFound("Dues bulunamadı");
            }

            var result = new
            {
                dues.Id,
                dues.Fee,
                dues.PaymentType,
                dues.Month,
                dues.Year,
                Users = dues.Users.Select(user => new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    CategoryGroup = user.CategoryGroups?.Age, // Kullanıcının ait olduğu kategori grubu ismi
                     CategoryGroupId = user.CategoryGroups?.Id // Kullanıcının ait olduğu kategori grubu ismi
                }).ToList() // Kullanıcıların sadece Id ve Username bilgisi
            };

            return Ok(result); // Rol bilgisi ve kullanıcı Id ve Username bilgileriyle döndür
        }

        // POST: api/Dues
        [HttpPost]
        public ActionResult<Dues> CreateDues([FromBody] DuesForInsertion duesForInsertion)
        {
            if (duesForInsertion == null)
                return BadRequest(); // Geçersiz istek

            var dues = _duesService.CreateDues(duesForInsertion);
            return CreatedAtAction(nameof(GetDues), new { id = dues.Id }, dues); // Yeni kategori oluşturulunca 201 döndür
        }

        [HttpPut("{id}")]
        public ActionResult UpdateDues([FromBody] DuesForUpdate duesForUpdate, int id)
        {
            if (duesForUpdate == null || duesForUpdate.Id != id)
                return BadRequest("ID uyuşmazlığı."); // ID uyuşmazlığı varsa 400 döndür

            var updatedDues = _duesService.UpdateDues(duesForUpdate);
            return NoContent(); // Güncelleme başarılıysa 204 döndür
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteDues(int id)
        {
            _duesService.DeleteDues(id);
            return NoContent(); // Silme işlemi başarılıysa 204 döndür
        }
    }
}
