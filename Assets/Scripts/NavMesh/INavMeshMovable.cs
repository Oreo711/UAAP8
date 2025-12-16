using UnityEngine;
using UnityEngine.AI;


public interface INavMeshMovable : ITransformPosition
{
	bool IsJumping {get;}

	bool IsActive  {get;}

	void WalkTo (Vector3 point);

	bool IsOnNavMeshLink (out OffMeshLinkData offMeshLinkData);

	void Jump (OffMeshLinkData offMeshLinkData);
}
