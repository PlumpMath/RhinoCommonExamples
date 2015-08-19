using Rhino;
using Rhino.Geometry;
using Rhino.DocObjects;
using Rhino.Commands;
using Rhino.Input.Custom;
/// <summary>
/// title: Extend a Surface
/// keywords: ['extend', 'surface']
/// </summary>
partial class Examples
{
  public static Result ExtendSurface(RhinoDoc doc)
  {
    var go = new Rhino.Input.Custom.GetObject();
    go.SetCommandPrompt("Select edge of surface to extend");
    go.GeometryFilter = ObjectType.EdgeFilter;
    go.GeometryAttributeFilter = GeometryAttributeFilter.EdgeCurve;
    go.Get();
    if (go.CommandResult() != Result.Success)
      return go.CommandResult();
    var obj_ref = go.Object(0);

    var surface = obj_ref.Surface();
    if (surface == null)
    {
      RhinoApp.WriteLine("Unable to extend polysurfaces.");
      return Result.Failure;
    }

    var brep = obj_ref.Brep();
    var face = obj_ref.Face();
    if (brep == null || face == null)
      return Result.Failure;
    if (face.FaceIndex < 0)
      return Result.Failure;

    if( !brep.IsSurface)
    {
      RhinoApp.WriteLine("Unable to extend trimmed surfaces.");
      return Result.Nothing;
    }

    var curve = obj_ref.Curve();

    var trim = obj_ref.Trim();
    if (trim == null)
      return Result.Failure;

    if (trim.TrimType == BrepTrimType.Seam)
    {
      RhinoApp.WriteLine("Unable to extend surface at seam.");
      return Result.Nothing;
    }

    var extended_surface = surface.Extend(trim.IsoStatus, 5.0, true);
    if (extended_surface != null)
    {
      var mybrep = Brep.CreateFromSurface(extended_surface);
      doc.Objects.Replace(obj_ref.ObjectId, mybrep);
      doc.Views.Redraw();
    }
    return Result.Success;
  }
}
