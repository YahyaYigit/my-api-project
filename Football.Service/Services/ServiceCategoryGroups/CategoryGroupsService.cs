using Basketball.Entity.DTOs.CategoryGroups;
using Basketball.Entity.Models;
using Football.DataAcces.Data;
using Microsoft.EntityFrameworkCore;

namespace Basketball.Service.Services.ServiceCategoryGroups
{
    public class CategoryGroupsService : ICategoryGroupsService
    {
        private readonly SampleDBContext _context;

        public CategoryGroupsService(SampleDBContext context)
        {
            _context = context;
        }

        public CategoryGroups CreateCategoryGroups(CategoryGroupsForInsertion categoryGroups)
        {
            if (string.IsNullOrWhiteSpace(categoryGroups.Age))
            {
                throw new ArgumentException("Tür alanı zorunludur."); // Hata kontrolü
            }
            var category = new CategoryGroups
            {
                Age = categoryGroups.Age,
            };

            _context.CategoryGroups.Add(category); // Yeni kategori ekle
            _context.SaveChanges(); // Değişiklikleri kaydet

            return category;
        }

        public void DeleteCategoryGroups(int id)
        {
            var category = GetCategoryGroupsById(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Kategori ID {id} bulunamadı."); // Hata kontrolü
            }

            category.IsDeleted = true; // Silinmiş olarak işaretle
            _context.SaveChanges(); // Değişiklikleri kaydet
        }

        public IEnumerable<CategoryGroups> GetAllCategoryGroups()
        {
           var categoryGroups = _context.CategoryGroups
                .Include(r => r.Users) // İlişkili kullanıcıları dahil et
                .Where(c => !c.IsDeleted) // Silinmemiş kategorileri filtrele
                .ToList();

            return categoryGroups;
        }

        public CategoryGroups GetCategoryGroupsById(int id)
        {
            var categoryGroups = _context.CategoryGroups
                .Include(r => r.Users) // İlişkili kullanıcıları dahil et
                .FirstOrDefault(c => c.Id == id && !c.IsDeleted); // Silinmemiş kategori bul

            if (categoryGroups == null)
            {
                throw new Exception("Kategori bulunamadı");
            }

            return categoryGroups;
        }

        public CategoryGroups UpdateCategoryGroups(CategoryGroupsForUpdate categoryGroups)
        {
            var existingCategory = GetCategoryGroupsById(categoryGroups.Id);


            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Kategori ID {categoryGroups.Id} bulunamadı."); // Hata kontrolü
            }



            existingCategory.Age = categoryGroups.Age;
            existingCategory.IsDeleted = categoryGroups.IsDeleted;// Tür güncelle


            _context.SaveChanges(); // Değişiklikleri kaydet

            return existingCategory; // Güncellenen kategoriyi döndür
        }
    }
}
