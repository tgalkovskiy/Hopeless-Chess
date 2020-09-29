using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afflictions : MonoBehaviour
{

}


/// <summary>
/// Контроллер злости персонажей
/// </summary>
public class Anger : MonoBehaviour
{

    #region Singleton


   

    public static Anger instance;

    protected Anger()
    {

    }
    public static Anger GetInstance()
    {
        if(instance == null)
        {
            instance = new Anger();
        }
        return instance;
    }

    #endregion
    
    /// <summary>
    /// Старая текстура ходя
    /// </summary>
    private Texture2D oldMoveTexture;

    /// <summary>
    /// Режим берсерка
    /// </summary>
    /// <param name="character"></param>
    public void RageMode(CharacterController character)
    {
        oldMoveTexture = character.moveTexture;
        character.moveTexture = character.rageTexture;
    }

    /// <summary>
    /// Выключение режима берсерка
    /// </summary>
    /// <param name="character"></param>
    public void DisableRageMode(CharacterController character)
    {
        character.moveTexture = oldMoveTexture;
        oldMoveTexture = null;
    }
}

/// <summary>
/// Оббработка морали
/// </summary>
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
    /// Восстанавливает половину морали фигуры
    /// </summary>
    /// <param name="piece"></param>
    public void Overcoming(CharacterController piece)
    {
        piece.moralityCount = piece.character.MaxMorality / 2;
    }

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
        piece.oldMoveTexture = piece.moveTexture;
        piece.moveTexture = piece.givingupTexture;
    }

    /// <summary>
    /// Переключение на обычную текстуру хода
    /// </summary>
    /// <param name="piece"></param>
    public void DisableGivingup(CharacterController piece)
    {
        if(piece.oldMoveTexture != null)
        {
            piece.moveTexture = piece.oldMoveTexture;
            piece.oldMoveTexture = null;
        }
    }

    public void CheckMorality(CharacterController[] pieces)
    {
        for(int i = 0; i < pieces.Length; i++)
        {
            
            if(pieces[i].moralityCount < 5f)
            {
                Givingup(pieces[i]);
            }
            else
            {
                
                DisableGivingup(pieces[i]);
            }
        }
    }
}

/// <summary>
/// Обработка отношений с другими фигурами
/// </summary>
public class Mood : MonoBehaviour
{

    #region Singleton
    public static Mood instance;

    protected Mood()
    {

    }
    public static Mood GetInstance()
    {
        if(instance == null)
        {
            instance = new Mood();
        }
        return instance;
    }

    #endregion

    /// <summary>
    /// Обработка смерти друга
    /// </summary>
    /// <param name="killedPiece"></param>
    public void FriendKilled(CharacterController killedPiece)
    {
        CharacterController[] friends = killedPiece.friends;
        
        for(int i = 0; i < friends.Length; i++)
        {
            if(!friends[i].isDead)
            {
                
                friends[i].moralityCount -= ((int)killedPiece.character.ChessType * 2);
                friends[i].angerCount -= ((int)killedPiece.character.ChessType * 4);
            }
        }
    }

    /// <summary>
    /// Обработка смерти врага
    /// </summary>
    /// <param name="killedPiece"></param>
    public void EnemyKilled(CharacterController killedPiece)
    {
        CharacterController[] enemies = killedPiece.enemies;
        for(int i = 0; i < enemies.Length; i++)
        {
            if(!enemies[i].isDead)
            {
                enemies[i].moralityCount += ((int)killedPiece.character.ChessType * 2);
            }
        }
    }
}