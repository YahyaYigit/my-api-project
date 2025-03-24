using Basketball.Entity.DTOs.TrainingHours;
using Basketball.Entity.Models;
using Football.DataAcces.Data;
using Microsoft.EntityFrameworkCore;


namespace Basketball.Service.Services.ServiceTrainingHours
{
    public class TrainingHoursService : ITrainingHoursService
    {
        private readonly SampleDBContext _context;

        public TrainingHoursService(SampleDBContext context)
        {
            _context = context;
        }
        public TrainingHours CreateTrainingHours(TrainingHoursForInsertion trainingHoursForInsertion)
        {
            // CategoryGroupsId doğrulaması
            var categoryGroups = _context.CategoryGroups.FirstOrDefault(r => r.Id == trainingHoursForInsertion.CategoryGroupsId);
            if (categoryGroups == null)
            {
                throw new ArgumentException("Geçersiz CategoryGroupsId.");
            }

            var trainingHours = new TrainingHours
            {
                CategoryGroupsId = trainingHoursForInsertion.CategoryGroupsId,
                CategoryGroup = categoryGroups,

                TrainingStartTime = trainingHoursForInsertion.TrainingStartTime,
                TrainingFinishTime =trainingHoursForInsertion.TrainingFinishTime,
                TrainingDate = trainingHoursForInsertion.TrainingDate,

            };

            _context.TrainingHours.Add(trainingHours); // Yeni kategori ekle
            _context.SaveChanges(); // Değişiklikleri kaydet

            return trainingHours;
        }

        public void DeleteTrainingHours(int id)
        {
            var trainingHourse = GetTrainingHoursById(id);
            if (trainingHourse == null)
            {
                throw new KeyNotFoundException($"trainingHourse ID {id} bulunamadı."); // Hata kontrolü
            }

            trainingHourse.IsDeleted = true; // Silinmiş olarak işaretle
            _context.SaveChanges(); // Değişiklikleri kaydet
        }

        public IEnumerable<TrainingHours> GetAllTrainingHourse()
        {
            return _context.TrainingHours
                .Include(f => f.CategoryGroup)//CategoryGroupu dahil et
                .Include(f => f.CategoryGroup.Users)
                .Where(c => !c.IsDeleted)
                .ToList();
        }

        public TrainingHours GetTrainingHoursById(int id)
        {
            var trainingHours = _context.TrainingHours
              .Include (r => r.CategoryGroup)// İlişkili CategoryGroupu dahil et
              .Include(f => f.CategoryGroup.Users)
              .FirstOrDefault(c => c.Id == id && !c.IsDeleted); // Silinmemiş kategori bul

            if (trainingHours == null)
            {
                throw new Exception("Aidat bulunamadı");
            }

            return trainingHours;
        }

        public TrainingHours UpdateTrainingHours(TrainingHoursForUpdate trainingHoursForUpdate)
        {
            // TrainingHours var mı diye kontrol et
            var existingTrainingHours = _context.TrainingHours.FirstOrDefault(t => t.Id == trainingHoursForUpdate.Id);

            if (existingTrainingHours == null)
            {
                throw new KeyNotFoundException($"TrainingHours ID {trainingHoursForUpdate.Id} bulunamadı.");
            }

            // UserId'yi güncelleme kontrolü
            if (trainingHoursForUpdate.Id != existingTrainingHours.Id)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == trainingHoursForUpdate.Id);
                if (user == null)
                {
                    throw new ArgumentException("Geçersiz UserId.");
                }
                existingTrainingHours.Id = trainingHoursForUpdate.Id;
            }

            // CategoryGroupsId'yi güncelleme kontrolü
            if (trainingHoursForUpdate.CategoryGroupsId != existingTrainingHours.CategoryGroupsId)
            {
                var categoryGroup = _context.CategoryGroups.FirstOrDefault(r => r.Id == trainingHoursForUpdate.CategoryGroupsId);
                if (categoryGroup == null)
                {
                    throw new ArgumentException("Geçersiz CategoryGroupsId.");
                }
                existingTrainingHours.CategoryGroupsId = trainingHoursForUpdate.CategoryGroupsId;
            }

            // Diğer alanları güncelle
            existingTrainingHours.TrainingDate = trainingHoursForUpdate.TrainingDate;
            existingTrainingHours.TrainingStartTime = trainingHoursForUpdate.TrainingStartTime;
            existingTrainingHours.TrainingFinishTime = trainingHoursForUpdate.TrainingFinishTime;
            existingTrainingHours.IsDeleted = trainingHoursForUpdate.IsDeleted;

            _context.SaveChanges(); // Değişiklikleri kaydet

            return existingTrainingHours; // Güncellenmiş veriyi geri döndür
        }

    }
}
