using Tasker.Common;
using Tasker.Services;

namespace Tasker.ViewModel
{
    public class MainViewModel : BaseNotify
    {
        private ITaskDbService _taskProxy;
        public NewTaskViewModel NewTasks { get; private set; }
        public ProgressTasksViewModel ProgressTasks { get; private set; }
        public DoneTaskViewModel DoneTasks { get; private set; }

        public MainViewModel(ITaskDbService taskProxy, NewTaskViewModel newTaskViewModel, ProgressTasksViewModel progressTasksViewModel, DoneTaskViewModel doneTaskViewModel)
        {
            _taskProxy = taskProxy;
            NewTasks = newTaskViewModel;
            ProgressTasks = progressTasksViewModel;
            DoneTasks = doneTaskViewModel;

        }


    }
}
