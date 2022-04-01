using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardCreater : MonoBehaviour
{
	[SerializeField] private int _columns = 2;
	[SerializeField] private int _rows = 2;

	public GameBoard GetGameBoard()
	{
		return new GameBoard(_columns, _rows);
	}
}
