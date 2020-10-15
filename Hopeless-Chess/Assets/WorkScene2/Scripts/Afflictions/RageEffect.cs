using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageEffect : MonoBehaviour
{
    private CharacterController piece;
    public void Start() 
    {
        piece = GetComponent<CharacterController>();
        Rage(piece, 3);
        Destroy(gameObject.GetComponent<RageEffect>());
    }
    public void Rage(CharacterController piece, int movesToRemove)
    {
        piece.movesToRemoveAffliction = movesToRemove;
        Texture2D moveTexture = new Texture2D(15,15);
        moveTexture.LoadImage(piece.moveTexture.EncodeToPNG());
        
            for(int y = 0; y < moveTexture.height; y++)
            {
               
                for(int x = 0; x < moveTexture.width; x++)
                {
                    if(moveTexture.GetPixel(x,y) == GameModule.instance.MoveColors[3]) 
                    {
                        moveTexture.SetPixel(x, y, GameModule.instance.MoveColors[2]);
                    }

                    else if (moveTexture.GetPixel(x,y) == GameModule.instance.MoveColors[4])
                    {
                        moveTexture.SetPixel(x,y, GameModule.instance.MoveColors[6]);
                    }

                    else if (moveTexture.GetPixel(x,y) == GameModule.instance.MoveColors[1])
                    {
                        moveTexture.SetPixel(x, y, GameModule.instance.MoveColors[0]);
                    }
                }
            }
        piece.currentMoveTexture = moveTexture;
    }
}
