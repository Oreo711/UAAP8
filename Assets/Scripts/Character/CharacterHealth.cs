using UnityEngine;

public class CharacterHealth : MonoBehaviour, IHealth
{
	[SerializeField] private float _maxHealth = 100f;

	public float MaxHealth => _maxHealth;

	public float CurrentHealth {get; private set;}

	private void Awake ()
	{
		CurrentHealth = _maxHealth;
	}

	public void TakeDamage (float damage)
	{
		CurrentHealth -= damage;

		if (CurrentHealth < 0)
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
