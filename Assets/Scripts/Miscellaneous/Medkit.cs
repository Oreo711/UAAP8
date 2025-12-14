using System;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] private float _healValue;

    private void Heal (IHealth health)
    {
        health.HealDamage(_healValue);
        Destroy(gameObject);
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.TryGetComponent(out IHealth health))
        {
            Heal(health);
        }
    }
}
