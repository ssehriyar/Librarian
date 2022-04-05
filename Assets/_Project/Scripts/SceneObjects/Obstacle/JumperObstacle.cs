using DG.Tweening;
using MyUtils.TimeManager;
using UnityEngine;

namespace LibraryGame
{
	public class JumperObstacle : MonoBehaviour
	{
		private int _jumpPos = 0;
		[SerializeField] private Transform[] _positionTransforms;

		private void Start()
		{
			Jump();
			Timer.Instance.On2Second += Jump;
		}

		private void OnDisable()
		{
			Timer.Instance.On2Second -= Jump;
		}

		private void Jump()
		{
			if(_jumpPos == _positionTransforms.Length)
				_jumpPos = 0;

			transform.DOJump(_positionTransforms[_jumpPos++].position + Vector3.up * 0.5f, 10f, 1, 0.5f);
		}
	}
}
