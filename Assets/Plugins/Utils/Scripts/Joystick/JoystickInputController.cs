using System;
using UnityEngine;

namespace MyUtils.Joysticks
{
	public class JoystickInputController : MonoBehaviour
	{
		public event Action<Vector2> OnJoystickInputChange;

		private Joystick _joystick;

		private void Start() => _joystick = FindObjectOfType<Joystick>();

		private void Update() => JoystickActionInvoke();

		private void JoystickActionInvoke()
		{
			//if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
			OnJoystickInputChange?.Invoke(_joystick.Direction.normalized);
		}
	}
}