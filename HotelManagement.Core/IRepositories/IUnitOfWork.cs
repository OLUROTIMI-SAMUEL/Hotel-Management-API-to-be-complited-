namespace HotelManagement.Core.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IHotelRepository hotelRepository { get; }
        IRoomRepository roomRepository { get; }
        IAmenityRepository AmenityRepository { get; }
        ICustomerRepository customerRepository { get; }
        

        IBookingRepository bookingRepository { get; }

        void SaveChanges();

        void BeginTransaction();

        void Rollback();
        Task SaveChangesAsync();
    }
}
