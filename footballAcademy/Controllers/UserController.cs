using Basketball.Entity.DTOs.User;
using Basketball.Service.Services.ServiceUser;
using Microsoft.AspNetCore.Mvc;

namespace Basketball.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult GetUsers(int page = 1, int pageSize = 10)
        {
            var users = _userService.GetAllUsers();
            var totalRecords = users.Count();
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

            var paginatedUsers = users
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(user => new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    // CategoryGroups kontrolü
                    CategoryGroupsId = user.CategoryGroups != null ? user.CategoryGroups.Id : 0, // nullsa 0 döndür
                    CategoryGroups = user.CategoryGroups != null ? user.CategoryGroups.Age.ToString() : "Bilgi Yok", // nullsa "Bilgi Yok" döndür
                    BirtDay = user.BirtDay,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    MotherName = user.MotherName,
                    MotherPhoneNumber = user.MotherPhoneNumber,
                    FatherName = user.FatherName,
                    FatherPhoneNumber = user.FatherPhoneNumber,
                    Email = user.Email,
                    
                    // Dues kontrolü
                    MonthlyFees = user.Dues != null
                        ? new Dictionary<string, string>
                        {
                    { $"{user.Dues.Month}/{user.Dues.Year}", $"Amount: {user.Dues.Fee}, Dues: {user.Dues.PaymentType}" }
                        }
                        : new Dictionary<string, string>() // Eğer Dues bilgisi yoksa boş bir dictionary döndür
                }).ToList();

            return Ok(new
            {
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize,
                Data = paginatedUsers
            });
        }





        // GET: api/User/deleted
        [HttpGet("getAllDeleted")]
        public IActionResult GetDeletedUsers(int page = 1, int pageSize = 10)
        {
            // Silinmiş kullanıcıları almak için servis metodunu kullanıyoruz.
            var users = _userService.GetAllDeleteUsers();

            var totalRecords = users.Count();
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

            // Sayfalama işlemini gerçekleştiriyoruz.
            var paginatedUsers = users
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(user => new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CategoryGroupsId = user.CategoryGroups != null ? user.CategoryGroups.Id : 0, // Null kontrolü
                    CategoryGroups = user.CategoryGroups != null ? user.CategoryGroups.Age.ToString() : "Bilgi Yok", // Null kontrolü

                    BirtDay = user.BirtDay,
                    Address = user.Address ?? "Bilgi Yok", // Address nullsa "Bilgi Yok" göster
                    PhoneNumber = user.PhoneNumber ?? "Bilgi Yok", // PhoneNumber nullsa "Bilgi Yok" göster
                    MotherName = user.MotherName ?? "Bilgi Yok", // MotherName nullsa "Bilgi Yok" göster
                    MotherPhoneNumber = user.MotherPhoneNumber ?? "Bilgi Yok", // MotherPhoneNumber nullsa "Bilgi Yok" göster
                    FatherName = user.FatherName ?? "Bilgi Yok", // FatherName nullsa "Bilgi Yok" göster
                    FatherPhoneNumber = user.FatherPhoneNumber ?? "Bilgi Yok", // FatherPhoneNumber nullsa "Bilgi Yok" göster
                    Email = user.Email,

                    // Dues kontrolü
                    MonthlyFees = user.Dues != null
                        ? new Dictionary<string, string>
                        {
                    { $"{user.Dues.Month}/{user.Dues.Year}", $"Amount: {user.Dues.Fee}, Dues: {user.Dues.PaymentType}" }
                        }
                        : new Dictionary<string, string>() // Eğer Dues bilgisi yoksa boş bir dictionary döndür
                })
                .ToList();

            return Ok(new
            {
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize,
                Data = paginatedUsers
            });
        }




        // Tekil kullanıcıyı döndüren metod
        [HttpGet("[action]")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUserById(id);

            if (user == null)
                return NotFound();

            var userDTO = new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,

                CategoryGroupsId = user.CategoryGroups != null ? user.CategoryGroups.Id : 0, // Null kontrolü
                CategoryGroups = user.CategoryGroups != null ? user.CategoryGroups.Age.ToString() : "Bilgi Yok", // Null kontrolü

                BirtDay = user.BirtDay,
                Address = user.Address ?? "Bilgi Yok", // Address nullsa "Bilgi Yok" göster
                PhoneNumber = user.PhoneNumber ?? "Bilgi Yok", // PhoneNumber nullsa "Bilgi Yok" göster
                MotherName = user.MotherName ?? "Bilgi Yok", // MotherName nullsa "Bilgi Yok" göster
                MotherPhoneNumber = user.MotherPhoneNumber ?? "Bilgi Yok", // MotherPhoneNumber nullsa "Bilgi Yok" göster
                FatherName = user.FatherName ?? "Bilgi Yok", // FatherName nullsa "Bilgi Yok" göster
                FatherPhoneNumber = user.FatherPhoneNumber ?? "Bilgi Yok", // FatherPhoneNumber nullsa "Bilgi Yok" göster
                Email = user.Email,

                // Dues kontrolü
                MonthlyFees = user.Dues != null
                    ? new Dictionary<string, string>
                    {
                { $"{user.Dues.Month}/{user.Dues.Year}", $"Amount: {user.Dues.Fee}, Dues: {user.Dues.PaymentType}" }
                    }
                    : new Dictionary<string, string>() // Eğer Dues bilgisi yoksa boş bir dictionary döndür
            };

            return Ok(userDTO); // Tek bir UserDTO döndür
        }




        // PUT: api/Film/{id}
        [HttpPut("[action]")]
        public ActionResult UpdateUser([FromBody] UserForUpdate userForUpdate)
        {
            if (userForUpdate == null || userForUpdate.Id != userForUpdate.Id)
                return BadRequest(); // ID uyuşmazlığı varsa 400 döndür

            var updatedUser = _userService.UpdateUser(userForUpdate);
            return Ok(updatedUser); // Güncelleme başarılıysa 204 döndür
        }
        // DELETE: api/Film/{id}
        [HttpDelete("[action]")]
        public ActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return NoContent(); // Silme işlemi başarılıysa 204 döndür
        }
    
}
}
