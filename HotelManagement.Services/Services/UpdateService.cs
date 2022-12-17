using HotelManagement.Core.Domains;
using HotelManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Services
{
    public class UpdateService
    {
        private readonly HotelDbContext _DbContext;
        public UpdateService(HotelDbContext dbContext)
        {
                _DbContext = dbContext;
        }
        public async Task Update(User user)
        {
            var UserToUpdate =  _DbContext.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            if (UserToUpdate != null)
            {

                UserToUpdate.FirstName = user.FirstName;
                UserToUpdate.LastName = user.LastName;
                UserToUpdate.Password = user.Password;
                UserToUpdate.Email = user.Email;
                UserToUpdate.Phone = user.Phone;
                


            }
            _DbContext.Users.Update(user);
            _DbContext.SaveChanges();
        }
    }
}
