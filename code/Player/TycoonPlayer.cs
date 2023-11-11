using Sandbox;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class TycoonPlayer : AnimatedEntity
{
	[ClientInput]
	public Vector3 InputDirection { get; protected set; }
	[ClientInput]
	public Angles ViewAngles { get; set; }

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/citizen/citizen.vmdl" );

		EnableDrawing = true;
		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;

	}

	public override void Simulate( IClient cl )
	{
		base.Simulate( cl );

		Rotation = ViewAngles.ToRotation();

	}

	public override void FrameSimulate( IClient cl )
	{
		base.FrameSimulate( cl );
		Camera.Position = Position + Vector3.Up * 1000f;
		Camera.Rotation = Rotation.FromPitch( 90f );
	}

	public override void OnKilled()
	{
		base.OnKilled();

		EnableDrawing = false;
	}

	public override void BuildInput( )
	{
		base.BuildInput( );

		InputDirection = Input.AnalogMove;

		var look = Input.AnalogLook;

		var viewAngles = ViewAngles;
		viewAngles += look;
		ViewAngles = viewAngles.Normal;

	}
}
