using Basketball.Entity.DTOs.Attendance;
using Basketball.Entity.Models;


namespace Basketball.Service.Services.ServiceAttendance
{
    public interface IAttendanceService
    {
        IEnumerable<Attendance> GetAllAttendance(); // Tüm Attendanceyi Getir
        Attendance GetAttendanceById(int id); // ID'ye göre Attendanceyi Getir
        Attendance CreateAttendance(AttendanceForInsertion attendanceForInsertion); // Yeni Attendance oluşturur
        Attendance UpdateAttendance(AttendanceForUpdate attendanceForUpdate); // Attendanceyi güncelle
        void DeleteAttendance(int id); // Attendanceyi sil
    }
}
