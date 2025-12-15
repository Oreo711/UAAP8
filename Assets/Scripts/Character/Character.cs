using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class Character : MonoBehaviour, INavMeshMovable, IHealth
{
	[SerializeField] private NavMeshAgent _agent;
	[SerializeField] private float _maxHealth = 100f;
	[SerializeField] private float _jumpSpeed;

	private AgentJumper _jumper;

	public float MaxHealth => _maxHealth;

	public float CurrentHealth {get; private set;}

	public float Velocity => _agent.velocity.magnitude;

	public Vector3 Position => transform.position;

	public bool IsJumping => _jumper.InProcess;

	private void Awake ()
	{
		CurrentHealth = _maxHealth;

		_jumper = new AgentJumper(_jumpSpeed, _agent, this);
	}

	public void WalkTo (Vector3 point)
	{
		_agent.SetDestination(point);
	}

	public void TakeDamage (float damage)
	{
		CurrentHealth -= damage;

		if (CurrentHealth <= 0)
		{
			CurrentHealth = 0;
		}
	}

	public void HealDamage (float heal)
	{
		CurrentHealth += heal;

		if (CurrentHealth > _maxHealth)
		{
			CurrentHealth = _maxHealth;
		}
	}

	public bool IsOnNavMeshLink (out OffMeshLinkData offMeshLinkData)
	{
		if (_agent.isOnOffMeshLink)
		{
			offMeshLinkData = _agent.currentOffMeshLinkData;
			return true;
		}

		offMeshLinkData = default;
		return false;
	}

	public void Jump (OffMeshLinkData offMeshLinkData)
	{
		_jumper.Jump(offMeshLinkData);
	}
}
