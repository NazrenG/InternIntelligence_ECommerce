using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Models;

namespace ECommerce.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> GetLastUserId()
        {
            var list=await _userRepository.GetAll();

            return list.LastOrDefault().Id; 
        }
    }
}
