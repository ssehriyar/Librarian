using LibraryGame;
using UnityEngine;
using UnityEngine.AI;

internal class EndingMode : IState
{
	private Librarian _librarian;
	private NavMeshAgent _navMeshAgent;
	private Animator _animator;
	private static readonly int Ending = Animator.StringToHash("Ending");

	public EndingMode(Librarian librarian, NavMeshAgent navMeshAgent, Animator animator)
	{
		_librarian = librarian;
		_navMeshAgent = navMeshAgent;
		_animator = animator;
	}

	public void OnEnter()
	{
		_navMeshAgent.enabled = false;
		_librarian.GetComponent<Rigidbody>().isKinematic = true;
		foreach (var collider in _librarian.GetComponentsInChildren<Collider>())
			collider.enabled = false;
		foreach (var book in _librarian.GetComponentsInChildren<Book>())
			book.gameObject.SetActive(false);
		_librarian.MyStackManager.Clear();
		_animator.SetTrigger(Ending);
		_librarian.GetComponent<Librarian>().enabled = false;
		_librarian.transform.rotation = Quaternion.Euler(0, 180, 0);
	}

	public void OnExit() { }

	public void Tick() { }
}