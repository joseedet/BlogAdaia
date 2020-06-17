using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdaia.Data
{
    public interface IFileManager
    {
        FileStream imageStream(string image);
        Task<string> SaveImage(IFormFile file);
        bool RemoveImage(string image);


    }
}
