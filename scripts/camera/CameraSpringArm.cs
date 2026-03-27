using Godot;
using System;

public partial class CameraSpringArm : SpringArm3D
{

	public const float ANGLE_ROTATE = 	Mathf.Pi / 2.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		RotateCamera((float) delta);
	}

	private void RotateCamera(float delta)
    {
		float inputRotationDir = Input.GetAxis("camera_rotate_right" , "camera_rotate_left");
		if(inputRotationDir == 0) return;

		RotateY(ANGLE_ROTATE * delta * inputRotationDir );

    }

}
