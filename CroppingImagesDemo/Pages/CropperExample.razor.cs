using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;

namespace CroppingImagesDemo.Pages
{
    public partial class CropperExample : ComponentBase
    {
        [Inject] private IDialogService? dialogService { get; set; }
        [Inject] private IJSRuntime? jsRuntime { get; set; }

        private IBrowserFile? file;

        protected override void OnInitialized()
        {

            base.OnInitialized();
        }

        private async Task OnInputFileChange(InputFileChangeEventArgs args)
        {
            if (jsRuntime is not null)
                await jsRuntime.InvokeVoidAsync("console.log", DateTime.Now.ToString());
            file = args.File;
        }

        private void CropImage()
        {
            var parameters = new DialogParameters();
            parameters.Add("File", file);
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            dialogService?.Show<CropperExampleDialog>("Crop Image", parameters, options);
        }
    }
}