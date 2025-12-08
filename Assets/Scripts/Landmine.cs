using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(SphereCollider))]
public class Landmine : MonoBehaviour
{
    [SerializeField] private float       _triggerDistance = 2f;
    [SerializeField] private float       _explosionDamage;
    [SerializeField] private float       _detonationTime;
    [SerializeField] private AudioSource _explosionSound;
    [SerializeField] private GameObject  _explosionIndicatorPrefab;
    [SerializeField] private float       _indicatorLinger;

    private SphereCollider _collider;
    private float    _remainingDetonationTime;

    private void Awake ()
    {
        _collider = GetComponent<SphereCollider>();
        _remainingDetonationTime = _detonationTime;
        _collider.radius = _triggerDistance;
    }

    private void OnTriggerStay (Collider other)
    {
        if (!other.TryGetComponent(out Character character))
        {
            return;
        }

        _remainingDetonationTime -= Time.deltaTime;

        if (_remainingDetonationTime <= 0)
        {
            _explosionSound.transform.position = transform.position;
            _explosionSound.Play();
            GameObject indicator = Instantiate(_explosionIndicatorPrefab, transform.position, Quaternion.identity);
            indicator.transform.localScale = Vector3.one * _triggerDistance;
            character.TakeDamage(_explosionDamage);
            Destroy(indicator, 2f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            _remainingDetonationTime = _detonationTime;
        }
    }
}
