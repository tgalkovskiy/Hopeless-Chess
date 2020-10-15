using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Обрабатывает состояние фигуры в текущий момент времени
/// </summary>
public class CharacterController : MonoBehaviour
{
    /// <summary>
    /// Основная информация о персонаже (не изменяется в ходе игры)
    /// </summary>
    public CharactersObject character;

    /// <summary>
    /// Текущий уровень морали фигуры
    /// </summary>
     public float moralityCount;

     public float startMorality;

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

    public GameObject currentEffect;

    public int significanceMultiply; 

    [HideInInspector] public int movesToRemoveAffliction;

    [HideInInspector] public bool isFirstMove;

    public AfflictionsData affliction;
    private void Start() 
    {
        isFirstMove = true;
        
        movesToRemoveAffliction = -1;
        if(character == null)
        {
            InitializeCharacters();
        }
        if(character != null)
        {
            //moralityCount = character.MaxMorality;
            moralityCount = startMorality;
            if(moralityCount > character.MaxMorality)
            {
                moralityCount = character.MaxMorality;
            }
            gameObject.GetComponent<PieceView>().ChangeMoralityBar();
        }
        currentMoveTexture = moveTexture;
        GetSignificance();
    }

    public void GetSignificance()
    {
        if((int)pieceType == 1 ) //pawn
        {
            significanceMultiply = 1;
        }
        else if((int)pieceType > 1 && (int)pieceType < 5) //rook, bishop, knight
        {
            significanceMultiply = 2;
        }
        else
        {
            significanceMultiply = 3;
        }
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
        //this.GetComponent<Animation>().Play();
        return this;
    }

    public CharacterController CanсelSelecteCharacter()
    {
        isSelected = false;
        // Анимация подпрыгивания.
        //this.GetComponent<Animation>().Stop();
        return null;
    }


    /// <summary>
    
    /// </summary>
    /// <returns></returns>
    public Texture2D GetMoveTexture()
    {
        return currentMoveTexture;
    }

    public void OnPieceFirstMoveEnded()
    {
        Texture2D moveTexture = new Texture2D(15,15);
        moveTexture.LoadImage(this.moveTexture.EncodeToPNG());
        int greenCellsCount = 0;
            for(int y = 0; y < moveTexture.height; y++)
            {
                if((greenCellsCount == 1 && isLight) || (greenCellsCount == 3 && !isLight))
                {
                    isFirstMove = false;
                    break;
                }
                for(int x = 0; x < moveTexture.width; x++)
                {
                    if(moveTexture.GetPixel(x, y) == GameModule.instance.MoveColors[1])
                    {
                        
                        greenCellsCount++;
                        if((greenCellsCount == 1 && isLight) || (greenCellsCount == 3 && !isLight))
                        {
                           // Debug.Log(greenCellsCount);
                            moveTexture.SetPixel(x, y, GameModule.instance.MoveColors[0]);
                            break;
                        }
                    }
                    
                }
               
            }
        this.moveTexture = moveTexture;
        currentMoveTexture = moveTexture;
    

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
