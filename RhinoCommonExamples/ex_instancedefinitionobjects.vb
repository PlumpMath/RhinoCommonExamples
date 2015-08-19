﻿Option Infer On

Partial Friend Class Examples
  Public Shared Function InstanceDefinitionObjects(ByVal doc As Rhino.RhinoDoc) As Rhino.Commands.Result
	Dim objref As Rhino.DocObjects.ObjRef = Nothing
	Dim rc = Rhino.Input.RhinoGet.GetOneObject("Select instance", False, Rhino.DocObjects.ObjectType.InstanceReference, objref)
	If rc IsNot Rhino.Commands.Result.Success Then
	  Return rc
	End If

	Dim iref = TryCast(objref.Object(), Rhino.DocObjects.InstanceObject)
	If iref IsNot Nothing Then
	  Dim idef = iref.InstanceDefinition
	  If idef IsNot Nothing Then
		Dim rhino_objects = idef.GetObjects()
		For i As Integer = 0 To rhino_objects.Length - 1
		  Rhino.RhinoApp.WriteLine("Object {0} = {1}", i, rhino_objects(i).Id)
		Next i
	  End If
	End If
	Return Rhino.Commands.Result.Success
  End Function
End Class