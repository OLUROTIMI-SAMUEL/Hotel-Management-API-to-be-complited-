
﻿using HotelManagement.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{ 
    public interface IUnitOfWork : IDisposable
    {
   
        ITransactionRepo Payment { get; }
        IHotelRepository hotelRepository { get; }
        IRoomRepository roomRepository { get; }
        IAmenityRepository AmenityRepository { get; }

        void SaveChanges();

        void BeginTransaction();

        void Rollback();

    }
}
