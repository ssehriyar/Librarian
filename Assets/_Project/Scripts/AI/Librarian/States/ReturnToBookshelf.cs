using LibraryGame;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ReturnToBookshelf : IState
{
	private Librarian _librarian;
	private NavMeshAgent _navMeshAgent;
	private Animator _animator;
	private static readonly int Speed = Animator.StringToHash("Speed");

	public ReturnToBookshelf(Librarian librarian, NavMeshAgent navMeshAgent, Animator animator)
	{
		_librarian = librarian;
		_navMeshAgent = navMeshAgent;
		_animator = animator;
	}

	public void OnEnter()
	{
		_librarian.TargetBookshelf = RandomEmptyBookshelf();
		_navMeshAgent.enabled = true;
		_navMeshAgent.SetDestination(_librarian.TargetBookshelf.transform.position);
		_animator.SetFloat(Speed, 1f);
	}

	private Bookshelf RandomEmptyBookshelf()
	{
		return Positions.Instance.BookShelfs
			.OrderBy(t => Vector3.Distance(_librarian.transform.position, t.transform.position))
			.Where(t => t.MyStackManager.IsStackEmpty == true)
			.OrderBy(t => Random.Range(0, int.MaxValue))
			.First<Bookshelf>();
	}

	public void Tick() { }

	public void OnExit()
	{
		_navMeshAgent.enabled = false;
		_animator.SetFloat(Speed, 0f);
	}
}