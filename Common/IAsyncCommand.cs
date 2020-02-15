using System.Threading.Tasks;
using System.Windows.Input;

namespace Tasker.Common
{
    public interface IAsyncCommand<T> : ICommand
    {
        Task ExecuteAsync(T parameter);
        bool CanExecute(T parameter);
    }

}
