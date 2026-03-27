using Godot;
using System;
using System.Diagnostics;

public partial class CameraController : Camera3D
{
	[Export] Node3D toFollow;
	public float Speed {get; set;} = 3.0f;
	public float angle = Mathf.Pi / 4; // 45

	private Vector3 _deltaTargetDistance;

	public Vector3 Forward{get { return -GlobalTransform.Basis.Z.Normalized(); } }

    public override void _Ready()
    {
        _deltaTargetDistance = Position - toFollow.Position;
    }


	public override void _Process(double delta)
	{
        ToFollowTarget(delta);
		RotateCamera(delta);

	}


    private void ToFollowTarget(double delta)
    {
        Vector3 dirToTarget = (toFollow.Position - Position + _deltaTargetDistance).Normalized();
		dirToTarget.Y = 0; // ignore height

		if(dirToTarget == Vector3.Zero) return;

		float speedPerFrame = Speed * (float) delta;

		Position += dirToTarget * speedPerFrame;
    }

	private void RotateCamera(double delta)
    {
		float inputRotationDir = Input.GetAxis("camera_rotate_right" , "camera_rotate_left");
		// //GD.Print($"ROTATE CAMERA: Dir -> {inputRotationDir}");
		if(inputRotationDir == 0) return;

        // Vector3 dirToTarget = (toFollow.Position - Position).Normalized();
		// float angle = dirToTarget.AngleTo(Forward);
		// //GD.Print($"ANGLE -> {angle}");
		float rotationSpeedDelta = angle * (float) delta * inputRotationDir;
		
		// angle += inputRotationDir * rotationSpeedDelta;
		//float velocity = (float) delta * rotationSpeed * inputRotationDir;
		//Position = Position.MoveToward(toFollow.Position, velocity);

		//LookAt(toFollow.GlobalPosition, Vector3.Up);
		//Smooth rotation
		// Vector3 targetDir = GlobalPosition.DirectionTo(toFollow.GlobalPosition);
		// Basis targetBasis = Basis.LookingAt(targetDir);
		// Basis = Basis.Slerp(targetBasis, .5f); // weight between 0-1

		//RotateY((float)delta * rotationSpeed);
		RotateObjectLocal(Vector3.Up, rotationSpeedDelta );

    }


	// private void DrawForward()
	// {
	// 	Vector3 forward  = Forward;
	// 	Vector3 pointForward = Position + (Forward * 5.0f);
	// 	Debug.DrawLine(Position, pointForward, Colors.Green, 2.0f);
	// }

}
