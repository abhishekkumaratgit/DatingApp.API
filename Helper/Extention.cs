using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helper
{
    public static class Extention
    {
        public static void AddApplicationError(this HttpResponse response, string error)
        {
            response.Headers.Add("Application-Error", error);
        }
    }
}