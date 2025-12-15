using UnityEngine;
using UnityEngine.AI;


public interface INavMeshMovable : ITransformPosition
{
	public bool IsJumping {get;}

	void WalkTo (Vector3 point);

	public bool IsOnNavMeshLink (out OffMeshLinkData offMeshLinkData);

	public void Jump (OffMeshLinkData offMeshLinkData);
}
