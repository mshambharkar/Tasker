using System;
using Tasker.Common;
using Tasker.Services;

namespace Tasker.Model
{
    public class TaskModel : BaseNotify
    {
        public TaskModel()
        {

        }
        public TaskModel(eTaskStatus eTaskStatus)
        {
            this.TaskStatus = eTaskStatus;
            this.Id = Guid.NewGuid();
            this.AddDate = DateTime.UtcNow;
        }
        private Guid _Id;

        public Guid Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                Notify();
            }
        }

        private DateTime _AddDate;

        public DateTime AddDate
        {
            get { return _AddDate; }
            set
            {
                _AddDate = value;
                Notify();
            }
        }
        private DateTime _DoneDate;

        public DateTime DoneDate
        {
            get { return _DoneDate; }
            set
            {
                _DoneDate = value;
                Notify();
            }
        }
        private bool IsExpired => (DoneDate - DateTime.UtcNow).Days > 5;
        private string _Title;

        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                Notify();
            }
        }
        private string _Details;

        public string Details
        {
            get { return _Details; }
            set
            {
                _Details = value;
                Notify();
            }
        }
        private eTaskStatus _TaskStatus;

        public eTaskStatus TaskStatus
        {
            get { return _TaskStatus; }
            set
            {
                _TaskStatus = value;
                Notify();
            }
        }
    }
}
