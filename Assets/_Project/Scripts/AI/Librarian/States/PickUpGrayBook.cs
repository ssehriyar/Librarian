using UnityEngine;
using UnityEngine.AI;

internal class PickUpGrayBook : IState
{
	private Librarian _librarian;
	private Animator _animator;
	private static readonly int Carry = Animator.StringToHash("Carry");

	public PickUpGrayBook(Librarian librarian, Animator animator)
	{
		_librarian = librarian;
		_animator = animator;
	}

	public void OnEnter() => _animator.SetBool(Carry, true);

	public void Tick()
	{
		if (_librarian.TargetBook != null)
		{
			_librarian.TakeBook();
		}
	}

	public void OnExit() { }
}