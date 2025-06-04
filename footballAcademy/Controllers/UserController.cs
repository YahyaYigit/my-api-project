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

        public IActionResult GetUsers(int page = 1, int pageSize = 10, string search = "")

        {

            // Kullanıcıları alıyoruz

            var users = _userService.GetAllUsers();

            // Arama yapıyorsanız, FirstName veya LastName'e göre filtreleme ekliyoruz

            if (!string.IsNullOrEmpty(search))

            {

                users = users.Where(user =>

                    (user.FirstName != null && user.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||

                    (user.LastName != null && user.LastName.Contains(search, StringComparison.OrdinalIgnoreCase))

                ).ToList();

            }

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

                    IsAdmin = user.IsAdmin,

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

                    BirthPlace = user.BirthPlace,

                    HealthProblem = user.HealthProblem,

                    Height = user.Height,

                    Weight = user.Weight,

                    School = user.School,

                    TcNo = user.TcNo,

                    IsAcceptedWhatsappGroup = user.IsAcceptedWhatsappGroup,

                    IsAcceptedFatherWhatsappGroup = user.IsAcceptedFatherWhatsappGroup,

                    IsAcceptedMotherWhatsappGroup = user.IsAcceptedMotherWhatsappGroup,

                    AcceptedImportant = user.AcceptedImportant,

                    AcceptedKVKK = user.AcceptedKVKK,

                    // MonthlyFees sözlüğünü dolduruyoruz

                    MonthlyFees = user.Dues != null ? user.Dues.ToDictionary(

                        d => $"{d.Month}-{d.Year}", // Anahtar: Ay-Yıl formatında

                        d => $"{d.PaymentType} - {d.Fee} TL") // Değer: PaymentType ve Fee bilgisi

                        : new Dictionary<string, string>()

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

                    IsAdmin = user.IsAdmin,

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

                    BirthPlace = user.BirthPlace,

                    HealthProblem = user.HealthProblem,

                    Height = user.Height,

                    Weight = user.Weight,

                    School = user.School,

                    TcNo = user.TcNo,

                    IsAcceptedWhatsappGroup = user.IsAcceptedWhatsappGroup,

                    IsAcceptedFatherWhatsappGroup = user.IsAcceptedFatherWhatsappGroup,

                    IsAcceptedMotherWhatsappGroup = user.IsAcceptedMotherWhatsappGroup,

                    AcceptedImportant = user.AcceptedImportant,

                    AcceptedKVKK = user.AcceptedKVKK,

                    IsDeleted = user.IsDeleted, // Silinmiş kullanıcı bilgisi   

                    // MonthlyFees sözlüğünü dolduruyoruz

                    MonthlyFees = user.Dues != null ? user.Dues.ToDictionary(

            d => $"{d.Month}-{d.Year}", // Anahtar: Ay-Yıl formatında

            d => $"{d.PaymentType} - {d.Fee} TL") // Değer: PaymentType ve Fee bilgisi

        : new Dictionary<string, string>()

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

                IsAdmin = user.IsAdmin,

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

                BirthPlace = user.BirthPlace,

                HealthProblem = user.HealthProblem,

                Height = user.Height,

                Weight = user.Weight,

                School = user.School,

                TcNo = user.TcNo,

                IsAcceptedWhatsappGroup = user.IsAcceptedWhatsappGroup,

                IsAcceptedFatherWhatsappGroup = user.IsAcceptedFatherWhatsappGroup,

                IsAcceptedMotherWhatsappGroup = user.IsAcceptedMotherWhatsappGroup,

                AcceptedImportant = user.AcceptedImportant,

                AcceptedKVKK = user.AcceptedKVKK,

                // MonthlyFees sözlüğünü dolduruyoruz

                MonthlyFees = user.Dues != null ? user.Dues.ToDictionary(

            d => $"{d.Month}-{d.Year}", // Anahtar: Ay-Yıl formatında

            d => $"{d.PaymentType} - {d.Fee} TL") // Değer: PaymentType ve Fee bilgisi

        : new Dictionary<string, string>()

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

