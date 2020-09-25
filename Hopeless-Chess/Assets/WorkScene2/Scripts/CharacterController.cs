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

    public Texture2D moveTexture;


    private void Start() 
    {
        if(character != null)
        {
            morality = character.MaxMorality;
            anger = character.MaxAnger;
            devotion = character.MaxDevotion;
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
    /// Возвращает текстуру хода персонажа (Синий(0;0;255) - ход, если нет препядствий;
    /// белый (0;0;0) - первый ход за игру;
    /// зелёный (0;255;0) - безпрепятственный ход
    /// красный (255;0;0) - ход только в случае атаки)
    /// </summary>
    /// <returns></returns>
    public Texture2D GetMoveTexture()
    {
        return moveTexture;
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
