using Basketball.Entity.DTOs.Attendance;
using Basketball.Entity.Models;
using Basketball.Service.Services.ServiceAttendance;
using Microsoft.AspNetCore.Mvc;


namespace Basketball.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public IActionResult GetAttendances(int page = 1, int pageSize = 10)
        {
            var attendance = _attendanceService.GetAllAttendance();

            var totalRecords = attendance.Count();
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
            var paginatedAttendance = attendance
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(attendance => new AttendanceDTO
                {
                    Id = attendance.Id,
                    Date = attendance.Date,
                    Status = attendance.Status.ToString(),
                    UserId = attendance.UserId,
                    FirstName = attendance.Users?.FirstName,  // Users üzerinden FirstName almak
                    LastName = attendance.Users?.LastName    // Users üzerinden LastName almak
                })
                .ToList();


            return Ok(new
            {
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize,
                Data = paginatedAttendance
            });
        }



        [HttpGet("[action]")]
        public IActionResult GetAttendance(int id)
        {
            var attendance = _attendanceService.GetAttendanceById(id);

            if (attendance == null)
                return NotFound();

            var attendanceDTO = new AttendanceDTO
            {
                Id = attendance.Id,
                Date = attendance.Date,
                Status = attendance.Status.ToString(),
                UserId = attendance.UserId,
                FirstName = attendance.Users?.FirstName,  // Users üzerinden FirstName almak
                LastName = attendance.Users?.LastName    // Users üzerinden LastName almak
            };

            return Ok(attendanceDTO); // Tek bir FilmDTO döndür
        }


        // POST: api/film
        [HttpPost("[action]")]
        public ActionResult<Attendance> CreateAttendance([FromBody] AttendanceForInsertion attendanceForInsertion)
        {
            if (attendanceForInsertion == null)
                return BadRequest(); // Geçersiz istek

            var attendance = _attendanceService.CreateAttendance(attendanceForInsertion);

            var attendanceDTO = new AttendanceDTO
            {
                Id = attendance.Id,
                Date = attendance.Date,
                Status = attendance.Status.ToString(),
                UserId = attendance.UserId,
                FirstName = attendance.Users?.FirstName,  // Users üzerinden FirstName almak
                LastName = attendance.Users?.LastName    // Users üzerinden LastName almak
            };

            return Ok(attendanceDTO); // Yeni kategori oluşturulunca 201 döndür
        }


        // PUT: api/traininghours/{id}
        [HttpPut("{id}")]
        public ActionResult UpdatAttendance(int id, [FromBody] AttendanceForUpdate attendanceForUpdate)
        {
            // Gelen verinin null olup olmadığını ve ID'nin doğru olup olmadığını kontrol et
            if (attendanceForUpdate == null || attendanceForUpdate.Id != id)
            {
                return BadRequest("ID uyuşmazlığı: URL'deki ID ile gövdeyi içeren ID uyuşmuyor.");
            }

            try
            {
                var updatedAttendance = _attendanceService.UpdateAttendance(attendanceForUpdate);

                if (updatedAttendance == null)
                {
                    return NotFound($"attendance ID {id} bulunamadı.");
                }

                return Ok(updatedAttendance); // Güncellenmiş veriyi döndür
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}"); // Hata durumunda 500 döndür
            }
        }

        // DELETE: api/Film/{id}
        [HttpDelete("[action]")]
        public ActionResult DeleteAttendance(int id)
        {
            _attendanceService.DeleteAttendance(id);
            return NoContent(); // Silme işlemi başarılıysa 204 döndür
        }


    }
}
