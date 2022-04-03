using LibraryGame;
using MyUtils.Colors;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using MyUtils.TimeManager;

public class GrayBookDetecter : MonoBehaviour
{
	public bool GrayBookInRange => _detectedGrayBook.Count != 0;

	private List<Book> _detectedGrayBook = new List<Book>();

	private void Start()
	{
		TimeManager.OnSecond += CheckForGrayBooks;
	}

	private void OnDestroy()
	{
		TimeManager.OnSecond -= CheckForGrayBooks;
	}

	//private void OnTriggerEnter(Collider other)
	//{
	//	if (other.GetComponent<BookTest>()?.MyColor == ColorEnum.Gray &&
	//		!_detectedGrayBook.Contains(other.GetComponent<BookTest>()))
	//		_detectedGrayBook.Add(other.GetComponent<BookTest>());
	//}

	private void OnTriggerExit(Collider other)
	{
		if (_detectedGrayBook.Contains(other.GetComponent<Book>()))
			_detectedGrayBook.Remove(other.GetComponent<Book>());
	}

	private void CheckForGrayBooks()
	{
		_detectedGrayBook.Clear();
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 6f, LayerMask.GetMask("Book"));
		foreach (var hitCollider in hitColliders)
		{
			if (hitCollider.GetComponent<Book>().MyColor == ColorEnum.Gray &&
				!_detectedGrayBook.Contains(hitCollider.GetComponent<Book>()))
			{
				_detectedGrayBook.Add(hitCollider.GetComponent<Book>());
			}
		}
	}
	private void OnDrawGizmos()
	{
		//Gizmos.color = Color.yellow;
		//Gizmos.DrawSphere(transform.position, 6);
	}

	public Book GetNearestGrayBook()
	{
		return _detectedGrayBook.FirstOrDefault();
	}
}