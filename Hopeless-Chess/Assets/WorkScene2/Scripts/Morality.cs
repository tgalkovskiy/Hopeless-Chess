using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Morality : MonoBehaviour
{
    #region Singleton
    public static Morality instance;

    protected Morality()
    {

    }
    public static Morality GetInstance()
    {
        if(instance == null)
        {
            instance = new Morality();
        }
        return instance;
    }

    #endregion

    

    /// <summary>
    /// Добавляет мораль фигурам
    /// </summary>
    /// <param name="pieces"></param>
    /// <param name="moralityCount"></param>
    public void AddMorality(CharacterController[] pieces, float moralityCount)
    {
        for(int i = 0; i < pieces.Length; i++)
        {
            pieces[i].moralityCount += moralityCount;
        }
    }

    /// <summary>
    /// Добавляет мораль фигуре
    /// </summary>
    /// <param name="piece"></param>
    /// <param name="moralityCount"></param>
    public void AddMorality(CharacterController piece, float moralityCount)
    {
            piece.moralityCount += moralityCount;
    }

    /// <summary>
    /// Добавление морали при превращении фигуры
    /// </summary>
    /// <param name="piece"></param>
    /// <param name="pieces"></param>
    public void OnTransformPiece(CharacterController piece, CharacterController[] pieces)
    {
        piece.moralityCount += 100f;
        AddMorality(pieces, 10f);
    }



   /// <summary>
   /// Изменение текстуры хода при отсутствии морали
   /// </summary>
   /// <param name="piece"></param>
    public void Givingup(CharacterController piece)
    {
    }

    /// <summary>
    /// Переключение на обычную текстуру хода
    /// </summary>
    /// <param name="piece"></param>
    public void DisableGivingup(CharacterController piece)
    {
        //if(piece.oldMoveTexture != null && piece.moveTexture.name.Contains("givingup"))
        //{
            //piece.moveTexture = piece.oldMoveTexture;
            //piece.oldMoveTexture = null;
        //}
    }

    public void CheckMorality(CharacterController[] pieces, BoardController2 board)
    {
        for(int i = 0; i < pieces.Length; i++)
        {
            if(pieces[i].moralityCount == 0)
            {
                if(board.IsMyQweenOrKingNear(pieces[i]))
                {
                    Afflictions.GetAfflictions().Heroism(pieces[i]);
                }
                else if(board.AlliesCount(pieces[i]) >= 3)
                {
                    Afflictions.GetAfflictions().Overcoming(pieces[i]);
                }
                else if(board.EmenyCount(pieces[i]) >= 3)
                {
                    Afflictions.GetAfflictions().Escape(pieces[i]);
                }
                else if(board.IsAlliesDieNear(pieces[i]))
                {
                    Afflictions.GetAfflictions().Rage(pieces[i]);
                }
                else if(board.IsMyQweenDie(pieces[i]))
                {
                    Afflictions.GetAfflictions().Panic(pieces[i]);
                }
                
            }
        }
    }
}
