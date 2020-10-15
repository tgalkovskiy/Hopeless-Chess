using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CharactersObject", menuName = "ScriptableObjects/AfflictionsData", order = 0)]
public class AfflictionsData : ScriptableObject 
{
    [Header("Основная информация")]

    [SerializeField] private string name;

    [SerializeField] [TextArea(3, 10)] private string discription;

    [SerializeField] private Sprite ico;



    /// <summary>
    /// Имя
    /// </summary>
    /// <returns></returns>
    public string Name 
    {
        get {return name;}
    }

    /// <summary>
    /// Описание
    /// </summary>
    /// <returns></returns>
    public string Disctiption
    {
        get {return discription;}
    }

    public Sprite Ico 
    {
        get {return ico;}
    }
    
}
