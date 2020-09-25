using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class BoardController2 : VirtualBoardController
{
	int[][] board;
	GameObject[][] squares;
	List<GameObject> lightPieces;
	List<GameObject> darkPieces;

	[SerializeField]
	Transform fatherBoard;

	private void Start()
	{
		lightPieces = new List<GameObject>();
		darkPieces = new List<GameObject>();

		SetupStartPosition();
		//ShowBoard();
		CreateBoard();
		PlacePieces();
		SwitchOffLhiteColliders();
		SwitchOffBlackColliders();
		SwitchOffAllSquars();

	}

	/// <summary>
	/// 1 - King
	/// 2 - Queen
	/// 3 - Bishop
	/// 4 - Knight
	/// 5 - Rock
	/// 6 - Pawn
	/// </summary>
	void SetupStartPosition()
	{
		board = new int[8][] {
			new int [8] {  5,  4,  3,  2,  1,  3,  4,  5 },
			new int [8] {  6,  6,  6,  6,  6,  6,  6,  6,},
			new int [8] {  0,  0,  0,  0,  0,  0,  0,  0 },
			new int [8] {  0,  0,  0,  0,  0,  0,  0,  0 },
			new int [8] {  0,  0,  0,  0,  0,  0,  0,  0 },
			new int [8] {  0,  0,  0,  0,  0,  0,  0,  0 },
			new int [8] { 16, 16, 16, 16, 16, 16, 16, 16 },
			new int [8] { 15, 14, 13, 12, 11, 13, 14, 15 }
		};
	}

	public void ShowBoard()
	{
		string ranks;
		string bordView = "\n";
		for (int i = board.GetLength(0) - 1; i >= 0; i--)
		{
			ranks = null;
			for (int j = 0; j < board[i].Length; j++)
			{
				if (board[i][j].ToString().Length == 2) ranks += board[i][j].ToString() + " ";
				else ranks += " " + board[i][j].ToString() + "  ";
			}
			bordView += ranks + "\n";
		}
		Debug.Log(bordView);
	}

	void CreateBoard()
	{
		squares = new GameObject[board.GetLength(0)][];

		// Узнаем размеры префаба.
		var squarSide = GameModule.instance.SquarePrefabs.GetComponent<BoxCollider>().size.x;
		var startPosition = new Vector3 (-3.5f * squarSide, 0 ,-3.5f * squarSide);

		for (int i = 0; i < board.GetLength(0); i++)
		{
			squares[i] = new GameObject[board[i].GetLength(0)];

			for (int j = 0; j < board[i].GetLength(0); j++)
			{
				squares[i][j] = Instantiate(
					GameModule.instance.SquarePrefabs,
					new Vector3 (j* squarSide, 0, i* squarSide) + startPosition,
					new Quaternion(),
					fatherBoard
					);
			}
		}
	}

	void PlacePieces()
	{
		for (int i = 0; i < board.GetLength(0); i++)
		{
			for (int j = 0; j < board[i].GetLength(0); j++)
			{
				// Пропускаем пустые клетки
				if (board[i][j] == 0) continue;

				var temp = Instantiate(
					GameModule.instance.PiecePrefabs[board[i][j]],
					squares[i][j].transform
					);

				if (board[i][j] < 10) lightPieces.Add(temp);
				else darkPieces.Add(temp);
			}
		}
	}

	public override void SwitchOffBlackColliders()
	{
		foreach (var item in darkPieces)
		{
			item.GetComponent<BoxCollider>().enabled = false;
		}
	}

	public override void SwitchOnBlackColliders()
	{
		foreach (var item in darkPieces)
		{
			item.GetComponent<BoxCollider>().enabled = true;
		}
	}

	public override void SwitchOffLhiteColliders()
	{
		foreach (var item in lightPieces)
		{
			item.GetComponent<BoxCollider>().enabled = false;
		}
	}

	public override void SwitchOnLhiteColliders()
	{
		foreach (var item in lightPieces)
		{
			item.GetComponent<BoxCollider>().enabled = true;
		}
	}

	public override void ShowFiguresMoves(CharacterController figur)
	{
		SwitchOnAllSquars();
	}

	public override void StopShowFiguresMoves(CharacterController piece)
	{
		SwitchOffAllSquars();
	}

	public void SwitchOffAllSquars()
	{
		for (int i = 0; i < squares.GetLength(0); i++)
		{
			for (int j = 0; j < squares[i].GetLength(0); j++)
			{
				squares[i][j].GetComponent<BoxCollider>().enabled = false;
			}
		}
	}

	public void SwitchOnAllSquars()
	{
		for (int i = 0; i < squares.GetLength(0); i++)
		{
			for (int j = 0; j < squares[i].GetLength(0); j++)
			{
				squares[i][j].GetComponent<BoxCollider>().enabled = true;
			}
		}
	}

	public override void MoveFigur(CharacterController piece, GameObject square)
	{
		var squarePosition = FindSquare(square);
		var piecePosition = FindSquare(piece.transform.parent.gameObject);

		//Удаление съеденной фигруы
		if (board[squarePosition.x][squarePosition.y] != 0)
		{
			var temp = squares[squarePosition.x][squarePosition.y].transform.GetChild(0).gameObject;
			temp.SetActive(false);
			//darkPieces.Remove(temp);
			//lightPieces.Remove(temp);
			//Destroy(temp);
		}

		//Перестановка фигуры
		piece.transform.parent = square.transform;
		piece.transform.position = square.transform.position;

		//Перестановка фигуры на вспомогательной доске
		board[squarePosition.x][squarePosition.y] = board[piecePosition.x][piecePosition.y];
		board[piecePosition.x][piecePosition.y] = 0;		

	}

	Vector2Int FindSquare(GameObject square)
	{
		for (int i = 0; i < squares.GetLength(0); i++)
		{
			for (int j = 0; j < squares[i].GetLength(0); j++)
			{
				if (squares[i][j] == square) return new Vector2Int(i, j);
			}
		}
		return new Vector2Int(9, 9);
	}
}
