using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using System;
using System.IO;
using System.Threading.Tasks;
using PhotoSauce.MagicScaler;

namespace BlogAdaia.Data.FileManager
{
    public class FileManager : IFileManager
    {
        private string _imagePath;
        public FileManager(IConfiguration config)
        {
            _imagePath = config["Path:Images"];

        }

        public FileStream imageStream(string image)
        {
            return new FileStream(Path.Combine(_imagePath, image),FileMode.Open, FileAccess.Read);
        }

        public bool RemoveImage(string image)
        {
            try { 

            var file=Path.Combine(_imagePath, image);
            if(File.Exists(file)) 
                File.Delete(file);
                return true;

                }
            catch(Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }


        public async Task<string> SaveImage(IFormFile image)
        {
            var save_path=Path.Combine(_imagePath);
            try { 

            if(!Directory.Exists(save_path))
            {
                Directory.CreateDirectory(save_path);
            }

            var mime = image.FileName.Substring(image.FileName.LastIndexOf("."));

            var fileName=$"img_{DateTime.Now.ToString("dd-MM-yyy-HH-mm-ss")}{mime}";

            using (var filestream = new FileStream(Path.Combine(save_path, fileName), FileMode.Create))
            {
                /*await image.CopyToAsync(filestream);*/
                    MagicImageProcessor.ProcessImage(image.OpenReadStream(), filestream,imageoptions());
                }

               
                return fileName;

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);

                return "Error-";
            }
        }
        private ProcessImageSettings imageoptions() => new ProcessImageSettings
        {

            Width = 500,
            Height = 500,
            ResizeMode = CropScaleMode.Crop,
            SaveFormat=FileFormat.Jpeg,
            JpegQuality = 100,
            JpegSubsampleMode=ChromaSubsampleMode.Subsample420

        };
    }
}
