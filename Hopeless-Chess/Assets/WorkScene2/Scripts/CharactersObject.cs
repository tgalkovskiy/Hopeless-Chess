using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharactersObject", menuName = "ScriptableObjects/CharactersObject", order = 0)]
public class CharactersObject : ScriptableObject 
{
    [Header("Основная информация")]

    [SerializeField] private string name;

    [SerializeField] private Type chessType;

    [SerializeField] [TextArea(3, 10)] private string discription;

    
    [Header("Характеристики")]

    [SerializeField] private float maxMorality;

    [SerializeField] private float maxAnger;

    [SerializeField] private float maxDevotion;



    /// <summary>
    /// Имя персонажа
    /// </summary>
    /// <returns></returns>
    public string Name 
    {
        get {return name;}
    }

    /// <summary>
    /// Тип фигуры
    /// </summary>
    /// <returns></returns>
    public Type ChessType
    {
        get {return chessType;}
    }

    /// <summary>
    /// Описание персонажа
    /// </summary>
    /// <returns></returns>
    public string Disctiption
    {
        get {return discription;}
    }

    /// <summary>
    /// Мораль персонажа, которая позволяет двигать фигуру (нет морали - персонаж стоит)
    /// </summary>
    /// <value></value>
    public float MaxMorality 
    { 
        get {return maxMorality;}
    }

    /// <summary>
    /// Злость персонажа (если доходит до максимума - включается режим "берсерка")
    /// </summary>
    /// <value></value>
    public float MaxAnger 
    {
        get {return maxAnger;}
    }

    /// <summary>
    /// Максимальный уровень преданности (если доходит до минимума - фигура переходит на сторону противника)
    /// </summary>
    /// <value></value>
    public float MaxDevotion 
    { 
        get {return maxDevotion;}
    }






    public enum Type
    {
        pawn,
        rook,
        knight,
        bishop,
        queen,
        king

    }
}
