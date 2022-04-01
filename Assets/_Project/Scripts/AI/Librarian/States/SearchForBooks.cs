using LibraryGame;
using System.Linq;
using UnityEngine;

internal class SearchForBooks : IState
{
	private Librarian _librarian;

	public SearchForBooks(Librarian librarian) => _librarian = librarian;

	public void Tick() => _librarian.TargetBook = ChooseNearestResources(5);

	private Book ChooseNearestResources(int pickFromNearest)
	{
		return Object.FindObjectsOfType<Book>()
			.Where(t => t.IsPickable == true && _librarian.MyColor == t.MyColor)
			.OrderBy(t => Vector3.Distance(_librarian.transform.position, t.transform.position))
			.Take(pickFromNearest)
			.OrderBy(t => Random.Range(0, int.MaxValue))
			.FirstOrDefault();
	}

	public void OnEnter() { }

	public void OnExit() { }
}