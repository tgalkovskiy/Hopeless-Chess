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
    GameObject boardTexture;
    EffectsController effectsController;
    List<Material> materials;

    Afflictions afflictions;


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

    public List<Material> Materials { get { return materials; } }


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

    public GameObject BoardTexture { get { return boardTexture; } }

    public EffectsController Effects { get {return effectsController; } }

    public Animation Rise { get; set; } 

    public Afflictions Afflictions {get {return afflictions;}}

    public void GetResources()
    {
        piecePrefabs = new GameObject[17];
        //piecePrefabs[1] = Resources.Load<GameObject>("PiecePrefabs/KingLight");
        //piecePrefabs[2] = Resources.Load<GameObject>("PiecePrefabs/QueenLight");
        //piecePrefabs[3] = Resources.Load<GameObject>("PiecePrefabs/BishopLight");
        //piecePrefabs[4] = Resources.Load<GameObject>("PiecePrefabs/KnightLight");
        //piecePrefabs[5] = Resources.Load<GameObject>("PiecePrefabs/RookLight");
        //piecePrefabs[6] = Resources.Load<GameObject>("PiecePrefabs/PawnLight");
        //piecePrefabs[11] = Resources.Load<GameObject>("PiecePrefabs/KingDark");
        //piecePrefabs[12] = Resources.Load<GameObject>("PiecePrefabs/QueenDark");
        //piecePrefabs[13] = Resources.Load<GameObject>("PiecePrefabs/BishopDark");
        //piecePrefabs[14] = Resources.Load<GameObject>("PiecePrefabs/KnightDark");
        //piecePrefabs[15] = Resources.Load<GameObject>("PiecePrefabs/RookDark");
        //piecePrefabs[16] = Resources.Load<GameObject>("PiecePrefabs/PawnDark");

        piecePrefabs[1] = Resources.Load<GameObject>("PiecePrefabs2/KingLight");
        piecePrefabs[2] = Resources.Load<GameObject>("PiecePrefabs2/QueenLight");
        piecePrefabs[3] = Resources.Load<GameObject>("PiecePrefabs2/BishopLight");
        piecePrefabs[4] = Resources.Load<GameObject>("PiecePrefabs2/KnightLight");
        piecePrefabs[5] = Resources.Load<GameObject>("PiecePrefabs2/RookLight");
        piecePrefabs[6] = Resources.Load<GameObject>("PiecePrefabs2/PawnLight");
        piecePrefabs[11] = Resources.Load<GameObject>("PiecePrefabs2/KingDark");
        piecePrefabs[12] = Resources.Load<GameObject>("PiecePrefabs2/QueenDark");
        piecePrefabs[13] = Resources.Load<GameObject>("PiecePrefabs2/BishopDark");
        piecePrefabs[14] = Resources.Load<GameObject>("PiecePrefabs2/KnightDark");
        piecePrefabs[15] = Resources.Load<GameObject>("PiecePrefabs2/RookDark");
        piecePrefabs[16] = Resources.Load<GameObject>("PiecePrefabs2/PawnDark");

        squarePrefab = Resources.Load<GameObject>("Square");
        selectedPrefab = Resources.Load<GameObject>("Selected");

        moveColors = new Color[] { none, move, attack, moveAndAttack, jump };

        emptyMoveTexture = Resources.Load<Texture2D>("Textures/Empty");
        boardTexture = Resources.Load<GameObject>("Board");

        PawnsData = Resources.LoadAll<CharactersObject>("Characters/Pawns");
        RooksData = Resources.LoadAll<CharactersObject>("Characters/Rooks");
        BishopsData = Resources.LoadAll<CharactersObject>("Characters/Bishops");
        KnightData = Resources.LoadAll<CharactersObject>("Characters/Knights");
        QueenData = Resources.LoadAll<CharactersObject>("Characters/Queens");
        KingData = Resources.LoadAll<CharactersObject>("Characters/Kings");
        effectsController = Resources.Load<GameObject>("EffectController").GetComponent<EffectsController>();

        materials = new List<Material>();
        materials.AddRange( Resources.LoadAll<Material>("Materials/Text"));

        Rise = Resources.Load<Animation>("Animation/RiseText");

        afflictions = GameObject.FindGameObjectWithTag("AfflictionController").GetComponent<Afflictions>();
    }
}
