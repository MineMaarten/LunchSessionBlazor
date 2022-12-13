using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace LunchSessionBlazor.UI.Pages
{
    public partial class CurrentTodoItems
    {
        [Inject] private TodoService TodoService { get; set; } = null!;

        private List<TodoItem> todoItems = new();
        private bool showAdditionalInfo = true;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await RefreshTodoItemsAsync();
        }

        private async Task CreateNewTodoItemAsync()
        {
            await TodoService.UpsertTodoItemAsync(new TodoItem
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTimeOffset.Now,
                Text = ""
            });

            await RefreshTodoItemsAsync();
        }

        private async Task RefreshTodoItemsAsync()
        {
            todoItems = await TodoService.GetCurrentTodoItemsAsync();
        }
    }
}
