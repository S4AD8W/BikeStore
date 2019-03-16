using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.core.Domain;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Infrastructure.Repositories {
  class ForksNotyficationRepository : IForksNotificationRepository {

    public readonly BikeStoreContext mDBContext;
    public readonly IMapper mMapper;
    public ForksNotyficationRepository(BikeStoreContext xDBContext, IMapper xMapper) {
      mDBContext = xDBContext;
      mMapper = xMapper;
    }

    public Task AddForksNotification(ForkNotification xForkNotification) {
      throw new NotImplementedException();
    }

    public Task DeleteForksNotyfication(int xId) {
      throw new NotImplementedException();
    }

    public Task DeleteForksNotyfication(ForkNotification xForkNotification) {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<ForkNotification>> GetAllForksNotyfication() {
      throw new NotImplementedException();
    }
  }
}
