using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp
{
    public class UserPictureModel
    {
        public IFormFile Base64Data { get; set; }
    }
}
