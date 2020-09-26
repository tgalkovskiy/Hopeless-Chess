using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAI : MonoBehaviour
{
	[SerializeField]
	VirtualBoardController board;

	[SerializeField]
	int AIPower = 1;

	[SerializeField]
	int depth = 2;

	private void Start()
	{
		
	}

	public string BestMove(bool isWhitesTurn)
	{
		string bestMove = null;

		string[] Moves;
		float boardScore;
		float tempBoardScore;

		switch (AIPower)
		{
			case 1:
				#region Первый уровень погружения (рандомные ходы)

				bestMove = board.GiveAllPieceMoves(isWhitesTurn)[UnityEngine.Random.Range(0, board.GiveAllPieceMoves(isWhitesTurn).Length)];

				#endregion
				break;
			case 2:
				#region Второй уровень погружения (если можно съесть - съест)

				Moves = board.GiveAllPieceMoves(isWhitesTurn);

				// Если ход белых - мы будем искать наибольшее значание, если черных - наменьшее.
				if (isWhitesTurn)
				{
					boardScore = -9999;
					foreach (var item in Moves)
					{
						tempBoardScore = board.BoardScore(item);
						if (tempBoardScore < boardScore)
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
						tempBoardScore = board.BoardScore(item);
						if (tempBoardScore > boardScore)
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
						if (tempBoardScore <= bestBoardScore)
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
						if (tempBoardScore >= bestBoardScore)
						{
							bestBoardScore = tempBoardScore;
							bestMove = item;
						}
					}
				}

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
		//Выход из рекурсии
		if (curentDepth == 0) return board.BoardScore(move);

		//Делвем выртуальный ход
		board.MoveFigurVirtual(move);
		isWhitesTurn = !isWhitesTurn;
		//Делвем выртуальный ход

		string[] Moves = board.GiveAllPieceMoves(isWhitesTurn);

		if (isWhitesTurn)
		{
			float bestBoardScore = -9999;
			foreach (var item in Moves)
			{
				bestBoardScore = Math.Max(bestBoardScore, MinMaxVirtualScore(curentDepth - 1, item, isWhitesTurn));
			}

			//Отменяем вмрутальный ход
			board.UndoMoveFigurVirtual(move);
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
			board.UndoMoveFigurVirtual(move);
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

		string[] Moves = board.GiveAllPieceMoves(isWhitesTurn);

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
}

