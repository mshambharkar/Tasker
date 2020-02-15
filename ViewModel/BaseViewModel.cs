using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Tasker.Common;
using Tasker.Model;
using Tasker.Services;

namespace Tasker.ViewModel
{
    public class BaseViewModel : BaseNotify, IObserver<Message>
    {
        public IAsyncCommand<TaskModel> SetNextStatus { get; protected set; }
        public IAsyncCommand<TaskModel> SetPreviousStatus { get; protected set; }
        public IAsyncCommand<TaskModel> SetTaskDeleted { get; protected set; }
        public IAsyncCommand<TaskModel> UpdateTask { get; private set; }
        public string State { get; protected set; }
        protected ITaskDbService TaskProxy { get; private set; }
        public ObservableCollection<TaskModel> DataSource { get; private set; }
        private eTaskStatus _currentStatus;
        private IDisposable unsubscriber;
        protected EventAgrregator Provider { get; private set; }
        private bool _IsBusy;
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                _IsBusy = value;
                Notify();
            }
        }
        public BaseViewModel(ITaskDbService taskProxy, EventAgrregator provider)
        {
            TaskProxy = taskProxy;
            DataSource = new ObservableCollection<TaskModel>();
            SetNextStatus = new AsyncCommand<TaskModel>(SetNextStatusAsync);
            SetPreviousStatus = new AsyncCommand<TaskModel>(SetPreviousStatusAsync);
            SetTaskDeleted = new AsyncCommand<TaskModel>(SetTaskDeletedAsync);
            UpdateTask = new AsyncCommand<TaskModel>(UpdateTaskAsync);
            if (provider != null)
            {
                Provider = provider;
                unsubscriber = provider.Subscribe(this);
            }
        }

        protected async Task LoadTasks(eTaskStatus eTaskStatus)
        {
            _currentStatus = eTaskStatus;
            var currentTasks = await TaskProxy.ReadAllTasksByStatusAsync(eTaskStatus).ConfigureAwait(false);
            Application.Current.Dispatcher.Invoke(() =>
            {
                DataSource.Clear();
            });
            foreach (var item in currentTasks)
            {
                Application.Current.Dispatcher.Invoke(() =>
               {
                   DataSource.Add(item);
               });
            }
        }
        public virtual async Task UpdateTaskAsync(TaskModel task)
        {
            if (task == null)
                return;
            await TaskProxy.UpdateTaskAsync(task).ConfigureAwait(false);
        }
        public virtual async Task SetNextStatusAsync(TaskModel task)
        {
            if (task == null)
                return;
            task.TaskStatus = task.TaskStatus + 1;
            if (task.TaskStatus == eTaskStatus.Done)
                task.DoneDate = DateTime.Now;
            await TaskProxy.UpdateTaskAsync(task).ConfigureAwait(false);
            await LoadTasks(_currentStatus).ConfigureAwait(false);
            Provider.Notify(new Message());
        }
        public virtual async Task SetTaskDeletedAsync(TaskModel task)
        {
            if (task == null)
                return;
            task.TaskStatus = eTaskStatus.Deleted;
            await TaskProxy.UpdateTaskAsync(task).ConfigureAwait(false);
            await LoadTasks(_currentStatus).ConfigureAwait(false);
        }
        public virtual async Task SetPreviousStatusAsync(TaskModel task)
        {
            if (task == null)
                return;
            if (task.TaskStatus == eTaskStatus.New)
            {
                await TaskProxy.DeleteTaskByIdAsync(task.Id).ConfigureAwait(false);
            }
            else
            {
                if (task.TaskStatus == eTaskStatus.Done)
                    task.DoneDate = DateTime.MinValue;
                task.TaskStatus = task.TaskStatus - 1;
                await TaskProxy.UpdateTaskAsync(task).ConfigureAwait(false);
                Provider.Notify(new Message());
            }
            await LoadTasks(_currentStatus).ConfigureAwait(false);
        }

        public void OnCompleted()
        {

        }

        public void OnError(Exception error)
        {

        }

        public void OnNext(Message value)
        {
            Task.Run(() => { LoadTasks(_currentStatus).ConfigureAwait(false); });
        }
    }
}
