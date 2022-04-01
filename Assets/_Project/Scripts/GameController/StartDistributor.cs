using MyUtils.Colors;
using MyUtils.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LibraryGame
{
	public class StartDistributor : Singleton<StartDistributor>
	{
		#region Player And AI
		private List<ColorEnum> _colors = new List<ColorEnum>();

		public ColorEnum GiveMeColor()
		{
			var color = (ColorEnum)UnityEngine.Random.Range(1, Enum.GetNames(typeof(ColorEnum)).Length);
			while (_colors.Contains(color))
				color = (ColorEnum)UnityEngine.Random.Range(1, Enum.GetNames(typeof(ColorEnum)).Length);

			_colors.Add(color);
			return color;
		}
		#endregion
	}
}