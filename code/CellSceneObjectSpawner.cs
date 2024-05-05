namespace Benchmark;

public class CellSceneObjectSpawner : CellSpawner
{
	[Property] public Model Model { get; set; }
	[Property] public Vector3 Scale { get; set; } = Vector3.One;

	private List<SceneModel> _soInstances;

	public override void SpawnCell( Vector3Int cell, Vector3 position )
	{
		if ( Model is null )
			return;

		_soInstances ??= new List<SceneModel>();
		var tx = new Transform( position, Rotation.Identity, Scale );
		_soInstances.Add( new SceneModel( Scene.SceneWorld,	Model, tx ) );
	}
}
