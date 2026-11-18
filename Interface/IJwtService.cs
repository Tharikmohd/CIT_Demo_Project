namespace CIt_Demo_Project.Interface
{
    public interface IJwtService
    {
        string GenerateToken(int userid, string username, string email);
    }
}
