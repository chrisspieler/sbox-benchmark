namespace Benchmark;

public class CellPrefabSpawner : CellSpawner
{
	[Property] public GameObject Prefab { get; set; }

	public override void SpawnCell( Vector3Int cell, Vector3 position )
	{
		if ( Prefab is null )
			return;

		Prefab.Clone( position );
	}
}
