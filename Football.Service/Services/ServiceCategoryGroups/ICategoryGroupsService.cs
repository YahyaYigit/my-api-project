using Basketball.Entity.DTOs.CategoryGroups;
using Basketball.Entity.Models;

namespace Basketball.Service.Services.ServiceCategoryGroups
{
    public interface ICategoryGroupsService
    {
        IEnumerable<CategoryGroups> GetAllCategoryGroups(); // Tüm kategorileri Getir
        CategoryGroups GetCategoryGroupsById(int id); // ID'ye göre kategori Getir
        CategoryGroups CreateCategoryGroups(CategoryGroupsForInsertion categoryGroups); // Yeni kategori oluşturur
        CategoryGroups UpdateCategoryGroups(CategoryGroupsForUpdate categoryGroups); // Kategoriyi güncelle
        void DeleteCategoryGroups(int id); // Kategoriyi sil
    }
}
