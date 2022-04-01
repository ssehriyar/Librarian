using MyUtils.Colors;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LibraryGame;

public class GameBoardView : MonoBehaviour
{
	[SerializeField] private Book _bookPrefab;
	[SerializeField] private GameObject _player;

	private GameBoard _gameboard;
	private Book[,] _block;

	public void BindToGameBoard(GameBoard gameBoard)
	{
		_gameboard = gameBoard;
		_block = new Book[gameBoard.Columns, gameBoard.Rows];
		int colorLoop = 0;

		//Instantiate(_player, new Vector3(0, 1, -1), Quaternion.identity);

		for (int column = 0; column < gameBoard.Columns; column++)
		{
			for (int row = 0; row < gameBoard.Rows; row++)
			{
				var position = new Vector3(column, 0, row);
				var instance = Instantiate(_bookPrefab, position, Quaternion.identity);

				if (colorLoop % 5 == 0) colorLoop = 1;
				instance.SetMyColor((ColorEnum)colorLoop++);
			}
		}
	}
}
