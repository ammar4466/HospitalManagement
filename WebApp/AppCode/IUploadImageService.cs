using AppUtility;
using Infrastructure;

namespace WebApp.AppCode
{
    public interface IUploadImageService
    {
        Response Upload(FileUploadModel model);
    }
}
