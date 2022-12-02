using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace LunchSessionBlazor.UI.Components
{
    public partial class TodoTable
    {
        [Parameter]
        [EditorRequired]
        public List<TodoItem> TodoItems { get; set; } = new();

        [Inject] private TodoService TodoService { get; set; } = null!;

        [Parameter]
        public EventCallback<TodoItem> TodoItemUpdated { get; set; }

        private async Task UpdateAsync(TodoItem todoItem)
        {
            await TodoService.UpsertTodoItemAsync(todoItem);
            await TodoItemUpdated.InvokeAsync(todoItem);
        }
    }
}
