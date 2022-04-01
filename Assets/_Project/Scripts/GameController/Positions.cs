using MyUtils.Singleton;
using UnityEngine;

namespace LibraryGame
{
	public class Positions : Singleton<Positions>
	{
		[SerializeField] private Bookshelf[] _bookshelfs;
		public Bookshelf[] BookShelfs { get => _bookshelfs; }
	}
}