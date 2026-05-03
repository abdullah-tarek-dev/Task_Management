using System.ComponentModel.DataAnnotations;
using Task_Management.Models;

namespace Task_Management.Queries
{
    public class TaskQueryParams
    {
        private const int MaxPageSize = 50;

        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        private int pageSize = 10;

        [Range(1, MaxPageSize)]
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? Search { get; set; }

        public TaskItemStatus? Status { get; set; }
    }
}