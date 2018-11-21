﻿using BikeStore.core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.core.Domain{
 public interface IUserRepository : IRepository {

   Task <User> Get(Guid id);
   Task <User> Get(string email);
   Task <IEnumerable<User>> GetAll();
   Task  Add(User user);
   Task  Update(User user);
   Task  Remove(Guid id);

  }
}
