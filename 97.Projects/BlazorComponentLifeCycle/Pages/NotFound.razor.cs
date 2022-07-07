using Microsoft.AspNetCore.Components;

namespace BlazorComponentLifeCycle.Pages
{
    public partial class NotFound
    {
        [Inject] NavigationManager? NavigationManager { get; set; }

        public void ClickEvent()
        {
            NavigationManager?.NavigateTo("/");
        }
    }
}
