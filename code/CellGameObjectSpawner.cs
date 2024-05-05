namespace Benchmark;

public class CellGameObjectSpawner : CellSpawner
{
	[Property] public GameObject Parent { get; set; }

	public override void SpawnCell( Vector3Int cell, Vector3 position )
	{
		var go = new GameObject( true, $"{cell}" );
		go.Transform.Position = position;
		go.SetParent( Parent );
	}
}
