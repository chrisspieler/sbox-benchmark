namespace Benchmark;

public sealed class Freecam : Component
{
	/// <summary>
	/// How many units per second the camera will move at normal speed.
	/// </summary>
	[Property] public float Speed { get; set; } = 500f;
	[Property] public string LowSpeedAction { get; set; } = "duck";
	[Property] public float LowSpeedFactor { get; set; } = 0.25f;
	[Property] public string HighSpeedAction { get; set; } = "run";
	[Property] public float HighSpeedFactor { get; set; } = 2.5f;
	/// <summary>
	/// If true, prevents the player from looking higher than directly up or lower
	/// than directly down. Prevents the camera from going upside-down and doing loop-de-loops.
	/// </summary>
	[Property] public bool ClampPitch { get; set; } = true;
	[RequireComponent] 
	public CameraComponent Camera { get; set; }

	private Angles _lookAngle;

	protected override void OnUpdate()
	{
		UpdateRotation();
		UpdatePosition();
	}

	private void UpdateRotation()
	{
		_lookAngle += Input.AnalogLook;
		if ( ClampPitch )
		{
			_lookAngle.pitch = _lookAngle.pitch.Clamp( -89f, 89f );
		}
		Transform.Rotation = _lookAngle;
	}

	private void UpdatePosition()
	{
		var speedFactor = Input.Down( LowSpeedAction )
			? LowSpeedFactor 
			: Input.Down( HighSpeedAction ) ? HighSpeedFactor : 1f;
		var movement = Input.AnalogMove * Speed * speedFactor;
		movement *= Scene.Camera.Transform.Rotation;
		Transform.Position += movement * Time.Delta;
	}
}
