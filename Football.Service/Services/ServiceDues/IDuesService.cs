using Basketball.Entity.DTOs.Dues;
using Basketball.Entity.Models;

namespace Basketball.Service.Services.ServiceDues
{
    public interface IDuesService
    {
        IEnumerable<Dues> GetAllDues(); // Tüm kategorileri Getir
        Dues GetDuesById(int id); // ID'ye göre kategori Getir
        Dues CreateDues(DuesForInsertion duesForInsertion); // Yeni kategori oluşturur
        Dues UpdateDues(DuesForUpdate duesForUpdate); // Kategoriyi güncelle
        void DeleteDues(int id);
    }
}
