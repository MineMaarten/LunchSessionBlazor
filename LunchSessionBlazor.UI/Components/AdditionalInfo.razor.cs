using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace LunchSessionBlazor.UI.Components
{
    public partial class AdditionalInfo
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; } = null!;

        [Parameter]
        [EditorRequired]
        public string SearchedWord { get; set; } = null!;

        [Parameter]
        [EditorRequired]
        public IEnumerable<TodoItem> TodoItems { get; set; } = null!;
    }
}
