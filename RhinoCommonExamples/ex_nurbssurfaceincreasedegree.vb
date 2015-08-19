﻿Option Infer On

Imports Rhino
Imports Rhino.Commands
Imports Rhino.Input
Imports Rhino.DocObjects

Partial Friend Class Examples
  Public Shared Function NurbsSurfaceIncreaseDegree(ByVal doc As RhinoDoc) As Result
	Dim obj_ref As ObjRef = Nothing
	Dim rc = RhinoGet.GetOneObject("Select surface", False, ObjectType.Surface, obj_ref)
	If rc IsNot Result.Success Then
		Return rc
	End If
	If obj_ref Is Nothing Then
		Return Result.Failure
	End If
	Dim surface = obj_ref.Surface()
	If surface Is Nothing Then
		Return Result.Failure
	End If
	Dim nurbs_surface = surface.ToNurbsSurface()

	Dim new_u_degree As Integer = -1
	rc = RhinoGet.GetInteger(String.Format("New U degree <{0}...11>", nurbs_surface.Degree(0)), True, new_u_degree, nurbs_surface.Degree(0), 11)
	If rc IsNot Result.Success Then
		Return rc
	End If

	Dim new_v_degree As Integer = -1
	rc = RhinoGet.GetInteger(String.Format("New V degree <{0}...11>", nurbs_surface.Degree(1)), True, new_v_degree, nurbs_surface.Degree(1), 11)
	If rc IsNot Result.Success Then
		Return rc
	End If

	rc = Result.Failure
	If nurbs_surface.IncreaseDegreeU(new_u_degree) Then
	  If nurbs_surface.IncreaseDegreeV(new_v_degree) Then
		If doc.Objects.Replace(obj_ref.ObjectId, nurbs_surface) Then
		  rc = Result.Success
		End If
	  End If
	End If

	RhinoApp.WriteLine("Result: {0}", rc.ToString())
	doc.Views.Redraw()
	Return rc
  End Function
End Class