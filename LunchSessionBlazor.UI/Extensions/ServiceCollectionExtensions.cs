using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LunchSessionBlazor.UI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void UseTodoApp(this IServiceCollection services, IConfiguration configuration, Func<HttpMessageHandler>? messageHandler = null)
        {
            var c = services.AddHttpClient<TodoService>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(configuration["ApiHost"] ?? throw new InvalidOperationException("No 'ApiHost' setting defined!"));
            });

            if (messageHandler != null)
            {
                c.ConfigurePrimaryHttpMessageHandler(messageHandler);
            }

            services.AddScoped<ExampleJsInterop>();
        }
    }
}
