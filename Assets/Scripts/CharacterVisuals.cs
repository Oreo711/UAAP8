using System;
using UnityEngine;

public class CharacterVisuals : MonoBehaviour
{
	[SerializeField] private Character _character;

	private float    _characterHealth;
	private Animator _animator;
	private bool     _isAlive = true;

	private void Awake ()
	{
		_animator = GetComponent<Animator>();
		_characterHealth = _character.CurrentHealth;
	}


	private void Update ()
	{
		if (!_isAlive)
		{
			return;
		}

		if ((_character.CurrentHealth / _character.MaxHealth) < 0.3f)
		{
			_animator.SetLayerWeight(0, 0f);
			_animator.SetLayerWeight(1, 1f);
		} else
		{
			_animator.SetLayerWeight(0, 1f);
			_animator.SetLayerWeight(1, 0f);
		}

		_animator.SetFloat("Health",   _character.CurrentHealth);

		if (_character.CurrentHealth <= 0)
		{
			_isAlive = false;
			_animator.SetTrigger("Died");
			return;
		}

		_animator.SetFloat("Velocity", _character.Velocity);

		if (_characterHealth > _character.CurrentHealth)
		{
			_animator.SetTrigger("Hurt");
		}

		_characterHealth = _character.CurrentHealth;
	}

	public void Destroy () => Destroy(gameObject);
}
