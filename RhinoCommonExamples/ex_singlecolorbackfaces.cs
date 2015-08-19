using Rhino;
using Rhino.Commands;
using Rhino.Display;
using System.Linq;
using Rhino.Input.Custom;
using Rhino.DocObjects;
/// <summary>
/// title: Single Color Back Faces
/// </summary>
partial class Examples
{
  public static Result SingleColorBackfaces(RhinoDoc doc)
  {
    var display_mode_descs = //DisplayModeDescription.GetDisplayModes();
      from dm in DisplayModeDescription.GetDisplayModes()
      where dm.EnglishName == "Shaded"
      select dm;

    foreach (var dmd in display_mode_descs)
    {
      RhinoApp.WriteLine("CurveColor {0}", dmd.DisplayAttributes.CurveColor.ToKnownColor());
      RhinoApp.WriteLine("ObjectColor {0}", dmd.DisplayAttributes.ObjectColor.ToKnownColor());
    }
    return Result.Success;
  }
}
