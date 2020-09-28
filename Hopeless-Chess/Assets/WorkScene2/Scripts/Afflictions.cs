using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afflictions : MonoBehaviour
{

    [HideInInspector] public Morality morality = Morality.GetInstance();
    
    [HideInInspector] public Mood mood = Mood.GetInstance();

    [HideInInspector] public Anger anger = Anger.GetInstance();
}


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
    private Texture2D oldMoveTexture;

    public void RageMode(CharacterController character)
    {
        oldMoveTexture = character.moveTexture;
        character.moveTexture = character.rageTexture;
    }

    public void DisableRageMode(CharacterController character)
    {
        character.moveTexture = oldMoveTexture;
        oldMoveTexture = null;
    }
}

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

    public void Overcoming(CharacterController piece)
    {
        piece.moralityCount = piece.character.MaxMorality / 2;
    }

    public void AddMorality(CharacterController[] pieces, float moralityCount)
    {
        for(int i = 0; i < pieces.Length; i++)
        {
            pieces[i].moralityCount += moralityCount;
        }
    }
    public void AddMorality(CharacterController piece, float moralityCount)
    {
            piece.moralityCount += moralityCount;
    }

    public void OnTransformPiece(CharacterController piece, CharacterController[] pieces)
    {
        piece.moralityCount += 100f;
        AddMorality(pieces, 10f);
    }


   Texture2D oldTexture;
    public void Givingup(CharacterController piece)
    {
        oldTexture = piece.moveTexture;
        piece.moveTexture = piece.givingupTexture;
    }

    public void DisableGivingup(CharacterController piece)
    {
        piece.moveTexture = oldTexture;
        oldTexture = null;
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