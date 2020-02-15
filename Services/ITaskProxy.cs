using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasker.Model;

namespace Tasker.Services
{
    public interface ITaskDbService
    {
        Task<TaskModel> ReadTaskByIdAsync(Guid id);
        Task<bool> DeleteTaskByIdAsync(Guid id);
        Task<bool> UpdateTaskAsync(TaskModel task);
        Task<Guid> CreateTaskAsync(TaskModel task);
        Task<IEnumerable<TaskModel>> ReadAllTasksAsync();
        Task<IEnumerable<TaskModel>> ReadAllTasksByStatusAsync(eTaskStatus taskStatus);
    }

    public enum eTaskStatus
    {
        Backlog = 0,
        New = 1,
        Working = 2,
        Done = 3,
        Expired = 4,
        Deleted = 5
    }
}
