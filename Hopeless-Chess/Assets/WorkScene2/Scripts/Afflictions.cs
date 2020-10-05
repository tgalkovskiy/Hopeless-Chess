using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afflictions : MonoBehaviour
{
    private static Afflictions instance;
    protected Afflictions()
    {

    }

    public static Afflictions GetAfflictions()
    {
        if(instance == null)
        {
            instance = new Afflictions();
        }
        return instance;
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

    public void Panic(CharacterController piece,int movesToRemove)
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
                        moveTexture.SetPixel(x, y, GameModule.instance.MoveColors[1]);
                    }
                    else if (moveTexture.GetPixel(x,y) == GameModule.instance.MoveColors[4])
                    {
                        moveTexture.SetPixel(x,y, GameModule.instance.MoveColors[5]);
                    }
                    else if (moveTexture.GetPixel(x,y) == GameModule.instance.MoveColors[2])
                    {
                        moveTexture.SetPixel(x, y, GameModule.instance.MoveColors[0]);
                    }
                }
            }
        piece.currentMoveTexture = moveTexture;
    }

    /// <summary>
    /// Восстанавливает половину морали фигуры
    /// </summary>
    /// <param name="piece"></param>
    public void Overcoming(CharacterController piece)
    {
        piece.moralityCount = piece.character.MaxMorality / 2;
    }

    /// <summary>
    /// Восстанавливает мораль фигуры
    /// </summary>
    /// <param name="piece"></param>
    public void Heroism(CharacterController piece)
    {
        piece.moralityCount = piece.character.MaxMorality;
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