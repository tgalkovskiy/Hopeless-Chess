using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
//using UniRx;
using UnityEngine;

public class ChessAI : MonoBehaviour
{

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

	private void Awake()
	{
		board = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardController2>();
	}


	public IEnumerator BestMoveCorutine(bool isWhitesTurn)
	{
		Debug.Log(BestMove(isWhitesTurn));
		yield return null;
	}

	public string BestMove (bool isWhitesTurn)
	{
		string bestMove = null;

		////Начало асинхрона
		//var heavyMethod = Observable.Start(() =>
		//{
		////Начало асинхрона

			Debug.Log("Процесс идет");


			if (isWhitesTurn && side == SideAI.dark) return null;
			if (!isWhitesTurn && side == SideAI.light) return null;

			float boardScore;
			float tempBoardScore;

			//List<string> Moves = new List<string>();

			switch (AIPower)
			{
				case 1:
					#region Первый уровень погружения (рандомные ходы)
					board.ShowBoard();
					//bestMove = board.MovesFilterKingDilema(isWhitesTurn)
					//	[UnityEngine.Random.Range(0, board.MovesFilterKingDilema(isWhitesTurn).Count - 1)];

					Debug.Log(new int[] {1,2,3 }.ToString());

					bestMove = board.MovesFilterKingDilema(isWhitesTurn) [0];

					#endregion
					break;
				case 2:
					#region Второй уровень погружения (если можно съесть - съест)

					Debug.Log( "  BoardScore =  " + BoardScore());

					List<string>  Moves =  board.MovesFilterKingDilema(isWhitesTurn);

					Debug.Log("  BoardScore =  " + BoardScore());

					// Если ход белых - мы будем искать наибольшее значание, если черных - наменьшее.
					if (isWhitesTurn)
					{

						Debug.Log("  BoardScore =  " + BoardScore());
						boardScore = -9999;
						foreach (var item in Moves)
						{
							board.MovePieceOnBoard(item);
							tempBoardScore = BoardScore();
							//Debug.Log(item + "  BoardScore =  " + tempBoardScore);
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
							//Debug.Log(item + "  BoardScore =  " + tempBoardScore);
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

					Moves = board.MovesFilterKingDilema(isWhitesTurn);

					//Debug.Log(Moves.Count);

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
						//Debug.Log("bestMove = " + bestMove + "MINbestBoardScore = " + bestBoardScore);
					}

					#endregion
					break;
				case 4:
					#region Четвертый уровень погружения (оптимизация 3-его уровня)

					//depth = 2;

					Moves = board.MovesFilterKingDilema(isWhitesTurn);

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

		////Конец  асинхрона
		//	return "0";
		//});
		////Конец  асинхрона

		////Метод необходимый для запуска асинхрона
		//Observable.WhenAll(heavyMethod)
		//.ObserveOnMainThread()
		//.Subscribe(result =>
		//{
		//	Debug.Log(string.Format("Thread = {0}, first result = {1} UtcNow = {2}", Thread.CurrentThread.ManagedThreadId, result[0], DateTime.UtcNow));
		//});


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
			//Debug.Log(temp);
			board.UndoMovePieceOnBord(move);
			return temp;
		}

		//Делаем виртуальный ход
		board.MovePieceOnBoard(move);
		isWhitesTurn = !isWhitesTurn;
		//Делаем виртуальный ход

		List<string> Moves = board.MovesFilterKingDilema(isWhitesTurn);

		if (isWhitesTurn)
		{
			float bestBoardScore = -9999;
			foreach (var item in Moves)
			{
				bestBoardScore = Math.Max(bestBoardScore, MinMaxVirtualScore(curentDepth - 1, item, isWhitesTurn));
			}

			//Отменяем вмрутальный ход
			board.UndoMovePieceOnBord(move);
			//if (curentDepth==2)
			//	Debug.Log("curentDepth = " + curentDepth + " move = " + move + "MAXSbestBoardScore =" + bestBoardScore);
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

				//Debug.Log("curentDepth = " + curentDepth + " move = " + move + "MINSbestBoardScore =" + bestBoardScore);
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

		List<string> Moves = board.MovesFilterKingDilema(isWhitesTurn);

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
		float tempPieceValue;

		//board.ShowBoard();

		for (int i = 0; i < curentBoard.Length; i++)
		{
			for (int j = 0; j < curentBoard[i].Length; j++)
			{
				piece = curentBoard[i][j] % 10;
				side = curentBoard[i][j] > 500 ? -1 : 1 ;

				switch (piece)
				{
					case 0: //Пустота
						tempPieceValue = 0;
						break;
					case 1: //Король
						tempPieceValue = 900 + ( side == 1 ? kingEvalWhite[i][j] : kingEvalBlack[i][j]) ;
						break;
					case 2: //Королева
						tempPieceValue = 90 +  qweenEval[i][j];
						//Debug.Log("tempPieceValue = " + tempPieceValue + "    j = " + j + "  i = " + i);
						break;
					case 3: //Слон
						tempPieceValue = 30 + (side == 1 ? bishopEvalWhite[i][j] : bishopEvalBlack[i][j]);
						break;
					case 4: //Конь
						tempPieceValue = 30 + knightEval[i][j];
						break;
					case 5: //Ладья
						tempPieceValue = 50 + (side == 1 ? rookEvalWhite[i][j] : rookEvalBlack[i][j]);
						break;
					case 6: //Пешка
						tempPieceValue = 10 + (side == 1 ? pawnEvalWhites[i][j] : pawnEvalBlack[i][j]);
						break;

					default:
						Debug.Log("Ошибка в подсчете стоимости доски!");
						tempPieceValue = 0;
						break;
				}

				boardScore += side * tempPieceValue;
			}
		}
		return boardScore;
	}

	#region Evals


	float[][] pawnEvalWhites = new float[][]
	{
		new float[] { 0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f },
		new float[] { 0.5f,  1.0f,  1.0f, -2.0f, -2.0f,  1.0f,  1.0f,  0.5f },
		new float[] { 0.5f, -0.5f, -1.0f,  0.0f,  0.0f, -1.0f, -0.5f,  0.5f },
		new float[] { 0.0f,  0.0f,  0.0f,  2.0f,  2.0f,  0.0f,  0.0f,  0.0f },
		new float[] { 0.5f,  0.5f,  1.0f,  2.5f,  2.5f,  1.0f,  0.5f,  0.5f },
		new float[] { 1.0f,  1.0f,  2.0f,  3.0f,  3.0f,  2.0f,  1.0f,  1.0f },
		new float[] { 5.0f,  5.0f,  5.0f,  5.0f,  5.0f,  5.0f,  5.0f,  5.0f },
		new float[] { 0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f }
	};

	float[][] pawnEvalBlack = new float[][]
	{
		new float[] { 0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f },
		new float[] { 5.0f,  5.0f,  5.0f,  5.0f,  5.0f,  5.0f,  5.0f,  5.0f },
		new float[] { 1.0f,  1.0f,  2.0f,  3.0f,  3.0f,  2.0f,  1.0f,  1.0f },
		new float[] { 0.5f,  0.5f,  1.0f,  2.5f,  2.5f,  1.0f,  0.5f,  0.5f },
		new float[] { 0.0f,  0.0f,  0.0f,  2.0f,  2.0f,  0.0f,  0.0f,  0.0f },
		new float[] { 0.5f, -0.5f, -1.0f,  0.0f,  0.0f, -1.0f, -0.5f,  0.5f },
		new float[] { 0.5f,  1.0f,  1.0f, -2.0f, -2.0f,  1.0f,  1.0f,  0.5f },
		new float[] { 0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f }
	};

	float[][] knightEval = new float[][]
	{
		new float[] { -5.0f, -4.0f, -3.0f, -3.0f, -3.0f, -3.0f, -4.0f, -5.0f },
		new float[] { -4.0f, -2.0f,  0.0f,  0.0f,  0.0f,  0.0f, -2.0f, -4.0f },
		new float[] { -3.0f,  0.0f,  1.0f,  1.5f,  1.5f,  1.0f,  0.0f, -3.0f },
		new float[] { -3.0f,  0.5f,  1.5f,  2.0f,  2.0f,  1.5f,  0.5f, -3.0f },
		new float[] { -3.0f,  0.0f,  1.5f,  2.0f,  2.0f,  1.5f,  0.0f, -3.0f },
		new float[] { -3.0f,  0.5f,  1.0f,  1.5f,  1.5f,  1.0f,  0.5f, -3.0f },
		new float[] { -4.0f, -2.0f,  0.0f,  0.5f,  0.5f,  0.0f, -2.0f, -4.0f },
		new float[] { -5.0f, -4.0f, -3.0f, -3.0f, -3.0f, -3.0f, -4.0f, -5.0f }
	};

	float[][] bishopEvalWhite = new float[][]
	{
		new float[] { -2.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -2.0f },
		new float[] { -1.0f,  0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.5f, -1.0f },
		new float[] { -1.0f,  1.0f,  1.0f,  1.0f,  1.0f,  1.0f,  1.0f, -1.0f },
		new float[] { -1.0f,  0.0f,  1.0f,  1.0f,  1.0f,  1.0f,  0.0f, -1.0f },
		new float[] { -1.0f,  0.5f,  0.5f,  1.0f,  1.0f,  0.5f,  0.5f, -1.0f },
		new float[] { -1.0f,  0.0f,  0.5f,  1.0f,  1.0f,  0.5f,  0.0f, -1.0f },
		new float[] { -1.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -1.0f },
		new float[] { -2.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -2.0f }
	};

	float[][] bishopEvalBlack = new float[][]
	{
		new float[] { -2.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -2.0f },
		new float[] { -1.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -1.0f },
		new float[] { -1.0f,  0.0f,  0.5f,  1.0f,  1.0f,  0.5f,  0.0f, -1.0f },
		new float[] { -1.0f,  0.5f,  0.5f,  1.0f,  1.0f,  0.5f,  0.5f, -1.0f },
		new float[] { -1.0f,  0.0f,  1.0f,  1.0f,  1.0f,  1.0f,  0.0f, -1.0f },
		new float[] { -1.0f,  1.0f,  1.0f,  1.0f,  1.0f,  1.0f,  1.0f, -1.0f },
		new float[] { -1.0f,  0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.5f, -1.0f },
		new float[] { -2.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -2.0f }
	};

	float[][] rookEvalWhite = new float[][]
	{
		new float[] {  0.0f,  0.0f,  0.0f,  0.5f,  0.5f,  0.0f,  0.0f,  0.0f },
		new float[] { -0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f },
		new float[] { -0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f },
		new float[] { -0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f },
		new float[] { -0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f },
		new float[] { -0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f },
		new float[] {  0.5f,  1.0f,  1.0f,  1.0f,  1.0f,  1.0f,  1.0f,  0.5f },
		new float[] {  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f }
	};

	float[][] rookEvalBlack = new float[][]
	{
		new float[] {  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f },
		new float[] {  0.5f,  1.0f,  1.0f,  1.0f,  1.0f,  1.0f,  1.0f,  0.5f },
		new float[] { -0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f },
		new float[] { -0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f },
		new float[] { -0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f },
		new float[] { -0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f },
		new float[] { -0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f },
		new float[] {  0.0f,  0.0f,  0.0f,  0.5f,  0.5f,  0.0f,  0.0f,  0.0f }
	};

	float[][] qweenEval = new float[][]
	{
		new float[] { -2.0f, -1.0f, -1.0f, -0.5f, -0.5f, -1.0f, -1.0f, -2.0f },
		new float[] { -1.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -1.0f },
		new float[] { -1.0f,  0.0f,  0.5f,  0.5f,  0.5f,  0.5f,  0.0f, -1.0f },
		new float[] { -0.5f,  0.0f,  0.5f,  0.5f,  0.5f,  0.5f,  0.0f, -0.5f },
		new float[] {  0.0f,  0.0f,  0.5f,  0.5f,  0.5f,  0.5f,  0.0f, -0.5f },
		new float[] { -1.0f,  0.5f,  0.5f,  0.5f,  0.5f,  0.5f,  0.0f, -1.0f },
		new float[] { -1.0f,  0.0f,  0.5f,  0.0f,  0.0f,  0.0f,  0.0f, -1.0f },
		new float[] { -2.0f, -1.0f, -1.0f, -0.5f, -0.5f, -1.0f, -1.0f, -2.0f }
	};

	float[][] kingEvalWhite = new float[][]
	{
		new float[] {  2.0f,  3.0f,  1.0f,  0.0f,  0.0f,  1.0f,  3.0f,  2.0f },
		new float[] {  2.0f,  2.0f,  0.0f,  0.0f,  0.0f,  0.0f,  2.0f,  2.0f },
		new float[] { -1.0f, -2.0f, -2.0f, -2.0f, -2.0f, -2.0f, -2.0f, -1.0f },
		new float[] { -2.0f, -3.0f, -3.0f, -4.0f, -4.0f, -3.0f, -3.0f, -2.0f },
		new float[] { -3.0f, -4.0f, -4.0f, -5.0f, -5.0f, -4.0f, -4.0f, -3.0f },
		new float[] { -3.0f, -4.0f, -4.0f, -5.0f, -5.0f, -4.0f, -4.0f, -3.0f },
		new float[] { -3.0f, -4.0f, -4.0f, -5.0f, -5.0f, -4.0f, -4.0f, -3.0f },
		new float[] { -3.0f, -4.0f, -4.0f, -5.0f, -5.0f, -4.0f, -4.0f, -3.0f }
	};

	float[][] kingEvalBlack = new float[][]
	{
		new float[] { -3.0f, -4.0f, -4.0f, -5.0f, -5.0f, -4.0f, -4.0f, -3.0f },
		new float[] { -3.0f, -4.0f, -4.0f, -5.0f, -5.0f, -4.0f, -4.0f, -3.0f },
		new float[] { -3.0f, -4.0f, -4.0f, -5.0f, -5.0f, -4.0f, -4.0f, -3.0f },
		new float[] { -3.0f, -4.0f, -4.0f, -5.0f, -5.0f, -4.0f, -4.0f, -3.0f },
		new float[] { -2.0f, -3.0f, -3.0f, -4.0f, -4.0f, -3.0f, -3.0f, -2.0f },
		new float[] { -1.0f, -2.0f, -2.0f, -2.0f, -2.0f, -2.0f, -2.0f, -1.0f },
		new float[] {  2.0f,  2.0f,  0.0f,  0.0f,  0.0f,  0.0f,  2.0f,  2.0f },
		new float[] {  2.0f,  3.0f,  1.0f,  0.0f,  0.0f,  1.0f,  3.0f,  2.0f }
	};

	#endregion

}

