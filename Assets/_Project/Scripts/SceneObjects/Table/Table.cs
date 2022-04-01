using MyUtils.Colors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LibraryGame
{
	public class Table : MonoBehaviour
	{
		[SerializeField] private Book[] books;

		private void Start()
		{
			int colorLoop = 1;
			foreach (var book in books)
			{
				if (colorLoop % 5 == 0) colorLoop = 1;
				book.SetMyColor((ColorEnum)colorLoop++);
			}
		}
	}
}