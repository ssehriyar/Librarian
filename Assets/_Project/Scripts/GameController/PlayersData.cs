using MyUtils.Colors;
using MyUtils.Singleton;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LibraryGame
{
	public class PlayersData : Singleton<PlayersData>
	{
		public List<Tuple<ColorEnum, List<Book>, GameObject>> Datas { get; set; }
			= new List<Tuple<ColorEnum, List<Book>, GameObject>>();

		//public Dictionary<GameObject, List<Book>> Datas { get; private set; }

		public void SendYourData(GameObject go, ColorEnum myColor)
		{
			Datas.Add(new Tuple<ColorEnum, List<Book>, GameObject>(myColor, new List<Book>(), go));
			//Datas.Add(go, new List<Book>());
		}

		public void AddToList(Book book)
		{
			foreach (var tuple in Datas)
			{
				if (tuple.Item1 == book.MyColor)
					tuple.Item2.Add(book);
			}
		}

		public List<Tuple<ColorEnum, List<Book>, GameObject>> LineUp()
		{
			var newList = Datas.OrderBy(x => x.Item2.Count).ToList();
			return newList;
		}

		private void OnDestroy()
		{
			Destroy(this.gameObject);
		}
	}
}