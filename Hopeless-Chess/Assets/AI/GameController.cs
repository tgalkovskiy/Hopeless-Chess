using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    bool isWhitesTurn;
    [SerializeField]
    GameObject whites;
    [SerializeField]
    GameObject blacks;

    BoxCollider[] whiteFiguresCollider;
    BoxCollider[] blackFiguresCollider;

    void Start()
    {
        whiteFiguresCollider = whites.GetComponentsInChildren<BoxCollider>();
        blackFiguresCollider = blacks.GetComponentsInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextTurn()
	{
        isWhitesTurn = !isWhitesTurn;
        switchWhiteColliders(isWhitesTurn);
        switchBlackColliders(isWhitesTurn);
    }

    void switchBlackColliders(bool enabled)
	{
		foreach (var item in blackFiguresCollider)
		{
            item.enabled = enabled;
		}
	}

    void switchWhiteColliders(bool enabled)
    {
        foreach (var item in whiteFiguresCollider)
        {
            item.enabled = enabled;
        }
    }
}
