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

    [Space]
    [SerializeField]
    private GameController gameController;


    /// <summary>
    /// 1 - King
    /// 2 - Queen
    /// 3 - Bishop
    /// 4 - Knight
    /// 5 - Rock
    /// 6 - Pawn
    /// </summary>
    private GameObject[] piecePrefabs;
    private GameObject squarePrefabs;

    public GameController GameContol { get { return gameController; } }

    /// <summary>
    /// 1 - King
    /// 2 - Queen
    /// 3 - Bishop
    /// 4 - Knight
    /// 5 - Rock
    /// 6 - Pawn
    /// </summary>
    public GameObject[] PiecePrefabs { get { return piecePrefabs; } }
    public GameObject SquarePrefabs { get { return squarePrefabs; } }

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

        squarePrefabs = Resources.Load<GameObject>("Square");
    }
}
