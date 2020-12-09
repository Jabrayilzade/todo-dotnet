using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TodoWish.DataContext;
using TodoWish.Dto;
using TodoWish.Models;
using TodoWish.Services;

namespace TodoWish.Services
{
    public class UserService : IUserService
    {
        private readonly AppDataContext context;

        private readonly IMapper mapper;

        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly IDbConnectionFactory dbConnection;

        public UserService(AppDataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper,
            IDbConnectionFactory dbConnection)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.dbConnection = dbConnection;
        }

        public async Task<GetTaskDto> GetTaskAsync(int taskId, int currentUserId)
        {
            using IDbConnection connection = dbConnection.CreateDbConnection();
            return await connection.QueryFirstOrDefaultAsync<GetTaskDto>(
                "select id, author, content, due, status from todos where id = @task_id and user_id = @current_user_id",
                new {task_id = taskId, current_user_id = currentUserId});
        }

        public async Task<GetProjectDto> GetProjectAsync(int projectId, int currentUserId)
        {
            using IDbConnection connection = dbConnection.CreateDbConnection();
            return await connection.QueryFirstOrDefaultAsync<GetProjectDto>(
                "select id, title, content, due from projects where id = @project_id and user_id = @current_use_id",
                new {task_id = projectId, current_user_id = currentUserId});
        }

        public async Task<GetStatisticsDto> GetStatisticsAsync(int currentUserId)
        {
            using IDbConnection connection = dbConnection.CreateDbConnection();

            var allTasks = await connection.QueryAsync<GetTaskDto>(
                "select id, author, content, due, status from todos where user_id = @current_user_id",
                new {current_user_id = currentUserId});

            var completedTasks = await connection.QueryAsync<GetTaskDto>(
                "select id, author, content, due, status from todos where user_id = @current_user_id and status = 'complete'",
                new {current_user_id = currentUserId});

            var overdueTasks = await connection.QueryAsync<GetTaskDto>(
                "select id, author, content, due, status from todos where user_id = @current_user_id and status = 'overdue'",
                new {current_user_id = currentUserId});

            var ongoingTasks = await connection.QueryAsync<GetTaskDto>(
                "select id, author, content, due, status from todos where user_id = @current_user_id and status = 'ongoing'",
                new {current_user_id = currentUserId});

            var totalCount = allTasks.Count();
            var overdueCount = overdueTasks.Count();
            var completedCount = completedTasks.Count();
            var ongoingCount = ongoingTasks.Count();
            var userPerformance = totalCount / overdueCount;

            GetStatisticsDto statistic = new GetStatisticsDto()
            {
                UserId = currentUserId,
                Tasks = totalCount,
                Overdue = overdueCount,
                Completed = completedCount,
                Ongoing = ongoingCount,
                Performance = userPerformance
            };
            return statistic;
        }

        public async Task<IEnumerable<GetTaskDto>> GetTasksAsync(int currentUserId)
        {
            using IDbConnection connection = dbConnection.CreateDbConnection();
            return await connection.QueryAsync<GetTaskDto>(
                "select id, author, content, due, status from todos where user_id = @current_user_id order by id desc",
                new {current_user_id = currentUserId});
        }

        public async Task<IEnumerable<GetProjectDto>> GetProjectsAsync(int currentUserId)
        {
            using IDbConnection connection = dbConnection.CreateDbConnection();
            return await connection.QueryAsync<GetProjectDto>(
                "select id, title, content, due from projects where user_id = @current_user_id",
                new {current_user_id = currentUserId});
        }

        public async Task<IEnumerable<GetTaskDto>> SearchAsync(string searchString, int currentUserId)
        {
            using IDbConnection connection = dbConnection.CreateDbConnection();
            return await connection
                .QueryAsync<GetTaskDto>(
                    "select id, author, content, due, status from todos where name ilike @str and userId = @current_user_id",
                    new {str = searchString + "%", current_user_id = currentUserId});
        }

        public async Task CreateTaskAsync(string taskContent, DateTime taskDue, int currentUserId)
        {
            using IDbConnection connection = dbConnection.CreateDbConnection();
            var user = await connection.QueryFirstAsync<User>(
                "select name, first_name as firstName from users where id = @current_user_id",
                new {current_user_id = currentUserId});
            var author = user.Name + " " + user.FirstName;
            await connection.ExecuteAsync(
                "insert into todos (author, content, due, status, user_id) values(@task_author, @task_content, @task_due, @status, @user_id)",
                new
                {
                    task_author = author, task_content = taskContent, task_due = taskDue, status = SetStatus(taskDue),
                    user_id = currentUserId
                });
        }

        public async Task CreateProjectAsync(string projectTitle, string projectContent, DateTime projectDue,
            int currentUserId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == currentUserId);
            var project = new Project()
            {
                Title = projectTitle,
                Content = projectContent,
                Due = projectDue,
                UserId = currentUserId
            };
            project.UserId = currentUserId;
            await context.Projects.AddAsync(project);
            await context.SaveChangesAsync();
        }

        public async Task RemoveTaskAsync(int taskId, int currentUserId)
        {
            using IDbConnection connection = dbConnection.CreateDbConnection();
            await connection.ExecuteAsync("delete from todos where user_id = @current_user_id and id = @task_id",
                new {current_user_id = currentUserId, task_id = taskId});
        }

        public async Task RemoveProjectAsync(int projectId, int currentUserId)
        {
            await Task.CompletedTask;
        }

        public async Task EditTaskAsync(int taskId, Todo editedTask, int currentUserId)
        {
            await Task.CompletedTask;
        }

        public async Task EditProjectAsync(int projectId, Project editedProject, int currentUserId)
        {
            await Task.CompletedTask;
        }

        private String SetStatus(DateTime due)
        {
            if (due < DateTime.Now) return "overdue";
            return due > DateTime.Now ? "ongoing" : null;
        }

        public async Task CheckStatusAsync(int currentUserId)
        {
            using IDbConnection connection = dbConnection.CreateDbConnection();
            await connection.ExecuteAsync(
                "update todos set status = 'overdue' where clock_timestamp() > due and status = 'ongoing' and user_id = @current_user_id",
                new {current_user_id = currentUserId});
        }

        public async Task ClearCompletedTasksAsync(int currentUserId)
        {
            using var connection = dbConnection.CreateDbConnection();
            await connection.ExecuteAsync("delete from todos where status = 'complete' and user_id = @current_user_id",
                new {current_user_id = currentUserId});
        }

        public async Task MarkAsCompleteAsync(int taskId, int currentUserId)
        {
            using IDbConnection connection = dbConnection.CreateDbConnection();
            await connection.ExecuteAsync(
                "update todos set status = 'complete' where id = @task_id and user_id = @current_user_id",
                new {task_id = taskId, current_user_id = currentUserId});
        }
    }
}