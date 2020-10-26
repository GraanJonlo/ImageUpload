using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ImageUpload.Models;
using Microsoft.AspNetCore.Http;

namespace ImageUpload.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View(new HomeViewModel());
		}

		[HttpPost]
		public IActionResult Index(IFormFile file, string someText)
		{
			byte[] imageBytes = new byte[file.Length];

			using var readStream = file.OpenReadStream();
			readStream.Read(imageBytes, 0, Convert.ToInt32(file.Length));

			var base64EncodedImage = Convert.ToBase64String(imageBytes);

			var imageSource = "data:" + file.ContentType + ";base64," + base64EncodedImage;

			return View(new HomeViewModel {Base64ImageSource = imageSource, ImageAlt = someText});
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
		}
	}
}
