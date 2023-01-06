using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using HotelManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.UnitOfWork
{

 //   public class UnitOfWork : IUnitOfWork
 //   {
 //       private readonly HotelDbContext _hotelDbContext;
 //       private readonly IMapper _mapper;
 //       private bool _disposed;
 //       private ITransactionRepo _transactionRepo;
 //       private IHotelRepository _hotelRepository;
 //       private IRoomRepository _roomRepository;
 //       private IAmenityRepository _amenityRepository;


 //       public UnitOfWork (HotelDbContext hotelDbContext, IMapper mapper)
	//{
 //       _hotelDbContext = hotelDbContext;
 //       _mapper = mapper;   
	//}

 //       public ITransactionRepo Payment =>
 //          _transactionRepo ??= new TransactionRepo(_hotelDbContext);

       
 //       public void BeginTransaction()
 //   {
 //      _disposed = false;
 //   }


 //   public void SaveChanges()
 //   {
 //      _hotelDbContext.SaveChangesAsync();
 //   }

 //   public void Rollback()
 //   {
 //       _hotelDbContext.Database.RollbackTransaction();
 //   }
      

 //   protected virtual void Dispose(bool disposing)
 //   {

 //         if (!_disposed)
 //         {
 //             if (disposing)
 //             {
 //               _hotelDbContext.Dispose();
 //             }
 //         }

 //           _disposed = true;
 //   }

    //public void Dispose()
    //{
    //   Dispose(true);
    //   GC.SuppressFinalize(this);
    ////}
    //------    public IHotelRepository hotelRepository =>
    //        _hotelRepository ??= new HotelRepository(_hotelDbContext);
    //    public IRoomRepository roomRepository =>
    //        _roomRepository ??= new RoomRespository(_hotelDbContext);



    //    public IAmenityRepository AmenityRepository =>
    //     _amenityRepository ??= new AmenityRepository(_hotelDbContext);
        //public void BeginTransaction()
        //{
        //    _disposed = false;
        //}


        //public void SaveChanges()
        //{
        //    _hotelDbContext.SaveChangesAsync();
        //}

        //public void Rollback()
        //{
        //    _hotelDbContext.Database.RollbackTransaction();
        //}


        //protected virtual void Dispose(bool disposing)
        //{

        //    if (!_disposed)
        //    {
        //        if (disposing)
        //        {
        //            _hotelDbContext.Dispose();
        //        }
        //    }

        //    _disposed = true;
        //}


    


	public class UnitOfWork : IUnitOfWork
	{
		private readonly HotelDbContext _hotelDbContext;
		private bool _disposed;
		private IHotelRepository _hotelRepository;
		private IRoomRepository _roomRepository;
        private IAmenityRepository _amenityRepository;
        private ITransactionRepo _transactionRepo;
    public UnitOfWork(HotelDbContext hotelDbContext)
		{
			_hotelDbContext = hotelDbContext;
		}
		public IHotelRepository hotelRepository =>
			_hotelRepository ??= new HotelRepository(_hotelDbContext);
		public IRoomRepository roomRepository =>
			_roomRepository ??= new RoomRespository(_hotelDbContext);

         public ITransactionRepo Payment =>
          _transactionRepo ??= new TransactionRepo(_hotelDbContext);



        public IAmenityRepository AmenityRepository =>
         _amenityRepository ??= new AmenityRepository(_hotelDbContext);
        public void BeginTransaction()
		{
			_disposed = false;
		}


		public void SaveChanges()
		{
			_hotelDbContext.SaveChangesAsync();
		}

		public void Rollback()
		{
			_hotelDbContext.Database.RollbackTransaction();
		}


		protected virtual void Dispose(bool disposing)
		{

			if (!_disposed)
			{
				if (disposing)
				{
					_hotelDbContext.Dispose();
				}
			}

			_disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}


	}

}
  