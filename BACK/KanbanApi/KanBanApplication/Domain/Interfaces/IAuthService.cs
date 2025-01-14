namespace KanBanApplication.Domain.Interfaces;

public interface IAuthService
{
    string Login(string login, string senha);
}