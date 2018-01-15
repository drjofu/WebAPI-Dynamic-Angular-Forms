using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAutoGenerateAnsatzWebApiServer.Reflection
{
  public static class PropertyDescriptorExtensions
  {
    public static bool GetAttributeExists<TAttribute>(this PropertyDescriptor propertyDescriptor)
    {
      foreach (object attrib in propertyDescriptor.Attributes)
      {
        if (attrib is TAttribute)
        {
          return true;
        }
      }
      return false;
    }

    public static TAttribute GetAttribute<TAttribute>(this PropertyDescriptor propertyDescriptor)
    {
      foreach (object attrib in propertyDescriptor.Attributes)
      {
        if (attrib is TAttribute)
        {
          return (TAttribute)attrib;
        }
      }
      return default(TAttribute);
    }

    public static void Validate(this PropertyDescriptor propertyDescriptor, object value)
    {
      foreach (object attrib in propertyDescriptor.Attributes)
      {
        ValidationAttribute validationAttribute = attrib as ValidationAttribute;
        if (validationAttribute != null)
          validationAttribute.Validate(value, propertyDescriptor.Name);
      }
    }

    public static string GetValidationError(this PropertyDescriptor propertyDescriptor, object value)
    {
      foreach (object attrib in propertyDescriptor.Attributes)
      {
        ValidationAttribute validationAttribute = attrib as ValidationAttribute;
        if (validationAttribute != null)
          if (!validationAttribute.IsValid(value))
            return validationAttribute.FormatErrorMessage(propertyDescriptor.Name);
      }
      return "";
    }

    public static IEnumerable<KeyValuePair<string, string>> GetInfosForAttributes(this PropertyDescriptor propertyDescriptor)
    {
      foreach (object attrib in propertyDescriptor.Attributes)
      {
        RangeAttribute rangeAttribute = attrib as RangeAttribute;
        if (rangeAttribute != null)
          yield return new KeyValuePair<string, string>("Wertebereich", rangeAttribute.Minimum + " bis " + rangeAttribute.Maximum);

        StringLengthAttribute stringLengthAttribute = attrib as StringLengthAttribute;
        if (stringLengthAttribute != null)
          yield return new KeyValuePair<string, string>("Textlänge", stringLengthAttribute.MinimumLength + " bis " + stringLengthAttribute.MaximumLength);
      }
    }

  }
}
