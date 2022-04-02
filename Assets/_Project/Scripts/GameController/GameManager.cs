using DG.Tweening;
using MyUtils.Colors;
using MyUtils.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LibraryGame
{
	public class GameManager : Singleton<GameManager>
	{
		public bool Ending { get; private set; } = false;
		private int countdownTime = 40;
		public bool GameStarted { get; private set; } = false;
		[SerializeField] private Slider _slider;
		[SerializeField] private Image _fill;
		[SerializeField] private PlayerMovement _playerMovement;
		[SerializeField] private GameObject _dragToPlay;
		[SerializeField] private GameObject _endingCamera;

		private void Start()
		{
			_slider.maxValue = countdownTime;
			_slider.value = countdownTime;
		}

		public void TapToPlay()
		{
			_dragToPlay.SetActive(false);

			foreach (var librarian in Positions.Instance.Librarians)
			{
				librarian.gameObject.SetActive(true);
			}

			StartCoroutine(StartCountdown());
		}

		private IEnumerator StartCountdown()
		{
			while (countdownTime > 0)
			{
				yield return new WaitForSeconds(1f);

				_fill.fillAmount = _slider.value;
				_slider.value--;
				countdownTime--;
			}

			StartCoroutine(GameEnding());
			GameEnding();
		}

		private IEnumerator GameEnding()
		{
			yield return new WaitForSeconds(1f);
			Ending = true;
			_playerMovement.EndingMode();
			_endingCamera.SetActive(true);
			foreach (var go in Positions.Instance.Obstacles)
				go.SetActive(false);

			var list = PlayersData.Instance.LineUp();
			for (int index = 0; index < list.Count; index++)
			{
				list[index].Item3.transform.DOMove(Positions.Instance.EndingPositions[index].position, 1f);
			}

			StartCoroutine(WinnerPositions(list));
		}

		private IEnumerator WinnerPositions(List<Tuple<ColorEnum, List<Book>, GameObject>> list)
		{
			yield return new WaitForSeconds(1f);
			int count = 0;
			foreach (var tuple in list)
			{
				foreach (var book in tuple.Item2)
				{
					tuple.Item3.transform.position += new Vector3(0, 0.3f, 0);
					Positions.Instance.EndingPositions[count]
						.GetComponentInChildren<StackManager>().EndingPush(book);
				}
				if (count == 3)
					tuple.Item3.GetComponent<EndAnimation>().YouWon();
				else
					tuple.Item3.GetComponent<EndAnimation>().YouLose();
				count++;

				StartCoroutine(RestartGame());
			}
		}

		private IEnumerator RestartGame()
		{
			yield return new WaitForSeconds(5f);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		private void OnDestroy()
		{
			Destroy(this.gameObject);
		}
	}
}