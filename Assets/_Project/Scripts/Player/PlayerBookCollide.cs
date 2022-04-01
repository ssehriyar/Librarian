using MyUtils.Colors;
using UnityEngine;

namespace LibraryGame
{
	public class PlayerBookCollide : MonoBehaviour
	{
		private static readonly int Carry = Animator.StringToHash("Carry");

		[SerializeField] private Player _player;
		[SerializeField] private Animator _animator;

		private void OnTriggerEnter(Collider other)
		{
			var book = other.GetComponent<Book>();
			if (book != null && book.IsPickable && !_player.MyStackManager.IsStackFull &&
				(book.MyColor == _player.MyColor || book.MyColor == ColorEnum.Gray))
			{
				_player.MyStackManager.PushBook(book, _player.MyColor);
				_animator.SetBool(Carry, true);
			}
		}
	}
}
