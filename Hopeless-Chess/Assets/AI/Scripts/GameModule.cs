using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModule : MonoBehaviour
{
    public static GameModule instance;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        GetResources();

    }


    GameObject[] piecePrefabs;
    GameObject squarePrefab;
    GameObject selectedPrefab;

    [SerializeField]
    Camera mainCamera;

    [Space]
    [SerializeField]
    Color none;
    [SerializeField]
    Color move;
    [SerializeField]
    Color attack;
    [SerializeField]
    Color moveAndAttack;
    [SerializeField]
    Color jump;
    [SerializeField]
    Color jumpAndMove;
    [SerializeField]
    Color jumpAndAttack;

    Color[] moveColors;
    Texture2D emptyMoveTexture;

    /// <summary>
    /// 1 - King
    /// 2 - Queen
    /// 3 - Bishop
    /// 4 - Knight
    /// 5 - Rock
    /// 6 - Pawn
    /// </summary>
    public GameObject[] PiecePrefabs { get { return piecePrefabs; } }

    public GameObject SquarePrefab { get { return squarePrefab; } }

    public GameObject SelectedPrefab { get { return selectedPrefab; } }

    public Camera MainCamera { get { return mainCamera; } }


    /// <summary>
    /// 0 - none; 
    /// 1 - move; 
    /// 2 - attack; 
    /// 3 - move and attack; 
    /// 4 - jump and move and attack; 
    /// 5 - junp and move; 
    /// 6 - jump and attack; 
    /// </summary>
    public Color[] MoveColors { get { return moveColors; } }
    public Texture2D EmptyMoveTexture { get { return emptyMoveTexture; } }

    public CharactersObject[] PawnsData {get; set;}
    public CharactersObject[] RooksData {get; set;}
    public CharactersObject[] BishopsData {get; set;}
    public CharactersObject[] KnightData {get; set;}
    public CharactersObject[] QueenData {get; set;}
    public CharactersObject[] KingData {get; set;}

    

    public void GetResources()
    {
        piecePrefabs = new GameObject[17];
        piecePrefabs[1] = Resources.Load<GameObject>("PiecePrefabs/KingLight");
        piecePrefabs[2] = Resources.Load<GameObject>("PiecePrefabs/QueenLight");
        piecePrefabs[3] = Resources.Load<GameObject>("PiecePrefabs/BishopLight");
        piecePrefabs[4] = Resources.Load<GameObject>("PiecePrefabs/KnightLight");
        piecePrefabs[5] = Resources.Load<GameObject>("PiecePrefabs/RookLight");
        piecePrefabs[6] = Resources.Load<GameObject>("PiecePrefabs/PawnLight");
        piecePrefabs[11] = Resources.Load<GameObject>("PiecePrefabs/KingDark");
        piecePrefabs[12] = Resources.Load<GameObject>("PiecePrefabs/QueenDark");
        piecePrefabs[13] = Resources.Load<GameObject>("PiecePrefabs/BishopDark");
        piecePrefabs[14] = Resources.Load<GameObject>("PiecePrefabs/KnightDark");
        piecePrefabs[15] = Resources.Load<GameObject>("PiecePrefabs/RookDark");
        piecePrefabs[16] = Resources.Load<GameObject>("PiecePrefabs/PawnDark");

        squarePrefab = Resources.Load<GameObject>("Square");
        selectedPrefab = Resources.Load<GameObject>("Selected");

        moveColors = new Color[] { none, move, attack, moveAndAttack, jump };

        emptyMoveTexture = Resources.Load<Texture2D>("Textures/Empty");

        PawnsData = Resources.LoadAll<CharactersObject>("Characters/Pawns");
        RooksData = Resources.LoadAll<CharactersObject>("Characters/Rooks");
        BishopsData = Resources.LoadAll<CharactersObject>("Characters/Bishops");
        KnightData = Resources.LoadAll<CharactersObject>("Characters/Knights");
        QueenData = Resources.LoadAll<CharactersObject>("Characters/Queens");
        KingData = Resources.LoadAll<CharactersObject>("Characters/Kings");
        
        

    }
}
