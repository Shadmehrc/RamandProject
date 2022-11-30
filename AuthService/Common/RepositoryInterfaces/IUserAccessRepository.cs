using System.Threading.Tasks;
using Core.Models;

namespace Core.RepositoryInterfaces
{
    public interface IUserAccessRepository
    {
        public Task<bool> ValidateUser(LoginUserModel model);
    }
}
