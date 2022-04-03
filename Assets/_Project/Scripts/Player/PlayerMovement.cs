using UnityEngine;
using DG.Tweening;
using System;
using MyUtils.Joysticks;
using System.Collections;

namespace LibraryGame
{
	public class PlayerMovement : MonoBehaviour
	{
		private const float STUN_TIME = 2f; // Stun duration after collision
		private static readonly int Speed = Animator.StringToHash("Speed");
		private static readonly int Ending = Animator.StringToHash("Ending");

		[SerializeField] private Rigidbody _rb;
		[SerializeField] private Animator _animator;
		[SerializeField] private JoystickInputController _joystickInputController;

		//private float _playerForceSpeed = 10f;
		private float _playerSharpSpeed = 5f;
		//private float _rotationSpeedDuration = 0.5f;

		private void OnEnable() => _joystickInputController.OnJoystickInputChange += Move;

		private void OnDisable() => _joystickInputController.OnJoystickInputChange -= Move;

		public void Move(Vector2 direction)
		{
			var input = Vector3.forward * direction.y + Vector3.right * direction.x;
			_animator.SetFloat(Speed, input.magnitude);

			/* Sliding feeling */
			//_rb.AddForce((Vector3)(_playerForceSpeed * Time.fixedDeltaTime * input), ForceMode.VelocityChange);

			/* Sharp sense of movement */
			_rb.velocity = _playerSharpSpeed * input;
			//transform.position += _playerSharpSpeed * Time.deltaTime * input;

			if (direction.x != 0 && direction.y != 0)
			{
				/* Sharp rotation */
				transform.rotation = Quaternion.LookRotation(input);

				/* Smooth rotation */
				//transform.DORotateQuaternion(Quaternion.LookRotation((Vector3)input), _rotationSpeedDuration);
			}
		}

		public IEnumerator DisableMoveForATime()
		{
			_joystickInputController.OnJoystickInputChange -= Move;
			yield return new WaitForSeconds(STUN_TIME);
			_joystickInputController.OnJoystickInputChange += Move;
		}

		public void EndingMode()
		{
			_joystickInputController.OnJoystickInputChange -= Move;
			_joystickInputController.enabled = false;
			_rb.isKinematic = true;
			foreach (var collider in GetComponentsInChildren<Collider>())
				collider.enabled = false;
			foreach (var book in GetComponentsInChildren<Book>())
				book.gameObject.SetActive(false);
			GetComponent<Player>().MyStackManager.Clear();
			_animator.SetTrigger(Ending);
			transform.rotation = Quaternion.Euler(0, 180, 0);
		}
	}
}