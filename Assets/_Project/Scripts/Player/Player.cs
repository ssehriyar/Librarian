using MyUtils.Colors;
using System;
using UnityEngine;

namespace LibraryGame
{
	public class Player : MonoBehaviour
	{
		private const int MAX_CARRY_CAPACITY = 20;

		[SerializeField] private Material _playerMaterial;

		[SerializeField] private StackManager _stackManager;
		public StackManager MyStackManager { get => _stackManager; }

		public ColorEnum MyColor { get; private set; }

		public void Start()
		{
			_stackManager.StackCapacity = MAX_CARRY_CAPACITY;
			SetMyColor(StartDistributor.Instance.GiveMeColor());
			PlayersData.Instance.SendYourData(gameObject, MyColor);
		}

		private void SetMyColor(ColorEnum color)
		{
			MyColor = color;
			switch (color)
			{
				case ColorEnum.Gray:
					_playerMaterial.color = Color.gray;
					break;
				case ColorEnum.Red:
					_playerMaterial.color = Color.red;
					break;
				case ColorEnum.Green:
					_playerMaterial.color = Color.green;
					break;
				case ColorEnum.Blue:
					_playerMaterial.color = Color.blue;
					break;
				case ColorEnum.Yellow:
					_playerMaterial.color = Color.yellow;
					break;
			}
		}
	}
}