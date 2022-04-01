using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	private GameBoard _gameBoard;
	[SerializeField] private GameBoardCreater _GameBoardCreater;
	[SerializeField] private GameBoardView _gameBoardView;

	private void OnEnable()
	{
		_gameBoard = _GameBoardCreater.GetGameBoard();
		_gameBoardView.BindToGameBoard(_gameBoard);
	}
}
