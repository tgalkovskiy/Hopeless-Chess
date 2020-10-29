using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    
{
    public Camera mainCamera;
    public GameObject pieceMenu;

    public GameObject bigPieceMenu;
    public RectTransform pieceMenuRect;
    Ray checkMousePosRay;
    RaycastHit checkMousePosHit;

    [Header("Меню фигуры")]
    [SerializeField]
    public Text pieceName;

    [SerializeField]
    Text moralityCount;

    [SerializeField]
    Text afflictionName;

    [SerializeField]
    Text afflictionShortDiscrip;

    /*[Header("Большое меню фигуры")]
    [SerializeField]
    public Text pieceNameInMenu;

    [SerializeField]
    Text moralityCountInMenu;

    [SerializeField]
    Text pieceDiscription;

    [SerializeField]
    Text afflictionNameInMenu;

    [SerializeField]
    Text afflictionDiscription;

    [SerializeField]
    Text movesToRemoveAfflition;
    */


    bool isMouseOnWindow;

    CharacterController lastCharacterSelected;
    
    /*public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerCurrentRaycast.gameObject.CompareTag("PieceMenu") && eventData.pointerCurrentRaycast.gameObject.layer == LayerMask.NameToLayer("UI"))
        {
            isMouseOnWindow = true;
            OpenBigPieceMenu();
        }
        
    }*/
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
                    lastCharacterSelected = hitObject.GetComponent<CharacterController>();
                    OpenPieceMenu(hitObject);
                }
                
                else
                {
                    if(pieceMenu.activeSelf)
                    {
                        pieceMenu.SetActive(false);
                        //StartCoroutine(CloseByDelay(pieceMenu, 0.1f));
                    }
                }
            }
            
    }


    private IEnumerator CloseByDelay(GameObject window, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        window.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {     
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        

    }

   /* public void OpenBigPieceMenu()
    {
        pieceNameInMenu.text = lastCharacterSelected.character.Name;
        moralityCountInMenu.text = lastCharacterSelected.moralityCount.ToString();
        pieceDiscription.text = lastCharacterSelected.character.Disctiption;
        if(lastCharacterSelected.affliction != null)
        {
            afflictionNameInMenu.text = lastCharacterSelected.affliction.Name;
            movesToRemoveAfflition.text = lastCharacterSelected.movesToRemoveAffliction.ToString();
            afflictionDiscription.text = lastCharacterSelected.affliction.Disctiption;
        }
        else
        {
            afflictionNameInMenu.text = "Нет";
            movesToRemoveAfflition.text = "0";
            afflictionDiscription.text = "Нет";
        }

        bigPieceMenu.SetActive(true);
    }*/

    public void OpenPieceMenu(GameObject hitObject)
    {
        if(!pieceMenu.activeSelf)
        {
            pieceMenu.SetActive(true);
                        
        }
            Vector3 hitRect = mainCamera.WorldToScreenPoint(new Vector3(hitObject.transform.position.x,
                                                                                hitObject.transform.position.y, hitObject.transform.position.z));
            pieceMenu.transform.position = new Vector3(hitRect.x - pieceMenuRect.rect.width / 3f, hitRect.y, hitRect.z);
            
                    
            pieceName.text = lastCharacterSelected.character.Name;
            moralityCount.text = lastCharacterSelected.moralityCount.ToString();
            if(lastCharacterSelected.affliction != null)
            {
                afflictionName.text = lastCharacterSelected.affliction.Name;
                afflictionShortDiscrip.text = lastCharacterSelected.affliction.ShortDisctiption;
            }
            else
            {
                afflictionName.text = "Нет";
                afflictionShortDiscrip.text = "Нет";
            }
    }

    public IEnumerator OpenPieceMenuOnEndOfFrame(GameObject hitObject)
    {
        yield return new WaitForSecondsRealtime(0.09f);
        OpenPieceMenu(hitObject);
        
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }

}
