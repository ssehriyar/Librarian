using LibraryGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
	public bool HitSomething => _hitDetected;

	private bool _hitDetected;
	[SerializeField] private Librarian _librarian;

	private void OnTriggerEnter(Collider other)
	{
		var obstacle = other.GetComponent<Obstacle>();
		if (obstacle != null)
		{
			_hitDetected = true;
			StartCoroutine(ClearDetection());
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		var player = collision.gameObject.GetComponent<Player>();
		var librarian = collision.gameObject.GetComponent<Librarian>();

		if (player != null && _librarian.MyStackManager.Books.Count < player.MyStackManager.Books.Count)
		{
			_hitDetected = true;
			StartCoroutine(ClearDetection());
		}

		else if (librarian != null && _librarian.MyStackManager.Books.Count < librarian.MyStackManager.Books.Count)
		{
			_hitDetected = true;
			StartCoroutine(ClearDetection());
		}
	}

	private IEnumerator ClearDetection()
	{
		yield return new WaitForSeconds(2f);
		_hitDetected = false;
	}
}
