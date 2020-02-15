using Tasker.Common;
using Tasker.Services;

namespace Tasker.ViewModel
{
    public class DoneTaskViewModel : BaseViewModel
    {
        public DoneTaskViewModel(ITaskDbService taskProxy, EventAgrregator provider) : base(taskProxy, provider)
        {
            State = "Done Task";
            _ = LoadTasks(eTaskStatus.Done);
        }
    }
}
