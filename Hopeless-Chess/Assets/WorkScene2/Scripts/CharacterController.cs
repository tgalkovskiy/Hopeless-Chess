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


    private void Start() 
    {
        morality = character.MaxMorality;
        anger = character.MaxAnger;
        devotion = character.MaxDevotion;
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
}
