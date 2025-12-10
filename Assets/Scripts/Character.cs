using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class Character : MonoBehaviour, INavMeshMovable
{
	[SerializeField] private NavMeshAgent _agent;

	public float Velocity => _agent.velocity.magnitude;

	public Vector3 Position => transform.position;

	public void WalkTo (Vector3 point)
	{
		_agent.SetDestination(point);
	}
}
