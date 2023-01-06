
﻿using HotelManagement.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//--namespace HotelManagement.Core.IRepositories
//{ 
    //public interface IUnitOfWork : IDisposable
    //{
   
        
﻿namespace HotelManagement.Core.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {

        IHotelRepository hotelRepository { get; }
        IRoomRepository roomRepository { get; }
        IAmenityRepository AmenityRepository { get; }
        ITransactionRepo Payment { get; }

        void SaveChanges();

        void BeginTransaction();

        void Rollback();


    }
}
