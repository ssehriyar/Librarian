using MyUtils.Singleton;
using UnityEngine;

namespace LibraryGame
{
	public class Positions : Singleton<Positions>
	{
		[SerializeField] private Bookshelf[] _bookshelfs;
		public Bookshelf[] BookShelfs { get => _bookshelfs; }

		[SerializeField] private Librarian[] _librarians;
		public Librarian[] Librarians { get => _librarians; }

		[SerializeField] private Transform[] _endingPositions;
		public Transform[] EndingPositions { get => _endingPositions; }

		[SerializeField] private GameObject[] _obstacles;
		public GameObject[] Obstacles { get => _obstacles; }

		private void OnDestroy()
		{
			Destroy(this.gameObject);
		}
	}
}