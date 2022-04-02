using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LibraryGame
{
	public class DragToPlay : MonoBehaviour, IPointerDownHandler
	{
		private bool _notPressed = true;
		public void OnPointerDown(PointerEventData eventData)
		{
			if (_notPressed)
			{
				GameManager.Instance.TapToPlay();
				_notPressed = false;
			}
		}
	}
}