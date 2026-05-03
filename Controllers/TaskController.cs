using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Task_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _service;

        public TasksController(TaskService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TaskDto>>> GetById(int id)
        {
            var task = await _service.GetTaskById(id);

            if (task == null)
                return NotFound(ApiResponse<TaskDto>.FailResponse("Task not found"));

            return Ok(ApiResponse<TaskDto>.SuccessResponse(task));
        }

        [HttpGet("filter")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<ApiResponse<List<TaskDto>>>> GetAll([FromQuery] TaskQueryParams query)
        {
            var tasks = await _service.GetTasks(query);

            return Ok(ApiResponse<List<TaskDto>>.SuccessResponse(tasks));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            // TODO: get userId from JWT later
            await _service.CreateTask(dto, 1);

            return Ok(ApiResponse<string>.SuccessResponse("Task Created"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTaskDto dto)
        {
            var result = await _service.UpdateTask(id, dto);

            if (!result)
                return NotFound(ApiResponse<string>.FailResponse("Task not found"));

            return Ok(ApiResponse<string>.SuccessResponse("Task Updated"));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteTask(id);

            if (!result)
                return NotFound(ApiResponse<string>.FailResponse("Task not found"));

            return Ok(ApiResponse<string>.SuccessResponse("Deleted successfully"));
        }
    }
}