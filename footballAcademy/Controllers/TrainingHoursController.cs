using Basketball.Entity.DTOs.TrainingHours;
using Basketball.Entity.Models;
using Basketball.Service.Services.ServiceTrainingHours;
using Microsoft.AspNetCore.Mvc;

namespace Basketball.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingHoursController : ControllerBase
    {
        private readonly ITrainingHoursService _trainingHoursService;

        public TrainingHoursController(ITrainingHoursService trainingHoursService)
        {
            _trainingHoursService = trainingHoursService;
        }

        // GET: api/User
        [HttpGet]
        public IActionResult GetTrainings(int page = 1, int pageSize = 10)
        {
            var training = _trainingHoursService.GetAllTrainingHourse();

            var totalRecords = training.Count();
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

               var paginatedTrainingHours = training
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
              .Select(training => new TrainingHoursDTO
              {


                  Id = training.Id,
                  TrainingDate = training.TrainingDate.ToString(),
                  TrainingStartTime = training.TrainingStartTime,
                  TrainingFinishTime = training.TrainingFinishTime,
                  CategoryGroupsId = training.CategoryGroupsId,
                  CategoryGroupName = training.CategoryGroup!.Age, // Burada CategoryGroup'un Age özelliğini alıyoruz

                  // Kategoriye bağlı kullanıcı isimlerini çekiyoruz
                 FirstNames = training.CategoryGroup!.Users?.Select(u => u.FirstName ?? string.Empty).ToList() ?? new List<string>(),
                 LastNames = training.CategoryGroup!.Users?.Select(u => u.LastName ?? string.Empty).ToList() ?? new List<string>()


              }).ToList();

            return Ok(new
            {
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize,
                Data = paginatedTrainingHours
            });
        }



        [HttpGet("[action]")]
        public IActionResult GetTrainingHourse(int id)
        {
            var trainingHours = _trainingHoursService.GetTrainingHoursById(id);

            if (trainingHours == null)
                return NotFound();

            var trainingHoursDTO = new TrainingHoursDTO
            {
                Id = trainingHours.Id,
                TrainingDate = trainingHours.TrainingDate.ToString(),
                TrainingStartTime = trainingHours.TrainingStartTime,
                TrainingFinishTime = trainingHours.TrainingFinishTime,
                CategoryGroupsId = trainingHours.CategoryGroupsId,
                CategoryGroupName = trainingHours.CategoryGroup!.Age,

                // Kategoriye bağlı kullanıcı isimlerini çekiyoruz
                FirstNames = trainingHours.CategoryGroup!.Users?.Select(u => u.FirstName ?? string.Empty).ToList() ?? new List<string>(),
              LastNames = trainingHours.CategoryGroup!.Users?.Select(u => u.LastName ?? string.Empty).ToList() ?? new List<string>()
            };

            return Ok(trainingHoursDTO); // Tek bir FilmDTO döndür
        }


        // POST: api/film
        [HttpPost("[action]")]
        public ActionResult<TrainingHours> CreateTrainingHourse([FromBody] TrainingHoursForInsertion trainingHoursForInsertion)
        {
            if (trainingHoursForInsertion == null)
                return BadRequest(); // Geçersiz istek

            var trainingHours = _trainingHoursService.CreateTrainingHours(trainingHoursForInsertion);

            var trainingHoursDto = new TrainingHoursDTO
            {
                Id = trainingHours.Id,
                TrainingDate = trainingHours.TrainingDate.ToString(),
                TrainingStartTime = trainingHours.TrainingStartTime,
                TrainingFinishTime = trainingHours.TrainingFinishTime,
                CategoryGroupsId = trainingHours.CategoryGroupsId,
                CategoryGroupName = trainingHours.CategoryGroup?.Age ?? "Bilinmiyor", // Null kontrolü yapıldı

                // Kategoriye bağlı kullanıcı isimlerini çekiyoruz
                FirstNames = trainingHours.CategoryGroup?.Users?.Select(u => u.FirstName ?? string.Empty).ToList() ?? new List<string>(),
                LastNames = trainingHours.CategoryGroup?.Users?.Select(u => u.LastName ?? string.Empty).ToList() ?? new List<string>()
            };

            return Ok(trainingHoursDto); // Yeni kategori oluşturulunca 201 döndür
        }

        // PUT: api/traininghours/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateTrainingHours(int id, [FromBody] TrainingHoursForUpdate trainingHoursForUpdate)
        {
            // Gelen verinin null olup olmadığını ve ID'nin doğru olup olmadığını kontrol et
            if (trainingHoursForUpdate == null || trainingHoursForUpdate.Id != id)
            {
                return BadRequest("ID uyuşmazlığı: URL'deki ID ile gövdeyi içeren ID uyuşmuyor.");
            }

            try
            {
                var updatedTrainingHours = _trainingHoursService.UpdateTrainingHours(trainingHoursForUpdate);

                if (updatedTrainingHours == null)
                {
                    return NotFound($"TrainingHours ID {id} bulunamadı.");
                }

                return Ok(updatedTrainingHours); // Güncellenmiş veriyi döndür
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}"); // Hata durumunda 500 döndür
            }
        }

        // DELETE: api/Film/{id}
        [HttpDelete("[action]")]
        public ActionResult DeleteTrainingHours(int id)
        {
            _trainingHoursService.DeleteTrainingHours(id);
            return NoContent(); // Silme işlemi başarılıysa 204 döndür
        }
    }
}
