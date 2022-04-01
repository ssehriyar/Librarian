using LibraryGame;
using MyUtils.Colors;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class GrayBookDetecter : MonoBehaviour
{
	public bool GrayBookInRange => _detectedGrayBook.Count != 0;

	private List<Book> _detectedGrayBook = new List<Book>();

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Book>()?.MyColor == ColorEnum.Gray)
			_detectedGrayBook.Add(other.GetComponent<Book>());
	}

	private void OnTriggerExit(Collider other)
	{
		if (_detectedGrayBook.Contains(other.GetComponent<Book>()))
			_detectedGrayBook.Remove(other.GetComponent<Book>());
	}

	public Book GetNearestGrayBook()
	{
		return _detectedGrayBook.FirstOrDefault();
	}
}