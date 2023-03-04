using Blazor.Cropper;
using CroppingImagesDemo.DTOs;
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
            CropImage();
        }

        private async void CropImage()
        {
            var parameters = new DialogParameters
            {
                { "File", file }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = dialogService?.Show<CropperExampleDialog>("Crop Image", parameters, options);
            
            if (dialog is null) return;

            var result = await dialog.Result;

            if (result is null) return;
            if (result.Canceled) return;

            var dto = result.Data as CropperExampleDto;

            await jsRuntime.SetImageAsync(dto.bs, "my-img", dto.args.Format.DefaultMimeType);
        }
    }
}