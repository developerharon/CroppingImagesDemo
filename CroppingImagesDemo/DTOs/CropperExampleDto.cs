using Blazor.Cropper;

namespace CroppingImagesDemo.DTOs
{
    public class CropperExampleDto
    {
        public byte[] bs { get; set; }
        public ImageCroppedResult? args { get; set; }
    }
}