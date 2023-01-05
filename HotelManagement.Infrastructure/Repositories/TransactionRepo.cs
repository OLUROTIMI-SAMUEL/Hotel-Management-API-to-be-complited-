﻿using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HotelManagement.Infrastructure.Repositories
{
    public class TransactionRepo : GenericRepository<Payment>, ITransactionRepo
    {
        
        public TransactionRepo(HotelDbContext hotelDbContext) : base(hotelDbContext)
        {
          
        }


    }
}
