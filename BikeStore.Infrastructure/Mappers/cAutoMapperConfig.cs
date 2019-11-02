using AutoMapper;
using BikeStore.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using BikeStore.core.Domain;


namespace BikeStore.Infrastructure.Mappers{

  public static class cAutoMapperConfig {
    //Klasa odpowiadająca z konfiguracje auto mappera 

    public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
              {
                cfg.CreateMap<User, UserDTO>();            //Przykładowa konfiguracja mapowania obiektu z UserDto na User
                cfg.ValidateInlineMaps = false;
                
            })
            .CreateMapper();

  }
}
