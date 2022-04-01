using UnityEngine;
using DG.Tweening;
using System;

namespace LibraryGame
{
	public class PlayerCollide : MonoBehaviour
	{
		private static readonly int Speed = Animator.StringToHash("Speed");
		private static readonly int Fall = Animator.StringToHash("Fall");
		private static readonly int Carry = Animator.StringToHash("Carry");

		[SerializeField] private Player _player;
		[SerializeField] private Animator _animator;
		[SerializeField] private PlayerMovement _playerMovement;

		private void OnTriggerEnter(Collider other)
		{
			if (other.GetComponent<Obstacle>() != null)
			{
				_animator.SetTrigger(Fall);
				_player.MyStackManager.Clear();
				_animator.SetBool(Carry, false);
				_player.transform.GetComponentInChildren<ModelHolder>().transform
					.DOShakePosition(1f, 1f, 10, 42f, false, true);
				StartCoroutine(_playerMovement.DisableMoveForATime());
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			var librarian = collision.gameObject.GetComponent<Librarian>();
			if (librarian != null && _player.MyStackManager.Books.Count < librarian.MyStackManager.Books.Count)
			{
				_animator.SetTrigger(Fall);
				_player.MyStackManager.Clear();
				_animator.SetBool(Carry, false);
				_player.transform.GetComponentInChildren<ModelHolder>().transform
					.DOShakePosition(1f, 1f, 10, 42f, false, true);
				StartCoroutine(_playerMovement.DisableMoveForATime());
			}
		}

		private void OnTriggerStay(Collider other)
		{
			var bookshelf = other.GetComponent<Bookshelf>();
			if (bookshelf != null && !_player.MyStackManager.IsStackEmpty && !bookshelf.MyStackManager.IsStackFull)
			{
				_player.MyStackManager.SendBookToBookshelf(bookshelf);
			}
			else if (_player.MyStackManager.IsStackEmpty)
				_animator.SetBool(Carry, false);
		}
	}
}
