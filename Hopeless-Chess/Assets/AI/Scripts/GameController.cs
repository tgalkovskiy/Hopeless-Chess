using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameController : MonoBehaviour
{

	bool isLightTurn;
	[SerializeField]
	BoardController2 board;
	[SerializeField]
	ChessAI AI; 

	[SerializeField]
	Camera mainCamera;
	Ray ray;
	RaycastHit hit;
	GameObject hitObject;
	[SerializeField]
	CharacterController lastCharacterSelected;

	bool isFirstMove;

	void Start()
	{
		isFirstMove = true;
		NextTurn();
		isFirstMove = false;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ray = mainCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit, 1000))
			{
				//Сокращаю строчку.
				hitObject = hit.collider.gameObject;

				if (hitObject.layer == LayerMask.NameToLayer("Piece"))
				{
					// Повторное нажатие отменеят выделение
					if ( lastCharacterSelected == hitObject.GetComponent<CharacterController>())
					{
						lastCharacterSelected = lastCharacterSelected.CanсelSelecteCharacter();
						board.StopShowPieceMoves();
					}
					else
					{
						//Выбираем новую фигуру, когда нажимаем на неё
						if (lastCharacterSelected != null )
						{
							lastCharacterSelected = lastCharacterSelected.CanсelSelecteCharacter();
							board.StopShowPieceMoves();
						}
						lastCharacterSelected = hitObject.GetComponent<CharacterController>().SelecteCharacter();
						board.GlowSquares(board.FindPieceMoves(lastCharacterSelected));

					}
				}
				//Перемещаем фигуру на нужную клетку
				else if (hitObject.layer == LayerMask.NameToLayer("Square") &&
						lastCharacterSelected != null)
				{
					//lastCharacterSelected.MoveCharacter(hitObject.transform.position);

					// Новое
					if (!board.IsItCheck(lastCharacterSelected, hitObject, isLightTurn))
					{
						board.MovePiece(lastCharacterSelected, hitObject);

						if (board.IsItMate(lastCharacterSelected, hitObject, isLightTurn))
						{
							Debug.Log("Игра окончена, это мат!");
							//Destroy(this);
						}

						if (lastCharacterSelected != null)  lastCharacterSelected = lastCharacterSelected.CanсelSelecteCharacter();
						NextTurn();
						board.StopShowPieceMoves();

					}
					else Debug.Log("Так ходить нельзя, тебе шах.");
				}
			}
			else
			{
				// Отменяем выделение
				board.StopShowPieceMoves();
				if(lastCharacterSelected!=null) lastCharacterSelected = lastCharacterSelected.CanсelSelecteCharacter();
			}
		}
	}

	void NextTurn()
	{
		//board.ShowBoard();

		isLightTurn = !isLightTurn;
		mainCamera.GetComponent<CameraController>().GoToPosition(isLightTurn);

		if (isLightTurn)
		{
			board.SwitchOffBlackColliders();
			//if (GameModule.instance.AISide != ChessAI.Side.light)
				board.SwitchOnLhiteColliders();
		}
		else
		{
			board.SwitchOffLhiteColliders();
			//if (GameModule.instance.AISide != ChessAI.Side.dark)
				board.SwitchOnBlackColliders();
		}

		if (AI.Side != ChessAI.SideAI.none)
		{
			Debug.Log(AI.BestMove(isLightTurn));
		}



		//sh255 //yukit

		
		
		if(isFirstMove == false)
		{
			PieceEated();
			Morality.GetInstance().CheckMorality(board.GaveupPieces(), board);
		}

	}

	public bool IsLightTurn
	{
		get
		{
			return isLightTurn;
		}
	}

	public void PieceEated()
	{
		if(board.LastEatenPiece != null)
		{
			if(board.LastEatenPiece.isLight)
			{
				Morality.GetInstance().AddMorality(board.GetControllers(board.LightPieces).ToArray(), -3);
				Morality.GetInstance().AddMorality(board.GetControllers(board.DarkPieces).ToArray(), 3);
			}
			else
			{
				Morality.GetInstance().AddMorality(board.GetControllers(board.LightPieces).ToArray(), 3);
				Morality.GetInstance().AddMorality(board.GetControllers(board.DarkPieces).ToArray(), -3);
			}
		}
	}

}