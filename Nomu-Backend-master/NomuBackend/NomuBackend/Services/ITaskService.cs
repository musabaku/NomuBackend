using System.Collections.Generic;
using NomuBackend.Model;

namespace NomuBackend.Services
{
    public interface ITaskService
    {
        List<Task> ListTasks();
        void AssignTask(Task task);
        void UpdateTaskStatus(string taskId, string status);
    }
}

