using UnityEngine;
using DG.Tweening;
using System;
using MyUtils.Joysticks;
using System.Collections;

namespace LibraryGame
{
	public class PlayerMovement : MonoBehaviour
	{
		private const float STUN_TIME = 1f; // Stun duration after collision
		private static readonly int Speed = Animator.StringToHash("Speed");
		private static readonly int VelocityX = Animator.StringToHash("VelocityX");
		private static readonly int VelocityY = Animator.StringToHash("VelocityY");

		[SerializeField] private Rigidbody _rb;
		[SerializeField] private Animator _animator;
		[SerializeField] private JoystickInputController _joystickInputController;
		[SerializeField] private float _playerForceSpeed = 10f;
		[SerializeField] private float _playerSharpSpeed = 5f;
		[SerializeField] private float _rotationSpeedDuration = 0.5f;

		private void OnEnable() => _joystickInputController.OnJoystickInputChange += Move;

		private void OnDisable() => _joystickInputController.OnJoystickInputChange -= Move;

		public void Move(Vector2 direction)
		{
			var input = Vector3.forward * direction.y + Vector3.right * direction.x;
			_animator.SetFloat(VelocityX, direction.y, 0.1f, Time.deltaTime);
			_animator.SetFloat(VelocityY, direction.x, 0.1f, Time.deltaTime);

			/* Sliding feeling */
			//_rb.AddForce((Vector3)(_playerForceSpeed * Time.fixedDeltaTime * input), ForceMode.VelocityChange);

			/* Sharp sense of movement */
			transform.position += _playerSharpSpeed * Time.deltaTime * input.normalized;

			/* Sharp rotation */
			transform.rotation = Quaternion.LookRotation(input);

			/* Smooth rotation */
			//transform.DORotateQuaternion(Quaternion.LookRotation((Vector3)input), _rotationSpeedDuration);
		}

		public IEnumerator DisableMoveForATime()
		{
			_joystickInputController.OnJoystickInputChange -= Move;
			yield return new WaitForSeconds(STUN_TIME);
			_joystickInputController.OnJoystickInputChange += Move;
		}
	}
}