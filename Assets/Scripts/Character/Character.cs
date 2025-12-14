using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class Character : MonoBehaviour, INavMeshMovable, IHealth
{
	[SerializeField] private NavMeshAgent _agent;
	[SerializeField] private float        _maxHealth = 100f;

	public float MaxHealth => _maxHealth;

	public float CurrentHealth {get; private set;}

	public float Velocity => _agent.velocity.magnitude;

	public Vector3 Position => transform.position;

	private void Awake ()
	{
		CurrentHealth = _maxHealth;
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
}
