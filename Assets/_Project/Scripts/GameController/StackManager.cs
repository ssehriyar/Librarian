using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MyUtils.Colors;

namespace LibraryGame
{
	public class StackManager : MonoBehaviour
	{
		[HideInInspector] public int StackCapacity;
		public Stack<Book> Books { get; private set; } = new Stack<Book>();
		public bool IsStackFull { get; private set; } = false;
		public bool IsStackEmpty { get; private set; } = true;

		public void PushBook(Book book, ColorEnum colorEnum)
		{
			book.IsPickable = false;
			book.transform.SetParent(transform);
			book.Collide(false);
			//book.transform.DOLocalMove(new Vector3(0, 0.3f, 0) * MyBooks.Count, 0.3f);
			book.GoTo(new Vector3(0, 0.3f, 0) * Books.Count, 0.5f);
			book.transform.DOLocalRotate(Vector3.zero, 0.2f);
			book.SetMyColor(colorEnum);

			Books.Push(book);
			IsStackEmpty = false;

			if (Books.Count == StackCapacity)
			{
				IsStackFull = true;
			}
		}

		public void Clear()
		{
			foreach (var book in Books)
			{
				book.transform.SetParent(null);
				book.Collide(true);
				book.AddRandomForce();
				book.SetMyColor(ColorEnum.Gray);
				StartCoroutine(book.MakePickableAfterATime());
			}

			Books.Clear();
			IsStackEmpty = true;
			IsStackFull = false;
		}

		public void SendBookToBookshelf(Bookshelf bookshelf)
		{
			var book = Books.Pop();
			book.transform.SetParent(bookshelf.MyStackManager.transform);
			book.transform.DOLocalMove(bookshelf.EmptyBookPosition(), 0.8f);
			//book.GoTo(bookshelfStack.EmptyBookPosition(), 2);
			book.transform.DOLocalRotate(new Vector3(0, 180, 90), 0.8f);

			IsStackFull = false;
			if (Books.Count == 0)
				IsStackEmpty = true;

			PlayersData.Instance.AddToList(book);
			bookshelf.MyStackManager.Books.Push(book);
		}

		public void EndingPush(Book book)
		{
			book.transform.SetParent(transform);
			book.Collide(false);
			book.transform.DOLocalMove(new Vector3(0, 0.3f, 0) * Books.Count, 0.5f);
			book.transform.DOLocalRotate(Vector3.zero, 0.2f);
			Books.Push(book);
		}
	}
}