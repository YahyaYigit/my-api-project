using Basketball.Entity.Models;


namespace Basketball.Service.Services.ServiceTrainingHours
{
    public interface ITrainingHoursService
    {
        IEnumerable<TrainingHours> GetAllTrainingHourse(); // Tüm TrainingHours DTO'larını getir
        TrainingHours GetTrainingHoursById(int id); // ID'ye göre TrainingHours Getir
        TrainingHours CreateTrainingHours(Basketball.Entity.DTOs.TrainingHours.TrainingHoursForInsertion trainingHoursForInsertion); // Yeni TrainingHours oluşturur
        TrainingHours UpdateTrainingHours(Basketball.Entity.DTOs.TrainingHours.TrainingHoursForUpdate trainingHoursForUpdate); // TrainingHours güncelle
        void DeleteTrainingHours(int id); // TrainingHours sil
    }
}
