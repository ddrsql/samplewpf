using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
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
    public class TodoItemManager : DomainService
    {
        private readonly IRepository<TodoItem, int> _TodoItemRepository;

        public TodoItemManager(IRepository<TodoItem, int> TodoItemRepository)
        {
            _TodoItemRepository = TodoItemRepository;
        }
        public async Task<TodoItem> CreateOrUpdateTodoItemAsync(CreateOrUpdateTodoItemInput input)
        {
            TodoItem entity = null;
            if (input.Id.HasValue)
            {
                entity = await _TodoItemRepository.FirstOrDefaultAsync(p => p.Id == input.Id.Value);
            }
            else
            {
                var isEx = await _TodoItemRepository.GetAll()
                    .CountAsync(p => p.Description == input.Description);
                if (isEx > 0)
                    throw new UserFriendlyException($"Description：{input.Description} 已存在！");
            }

            if (entity == null)
            {
                entity = new TodoItem(input.Description, input.IsChecked);
                entity.Id = await _TodoItemRepository.InsertAndGetIdAsync(entity);
            }
            else
            {
                var isEx = await _TodoItemRepository.GetAll()
                    .CountAsync(p => p.Id != input.Id.Value && p.Description == input.Description);
                if (isEx > 0)
                    throw new UserFriendlyException($"Description：{input.Description} 已存在！");
                entity.SetProperties(input.Description, input.IsChecked);
                entity = await _TodoItemRepository.UpdateAsync(entity);
            }
            return entity;
        }
    }
}
