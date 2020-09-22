using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

	bool isWhitesTurn;
	[SerializeField]
	VirtualBoardController board;

	[SerializeField]
	Camera mainCamera;
	Ray ray;
	RaycastHit hit;
	GameObject hitObject;
	[SerializeField]
	CharacterController lastCharacterSelected;


	void Start()
	{
		NextTurn();
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

				if (hitObject.layer == LayerMask.NameToLayer("Figure"))
				{
					//Выбираем новую фигуру, когда нажимаем на неё
					if (lastCharacterSelected != null)
					{
						lastCharacterSelected.isSelected = false;
					}
					lastCharacterSelected = hitObject.GetComponent<CharacterController>();
					lastCharacterSelected.isSelected = true;


					// Новое
					board.ShowFiguresMoves(lastCharacterSelected);


				}
				//Перемещаем фигуру на нужную клетку
				else if (hitObject.layer == LayerMask.NameToLayer("Cell") &&
						lastCharacterSelected != null)
				{
					lastCharacterSelected.MoveCharacter(hitObject.transform.position);


					// Новое
					if (!board.IsItShah(lastCharacterSelected, hitObject, isWhitesTurn))
					{
						if (!board.IsItMate(lastCharacterSelected, hitObject, isWhitesTurn))
						{
							board.MoveFigur(lastCharacterSelected, hitObject);
							NextTurn();
						}
						else Debug.Log("Игра окончена, это мат!");
					}
					else Debug.Log("Так ходить нельзя, будет шах.");

				}
				// Отменяем выделение
				else board.StopShowFiguresMoves(lastCharacterSelected);

			}
		}
	}

	void NextTurn()
	{
		isWhitesTurn = !isWhitesTurn;
		if (isWhitesTurn)
		{
			board.SwitchOffBlackColliders();
			board.SwitchOnWhiteColliders();
		}
		else
		{
			board.SwitchOffWhiteColliders();
			board.SwitchOnBlackColliders();
		}
	}

	public bool IsWhitesTurn
	{
		get
		{
			return isWhitesTurn;
		}
	}
}