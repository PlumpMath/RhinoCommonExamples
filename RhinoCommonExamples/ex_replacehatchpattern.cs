using Rhino;
using Rhino.DocObjects;
using Rhino.Commands;
using Rhino.Input;
using Rhino.Input.Custom;
/// <summary>
/// title: Replacing a Hatch Object's Pattern
/// keywords: ['replacing', 'hatch', 'objects', 'pattern']
/// categories: ['Adding Objects']
/// </summary>
partial class Examples
{
  public static Result ReplaceHatchPattern(RhinoDoc doc)
  {
    ObjRef[] obj_refs;
    var rc = RhinoGet.GetMultipleObjects("Select hatches to replace", false, ObjectType.Hatch, out obj_refs);
    if (rc != Result.Success || obj_refs == null)
      return rc;

    var gs = new GetString();
    gs.SetCommandPrompt("Name of replacement hatch pattern");
    gs.AcceptNothing(false);
    gs.Get();
    if (gs.CommandResult() != Result.Success)
      return gs.CommandResult();
    var hatch_name = gs.StringResult();

    var pattern_index = doc.HatchPatterns.Find(hatch_name, true);

    if (pattern_index < 0)
    {
      RhinoApp.WriteLine("The hatch pattern \"{0}\" not found  in the document.", hatch_name);
      return Result.Nothing;
    }

    foreach (var obj_ref in obj_refs)
    {
      var hatch_object = obj_ref.Object() as HatchObject;
      if (hatch_object.HatchGeometry.PatternIndex != pattern_index)
      {
        hatch_object.HatchGeometry.PatternIndex = pattern_index;
        hatch_object.CommitChanges();
      }
    }
    doc.Views.Redraw();
    return Result.Success;
  }
}
