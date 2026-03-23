using Godot;
using System;

public partial class PlayerController : CharacterBody3D
{
	public float Speed {get; set;} = 5.0f;

	public Vector3 Forward{get { return -GlobalTransform.Basis.Z; } }

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		// if (!IsOnFloor())
		// {
		// 	velocity += GetGravity() * (float)delta;
		// }

		// Get the input direction and handle the movement/deceleration.
		Vector2 inputDir = Input.GetVector("move_right" , "move_left", "move_back", "move_forward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction == Vector3.Zero)
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		} else
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}

		Velocity = velocity;
		MoveAndSlide();

		//DebugDraw.Instance.DrawVector(Forward, 5, Colors.Green);
		DebugDraw.Instance.DrawLine(Position, Position + Vector3.Forward * 5, Colors.Green);
	}
}
