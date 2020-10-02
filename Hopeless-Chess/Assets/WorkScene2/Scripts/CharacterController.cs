using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Обрабатывает состояние фигуры в текущий момент времени
/// </summary>
public class CharacterController : Afflictions
{
    /// <summary>
    /// Основная информация о персонаже (не изменяется в ходе игры)
    /// </summary>
    public CharactersObject character;

    /// <summary>
    /// Текущий уровень морали фигуры
    /// </summary>
     public float moralityCount;

    public CharacterController[] friends;

    public CharacterController[] enemies;
    
    /// <summary>
    /// Проверка, мёртв ли персонаж
    /// </summary>
    public bool isDead;

    /// <summary>
    /// Проверяет, выбрана ли сейчас эта фигура
    /// </summary>
    public bool isSelected;

    public Texture2D moveTexture;

    public Texture2D currentMoveTexture;

    public ChessType pieceType;

    public bool isLight;

    public Texture2D oldMoveTexture = null;
 //
    public int boardIndex;

    private void Start() 
    {

        if(character == null)
        {
            InitializeCharacters();
        }
        if(character != null)
        {
            moralityCount = character.MaxMorality;
        }
        currentMoveTexture = moveTexture;

    }

    /// <summary>
    /// Возвращает скрипт персонажа, включает флаг isSelected
    /// </summary>
    /// <returns></returns>
    public  CharacterController GetCharacter()
    {
        isSelected = true;
        return gameObject.GetComponent<CharacterController>();
    }

    public void InitializeCharacters()
    {
        GameModule gameModule = GameModule.instance;
        if(pieceType == ChessType.pawn)
        {
            for(int i = 0; i < gameModule.PawnsData.Length; i++)
            {
                
                if(gameModule.PawnsData[i] != null)
                {
                    //Debug.Log(gameModule.PawnsData.Length);
                    character = gameModule.PawnsData[i];
                    gameModule.PawnsData[i] = null;
                    break;
                }
            }
        }
        else if(pieceType == ChessType.rook)
        {
            for(int i = 0; i < gameModule.RooksData.Length; i++)
            {
                if(gameModule.RooksData[i] != null)
                {
                    //Debug.Log(gameModule.RooksData.Length);
                    character = gameModule.RooksData[i];
                    gameModule.RooksData[i] = null;
                    break;
                }
            }
        }
        else if(pieceType == ChessType.bishop)
        {
            for(int i = 0; i < gameModule.BishopsData.Length; i++)
            {
                if(gameModule.BishopsData[i] != null)
                {
                    //Debug.Log(gameModule.BishopsData.Length);
                    character = gameModule.BishopsData[i];
                    gameModule.BishopsData[i] = null;
                    break;
                }
            }
        }
        else if(pieceType == ChessType.knight)
        {
            for(int i = 0; i < gameModule.KnightData.Length; i++)
            {
                if(gameModule.KnightData[i] != null)
                {
                    //Debug.Log(gameModule.KnightData.Length);
                    character = gameModule.KnightData[i];
                    gameModule.KnightData[i] = null;
                    break;
                }
            }
        }
        else if(pieceType == ChessType.queen)
        {
            for(int i = 0; i < gameModule.QueenData.Length; i++)
            {
                if(gameModule.QueenData[i] != null)
                {
                    //Debug.Log(gameModule.QueenData.Length);
                    character = gameModule.QueenData[i];
                    gameModule.QueenData[i] = null;
                    break;
                }
            }
        }
        else if(pieceType == ChessType.king)
        {
            for(int i = 0; i < gameModule.KingData.Length; i++)
            {
                if(gameModule.QueenData[i] != null)
                {
                    //Debug.Log(gameModule.KingData.Length);
                    character = gameModule.KingData[i];
                    gameModule.KingData[i] = null;
                    break;
                }
            }
        }
    }


    public CharacterController SelecteCharacter()
    {
        isSelected = true;
        // Анимация подпрыгивания.
        this.GetComponent<Animation>().Play();
        return this;
    }

    public CharacterController CanсelSelecteCharacter()
    {
        isSelected = false;
        // Анимация подпрыгивания.
        this.GetComponent<Animation>().Stop();
        return null;
    }


    /// <summary>
    
    /// </summary>
    /// <returns></returns>
    public Texture2D GetMoveTexture()
    {
        return currentMoveTexture;
    }


    /// <summary>
    /// Обрабатывает перемещение фигуры по доске
    /// </summary>
    /// <param name="cellCoords"></param>
    public void MoveCharacter(Vector3 cellCoords)
    {
        transform.position = cellCoords;
    }

    public enum ChessType
    {
        none,
        pawn,
        rook,
        knight,
        bishop,
        queen,
        king
    }

    private IEnumerator Giving()
    {
        yield return new WaitForSeconds(1);
    }
}
