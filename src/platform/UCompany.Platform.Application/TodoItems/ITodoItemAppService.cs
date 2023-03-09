using Abp.Application.Services;
using UCompany.Platform.TodoItems.Aggregates;
using UCompany.Platform.TodoItems.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCompany.Platform.TodoItems
{
    public interface ITodoItemAppService : IApplicationService
    {
        Task<TodoItem> CreateOrUpdateTodoItemAsync(CreateOrUpdateTodoItemInput input);
        Task<List<GetTodoItemOutput>> GetTodoItemListAsync();
    }
}
