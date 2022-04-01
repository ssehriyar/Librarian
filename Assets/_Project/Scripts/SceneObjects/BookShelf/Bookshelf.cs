using UnityEngine;

namespace LibraryGame
{
	public class Bookshelf : MonoBehaviour
	{
		private const int MAX_STACK_CAPACITY = 24;
		[SerializeField] private StackManager _stackManager;

		public StackManager MyStackManager { get => _stackManager; }

		private Vector3 _emptyBookPosition;

		private void Start()
		{
			_stackManager.StackCapacity = MAX_STACK_CAPACITY;
		}

		public Vector3 EmptyBookPosition()
		{
			switch (_stackManager.Books.Count)
			{
				case 0:
					return _emptyBookPosition = new Vector3(0, 0, 0);
				case 15:
					return _emptyBookPosition = new Vector3(0, 0.75f, 0);
				case 30:
					return _emptyBookPosition = new Vector3(0, 1.5f, 0);
				case 45:
					return _emptyBookPosition = new Vector3(0, 2.35f, 0);

				default: return _emptyBookPosition += new Vector3(0.25f, 0, 0);
			}
		}

		public void CheckStackForColorChange()
		{
			//foreach (var book in MyStackManager.MyBooks)
			//{
			//	book
			//}
		}
	}
}