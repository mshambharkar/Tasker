using LiteDB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasker.Model;

namespace Tasker.Services
{
    public class TaskDbService : ITaskDbService
    {
        private const string DbName = @"MyData.db";

        public TaskDbService()
        {

        }

        public Task<Guid> CreateTaskAsync(TaskModel task)
        {
            using (var db = new LiteDatabase(DbName))
            {
                var allTasks = db.GetCollection<TaskModel>();
                return Task.FromResult(allTasks.Insert(task).AsGuid);
            }
        }

        public Task<bool> DeleteTaskByIdAsync(Guid id)
        {
            using (var db = new LiteDatabase(DbName))
            {
                var allTasks = db.GetCollection<TaskModel>();
                return Task.FromResult(allTasks.Delete(id));
            }
        }


        public Task<IEnumerable<TaskModel>> ReadAllTasksAsync()
        {
            using (var db = new LiteDatabase(DbName))
            {
                var allTasks = db.GetCollection<TaskModel>();
                return Task.FromResult(allTasks.FindAll());
            }
        }

        public Task<IEnumerable<TaskModel>> ReadAllTasksByStatusAsync(eTaskStatus taskStatus)
        {
            using (var db = new LiteDatabase(DbName))
            {
                var allTasks = db.GetCollection<TaskModel>();
                return Task.FromResult(allTasks.Find(t => t.TaskStatus == taskStatus));
            }
        }

        public Task<TaskModel> ReadTaskByIdAsync(Guid id)
        {
            using (var db = new LiteDatabase(DbName))
            {
                var allTasks = db.GetCollection<TaskModel>();
                return Task.FromResult(allTasks.FindOne(t => t.Id == id));
            }
        }

        public Task<bool> UpdateTaskAsync(TaskModel task)
        {
            using (var db = new LiteDatabase(DbName))
            {
                var allTasks = db.GetCollection<TaskModel>();
                return Task.FromResult(allTasks.Update(task));
            }
        }
    }
}
