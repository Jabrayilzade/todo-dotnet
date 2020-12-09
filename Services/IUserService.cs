using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWish.Dto;
using TodoWish.Models;

namespace TodoWish.Services
{
    public interface IUserService
    {
        Task<GetTaskDto> GetTaskAsync(int taskId, int currentUserId);
        Task<GetProjectDto> GetProjectAsync(int projectId, int currentUserId);
        Task<GetStatisticsDto> GetStatisticsAsync(int currentUserId);

        Task<IEnumerable<GetTaskDto>> GetTasksAsync(int currentUserId);
        Task<IEnumerable<GetProjectDto>> GetProjectsAsync(int currentUserId);

        Task<IEnumerable<GetTaskDto>> SearchAsync(string searchString, int currentUserId);

        Task CreateTaskAsync(string taskContent, DateTime taskDue, int currentUserId);
        Task CreateProjectAsync(string projectTitle, string projectContent, DateTime projectDue, int currentUserId);

        Task RemoveTaskAsync(int taskId, int currentUserId);
        Task RemoveProjectAsync(int projectId, int currentUserId);

        Task EditTaskAsync(int taskId, Todo editedTask, int currentUserId);
        Task EditProjectAsync(int projectId, Project editedProject, int currentUserId);

        Task ClearCompletedTasksAsync(int currentUserId);
        Task MarkAsCompleteAsync(int taskId, int currentUserId);
        Task CheckStatusAsync(int currentUserId);
    }
}