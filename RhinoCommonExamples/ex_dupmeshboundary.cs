using Rhino;
using Rhino.Commands;
using Rhino.Input.Custom;
using Rhino.DocObjects;
/// <summary>
/// title: Bounding Polyline from Naked Edges of Open Mesh
/// keywords: ['bounding', 'polyline', 'naked', 'edges', 'open', 'mesh']
/// categories: ['Other']
/// </summary>
partial class Examples
{
  public static Result DupMeshBoundary(RhinoDoc doc)
  {
    var gm = new GetObject();
    gm.SetCommandPrompt("Select open mesh");
    gm.GeometryFilter = ObjectType.Mesh;
    gm.GeometryAttributeFilter = GeometryAttributeFilter.OpenMesh;
    gm.Get();
    if (gm.CommandResult() != Result.Success)
      return gm.CommandResult();
    var mesh = gm.Object(0).Mesh();
    if (mesh == null)
      return Result.Failure;

    var polylines = mesh.GetNakedEdges();
    foreach (var polyline in polylines)
    {
      doc.Objects.AddPolyline(polyline);
    }

    return Result.Success;
  }
}
