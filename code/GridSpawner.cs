namespace Benchmark;

public sealed class GridSpawner : Component
{
	public delegate void OnAfterSpawnDelegate( int spawnedCount );
	[Property] public OnAfterSpawnDelegate OnAfterSpawn { get; set; }

	[Property] public CellSpawner CellSpawner { get; set; }
	[Property] public int XCount { get; set; } = 5;
	[Property] public float XSpacing { get; set; } = 64f;
	[Property] public int YCount { get; set; } = 5;
	[Property] public float YSpacing { get; set; } = 64f;
	[Property] public int ZCount { get; set; } = 5;
	[Property] public float ZSpacing { get; set; } = 64f;
	[Property] public bool SpawnOnStart { get; set; } = true;
	/// <summary>
	/// Returns the worldspace position at the center of the grid.
	/// </summary>
	[Property] public Vector3 Center
	{
		get
		{
			var localCenter = new Vector3()
			{
				x = XCount * XSpacing / 2,
				y = YCount * YSpacing / 2,
				z = ZCount * ZSpacing / 2
			};
			return Transform.World.PointToWorld( localCenter );
		}
	}
	protected override void OnStart()
	{
		if ( SpawnOnStart )
		{
			SpawnAll();
		}
	}

	public void SpawnAll()
	{
		CellSpawner ??= Components.Get<CellSpawner>();
		if ( CellSpawner is null )
			return;

		for ( int x = 0; x < XCount; x++ )
		{
			for ( int y = 0; y < YCount; y++ )
			{
				for ( int z = 0; z < ZCount; z++ )
				{
					var offset = new Vector3( x * XSpacing, y * YSpacing, z * ZSpacing );
					CellSpawner.SpawnCell( new Vector3Int( x, y, z ), offset );
				}
			}
		}
		OnAfterSpawn?.Invoke( XCount * YCount * ZCount );
	}
}
