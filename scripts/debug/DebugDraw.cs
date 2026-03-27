using Godot;
using System;

public partial class DebugDraw : Node
{
	private static DebugDraw _instance;
	public static DebugDraw Instance { get{ return _instance;} }

	private ImmediateMesh _drawDebug;

	private Color _defaultColor = Colors.Red;

	public override void _Ready()
	{
		if(_instance == null) _instance = this;
		MeshInstance3D meshInstance3D = GetNode<MeshInstance3D>("MeshInstance3D");
		_drawDebug = meshInstance3D.Mesh as ImmediateMesh;
	}

    public override void _PhysicsProcess(double delta)
    {
       _drawDebug.ClearSurfaces();
    }

	public void DrawVector(Vector3 start, Vector3 vector, float size, Color color = default)
	{
		Vector3 end = start * vector * size;
		//GD.Print($"DEBUG - DIRECTION : {end}");
		DrawLine(start, end, color);
	}


	public void DrawLine(Vector3 start, Vector3 end, Color color = default )
	{
		if(start.IsEqualApprox(end)) return;

		_drawDebug.SurfaceBegin(Mesh.PrimitiveType.Lines);
		_drawDebug.SurfaceSetColor(color);
		_drawDebug.SurfaceAddVertex(start);
		_drawDebug.SurfaceAddVertex(end);

		_drawDebug.SurfaceEnd();
	}

	
}
