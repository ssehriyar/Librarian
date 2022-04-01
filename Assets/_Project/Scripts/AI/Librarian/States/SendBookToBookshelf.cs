using UnityEngine;

public class SendBookToBookshelf : IState
{
	private Librarian _librarian;
	private Animator _animator;
	private static readonly int Carry = Animator.StringToHash("Carry");

	public SendBookToBookshelf(Librarian librarian, Animator animator)
	{
		_librarian = librarian;
		_animator = animator;
	}

	public void OnEnter() => _animator.SetBool(Carry, false);

	public void Tick()
	{
		if (!_librarian.MyStackManager.IsStackEmpty)
			_librarian.SendBookToBookshelf();
	}

	public void OnExit() { }
}