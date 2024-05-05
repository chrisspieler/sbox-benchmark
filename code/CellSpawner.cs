namespace Benchmark;

public abstract class CellSpawner : Component
{
	public abstract void SpawnCell( Vector3Int cell, Vector3 position );
}
