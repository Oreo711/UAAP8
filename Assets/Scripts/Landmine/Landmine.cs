using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;


[RequireComponent(typeof(SphereCollider))]
public class Landmine : MonoBehaviour
{
	[SerializeField] private float _triggerDistance = 2f;
	[SerializeField] private float _explosionDamage;
	[SerializeField] private float _explosionTime;

	private SphereCollider _collider;
	private GameObject _activator;

	public bool IsActivated {get; private set;}
	public bool IsDetonated {get; private set;}
	public float TriggerDistance => _triggerDistance;

	private void Awake ()
	{
		_collider = GetComponent<SphereCollider>();
		_collider.radius = _triggerDistance;
	}

	private void OnTriggerEnter (Collider other)
	{
			IsActivated = true;
			_activator = other.gameObject;

			StartCoroutine(Fuse());
	}

	private IEnumerator Fuse ()
	{
		yield return new WaitForSeconds(_explosionTime);

		if (_activator && (_activator.transform.position - transform.position).magnitude < _triggerDistance)
		{
			IHealth activator = _activator.GetComponent<IHealth>();
			activator?.TakeDamage(_explosionDamage);
		}

		IsDetonated = true;
	}
}
