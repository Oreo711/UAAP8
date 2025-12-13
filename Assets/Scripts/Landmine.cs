using System;
using UnityEngine;
using UnityEngine.Serialization;


[RequireComponent(typeof(SphereCollider))]
public class Landmine : MonoBehaviour
{
	[SerializeField] private float _triggerDistance = 2f;
	[SerializeField] private float _explosionDamage;
	[SerializeField] private float _detonationTime;

	private SphereCollider _collider;
	private float _remainingDetonationTime;
	private GameObject _activator;

	public bool IsActivated {get; private set;}
	public bool IsDetonated {get; private set;}
	public float TriggerDistance => _triggerDistance;

	private void Awake ()
	{
		_collider = GetComponent<SphereCollider>();
		_remainingDetonationTime = _detonationTime;
		_collider.radius = _triggerDistance;
	}

	private void Update ()
	{
		if (IsActivated)
		{
			_remainingDetonationTime -= Time.deltaTime;

			if (_remainingDetonationTime <= 0)
			{
				if (_activator && (_activator.transform.position - transform.position).magnitude < _triggerDistance)
				{
					Character activator = _activator.GetComponent<Character>();
					activator.TakeDamage(_explosionDamage);
				}

				IsDetonated = true;
			}
		}
	}

	private void OnTriggerEnter (Collider other)
	{
			IsActivated = true;
			_activator = other.gameObject;
	}

	private void OnTriggerExit (Collider other)
	{
			_remainingDetonationTime = _detonationTime;
	}
}
