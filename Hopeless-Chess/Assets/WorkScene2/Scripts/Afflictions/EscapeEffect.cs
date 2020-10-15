using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeEffect : MonoBehaviour
{
    private CharacterController piece;
    public void Start() 
    {
        piece = GetComponent<CharacterController>();
        Escape(piece, 3);
    }
    public void Escape(CharacterController piece, int movesToRemove)
    {
        piece.movesToRemoveAffliction = movesToRemove;
        Texture2D moveTexture = new Texture2D(15,15);
        moveTexture.LoadImage(piece.moveTexture.EncodeToPNG());
        
            for(int y = 0; y < moveTexture.height; y++)
            {
               
                for(int x = 0; x < moveTexture.width; x++)
                {

                    if(y < 7 && piece.isLight)
                    {
                        moveTexture.SetPixel(x, y, GameModule.instance.MoveColors[0]);
                    }
                    else if(y > 7 && !piece.isLight)
                    {
                        moveTexture.SetPixel(x, y, GameModule.instance.MoveColors[0]);
                    }
                    else
                    {
                        if(moveTexture.GetPixel(x,y) == GameModule.instance.MoveColors[3])
                        {
                            moveTexture.SetPixel(x, y, GameModule.instance.MoveColors[1]);
                        }
                    }
                }
            }
        piece.currentMoveTexture = moveTexture;
        
    }
}
