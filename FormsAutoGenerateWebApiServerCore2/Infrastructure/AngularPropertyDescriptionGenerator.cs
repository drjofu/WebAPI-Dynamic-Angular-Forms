using FormsAutoGenerateAnsatzWebApiServer.Reflection;
using FormsAutoGenerateWebApiServerCore2.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FormsAutoGenerateAnsatzWebApiServer.Infrastructure
{
  public class AngularPropertyDescriptionGenerator
  {

    /// <summary>
    /// Metadatenstruktur aus Property-Informationen für Model-Klasse ermitteln
    /// Namen werden für den Zugriff in JavaScript in CamelCase-Notation gewandelt
    /// </summary>
    /// <param name="type">Typ der Model-Klasse</param>
    /// <param name="ctx">DbContext (optional)</param>
    /// <returns></returns>
    public static IEnumerable<AngularPropertyDescriptorBase> GetAngularPropertyDescriptionForType(Type type, DbContext ctx = null)
    {
      // Metadaten für alle Properties der Klasse
      PropertyDescriptorCollection propDescriptors = TypeDescriptor.GetProperties(type);

      foreach (PropertyDescriptor propertyDescriptor in propDescriptors)
      {
        // Bei JsonIgnore diese Eigenschaft nicht berücksichtigen
        if (propertyDescriptor.GetAttributeExists<JsonIgnoreAttribute>())
          continue;

        AngularPropertyDescriptorBase angularPropertyDescription;

        // Wenn LookupTable-Attribut gesetzt ist, dann Daten für <select>-Control generieren, sonst für <input>
        var lookupNameAttr = propertyDescriptor.GetAttribute<LookupTableAttribute>();
        if (lookupNameAttr == null)
          angularPropertyDescription = new AngularPropertyDescriptorTextbox();
        else
        {
          angularPropertyDescription = new AngularPropertyDescriptorSelektor()
          {
            LookupUrl = lookupNameAttr.LookupUrl,                      // Url zum Abruf der Daten
            LookupIdField = lookupNameAttr.KeyField.ToCamelCase(),     // Schlüsselfeld
            LookupValueField = lookupNameAttr.ValueField.ToCamelCase() // Feld mit den anzuzeigenden Informationen
          };
        }

        // Feldname der Eigenschaft
        angularPropertyDescription.Name = propertyDescriptor.Name.ToCamelCase();

        // Verschiedene Informationen aus dem Display-Attribut
        var displayAttr = propertyDescriptor.GetAttribute<DisplayAttribute>();

        if (displayAttr == null)
        {
          // Falls nicht gesetzt, für die Anzeige den Eigenschaftsnamen verwenden
          angularPropertyDescription.Label = propertyDescriptor.Name;
        }
        else
        {
          // Labeltext
          angularPropertyDescription.Label = displayAttr.Name;
          try
          {
            // Reihenfolge
            angularPropertyDescription.Order = displayAttr.Order;
          }
          catch (Exception)
          {
          }
        }

        // Anzeige unterdrücken mit ScaffoldColumn
        var scaffoldColumnAttr = propertyDescriptor.GetAttribute<ScaffoldColumnAttribute>();
        if (scaffoldColumnAttr != null)
        {
          angularPropertyDescription.Visible = scaffoldColumnAttr.Scaffold;
        }

        // ReadOnly-Attribut berücksichtigen
        var readOnlyAttr = propertyDescriptor.GetAttribute<ReadOnlyAttribute>();
        if (readOnlyAttr != null)
        {
          angularPropertyDescription.ReadOnly = readOnlyAttr.IsReadOnly;
        }

        // Andere ReadOnly-Varianten berücksichtigen
        angularPropertyDescription.ReadOnly |= propertyDescriptor.IsReadOnly;

        // Typ für <input>-Feld
        var dataTypeAttr = propertyDescriptor.GetAttribute<DataTypeAttribute>();
        if (dataTypeAttr != null)
        {
          angularPropertyDescription.DisplayType = dataTypeAttr.GetDataTypeName();
        }

        // Required-Attribut
        angularPropertyDescription.Required = propertyDescriptor.GetAttributeExists<RequiredAttribute>();

        // Required auch für non Nullable Value Types
        angularPropertyDescription.Required |= propertyDescriptor.PropertyType.IsValueType 
          && Nullable.GetUnderlyingType(propertyDescriptor.PropertyType) == null;

        // String-Längen-Einschränkungen
        angularPropertyDescription.MinLength = propertyDescriptor.GetAttribute<MinLengthAttribute>()?.Length;
        angularPropertyDescription.MaxLength = propertyDescriptor.GetAttribute<MaxLengthAttribute>()?.Length;

        var stringlength = propertyDescriptor.GetAttribute<StringLengthAttribute>();
        if(stringlength != null)
        {
          angularPropertyDescription.MaxLength = stringlength.MaximumLength;
          angularPropertyDescription.MinLength = stringlength.MinimumLength;
        }

        // Bereichseinschränkungen (wird nur bei numerischen Typen berücksichtigt)
        angularPropertyDescription.Minimum = propertyDescriptor.GetAttribute<RangeAttribute>()?.Minimum;
        angularPropertyDescription.Maximum = propertyDescriptor.GetAttribute<RangeAttribute>()?.Maximum;

        //angularPropertyDescription.EvenNumber = propertyDescriptor.GetAttribute<EvenNumberAttribute>() != null;

        //angularPropertyDescription.DividableBy = propertyDescriptor.GetAttribute<DividableByAttribute>()?.Divisor;


        // Falls der DbContext übergeben wurde, prüfen, ob Property der Primärschlüssel ist
        // wird derzeit nicht ausgewertet, nur der in der Tabellendefinition angegebene Primärschlüssel
        if (ctx != null)
        {
          var et = ctx.Model.FindEntityType(type);
          var p = et.FindProperty(propertyDescriptor.Name);
          angularPropertyDescription.IsPrimaryKey |= p.IsPrimaryKey();
          //angularPropertyDescription.ReadOnly |= angularPropertyDescription.IsPrimaryKey;
        }

        yield return angularPropertyDescription;

      }

    }
  }
}
