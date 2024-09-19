using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MVC.PL.Helpers
{
	public static class DocumentSettings
	{
		public static string UploadFile(IFormFile file, string folderName)
		{
			// 1. Get Located FolderPath
			string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

			// 2. Get FileName and make it unique
			string fileName = $"{Guid.NewGuid()}{file.FileName}";

			// 3. Get FilePath [FolderPath + FileName]
			string filePath = Path.Combine(folderPath, fileName);

			// 4. Save Files as Streams
			using var fileStream = new FileStream(filePath , FileMode.Create);
			file.CopyTo(fileStream);

			return fileName;
		}

		public static void DeleteFile(string fileName, string folderName)
		{
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files" ,fileName);
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
		}
	}
}
