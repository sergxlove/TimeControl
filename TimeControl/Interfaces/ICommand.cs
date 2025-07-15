using Microsoft.Extensions.DependencyInjection;

namespace TimeControl.Interfaces
{
    public interface ICommand
    {
        string Name { get; } 
        string Description { get; } 
        Task Execute(string[] args, DataCore data, ServiceProvider provider);
    }
}
