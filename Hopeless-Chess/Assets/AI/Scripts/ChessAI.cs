using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAI : MonoBehaviour
{
	[SerializeField]
	BoardController2 board;
	[Space]

	[SerializeField]
	SideAI side;
	[SerializeField]
	int AIPower = 1;
	[SerializeField]
	int depth = 2;

	public enum SideAI
	{
		light, dark, none
	}

	public SideAI Side { get { return side; } } 

	private void Start()
	{
		
	}

	public string BestMove(bool isWhitesTurn)
	{
		if (isWhitesTurn && side == SideAI.dark) return null;
		if (!isWhitesTurn && side == SideAI.light) return null;

		string bestMove = null;

		float boardScore;
		float tempBoardScore;

		//List<string> Moves = new List<string>();

		switch (AIPower)
		{
			case 1:
				#region Первый уровень погружения (рандомные ходы)
			
				bestMove = board.GiveAllPieceMoves(isWhitesTurn)[UnityEngine.Random.Range(0, board.GiveAllPieceMoves(isWhitesTurn).Count - 1)];

				#endregion
				break;
			case 2:
				#region Второй уровень погружения (если можно съесть - съест)


				List<string>  Moves=  board.GiveAllPieceMoves(isWhitesTurn);

				// Если ход белых - мы будем искать наибольшее значание, если черных - наменьшее.
				if (isWhitesTurn)
				{
					boardScore = -9999;
					foreach (var item in Moves)
					{
						board.MovePieceOnBoard(item);
						tempBoardScore = BoardScore();
						board.UndoMovePieceOnBord(item);

						if (tempBoardScore > boardScore)
						{
							boardScore = tempBoardScore;
							bestMove = item;
						}
					}
				}
				else
				{
					boardScore = 9999;
					foreach (var item in Moves)
					{
						board.MovePieceOnBoard(item);
						tempBoardScore = BoardScore();
						board.UndoMovePieceOnBord(item);

						//Debug.Log(boardScore);

						if (tempBoardScore < boardScore)
						{
							boardScore = tempBoardScore;
							bestMove = item;
						}
					}
				}

				#endregion
				break;
			case 3:
				#region Третий уровень погружения (просчет ходов)

				Moves = board.GiveAllPieceMoves(isWhitesTurn);
				
				// Если ход белых - мы будем искать наибольшее значание, если черных - наменьшее.
				if (isWhitesTurn)
				{
					float bestBoardScore = -9999;
					foreach (var item in Moves)
					{
						tempBoardScore = MinMaxVirtualScore(depth -1,item, isWhitesTurn);
						
						if (tempBoardScore >= bestBoardScore)
						{
							bestBoardScore = tempBoardScore;
							bestMove = item;
						}
					}
				}
				else
				{
					float bestBoardScore = 9999;
					foreach (var item in Moves)
					{
						tempBoardScore = MinMaxVirtualScore(depth -1,item, isWhitesTurn);

						if (tempBoardScore <= bestBoardScore)
						{
							bestBoardScore = tempBoardScore;
							bestMove = item;
						}
					}
				}

				board.ShowBoard();

				#endregion
				break;
			case 4:
				#region Четвертый уровень погружения (оптимизация 3-его уровня)

				depth = 2;

				Moves = board.GiveAllPieceMoves(isWhitesTurn);

				// Если ход белых - мы будем искать наибольшее значание, если черных - наменьшее.
				if (isWhitesTurn)
				{
					boardScore = -9999;
					foreach (var item in Moves)
					{
						tempBoardScore = MinMaxVirtualScoreAlphaBeta(depth -1 , item, isWhitesTurn, -9999, 9999);
						if (tempBoardScore <= boardScore)
						{
							boardScore = tempBoardScore;
							bestMove = item;
						}
					}
				}
				else
				{
					boardScore = 9999;
					foreach (var item in Moves)
					{
						tempBoardScore = MinMaxVirtualScoreAlphaBeta(depth -1, item, isWhitesTurn, - 9999, 9999);
						if (tempBoardScore >= boardScore)
						{
							boardScore = tempBoardScore;
							bestMove = item;
						}
					}
				}

				#endregion
				break;
		}

		return bestMove;
	}

	float MinMaxVirtualScore (float curentDepth, string move, bool isWhitesTurn)
	{
		//Debug.Log("curentDepth = " + curentDepth +" move = " + move);

		//Выход из рекурсии
		if (curentDepth == 0)
		{
			board.MovePieceOnBoard(move);
			var temp = BoardScore();
			board.UndoMovePieceOnBord(move);
			return temp;
		}

		//Делаем виртуальный ход
		board.MovePieceOnBoard(move);
		isWhitesTurn = !isWhitesTurn;
		//Делаем виртуальный ход

		List<string> Moves = board.GiveAllPieceMoves(isWhitesTurn);

		//Debug.Log(Moves.Count);

		if (isWhitesTurn)
		{
			float bestBoardScore = -9999;
			foreach (var item in Moves)
			{
				bestBoardScore = Math.Max(bestBoardScore, MinMaxVirtualScore(curentDepth - 1, item, isWhitesTurn));
			}

			//Отменяем вмрутальный ход
			board.UndoMovePieceOnBord(move);
			return bestBoardScore;
		}
		else
		{
			float bestBoardScore = 9999;
			foreach (var item in Moves)
			{
				bestBoardScore = Math.Min(bestBoardScore, MinMaxVirtualScore(curentDepth - 1, item, isWhitesTurn));
			}

			//Отменяем вмрутальный ход
			board.UndoMovePieceOnBord(move);
			return bestBoardScore;
		}
	}


	float MinMaxVirtualScoreAlphaBeta (float curentDepth, string move, bool isWhitesTurn, float alpha, float beta)
	{
		//Выход из рекурсии
		if (curentDepth == 0) return board.BoardScore(move);

		//Делвем выртуальный ход
		board.MoveFigurVirtual(move);
		isWhitesTurn = !isWhitesTurn;
		//Делвем выртуальный ход

		List<string> Moves = board.GiveAllPieceMoves(isWhitesTurn);

		if (isWhitesTurn)
		{
			float bestBoardScore = -9999;
			foreach (var item in Moves)
			{
				bestBoardScore = Math.Max(bestBoardScore, MinMaxVirtualScoreAlphaBeta(curentDepth - 1, item, isWhitesTurn, alpha, beta));

				//Отменяем вмрутальный ход
				board.UndoMoveFigurVirtual(move);

				alpha = Math.Max(alpha, bestBoardScore);
				if (beta<alpha) return bestBoardScore;
			}


			return bestBoardScore;
		}
		else
		{
			float bestBoardScore = 9999;
			foreach (var item in Moves)
			{
				bestBoardScore = Math.Min(bestBoardScore, MinMaxVirtualScoreAlphaBeta(curentDepth - 1, item, isWhitesTurn, alpha, beta));

				//Отменяем вмрутальный ход
				board.UndoMoveFigurVirtual(move);

				beta = Math.Min(alpha, bestBoardScore);
				if (beta < alpha) return bestBoardScore;
			}

			return bestBoardScore;
		}
	}


	float BoardScore()
	{
		var curentBoard = board.Board;
		float boardScore = 0;
		int piece;
		float side;

		//board.ShowBoard();

		for (int i = 0; i < curentBoard.Length; i++)
		{
			for (int j = 0; j < curentBoard[i].Length; j++)
			{
				piece = curentBoard[i][j] % 10;
				side = curentBoard[i][j] > 500 ? -1 : 1 ;
				boardScore += side * pieceCost[piece];
				//Debug.Log($"piece= {piece} side= {side} ++ = {side * pieceCost[piece]}  boardScore= {boardScore}");
			}
		}
		return boardScore;
	}

	static float[] pieceCost = new float[]
	{
		0,   //Пустое место
		900, //Король
		90,  //Королева
		30,  //Слон
		30,  //Конь
		50,  //Ладья
		10	 //Пешка
	};




}

