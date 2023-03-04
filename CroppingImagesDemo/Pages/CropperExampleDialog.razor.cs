using Blazor.Cropper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace CroppingImagesDemo.Pages
{
    public partial class CropperExampleDialog : ComponentBase
    {
        [Inject] private IJSRuntime? jsRuntime { get; set; }

        [Parameter] public IBrowserFile? File { get; set; }

        private Cropper? cropper;
        private CropInfo? state;

        private bool show = true;
        private bool parsing = false;
        private bool purecs = false;
        private bool isCropLocked = false;
        private bool isImageLocked = false;

        private string enableProportion = "false";
        private string prompt = "Image cropped! Parsing to base64...";

        private int offsetx, offsety;
        private int initw = 50;
        private int inith = 50;
        private int quality = 100;

        private double ratio = 1;
        private double proportion = 1d;
        private double width;
        private double height;

        private void SaveState()
        {
            state = cropper?.GetCropInfo();
        }

        private async Task RestoreState()
        {
            if (state != null)
            {
                (offsetx, offsety, initw, inith, ratio) = state.GetInitParams();
                show = false;
                StateHasChanged();
                await Task.Delay(10);
                show = true;
            }
        }

        private async Task DoneCrop()
        {
            var args = await cropper.GetCropedResult();
            File = null;
            parsing = true;
            base.StateHasChanged();
            await Task.Delay(10);// a hack, otherwise prompt won't show
            await jsRuntime.InvokeVoidAsync("console.log", "converted!");
            var bs = await args.GetDataAsync();
            await jsRuntime.SetImageAsync(bs, "my-img", args.Format.DefaultMimeType);
            parsing = false;
        }
    }
}