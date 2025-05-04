namespace WebApplication4.Helper
{
        public static class FileHelper
        {
            public static async Task<string> SaveFileAsync(this IFormFile file, string webrootpath,string folder)
            {
                string filepath=Path.Combine(webrootpath,folder).ToLower();
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                var path=$"/{folder}?"+Guid.NewGuid()+file.FileName;
                using FileStream fileStream=new FileStream(webrootpath+file,FileMode.Create);

                await file.CopyToAsync(fileStream);
                return path;
            }
        }
    }
