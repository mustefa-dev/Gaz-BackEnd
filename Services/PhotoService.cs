namespace Gaz_BackEnd.Services;
public interface IPhotoService
{
    Task<string> UploadImages(IFormFile file);
}
public class PhotoService : IPhotoService
{

    public async Task<string> UploadImages(IFormFile file)
    {
        // upload images to wwwwroot/images 
            
        var date = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        var fileName = Path.GetFileName(file.FileName);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", date + fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return Path.Combine("images" , date + fileName);
    }
}