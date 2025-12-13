using System;
using UnityEngine;

public class LandmineVisuals : MonoBehaviour
{
	[SerializeField] private Landmine    _landmine;
	[SerializeField] private GameObject  _explosionIndicatorPrefab;
	[SerializeField] private float       _indicatorLinger;
	[SerializeField] private AudioSource _explosionSound;

	private void Update ()
	{
		if (_landmine.IsDetonated)
		{
			_explosionSound.transform.position = transform.position;
			_explosionSound.Play();

			GameObject indicator = Instantiate(_explosionIndicatorPrefab, transform.position, Quaternion.identity);
			indicator.transform.localScale = Vector3.one * (_landmine.TriggerDistance * 2);
			Destroy(indicator, _indicatorLinger);
			Destroy(_landmine.gameObject);
		}
	}
}
