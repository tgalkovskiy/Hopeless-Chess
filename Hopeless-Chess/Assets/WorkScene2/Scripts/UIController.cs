using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject pieceMenu;
    Ray checkMousePosRay;
    RaycastHit checkMousePosHit;

    [Header("Меню фигуры")]
    [SerializeField]
    Text pieceName;

    [SerializeField]
    Text moralityCount;

    [SerializeField]
    Text afflictionName;

    [SerializeField]
    Text afflictionShortDiscrip;
    

    private void Update() 
    {
        ActivatePieceMenu();
    }
    private void ActivatePieceMenu()
    {
        checkMousePosRay = mainCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(checkMousePosRay, out checkMousePosHit, 1000))
			{
                GameObject hitObject = checkMousePosHit.collider.gameObject;
				if (hitObject.layer == LayerMask.NameToLayer("Piece"))
				{
                    if(!pieceMenu.activeSelf)
                    {
                        pieceMenu.SetActive(true);
                    }
                    CharacterController character = hitObject.GetComponent<CharacterController>();

                    pieceName.text = character.character.Name;
                    moralityCount.text = character.moralityCount.ToString();
                    if(character.affliction != null)
                    {
                        afflictionName.text = character.affliction.Name;
                        afflictionName.text = character.affliction.ShortDisctiption;
                    }
                    else
                    {
                        afflictionName.text = "Нет";
                        afflictionShortDiscrip.text = "Нет";
                    }
                    
                }
                else
                {
                    if(pieceMenu.activeSelf)
                    {
                        pieceMenu.SetActive(false);
                    }
                }
            }
            
    }

}
