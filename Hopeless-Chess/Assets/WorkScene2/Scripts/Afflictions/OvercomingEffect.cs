using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvercomingEffect : MonoBehaviour
{
    private CharacterController piece;
    public void Start() 
    {
        piece = GetComponent<CharacterController>();
        Overcoming(piece);
    }

    /// <summary>
    /// Восстанавливает половину морали фигуры
    /// </summary>
    /// <param name="piece"></param>
    public void Overcoming(CharacterController piece)
    {
        piece.moralityCount = piece.character.MaxMorality / 2;
    }
}
