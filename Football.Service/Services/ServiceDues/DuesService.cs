using Basketball.Entity.DTOs.Dues;
using Basketball.Entity.Models;
using Football.DataAcces.Data;
using Microsoft.EntityFrameworkCore;


namespace Basketball.Service.Services.ServiceDues
{
    public class DuesService : IDuesService
    {
        private readonly SampleDBContext _context;

        public DuesService(SampleDBContext context)
        {
            _context = context;
        }

        public Dues CreateDues(DuesForInsertion duesForInsertion)
        {
            if (string.IsNullOrWhiteSpace(duesForInsertion.Fee.ToString()))
            {
                throw new ArgumentException("Ücret alanı zorunludur."); // Hata kontrolü
            }
            var dues = new Dues
            {
                Fee = duesForInsertion.Fee,
                PaymentType = duesForInsertion.PaymentType,
                Month = duesForInsertion.Month,
                Year = duesForInsertion.Year,
            };

            _context.Dues.Add(dues); // Yeni kategori ekle
            _context.SaveChanges(); // Değişiklikleri kaydet

            return dues;
        }

        public void DeleteDues(int id)
        {
            var dues = GetDuesById(id);
            if (dues == null)
            {
                throw new KeyNotFoundException($"Aidat ID {id} bulunamadı."); // Hata kontrolü
            }

            dues.IsDeleted = true; // Silinmiş olarak işaretle
            _context.SaveChanges(); // Değişiklikleri kaydet
        }

        public IEnumerable<Dues> GetAllDues()
        {
            var dues = _context.Dues
                .Include(d => d.Users) // Kullanıcıları dahil et
                .ThenInclude(u => u.CategoryGroups) // Kullanıcıların CategoryGroup ilişkisini dahil et
                .Where(d => !d.IsDeleted) // Silinmemiş kategorileri filtrele
                .ToList();

            return dues;
        }

        public Dues GetDuesById(int id)
        {
            var dues = _context.Dues
                .Include(d => d.Users) // Kullanıcıları dahil et
                .ThenInclude(u => u.CategoryGroups) // Kullanıcıların CategoryGroup ilişkisini dahil et
                .FirstOrDefault(d => d.Id == id && !d.IsDeleted); // Silinmemiş kategori bul

            if (dues == null)
            {
                throw new Exception("Aidat bulunamadı");
            }

            return dues;
        }


        public Dues UpdateDues(DuesForUpdate duesForUpdate)
        {
            var existingDues = GetDuesById(duesForUpdate.Id);
            if (existingDues == null)
            {
                throw new KeyNotFoundException($"Aidat ID {duesForUpdate.Id} bulunamadı."); // Hata kontrolü
            }

            existingDues.Fee = duesForUpdate.Fee;
            existingDues.PaymentType = duesForUpdate.PaymentType;
            existingDues.Year = duesForUpdate.Year;
            existingDues.Month = duesForUpdate.Month;
            existingDues.IsDeleted = duesForUpdate.IsDeleted;

            if (_context.SaveChanges() == 0)
            {
                throw new Exception("Güncelleme işlemi başarısız oldu.");
            }

            return existingDues; // Güncellenen kategoriyi döndür
        }
    }
}
