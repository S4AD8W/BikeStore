using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.Core;
using BikeStore.Core.Domain.Repository;
using BikeStore.Infrastructure.DTO;

namespace BikeStore.Infrastructure.Services
{
  public class UserService : IUserService
  {

    private readonly IUserRepository _UserRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository xUserRepository, IMapper xMapper)
    {
      _UserRepository = xUserRepository;
      _mapper = xMapper;

    }

    public async Task<UserDTO> GetAsync(string Email)
    {

      var pUser = await _UserRepository.GetAsync(Email);

      return _mapper.Map<User, UserDTO>(pUser);
    }

    public async Task RegisterAsync(string xEmail, string xUserName, string xPassword)
    {
      var user = await _UserRepository.GetAsync(xEmail);
      if (user != null)
      {
        throw new Exception($"User with email: '{xEmail}' already exists.");
      }
      var pSalt = Guid.NewGuid().ToString("N");
      user = new User(xEmail, xPassword, xUserName, pSalt);
      await _UserRepository.AddAsync(user);


    }

    Task<UserDTO> IUserService.GetAsync(string xEmail)
    {
      throw new NotImplementedException();
    }
  }
}