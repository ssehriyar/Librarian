using DG.Tweening;
using MyUtils.TimeManager;
using System.Collections;
using UnityEngine;

namespace LibraryGame
{
	public class FallingObstacle : MonoBehaviour
	{
		private const float FALL_TIME = 0.7f;
		private const float RESET_POSITION_TIME = 0.5f;

		private Vector3 _startPosition;
		[SerializeField] private Transform _fallingMark;

		private void Start()
		{
			_startPosition = transform.position;
			Timer.Instance.On5Second += FallAndReset;
		}

		private void OnDisable()
		{
			Timer.Instance.On5Second -= FallAndReset;
		}

		private void FallAndReset()
		{
			StartCoroutine(FallWaitReset());
		}

		private IEnumerator FallWaitReset()
		{
			transform.DOMove(_fallingMark.position + Vector3.up * 0.5f, FALL_TIME);
			_fallingMark.GetComponent<SpriteRenderer>().DOColor(Color.red, FALL_TIME);

			yield return new WaitForSeconds(FALL_TIME - 0.08f);
			transform.GetComponentInChildren<ModelHolder>().transform.DOShakeScale(0.2f, 1f, 10, 42);
			yield return new WaitForSeconds(1f);

			_fallingMark.GetComponent<SpriteRenderer>().DOColor(Color.white, RESET_POSITION_TIME);
			transform.DOMove(_startPosition, RESET_POSITION_TIME);
		}
	}
}
