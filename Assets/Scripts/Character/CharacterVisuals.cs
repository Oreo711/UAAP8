using System;
using System.Collections;
using UnityEngine;

public class CharacterVisuals : MonoBehaviour
{
	[SerializeField] private Character _character;

	private static readonly int Hurt     = Animator.StringToHash("Hurt");
	private static readonly int Velocity = Animator.StringToHash("Velocity");
	private static readonly int Died     = Animator.StringToHash("Died");
	private static readonly int Health   = Animator.StringToHash("Health");
	private static readonly int InJump   = Animator.StringToHash("InJump");

	private const int BaseLayerIndex    = 0;
	private const int InjuredLayerIndex = 1;

	private float _lastFrameCharacterHealth;
	private Animator _animator;
	private bool _isAlive  = true;

	private MeshRenderer[] _renderers;

	private void Awake ()
	{
		_animator = GetComponent<Animator>();
		_renderers = GetComponentsInChildren<MeshRenderer>();
		_lastFrameCharacterHealth = _character.CurrentHealth;
	}

	private void Update ()
	{
		if (_isAlive == false)
			return;

		SelectLayer();
		SetParameters();

		_lastFrameCharacterHealth = _character.CurrentHealth;
	}

	private void SetParameters ()
	{
		_animator.SetFloat(Health, _character.CurrentHealth);

		if (_character.CurrentHealth <= 0)
		{
			_isAlive = false;
			_animator.SetTrigger(Died);

			return;
		}


		_animator.SetBool(InJump, _character.IsJumping);
		_animator.SetFloat(Velocity, _character.Velocity);

		if (_lastFrameCharacterHealth > _character.CurrentHealth)
		{
			_animator.SetTrigger(Hurt);
		}
	}

	private void SelectLayer ()
	{
		if ((_character.CurrentHealth / _character.MaxHealth) < 0.3f)
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

	public void Dissolve()
	{
		StartCoroutine(DissolveProcess());
	}

	private IEnumerator DissolveProcess ()
	{
		float progress = 0f;

		while (progress < 1f)
		{
			foreach (MeshRenderer renderer in _renderers)
			{
				renderer.material.SetFloat("_threshold", progress);
			}

			progress += Time.deltaTime;
			yield return null;
		}

		Destroy(_character.gameObject);
	}
}
