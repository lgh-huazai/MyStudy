using Microsoft.AspNetCore.Components;

namespace BlazorComponentLifeCycle.Pages
{
    public partial class Main
    {
        private string activeKey = "1";

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("调用的Main组件的SetParametersAsync方法.");
            await base.SetParametersAsync(parameters);
        }

        protected override async Task OnInitializedAsync()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("调用的Main组件的OnInitializedAsync方法.");
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("调用的Main组件的OnParametersSetAsync方法.");
            await base.OnParametersSetAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            if (!firstRender)
            {
                Console.WriteLine("调用的Main组件的OnAfterRenderAsync方法.");
                await base.OnAfterRenderAsync(firstRender);
            }
        }
    }
}
