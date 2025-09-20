namespace Common.Application.Interfaces
{
    public interface IDatabaseApplication
    {
        Task<bool> TestConnectionAsync();
        Task MigrateDatabaseAsync();
        Task SeedDataAsync();
    }
}
