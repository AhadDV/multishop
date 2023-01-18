namespace MultiShopBB202.Extentions
{
    public static class FileExtention
    {
        public static string FileCreate(this IFormFile file,string env,string folderPath)
        {
            string filename=$"{Guid.NewGuid()}{file.FileName}";
            string FullPath=Path.Combine(env, folderPath,filename);


            using(FileStream fileStream=new FileStream(FullPath,FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return filename;
        }
    }
}
