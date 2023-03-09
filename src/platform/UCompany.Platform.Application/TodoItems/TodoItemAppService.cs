using Abp.Domain.Repositories;
using UCompany.Platform.TodoItems.Aggregates;
using UCompany.Platform.TodoItems.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCompany.Platform.TodoItems
{
    public class TodoItemAppService : PlatformAppServiceBase, ITodoItemAppService
    {
        private readonly TodoItemManager _todoItemManager;
        private readonly IRepository<TodoItem, int> _todoItemRepository;

        public TodoItemAppService(TodoItemManager todoItemManager, IRepository<TodoItem, int> todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
            _todoItemManager = todoItemManager;
        }

        public async Task<TodoItem> CreateOrUpdateTodoItemAsync(CreateOrUpdateTodoItemInput input)
        {
            return await _todoItemManager.CreateOrUpdateTodoItemAsync(input);
        }
        private async Task<List<TodoItem>> AddData()
        {
            var result = new List<TodoItem>();
            List<CreateOrUpdateTodoItemInput> list = new List<CreateOrUpdateTodoItemInput>
            {
                new CreateOrUpdateTodoItemInput { Description = "Walk the dog" },
                new CreateOrUpdateTodoItemInput { Description = "Buy some milk" },
                new CreateOrUpdateTodoItemInput { Description = "Learn Avalonia", IsChecked = true }
            };

            foreach (var item in list)
            {
                result.Add(await CreateOrUpdateTodoItemAsync(item));
            }
            return result;
        }

        public async Task<List<GetTodoItemOutput>> GetTodoItemListAsync()
        {
            var todoItems = await _todoItemRepository.GetAll().AsNoTracking()
            //.WhereIf(!input.Description.IsNullOrEmpty(), p => p.Name.Contains(input.Description))
            .OrderBy(p => p.Description)
            .ToListAsync();

            //临时添加数据
            if (todoItems?.Count == 0)
            {
                todoItems = await AddData();
            }
            return todoItems.Select(p => new GetTodoItemOutput() { Id = p.Id, Description = p.Description, IsChecked = p.IsChecked }).ToList();
        }
    }
}
