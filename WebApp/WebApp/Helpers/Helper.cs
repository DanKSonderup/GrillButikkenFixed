using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Helpers
{
    public static class Helper
    {
        public static string CapitalizeFirstLetter(string input)
        {
            return $"{input[0].ToString().ToUpper()}{input.Substring(1)}";
        }
    }
}