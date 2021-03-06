using Rhino;
using Rhino.Commands;
/// <summary>
/// title: Determine Rhino's Language Setting
/// keywords: ['determine', 'rhinos', 'language', 'setting']
/// categories: ['Other']
/// </summary>
partial class Examples
{
  public static Result DetermineCurrentLanguage(RhinoDoc doc)
  {
    var language_id = Rhino.ApplicationSettings.AppearanceSettings.LanguageIdentifier;
    var culture = new System.Globalization.CultureInfo(language_id);
    RhinoApp.WriteLine("The current language is {0}", culture.EnglishName);
    return Result.Success;
  }
}
