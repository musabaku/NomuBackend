using System.Collections.Generic;
using MongoDB.Driver;
using NomuBackend.Services;
using NomuBackend.Model;

namespace NomuBackend.Services
{
    public class TaskService : ITaskService
    {
        private readonly IMongoCollection<Task> _tasks;

        public TaskService(IMongoClient client, string databaseName, string collectionName)
        {
            var database = client.GetDatabase(databaseName);
            _tasks = database.GetCollection<Task>(collectionName);
        }

        public List<Task> ListTasks()
        {
            return _tasks.Find(task => true).ToList();
        }

        public void AssignTask(Task task)
        {
            _tasks.InsertOne(task);
        }

        public void UpdateTaskStatus(string taskId, string status)
        {
            var filter = Builders<Task>.Filter.Eq(t => t.TaskId, taskId);
            var update = Builders<Task>.Update.Set("Status", status);
            _tasks.UpdateOne(filter, update);
        }
    }
}
