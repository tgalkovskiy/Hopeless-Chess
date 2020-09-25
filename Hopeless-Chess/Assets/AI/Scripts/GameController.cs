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

				if (hitObject.layer == LayerMask.NameToLayer("Piece"))
				{
					// Повторное нажатие отменеят выделение
					if ( lastCharacterSelected == hitObject.GetComponent<CharacterController>())
					{
						lastCharacterSelected = lastCharacterSelected.CanсelSelecteCharacter();
					}
					else
					{
						//Выбираем новую фигуру, когда нажимаем на неё
						if (lastCharacterSelected != null )
						{
							lastCharacterSelected = lastCharacterSelected.CanсelSelecteCharacter();
						}
						lastCharacterSelected = hitObject.GetComponent<CharacterController>().SelecteCharacter();

						board.ShowFiguresMoves(lastCharacterSelected);
					}
				}
				//Перемещаем фигуру на нужную клетку
				else if (hitObject.layer == LayerMask.NameToLayer("Square") &&
						lastCharacterSelected != null)
				{
					//lastCharacterSelected.MoveCharacter(hitObject.transform.position);

					// Новое
					if (!board.IsItShah(lastCharacterSelected, hitObject, isLightTurn))
					{
						if (!board.IsItMate(lastCharacterSelected, hitObject, isLightTurn))
						{
							board.MoveFigur(lastCharacterSelected, hitObject);
							lastCharacterSelected = lastCharacterSelected.CanсelSelecteCharacter();
							NextTurn();
						}
						else Debug.Log("Игра окончена, это мат!");
					}
					else Debug.Log("Так ходить нельзя, будет шах.");
				}
			}
			else
			{
				// Отменяем выделение
				board.StopShowFiguresMoves(lastCharacterSelected);
				lastCharacterSelected = lastCharacterSelected.CanсelSelecteCharacter();
			}
		}
	}

	void NextTurn()
	{
		board.ShowBoard();

		isLightTurn = !isLightTurn;
		if (isLightTurn)
		{
			board.SwitchOffBlackColliders();
			board.SwitchOnLhiteColliders();
		}
		else
		{
			board.SwitchOffLhiteColliders();
			board.SwitchOnBlackColliders();
		}
	}

	public bool IsLightTurn
	{
		get
		{
			return isLightTurn;
		}
	}
}