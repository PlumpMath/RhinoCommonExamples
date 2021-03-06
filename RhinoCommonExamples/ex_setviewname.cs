using Rhino;
using Rhino.Commands;
/// <summary>
/// title: Setting a Viewport's Title
/// keywords: ['setting', 'viewports', 'title']
/// categories: ['Viewports and Views']
/// </summary>
partial class Examples
{
  public static Result SetViewName(RhinoDoc doc)
  {
    var view = doc.Views.ActiveView;
    if (view == null)
      return Result.Failure;
   
    view.MainViewport.Name = "Facade";
    return Result.Success;
  }
}
