using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAutoGenerateWebApiServerCore2.Utilities
{
  public static class StringHelper
  {
    public static string ToCamelCase(this string text)
    {
      if (string.IsNullOrEmpty(text) || text.Length < 2) throw new ArgumentException("Stringlength > 1 expected");
      return text.Substring(0, 1).ToLower() + text.Substring(1);
    }

    public static string ToPascalCase(this string text)
    {
      if (string.IsNullOrEmpty(text) || text.Length < 2) throw new ArgumentException("Stringlength > 1 expected");
      return text.Substring(0, 1).ToUpper() + text.Substring(1);
    }


  }
}
