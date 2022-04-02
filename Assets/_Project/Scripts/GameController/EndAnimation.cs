using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimation : MonoBehaviour
{
	private static readonly int youWon = Animator.StringToHash("YouWon");
	private static readonly int youLose = Animator.StringToHash("YouLose");

	[SerializeField] private Animator _animator;

	public void YouWon()
	{
		_animator.SetTrigger(youWon);
	}

	public void YouLose()
	{
		_animator.SetTrigger(youLose);
	}
}
