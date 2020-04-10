using Microsoft.AspNetCore.Http;
using System;

namespace DatingApp.API.Helper
{
    public static class Extention
    {
        public static void AddApplicationError(this HttpResponse response, string error)
        {
            response.Headers.Add("Application-Error", error);
        }

        public static int CalculateAge(this DateTime dateOfBirth)
        {
            var age = DateTime.Now.Year - dateOfBirth.Year;
            if (dateOfBirth.AddYears(age) > DateTime.Today)
                age--;
            return age;
        }
    }
}