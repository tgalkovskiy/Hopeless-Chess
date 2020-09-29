using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class BoardController2 : MonoBehaviour
{
	int[][] board;
	List<string> movesArchive;
	int moveNumber;
	GameObject[][] squares;
	List<GameObject> selectedSquars;
	List<GameObject> lightPieces;	
	List<GameObject> darkPieces;

	[SerializeField]
	Transform fatherBoard2;
	[SerializeField]
	GameObject image;

	Vector2[] spiralSequence;

	private void Start()
	{
		selectedSquars = new List<GameObject>();
		lightPieces = new List<GameObject>();
		darkPieces = new List<GameObject>();
		movesArchive = new List<string>();

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
			new int [8] {  5,  4,  3,  1,  2,  3,  4,  5 },
			new int [8] {  6,  6,  6,  6,  6,  6,  6,  6,},
			new int [8] {  0,  0,  0,  0,  0,  0,  0,  0 },
			new int [8] {  0,  0,  0,  0,  0,  0,  0,  0 },
			new int [8] {  0,  0,  0,  0,  0,  0,  0,  0 },
			new int [8] {  0,  0,  0,  0,  0,  0,  0,  0 },
			new int [8] { 16, 16, 16, 16, 16, 16, 16, 16 },
			new int [8] { 15, 14, 13, 11, 12, 13, 14, 15 }
		};

		moveNumber = 1;
	}

	/// <summary>
	/// Получает список контроллеров персонажей из листа фигур
	/// </summary>
	/// <param name="keys"></param>
	/// <returns></returns>
	public List<CharacterController> GetControllers(List<GameObject> keys)
	{
		List<CharacterController> characters = new List<CharacterController>();
		for(int i = 0; i < keys.Count; i++)
		{
			characters.Add(keys[i].GetComponent<CharacterController>());
		}
		return characters;
	}

	public void ShowBoard()
	{
		string ranks;
		string bordView = "\n";
		for (int i = 0; i < board.GetLength(0); i++)
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
		var squarSide = GameModule.instance.SquarePrefab.GetComponent<BoxCollider>().size.x;
		var startPosition = new Vector3 (- 3.5f * squarSide, 0 ,3.5f * squarSide);

		for (int i = 0; i < board.GetLength(0); i++)
		{
			squares[i] = new GameObject[board[i].GetLength(0)];

			for (int j = 0; j < board[i].GetLength(0); j++)
			{
				squares[i][j] = Instantiate(
					GameModule.instance.SquarePrefab,
					new Vector3 (j* squarSide, 0, -i* squarSide) + startPosition,
					new Quaternion(),
					fatherBoard2
					);
				squares[i][j].name = i + " " + j;
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

				if (board[i][j] < 10) { lightPieces.Add(temp); temp.GetComponent<CharacterController>().isLight = true;}
				else darkPieces.Add(temp);
			}
		}
	}

	public void SwitchOffBlackColliders()
	{
		foreach (var item in darkPieces)
		{
			item.GetComponent<BoxCollider>().enabled = false;
		}
	}

	public void SwitchOnBlackColliders()
	{
		foreach (var item in darkPieces)
		{
			item.GetComponent<BoxCollider>().enabled = true;
		}
	}

	public void SwitchOffLhiteColliders()
	{
		foreach (var item in lightPieces)
		{
			item.GetComponent<BoxCollider>().enabled = false;
		}
	}

	public void SwitchOnLhiteColliders()
	{
		foreach (var item in lightPieces)
		{
			item.GetComponent<BoxCollider>().enabled = true;
		}
	}

	public List <Vector2Int> FindPieceMoves(CharacterController piece)
	{
		var selectedSquars2 = new List<Vector2Int>();

		var piecePosition = FindSquare(piece.transform.parent.gameObject);
		var moveTexture = CutMoveTexture( piece.GetMoveTexture() , piecePosition);

		// Координаты клетки обхода по спирали
		int X = 0;
		int Y = 0;

		// Создаем новую текстуру, котрую будем заполнять по спирали.
		var blackTexture = new Texture2D(moveTexture.width, moveTexture.height);
		// Чтобы не блюрила
		blackTexture.filterMode = FilterMode.Point;

		for (int i = 0; i < blackTexture.height; i++)
		{
			for (int j = 0; j < blackTexture.width; j++)
			{
				blackTexture.SetPixel(j, i, new Color(0, 0, 0));
			}
		}

		//Красим стартовую клетку в цвет движения для коретной работы.
		blackTexture.SetPixel(piecePosition.x, blackTexture.height -1 - piecePosition.y, GameModule.instance.MoveColors[1]);
		blackTexture.Apply();

		image.GetComponent<MeshRenderer>().material.mainTexture = moveTexture;

		StartSpiralSecuence(board.GetLength(0));

		for (int i = 1; i < spiralSequence.Length; i++)
		{
			X = (int)(piecePosition.x + spiralSequence[i].x);
			// Y в texture2d отчитывается снизу вверх.
			Y = (int)(piecePosition.y + spiralSequence[i].y);
			if (X > 7 || Y > 7 || X<0 || Y<0) continue;

			//if (FindMoveColor(moveTexture.GetPixel(X, moveTexture.height - 1 - Y))==2)
			//Debug.Log(FindMoveColor(moveTexture.GetPixel(X, moveTexture.height -1 -Y)) + "X=" +X+ " Y="+  Y);

			switch (FindMoveColor(moveTexture.GetPixel(X, moveTexture.height - 1 - Y)))
			{
				case 1:
					if (!(isMoveSquareAround(blackTexture, X, Y) && board[Y][X] == 0)) continue;
					// Исправления бага с ферзем
					if (Math.Abs(spiralSequence[i].x) == 2 && spiralSequence[i].y ==0) 
						if (board[Y][(int)(piecePosition.x + spiralSequence[i].x/2)] != 0) continue;
					if (Math.Abs(spiralSequence[i].y) == 2 && spiralSequence[i].x == 0) 
						if (board[(int)(piecePosition.y + spiralSequence[i].y/2)][X] != 0) continue;
					// Заполняем для дальнещего анализа
					blackTexture.SetPixel(X, blackTexture.height - 1 - Y, GameModule.instance.MoveColors[1]);
					blackTexture.Apply();
					selectedSquars2.Add(new Vector2Int(X, Y));					
					break;
				case 2:
					if (board[Y][X] == 0) continue;
					if (!isMoveSquareAround(blackTexture, X, Y)) continue;
					// Проверяем что фигуры из разных команд
					if (Math.Abs( board[Y][X] - board[piecePosition.y][piecePosition.x]) >=5)
						if (board[piecePosition.y][piecePosition.x] != 1 && board[piecePosition.y][piecePosition.x] != 11)
						{
							blackTexture.SetPixel(X, blackTexture.height - 1 - Y, GameModule.instance.MoveColors[2]);
							blackTexture.Apply();
							selectedSquars2.Add(new Vector2Int(X, Y));
						}
					break;
				case 3:
					if (!isMoveSquareAround(blackTexture, X, Y)) continue;
					// Исправления бага с ферзем
					if (Math.Abs(spiralSequence[i].x) == 2 && spiralSequence[i].y == 0)
						if (board[Y][(int)(piecePosition.x + spiralSequence[i].x / 2)] != 0) continue;
					if (Math.Abs(spiralSequence[i].y) == 2 && spiralSequence[i].x == 0)
						if (board[(int)(piecePosition.y + spiralSequence[i].y / 2)][X] != 0) continue;
					if (board[Y][X] == 0)
					{
						if (isMoveSquareAround(blackTexture, X, Y))
						{
							// Заполняем для дальнещего анализа
							blackTexture.SetPixel(X, blackTexture.height - 1 - Y, GameModule.instance.MoveColors[1]);
							blackTexture.Apply();
							selectedSquars2.Add(new Vector2Int(X, Y));
						}
					}
					else
					{
						if (Math.Abs(board[Y][X] - board[piecePosition.y][piecePosition.x]) >= 5)
							if (board[piecePosition.y][piecePosition.x] != 1 && board[piecePosition.y][piecePosition.x] != 11)
							{
								blackTexture.SetPixel(X, blackTexture.height - 1 - Y, GameModule.instance.MoveColors[2]);
								blackTexture.Apply();
								selectedSquars2.Add(new Vector2Int(X, Y));
							}
					}
					break;
				case 4:
					if (board[Y][X] == 0)
					{
						// Заполняем для дальнещего анализа
						blackTexture.SetPixel(X, blackTexture.height - 1 - Y, GameModule.instance.MoveColors[4]);
						blackTexture.Apply();
						selectedSquars2.Add(new Vector2Int(X, Y));
					}
					else
					{
						if (Math.Abs(board[Y][X] - board[piecePosition.y][piecePosition.x]) >= 5)
							if (board[piecePosition.y][piecePosition.x] != 1 && board[piecePosition.y][piecePosition.x] != 11)
							{
								blackTexture.SetPixel(X, blackTexture.height - 1 - Y, GameModule.instance.MoveColors[2]);
								blackTexture.Apply();
								selectedSquars2.Add(new Vector2Int(X, Y));
							}
					}
					break;
				default:
					//Debug.Log("Нет такого цвета!");
					break;
			}
		}

		return selectedSquars2;
	}


	#region Дополнительный функции для ShowPieceMoves

	/// <summary>
	/// 0 - none
	/// 1 - move
	/// 2 - attack
	/// 3 - move and attack
	/// 4 - jump
	/// </summary>
	/// <param name="color"></param>
	/// <returns></returns>
	int FindMoveColor(Color color)
	{
		for (int i = 0; i < GameModule.instance.MoveColors.Length; i++)
		{
			if (GameModule.instance.MoveColors[i] == color) return i;
		}
		return 5;
	}

	/// <summary>
	/// Создание спиральной последовательности
	/// </summary>
	/// <param name="size"></param>
	void StartSpiralSecuence(int size)
	{
		spiralSequence = new Vector2[size * size * 4];

		bool isItX = false;

		float change = -1;
		int changeCount = 1;

		float repeat = 1;
		int repeatCount = 0;

		for (int i = 1; i < spiralSequence.Length - 1; i++)
		{
			spiralSequence[i] = spiralSequence[i - 1];
			if (isItX) spiralSequence[i].x += change;
			else spiralSequence[i].y += change;

			repeatCount++;

			if (repeatCount == (int)repeat)
			{
				repeat += 0.5f;
				isItX = !isItX;
				repeatCount = 0;

				changeCount++;
				if (changeCount == 2)
				{
					changeCount = 0;
					change *= -1;
				}
			}
		}
	}

	Vector2Int FindSquare(GameObject square)
	{
		for (int i = 0; i < squares.GetLength(0); i++)
		{
			for (int j = 0; j < squares[i].GetLength(0); j++)
			{
				if (squares[i][j] == square) return new Vector2Int(j, i);
			}
		}
		return new Vector2Int(squares.GetLength(0)+1, squares[0].GetLength(0)+1);
	}

	Vector2Int FindPosition(int index, bool isItLight)
	{
		//var index = 0;
		//if (!piece.gameObject.name.Contains("Light")) index += 10;
		//if (piece.pieceType == CharacterController.ChessType.pawn) index += 6;
		//else if (piece.pieceType == CharacterController.ChessType.rook) index += 5;
		//else if (piece.pieceType == CharacterController.ChessType.knight) index += 4;
		//else if (piece.pieceType == CharacterController.ChessType.bishop) index += 3;
		//else if (piece.pieceType == CharacterController.ChessType.queen) index += 2;
		//else if (piece.pieceType == CharacterController.ChessType.king) index += 1;
		//else index += 9;

		if (!isItLight) index += 10;

		for (int i = 0; i < board.GetLength(0); i++)
		{
			for (int j = 0; j < board[i].GetLength(0); j++)
			{
				if (board[i][j] == index) return new Vector2Int(j, i);
			}
		}
		return new Vector2Int(board.GetLength(0) + 1, board[0].GetLength(0) + 1);
	}

	/// <summary>
	/// Проверяем есть ли в соседних пикселях цвет движения.
	/// </summary>
	/// <param name="textur"></param>
	/// <returns></returns>
	bool isMoveSquareAround(Texture2D textur, int X, int Y)
	{
		for (int i = X-1; i <= X+1; i++)
		{
			for (int j = Y-1; j <= Y+1; j++)
			{
				if (i > 7 || j > 7 || i < 0 || j < 0) continue;
				if (textur.GetPixel(i, textur.height - 1 - j) == GameModule.instance.MoveColors[1]) return true;
			}
		}
		return false;
	}

	Texture2D CutMoveTexture ( Texture2D texture, Vector2Int shift)
	{
		var start = new Vector2Int((int)texture.width/2 +1 , (int)texture.height / 2 + 1);
		var width = board.GetLength(0);
		var height = board[0].GetLength(0);

		var moveTexture = new Texture2D(width, height);
		// Чтобы не блюрила
		moveTexture.filterMode = FilterMode.Point ;
		moveTexture.SetPixels(texture.GetPixels(start.x - 1 - shift.x, shift.y , width, height));
		moveTexture.Apply();
		//image.GetComponent<MeshRenderer>().material.mainTexture = texture;
		return moveTexture;
	}

	#endregion


	public void GlowSquares(List<Vector2Int> positions)
	{
		foreach (var item in positions)
		{
			Instantiate(
				GameModule.instance.SelectedPrefab,
				squares[item.y][item.x].transform
				).transform.position = squares[item.y][item.x].transform.position;
			squares[item.y][item.x].GetComponent<BoxCollider>().enabled = true;
		}
	}

	public void StopShowPieceMoves()
	{
		foreach (var item in GameObject.FindGameObjectsWithTag("Selected"))
		{
			Destroy(item);
		}
		foreach (var item in selectedSquars)
		{
			item.GetComponent<BoxCollider>().enabled = false;
		}
		selectedSquars.Clear();
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

	public void MovePiece(CharacterController piece, GameObject square)
	{
		var squarePosition = FindSquare(square);
		var piecePosition = FindSquare(piece.transform.parent.gameObject);
		Morality morality = Morality.GetInstance();
		//Удаление съеденной фигруы
		if (board[squarePosition.y][squarePosition.x] != 0)
		{
			var temp = squares[squarePosition.y][squarePosition.x].transform.GetChild(0).gameObject;


			//YTANTEIMORALITY
			//изменение морали дружеским и вражеским фигурам
			if(temp.GetComponent<CharacterController>().isLight)
			{
				morality.AddMorality(GetControllers(lightPieces).ToArray(), -3);
				morality.AddMorality(GetControllers(darkPieces).ToArray(), 3);
			}
			else
			{
				morality.AddMorality(GetControllers(darkPieces).ToArray(), -3);
				morality.AddMorality(GetControllers(lightPieces).ToArray(), +3);
			}

			temp.SetActive(false);
			
			//darkPieces.Remove(temp);
			//lightPieces.Remove(temp);
			//Destroy(temp);
		}

		//Перестановка фигуры
		piece.transform.parent = square.transform;
		piece.transform.position = square.transform.position;

		//Перестановка фигуры на вспомогательной доске
		board[squarePosition.y][squarePosition.x] = board[piecePosition.y][piecePosition.x];
		board[piecePosition.y][piecePosition.x] = 0;		

	}

	public bool IsItCheck(CharacterController piece, GameObject square, bool isLightTurn)
	{

		MovePieceOnBoard(piece, square);

		foreach (var item in isLightTurn? darkPieces:lightPieces)
		{
			foreach (var item2 in FindPieceMoves(item.GetComponent<CharacterController>()))
			{
				if (FindPosition(1,isLightTurn) == item2)
				{
					UndoMovePieceOnBord();
					return true;
				}
			}
		}

		UndoMovePieceOnBord();
		return false;
	}

	#region Дополнительные функции для IsItCheck

	Vector2Int FindPieceOnBoard (int pieceIndex)
	{
		for (int i = 0; i < board.GetLength(0); i++)
		{
			for (int j = 0; j < board[i].GetLength(0); j++)
			{
				if (board[i][j] == pieceIndex) return new Vector2Int(j, i);
			}
		}
		return new Vector2Int(board.GetLength(0)+1, board[0].GetLength(0)+1);
	}

	#endregion

	void MovePieceOnBoard (CharacterController piece, GameObject square)
	{
		var piecePosition = FindSquare(piece.transform.parent.gameObject);
		var squarePosition = FindSquare(square);

		movesArchive.Add($"{moveNumber}-{piecePosition.x}:{piecePosition.y}-{squarePosition.x}:{squarePosition.y}-{board[squarePosition.y][squarePosition.x]}");

		//Перестановка фигуры на вспомогательной доске
		board[squarePosition.y][squarePosition.x] = board[piecePosition.y][piecePosition.x];
		board[piecePosition.y][piecePosition.x] = 0;

	}

	void UndoMovePieceOnBord()
	{
		var temp = movesArchive[movesArchive.Count - 1].Split(new char[] { '-' });
		var piecePosition = new Vector2Int(int.Parse(temp[1].Split(new char[] { ':' })[0]), int.Parse(temp[1].Split(new char[] { ':' })[1]));
		var squarePosition = new Vector2Int(int.Parse(temp[2].Split(new char[] { ':' })[0]), int.Parse(temp[2].Split(new char[] { ':' })[1]));
		var eatenPiece = int.Parse(temp[3]);

		board[piecePosition.y][piecePosition.x] = board[squarePosition.y][squarePosition.x];
		board[squarePosition.y][squarePosition.x] = eatenPiece;
	}

	public bool IsItMate(CharacterController piece, GameObject square, bool isLightTurn) 
	{
		return false;
	}
}
