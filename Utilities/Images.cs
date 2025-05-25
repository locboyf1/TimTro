namespace TimTro.Utilities
{
    public class Images
    {
        public static async Task<byte[]> ConvertToByte(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        public static string ConvertToBase64Src(byte[] imageData, string contentType = "image/jpeg")
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            var base64 = Convert.ToBase64String(imageData);
            return $"data:{contentType};base64,{base64}";
        }

        public static async Task<string> getImageType(IFormFile imageFile)
        {
            if (imageFile == null)
                return null;

            var imageBytes = await ConvertToByte(imageFile);
            var imageType = imageFile.ContentType;
            return imageType;
        }

    }
}
