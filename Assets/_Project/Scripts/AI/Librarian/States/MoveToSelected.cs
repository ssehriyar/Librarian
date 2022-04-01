using UnityEngine;
using UnityEngine.AI;

public class MoveToSelected : IState
{
	private Librarian _librarian;
	private NavMeshAgent _navMeshAgent;
	private Animator _animator;

	private Vector3 _lastPosition = Vector3.zero;

	public float TimeStuck;
	private static readonly int Speed = Animator.StringToHash("Speed");

	public MoveToSelected(Librarian librarian, NavMeshAgent navMeshAgent, Animator animator)
	{
		_librarian = librarian;
		_navMeshAgent = navMeshAgent;
		_animator = animator;
	}

	public void OnEnter()
	{
		TimeStuck = 0f;
		_navMeshAgent.enabled = true;
		_navMeshAgent.SetDestination(_librarian.TargetBook.transform.position);
		_animator.SetFloat(Speed, 1f);
	}

	public void Tick()
	{
		if (Vector3.Distance(_librarian.transform.position, _lastPosition) <= 0f)
			TimeStuck += Time.deltaTime;

		_lastPosition = _librarian.transform.position;
	}

	public void OnExit()
	{
		_navMeshAgent.enabled = false;
		_animator.SetFloat(Speed, 0f);
	}
}