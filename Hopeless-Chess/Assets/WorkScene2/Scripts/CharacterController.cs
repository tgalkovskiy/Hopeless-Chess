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

    /// <summary>
    /// Проверка, мёртв ли персонаж
    /// </summary>
    public bool isDead;

    /// <summary>
    /// Проверяет, выбрана ли сейчас эта фигура
    /// </summary>
    public bool isSelected;

    public GameObject[] cellsAvalible;

    private bool isFirstMove;


    private void Start() 
    {
        if(character != null)
        {
            morality = character.MaxMorality;
            anger = character.MaxAnger;
            devotion = character.MaxDevotion;
        }
        isFirstMove = true;
        

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


    /// <summary>
    /// Обрабатывает перемещение фигуры по доске
    /// </summary>
    /// <param name="cellCoords"></param>
    public void MoveCharacter(Vector3 cellCoords)
    {
        transform.position = cellCoords;
        if(isFirstMove && (int)character.ChessType == (int)ChessType.pawn)
        {
            isFirstMove = false;
            GameObject cellsAfterFirstMove = cellsAvalible[0];
            cellsAvalible[1].SetActive(false);
            cellsAvalible = new GameObject[1] {cellsAfterFirstMove};
        }
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
