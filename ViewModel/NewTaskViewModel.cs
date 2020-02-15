using System;
using System.Threading.Tasks;
using Tasker.Common;
using Tasker.Model;
using Tasker.Services;

namespace Tasker.ViewModel
{
    public class NewTaskViewModel : BaseViewModel
    {
        public IAsyncCommand<TaskModel> CreateNewTask { get; private set; }

        public NewTaskViewModel(ITaskDbService taskProxy, EventAgrregator provider) : base(taskProxy, provider)
        {
            State = "New Task";
            CreateNewTask = new AsyncCommand<TaskModel>(CreateNewTaskAsync);

            _ = LoadTasks(eTaskStatus.New);
        }

        private async Task CreateNewTaskAsync(object arg)
        {
            _ = await TaskProxy.CreateTaskAsync(new Model.TaskModel(eTaskStatus.New) { AddDate = DateTime.Now }).ConfigureAwait(false);
            _ = LoadTasks(eTaskStatus.New);
        }
    }
}
