using ToDoApi.Models;

namespace ToDoApi.interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
