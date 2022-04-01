using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSomething : IState
{
	private Librarian _librarian;
	private Animator _animator;
	private static readonly int Fall = Animator.StringToHash("Fall");
	private static readonly int Carry = Animator.StringToHash("Carry");

	public HitSomething(Librarian librarian, Animator animator)
	{
		_librarian = librarian;
		_animator = animator;
	}

	public void OnEnter()
	{
		_librarian.StunAndClearStack();
		_animator.SetTrigger(Fall);
		_animator.SetBool(Carry, false);
	}

	public void Tick() { }

	public void OnExit() { }
}