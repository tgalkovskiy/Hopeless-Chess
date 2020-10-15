using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroismEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController piece;
    public void Start() 
    {
        piece = GetComponent<CharacterController>();
        Heroism(piece);
    }
    

    /// <summary>
    /// Восстанавливает мораль фигуры
    /// </summary>
    /// <param name="piece"></param>
    public void Heroism(CharacterController piece)
    {
        piece.moralityCount = piece.character.MaxMorality;
    }
}
