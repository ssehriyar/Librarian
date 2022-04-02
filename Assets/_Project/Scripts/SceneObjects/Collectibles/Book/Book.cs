using DG.Tweening;
using MyUtils.Colors;
using System;
using System.Collections;
using UnityEngine;

namespace LibraryGame
{
	public class Book : MonoBehaviour
	{
		private const float PICKABLE_AFTER_A_TIME = 1f; // Less than 1.5f is recomended
		private const float MIN_FORCE_VALUE = 100F;
		private const float MAX_FORCE_VALUE = 300F;

		private Rigidbody _rb;
		private BoxCollider _boxCollider;

		public ColorEnum MyColor { get; private set; }

		[HideInInspector] public bool IsPickable = true;

		private void Start()
		{
			_rb = GetComponent<Rigidbody>();
			_boxCollider = GetComponent<BoxCollider>();
		}

		public void SetMyColor(ColorEnum colorEnum)
		{
			if (colorEnum == MyColor)
				return;

			MyColor = colorEnum;
			var rendererArr = GetComponentsInChildren<MeshRenderer>();
			switch (colorEnum)
			{
				case ColorEnum.Gray:
					foreach (Renderer renderer in rendererArr)
						renderer.material.color = Color.gray;
					break;
				case ColorEnum.Red:
					foreach (Renderer renderer in rendererArr)
						renderer.material.color = Color.red;
					break;
				case ColorEnum.Green:
					foreach (Renderer renderer in rendererArr)
						renderer.material.color = Color.green;
					break;
				case ColorEnum.Blue:
					foreach (Renderer renderer in rendererArr)
						renderer.material.color = Color.blue;
					break;
				case ColorEnum.Yellow:
					foreach (Renderer renderer in rendererArr)
						renderer.material.color = Color.yellow;
					break;

			}
		}

		public void Rotate(Quaternion quaternion) => transform.rotation = quaternion;

		public IEnumerator MakePickableAfterATime()
		{
			yield return new WaitForSeconds(PICKABLE_AFTER_A_TIME);
			IsPickable = true;
		}

		public void Collide(bool b)
		{
			if (b)
			{
				_rb.isKinematic = false;
				_boxCollider.enabled = true;
			}
			else
			{
				_rb.isKinematic = true;
				_boxCollider.enabled = false;
			}
		}

		public void GoTo(Vector3 targetPosition, float duration)
		{
			// Block is following the Vector3[] path to go to Stack
			Vector3[] vector3s = { Vector3.right, Vector3.one, targetPosition };
			transform.DOLocalPath(vector3s, duration, PathType.CatmullRom);
		}

		public void AddRandomForce()
		{
			float randX = UnityEngine.Random.Range(MIN_FORCE_VALUE, MAX_FORCE_VALUE) * (UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1);
			float randY = UnityEngine.Random.Range(MIN_FORCE_VALUE, MAX_FORCE_VALUE) * (UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1);
			float randZ = UnityEngine.Random.Range(MIN_FORCE_VALUE, MAX_FORCE_VALUE) * (UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1);
			Vector3 forceDirection = new Vector3(randX, randY, randZ);

			_rb.AddForce(forceDirection, ForceMode.Force);
		}
	}
}
