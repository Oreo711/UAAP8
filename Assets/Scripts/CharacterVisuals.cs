using System;
using UnityEngine;

public class CharacterVisuals : MonoBehaviour
{
	[SerializeField] private Character _character;
	[SerializeField] private CharacterHealth _characterHealth;

	private static readonly int Hurt     = Animator.StringToHash("Hurt");
	private static readonly int Velocity = Animator.StringToHash("Velocity");
	private static readonly int Died     = Animator.StringToHash("Died");
	private static readonly int Health   = Animator.StringToHash("Health");

	private const int BaseLayerIndex    = 0;
	private const int InjuredLayerIndex = 1;

	private                 float    _lastFrameCharacterHealth;
	private                 Animator _animator;
	private                 bool     _isAlive = true;

	private void Awake ()
	{
		_animator = GetComponent<Animator>();
		_lastFrameCharacterHealth = _characterHealth.CurrentHealth;
	}

	private void Update ()
	{
		if (!_isAlive)
			return;

		SelectLayer();
		SetParameters();

		_lastFrameCharacterHealth = _characterHealth.CurrentHealth;
	}

	private void SetParameters ()
	{
		_animator.SetFloat(Health,   _characterHealth.CurrentHealth);

		if (_characterHealth.CurrentHealth <= 0)
		{
			_isAlive = false;
			_animator.SetTrigger(Died);

			return;
		}

		_animator.SetFloat(Velocity, _character.Velocity);

		if (_lastFrameCharacterHealth > _characterHealth.CurrentHealth)
		{
			_animator.SetTrigger(Hurt);
		}
	}

	public void Destroy () => Destroy(_character.gameObject);

	private void SelectLayer ()
	{
		if ((_characterHealth.CurrentHealth / _characterHealth.MaxHealth) < 0.3f)
		{
			_animator.SetLayerWeight(BaseLayerIndex, 0f);
			_animator.SetLayerWeight(InjuredLayerIndex, 1f);
		}
		else
		{
			_animator.SetLayerWeight(BaseLayerIndex, 1f);
			_animator.SetLayerWeight(InjuredLayerIndex, 0f);
		}
	}
}
