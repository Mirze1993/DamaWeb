﻿using MicroORM.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error")]
        public IActionResult Error(int statusCode)
        {
            ViewBag.Message = statusCode;
            return View();
        }

        [Route("/Account/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Route("/Exception")]
        public async Task<IActionResult> Exception(int statusCode)
        {
            var errorInfo = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var l = new LogWriteFile();
            await l.WriteFileAsync($"{errorInfo.Path} message:{errorInfo.Error.Message} {Environment.NewLine}Track: {errorInfo.Error.StackTrace}", LogLevel.Error);
            return View("Error");
        }
    }
}
