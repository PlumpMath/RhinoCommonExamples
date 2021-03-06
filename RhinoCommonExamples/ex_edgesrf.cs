using System.Linq;
using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Input.Custom;
/// <summary>
/// title: Create a Surface from Edge Curves
/// keywords: ['create', 'surface', 'edge', 'curves']
/// categories: ['Curves']
/// </summary>
partial class Examples
{
  public static Result EdgeSrf(RhinoDoc doc)
  {
    var go = new GetObject();
    go.SetCommandPrompt("Select 2, 3, or 4 open curves");
    go.GeometryFilter = ObjectType.Curve;
    go.GeometryAttributeFilter = GeometryAttributeFilter.OpenCurve;
    go.GetMultiple(2, 4);
    if (go.CommandResult() != Result.Success)
      return go.CommandResult();

    var curves = go.Objects().Select(o => o.Curve());

    var brep = Brep.CreateEdgeSurface(curves);

    if (brep != null)
    {
      doc.Objects.AddBrep(brep);
      doc.Views.Redraw();
    }

    return Result.Success;
  }
}
