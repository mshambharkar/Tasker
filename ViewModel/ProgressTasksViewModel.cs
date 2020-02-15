using Tasker.Common;
using Tasker.Services;

namespace Tasker.ViewModel
{
    public class ProgressTasksViewModel : BaseViewModel
    {
        public ProgressTasksViewModel(ITaskDbService taskProxy, EventAgrregator provider) : base(taskProxy, provider)
        {
            State = "In Progress";
            LoadTasks(eTaskStatus.Working);
        }
    }
}
