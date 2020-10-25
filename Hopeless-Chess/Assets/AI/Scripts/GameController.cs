using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor;

public class GameController : MonoBehaviour
{
	bool isLightTurn;

	[SerializeField]
	BoardController2 board;
	[SerializeField]
	ChessAI AI; 

	[SerializeField]
	public float moralityDamage;

	[SerializeField]
	public float moralityHeal;

	[SerializeField]
	Camera mainCamera;
	Ray ray;
	RaycastHit hit;
	GameObject hitObject;
	[SerializeField]
	CharacterController lastCharacterSelected;

	bool isInit;

	public MoralityPreset moralityPreset;

	[ConditionalHide("moralityPreset", (int)MoralityPreset.different)]
	public int pawnMorality;
	[ConditionalHide("moralityPreset", (int)MoralityPreset.different)]
	public int rookMorality;
	[ConditionalHide("moralityPreset", (int)MoralityPreset.different)]
	public int bishopMorality;
	[ConditionalHide("moralityPreset", (int)MoralityPreset.different)]
	public int knightMorality;
	[ConditionalHide("moralityPreset", (int)MoralityPreset.different)]
	public int queenMorality;



	void Start()
	{
		moralityDamage = PPM.instance.MoralityDamage;
		moralityHeal = PPM.instance.MoralityHeal;
		moralityPreset = PPM.instance.MoralityPreset;

		isInit = true;
		NextTurn();
		isInit = false;
		StartCoroutine(InitInTheEnd());
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

				//Debug.Log(hitObject.name);

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

						if(board.LastEatenPiece != null)
						{
							PieceEated();
						}

						if (board.IsItMate(lastCharacterSelected, hitObject, isLightTurn))
						{
							Debug.Log("Игра окончена, это мат!");
							//Destroy(this);
						}
						if(lastCharacterSelected.pieceType == CharacterController.ChessType.pawn)
						{
							if(lastCharacterSelected.isFirstMove == true)
							{
								lastCharacterSelected.OnPieceFirstMoveEnded();
							}
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
			//StartCoroutine(AI.BestMoveCorutine(isLightTurn));
			Debug.Log(AI.BestMove(isLightTurn));

		}



		//sh255 //yukit

		
		
		//if(isInit == false)
		//{
		//	PieceEated();
		//	Morality.GetInstance().CheckMorality(board.AllPieces(), board);
		//}

	}

	public bool IsLightTurn
	{
		get
		{
			return isLightTurn;
		}
	}

	public void PieceEated()
	{Debug.Log("Eated");
		if(board.LastEatenPiece != null)
		{
			if(board.LastEatenPiece.isLight)
			{
				Debug.Log("Eated");
				Morality.GetInstance().AddMorality(board.GetControllers(board.LightPieces).ToArray(), -moralityDamage * board.LastEatenPiece.significanceMultiply);
				Morality.GetInstance().AddMorality(board.GetControllers(board.DarkPieces).ToArray(), moralityHeal * board.LastEatenPiece.significanceMultiply);
			}
			else
			{
				Morality.GetInstance().AddMorality(board.GetControllers(board.LightPieces).ToArray(), moralityHeal * board.LastEatenPiece.significanceMultiply);
				Morality.GetInstance().AddMorality(board.GetControllers(board.DarkPieces).ToArray(), -moralityDamage * board.LastEatenPiece.significanceMultiply);
			}
		}
	}
	
	public void SetCharacterMoralityPreset(CharacterController[] pieces)
	{
		for(int i = 0; i < pieces.Length; i++)
		{
			if(moralityPreset == MoralityPreset.nothing)
			{
				pieces[i].moralityCount = 0;
			}
			else if(moralityPreset == MoralityPreset.full)
			{
				pieces[i].moralityCount = pieces[i].character.MaxMorality;
			}
			else if(moralityPreset == MoralityPreset.different)
			{
				if(pieces[i].pieceType == CharacterController.ChessType.pawn)
				{
					pieces[i].moralityCount = pawnMorality;
				}
				else if(pieces[i].pieceType == CharacterController.ChessType.rook)
				{
					pieces[i].moralityCount = rookMorality;
				}
				else if(pieces[i].pieceType == CharacterController.ChessType.bishop)
				{
					pieces[i].moralityCount = bishopMorality;
				}
				else if(pieces[i].pieceType == CharacterController.ChessType.knight)
				{
					pieces[i].moralityCount = knightMorality;
				}
				else if(pieces[i].pieceType == CharacterController.ChessType.queen)
				{
					pieces[i].moralityCount = queenMorality;
				}
			}
			
		}
	}

	public enum MoralityPreset
	{
		none,
		full,
		different,
		nothing
	}
	private IEnumerator InitInTheEnd()
	{
		yield return new WaitForEndOfFrame();
		SetCharacterMoralityPreset(board.AllPieces());
	}

}