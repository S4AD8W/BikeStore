using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BikeStore.Core;
using BikeStore.Infrastructure.DTO;

namespace BikeStore.Infrastructure.Mappers {
  public static class AutoMapperConfig {
    //Klasa odpowiadająca z konfiguracje auto mappera 

    public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
              cfg.CreateMap<User, UserDto>();              //Przykładowa konfiguracja mapowania obiektu z UserDto na User
            })
            .CreateMapper();

  }
}
