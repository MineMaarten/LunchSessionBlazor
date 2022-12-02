using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LunchSessionBlazor.UI
{
    internal class TodoService
    {
        private readonly HttpClient httpClient;

        public TodoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<TodoItem>> GetCurrentTodoItemsAsync()
        {
            return (await httpClient.GetFromJsonAsync<List<TodoItem>>("todo/current"))!;
        }

        public async Task UpsertTodoItemAsync(TodoItem item)
        {
            var response = await httpClient.PostAsJsonAsync("todo", item);
            response.EnsureSuccessStatusCode();
        }
    }
}
