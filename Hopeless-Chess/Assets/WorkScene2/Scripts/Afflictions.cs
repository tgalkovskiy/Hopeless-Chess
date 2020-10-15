using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afflictions : MonoBehaviour
{
    public AfflictionsData rage;
    public AfflictionsData panic;
    public AfflictionsData escape;
    public AfflictionsData heroism;
    public AfflictionsData overcoming;
    
}


/*/// <summary>
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
    /// Режим берсерка
    /// </summary>
    /// <param name="character"></param>
    public void RageMode(CharacterController character)
    {
        character.oldMoveTexture = character.moveTexture;
        character.moveTexture = character.rageTexture;
    }

    /// <summary>
    /// Выключение режима берсерка
    /// </summary>
    /// <param name="character"></param>
    public void DisableRageMode(CharacterController character)
    {
        character.moveTexture = character.oldMoveTexture;
        character.oldMoveTexture = null;
    }

    public void CheckAnger(CharacterController[] characters)
    {
        
        for(int i = 0; i < characters.Length; i++)
        {
            
            if(characters[i].angerCount < 3f)
            {
                RageMode(characters[i]);
                
            }
            else
            {
                if(characters[i].oldMoveTexture != null && characters[i].moveTexture.name.Contains("rage"))
                {
                    DisableRageMode(characters[i]);
                }
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
*/