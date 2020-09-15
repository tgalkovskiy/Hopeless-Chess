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
    [HideInInspector] public float morality;

    /// <summary>
    /// Текущий уровень злости (временно)
    /// </summary>
    [HideInInspector] public float anger;

    /// <summary>
    /// Текущий уровень преданности (временно)
    /// </summary>
    [HideInInspector] public float devotion;

    public Vector3Int moveModel;

    public Vector3Int attackModel;

    /// <summary>
    /// Проверка, мёртв ли персонаж
    /// </summary>
    public bool isDead;

    /// <summary>
    /// Проверяет, выбрана ли сейчас эта фигура
    /// </summary>
    public bool isSelected;


    private void Start() 
    {
        if(character != null)
        {
            morality = character.MaxMorality;
            anger = character.MaxAnger;
            devotion = character.MaxDevotion;
            moveModel = InitializeMoveModel();
        }

    }

    

    /// <summary>
    /// Обрабатывает отказ фигуры ходить в определённом направлении
    /// </summary>
    public void GivingUp()
    {
        if(morality == 0f)
        {
            //...
        }
    }


    /// <summary>
    /// Обрабатывает режим ярости фигуры
    /// </summary>
    public void RageMode()
    {
        if(anger == 0)
        {
            //...
        }
    }

    /// <summary>
    /// Обрабатывает предательство фигуры
    /// </summary>
    public void Treason()
    {
        if(devotion == 0)
        {
            //...
        }
    }

    public Vector3Int InitializeMoveModel()
    {
        if((int)character.ChessType == (int)ChessType.pawn)
        {
            return new Vector3Int(0, 1, 0);
        }
        else if((int)character.ChessType == (int)ChessType.rook)
        {
            return new Vector3Int(8, 8, 0);
        }
        else if((int)character.ChessType == (int)ChessType.knight)
        {
            return new Vector3Int(8, 8, 0);
        }
        else if((int)character.ChessType == (int)ChessType.bishop)
        {
            return new Vector3Int(0, 0, 8);
        }
        else if((int)character.ChessType == (int)ChessType.queen)
        {
            return new Vector3Int(8, 8, 8);
        }
        else if((int)character.ChessType == (int)ChessType.king)
        {
            return new Vector3Int(1, 1, 1);
        }
        else return Vector3Int.zero;
    }

    /// <summary>
    /// Обрабатывает перемещение фигуры по доске
    /// </summary>
    /// <param name="cellCoords"></param>
    public void MoveCharacter(Vector3 cellCoords)
    {
        transform.position = cellCoords;
    }

    private enum ChessType
    {
        pawn,
        rook,
        knight,
        bishop,
        queen,
        king
    }
}
