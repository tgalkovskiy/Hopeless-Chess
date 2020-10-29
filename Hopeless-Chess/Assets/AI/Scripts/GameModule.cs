using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModule : MonoBehaviour
{
    public static GameModule instance;

    void Awake()
    {
        if (instance) Destroy(gameObject);
        else instance = this;

        GetResources();
    }







    public Camera MainCamera { get { return mainCamera; } }
    [SerializeField]
    Camera mainCamera;


    #region Prefabs

    /// <summary>
    /// 1 - King
    /// 2 - Queen
    /// 3 - Bishop
    /// 4 - Knight
    /// 5 - Rock
    /// 6 - Pawn
    /// </summary>
    public GameObject[] PiecePrefabs { get { return piecePrefabs; } }
    GameObject[] piecePrefabs;

    public GameObject SquarePrefab { get { return squarePrefab; } }
    GameObject squarePrefab;

    public GameObject SelectedPrefab { get { return selectedPrefab; } }
    GameObject selectedPrefab;

    #endregion



    #region Color

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
    Color[] moveColors;

    #endregion



    public List<Material> Materials { get { return materials; } }
    List<Material> materials;


    public Texture2D EmptyMoveTexture { get { return emptyMoveTexture; } }
    Texture2D emptyMoveTexture;
    public GameObject BoardTexture { get { return boardTexture; } }
    GameObject boardTexture;



    #region PieceData
    public CharactersObject[] PawnsData { get; set; }
    public CharactersObject[] RooksData { get; set; }
    public CharactersObject[] BishopsData { get; set; }
    public CharactersObject[] KnightData { get; set; }
    public CharactersObject[] QueenData { get; set; }
    public CharactersObject[] KingData { get; set; }

    #endregion



    public EffectsController Effects { get { return effectsController; } }
    EffectsController effectsController;


    public Afflictions Afflictions { get { return afflictions; } }
    Afflictions afflictions;


    #region BoradSetups

    public List<int[][]> BoardSetups { get { return boardSetups; } }
    List<int[][]> boardSetups = new List<int[][]>() {

    /// <summary>
	/// 1 - King
	/// 2 - Queen
	/// 3 - Bishop
	/// 4 - Knight
	/// 5 - Rock
	/// 6 - Pawn
	/// </summary>

        // Setup 0
        new int[8][] {
            new int [8] {  105, 104, 103, 101, 102, 113, 114, 115 },
            new int [8] {  106, 116, 126, 136, 146, 156, 166, 176 },
            new int [8] {    0,   0,   0,   0,   0,   0,   0,   0 },
            new int [8] {    0,   0,   0,   0,   0,   0,   0,   0 },
            new int [8] {    0,   0,   0,   0,   0,   0,   0,   0 },
            new int [8] {    0,   0,   0,   0,   0,   0,   0,   0 },
            new int [8] {  906, 916, 926, 936, 946, 956, 966, 976 },
            new int [8] {  905, 904, 903, 901, 902, 913, 914, 915 }

        },

        // Setup 1
        new int[8][] {
            new int [8] {  105,   0, 103, 101, 102, 113, 114, 115 },
            new int [8] {  106, 116, 126,   0, 146, 156, 166, 176 },
            new int [8] {  104,   0,   0, 136,   0,   0,   0,   0 },
            new int [8] {    0,   0,   0,   0,   0,   0,   0,   0 },
            new int [8] {    0, 916,   0,   0,   0,   0,   0,   0 },
            new int [8] {    0,   0, 926,   0,   0,   0,   0,   0 },
            new int [8] {  906,   0,   0, 936, 946, 956, 966, 976 },
            new int [8] {  905, 904, 903, 901, 902, 913, 914, 915 }
        },

        // Setup 2
        new int[][] {
            new int[] {  105,   0,   0 },
            new int[] { 104, 0, 103 },
            new int[] { 0, 903, 0 }
        },

        // Setup 3
        new int[][] {
            new int [] { 101,   0,   0,   0,   0},
            new int [] {   0,   0, 901,   0, 905},
            new int [] {   0,   0,   0,   0,   0},
            new int [] {   0,   0,   0,   0,   0},
        }
        
        //,

        //// Setup 4
        //new int[][] {
        //    new int [] {  105, 104, 103, 101 },
        //    new int [] {  106, 116, 126, 136 },
        //    new int [] {    0,   0,   0,   0 },
        //    new int [] {    0,   0,   0,   0 },
        //    new int [] {    0,   0,   0,   0 },
        //    new int [] {    0,   0,   0,   0 },
        //    new int [] {  906, 916, 926, 936 },
        //    new int [] {  905, 904, 903, 901 }
        //}

    };

    #endregion


    public void GetResources()
    {
		#region prefabs

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

		#endregion

        moveColors = new Color[] { none, move, attack, moveAndAttack, jump };

        materials = new List<Material>();
        materials.AddRange( Resources.LoadAll<Material>("Materials/Text"));

        emptyMoveTexture = Resources.Load<Texture2D>("Textures/Empty");
        boardTexture = Resources.Load<GameObject>("Board");

		#region pieceData

		PawnsData = Resources.LoadAll<CharactersObject>("Characters/Pawns");
        RooksData = Resources.LoadAll<CharactersObject>("Characters/Rooks");
        BishopsData = Resources.LoadAll<CharactersObject>("Characters/Bishops");
        KnightData = Resources.LoadAll<CharactersObject>("Characters/Knights");
        QueenData = Resources.LoadAll<CharactersObject>("Characters/Queens");
        KingData = Resources.LoadAll<CharactersObject>("Characters/Kings");

		#endregion

        effectsController = Resources.Load<GameObject>("EffectController").GetComponent<EffectsController>();

        afflictions = GameObject.FindGameObjectWithTag("AfflictionController").GetComponent<Afflictions>();
    }
}
