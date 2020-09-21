using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    //
    public Camera mainCamera;

    public Transform cells;

    public GameObject cellPrefab;

    public CharacterController lastCharacterSelected;

    private void Start() 
    {   
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            Vector3 mousePos = Input.mousePosition;
            Ray ray;
            RaycastHit hit;

            ray = mainCamera.ScreenPointToRay(Input.mousePosition);


            if(Physics.Raycast(ray, out hit, 1000))
            {
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Figure"))
                {
                    //Выбираем новую фигуру, когда нажимаем на неё
                    if(lastCharacterSelected != null)
                    {
                        lastCharacterSelected.isSelected = false;
                    }
                    lastCharacterSelected = hit.collider.gameObject.GetComponent<CharacterController>();
                    lastCharacterSelected.isSelected = true;
                }
                //Перемещаем фигуру на нужную клетку
                else if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Cell") && 
                        lastCharacterSelected != null)
                {
                    lastCharacterSelected.MoveCharacter(hit.collider.gameObject.transform.position);
                }
            }
        }
    }

}
