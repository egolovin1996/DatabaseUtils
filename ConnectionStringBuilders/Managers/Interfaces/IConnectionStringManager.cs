namespace GRC.DatabaseUtils.ConnectionStringBuilders.Managers.Interfaces
{
    public interface IConnectionStringManager
    {
        string GetPassword();
        void SetPassword(string newPassword);
        void SetUser(string newUser);
        string GetConnectionString();
    }
}
