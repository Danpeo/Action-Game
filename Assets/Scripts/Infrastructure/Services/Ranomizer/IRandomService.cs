namespace Infrastructure.Services.Ranomizer
{
    public interface IRandomService : IService
    {
        int Next(int min, int max);
    }
}