using UnityEngine;
using UnityEngine.AI;


public class Character : MonoBehaviour, INavMeshMovable
{
	[SerializeField] private NavMeshAgent _agent;
	[SerializeField] private float _jumpSpeed;

	private AgentJumper _jumper;

	public CharacterHealth Health {get; private set;}

	public float Velocity => _agent.velocity.magnitude;

	public Vector3 Position => transform.position;

	public bool IsJumping => _jumper.InProcess;

	public bool IsActive => Health.CurrentHealth > 0;

	private void Awake ()
	{
		Health = GetComponent<CharacterHealth>();
		_agent = GetComponent<NavMeshAgent>();
		_jumper = new AgentJumper(_jumpSpeed, _agent, this);
	}

	public void WalkTo (Vector3 point)
	{
		_agent.SetDestination(point);
	}

	public bool IsOnNavMeshLink (out OffMeshLinkData offMeshLinkData)
	{
		if (_agent.isOnOffMeshLink)
		{
			offMeshLinkData = _agent.currentOffMeshLinkData;
			return true;
		}

		offMeshLinkData = default;
		return false;
	}

	public void Jump (OffMeshLinkData offMeshLinkData)
	{
		_jumper.Jump(offMeshLinkData);
	}

	public void Destroy ()
	{
		Destroy(gameObject);
	}
}
