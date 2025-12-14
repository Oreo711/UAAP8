using UnityEngine;

public interface INavMeshMovable : ITransformPosition
{
	void WalkTo (Vector3 point);
}
