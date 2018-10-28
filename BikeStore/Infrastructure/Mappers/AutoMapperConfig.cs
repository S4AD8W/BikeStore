using AutoMapper;
using BikeStore.Core;
using BikeStore.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Mappers
{
  public class AutoMapperConfig { 

   public static IMapper Initialize()
        => new MapperConfiguration(cfg => {
          cfg.CreateMap<User, UserDTO>();
        })
        
      .CreateMapper();


}

}