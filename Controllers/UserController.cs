using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoWish.Dto;
using TodoWish.Models;
using TodoWish.Services;

namespace TodoWish.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.userService = userService;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTask(int taskId)
        {
            return Ok(await userService.GetTaskAsync(taskId, Int32.Parse(httpContextAccessor.HttpContext.User
                .FindFirst("Id").Value)));
        }

        [HttpGet]
        public async Task<IActionResult> GetProject(int projectId)
        {
            return Ok(await userService.GetProjectAsync(projectId,
                Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst("Id").Value)));
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            return Ok(await userService.GetStatisticsAsync(Int32.Parse(httpContextAccessor.HttpContext.User
                .FindFirst("Id").Value)));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            return Ok(await userService.SearchAsync(searchString, Int32.Parse(httpContextAccessor.HttpContext.User
                .FindFirst("Id").Value)));
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            return Ok(await userService.GetTasksAsync(Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst("Id")
                .Value)));
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            return Ok(await userService.GetProjectsAsync(Int32.Parse(httpContextAccessor.HttpContext.User
                .FindFirst("Id").Value)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDto task)
        {
            await userService.CreateTaskAsync(task.Content, task.Due, Int32.Parse(httpContextAccessor.HttpContext.User
                .FindFirst("Id").Value));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto project)
        {
            await userService.CreateProjectAsync(project.Title, project.Title, project.Due,
                Int32.Parse(httpContextAccessor.HttpContext.User
                    .FindFirst("Id").Value));
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveTask(int taskId)
        {
            await userService.RemoveTaskAsync(taskId, Int32.Parse(httpContextAccessor.HttpContext.User
                .FindFirst("Id").Value));
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveProject(int taskId)
        {
            await Task.CompletedTask;
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditProject(int taskId, Project editedProject)
        {
            await Task.CompletedTask;
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditTask(int taskId, Todo editedTask)
        {
            await Task.CompletedTask;
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCompletedTasks()
        {
            await userService.ClearCompletedTasksAsync(Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst("Id")
                .Value));
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> MarkAsCompleted(int taskId)
        {
            await userService.MarkAsCompleteAsync(taskId, Int32.Parse(httpContextAccessor.HttpContext.User
                .FindFirst("Id")
                .Value));
            return Ok();
        }
    }
}