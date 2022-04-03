using MyUtils.Colors;
using UnityEngine;
using UnityEngine.AI;

internal class GoToGrayBook : IState
{
	private Librarian _librarian;
	private NavMeshAgent _navMeshAgent;
	private GrayBookDetecter _grayBookDetector;
	private Animator _animator;
	private static readonly int Speed = Animator.StringToHash("Speed");

	public GoToGrayBook(Librarian librarian, NavMeshAgent navMeshAgent, GrayBookDetecter grayBookDetector, Animator animator)
	{
		_librarian = librarian;
		_navMeshAgent = navMeshAgent;
		_grayBookDetector = grayBookDetector;
		_animator = animator;
	}

	public void OnEnter()
	{
		_librarian.TargetBook = _grayBookDetector.GetNearestGrayBook();
		_navMeshAgent.enabled = true;
		_navMeshAgent.SetDestination(_librarian.TargetBook.transform.position);
		_animator.SetFloat(Speed, 1f);
	}

	public void Tick()
	{
		_librarian.TargetBook = _grayBookDetector.GetNearestGrayBook();
		_navMeshAgent.SetDestination(_librarian.TargetBook.transform.position);
	}

	public void OnExit()
	{
		_navMeshAgent.enabled = false;
		_animator.SetFloat(Speed, 0f);
	}
}