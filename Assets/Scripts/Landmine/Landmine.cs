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

	public bool IsDetonated {get; private set;}
	public float TriggerDistance => _triggerDistance;

	private void Awake ()
	{
		_collider = GetComponent<SphereCollider>();
		_collider.radius = _triggerDistance;
	}

	private void OnTriggerEnter (Collider other)
	{
			StartCoroutine(Fuse());
	}

	private IEnumerator Fuse ()
	{
		yield return new WaitForSeconds(_explosionTime);

		Collider[] hitObjects = Physics.OverlapSphere(transform.position, _triggerDistance);

		foreach (Collider hitObject in hitObjects)
		{
			if (hitObject.TryGetComponent(out IHealth health))
			{
				health.TakeDamage(_explosionDamage);
			}
		}

		IsDetonated = true;
	}
}
