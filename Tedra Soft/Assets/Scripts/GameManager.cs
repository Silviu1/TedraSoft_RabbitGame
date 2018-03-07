using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gameStatus { next, play, gameOver, win }

public class GameManager : Singleton<MonoBehaviour> {
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject rock;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject cave;
    [SerializeField] private GameObject carrot;
    [SerializeField] private int maxTrees;
    [SerializeField] private int maxRocks;
    [SerializeField] private int maxCaves;
    [SerializeField] private int maxCarrots;
    private Transform[][] tiles;
    private int[][] occupiedTiles;
    private Transform[] caves;
    private int[] cavesX;
    private int[] cavesY;

    public void SetOccupiedTile(int i, int j, int value) {
        occupiedTiles[i][j] = value;
    }

    public int GetOccupiedTile(int i, int j) {
        return occupiedTiles[i][j];
    }

    public Transform getTile(int i, int j) {
        return tiles[i][j];
    }

    public Transform[] getCaves() {
        return caves;
    }
    public int[] getCavesX()
    {
        return cavesX;
    }
    public int[] getCavesY() {
        return cavesY;
    }

    // Use this for initialization
    void Start () {
        tiles = new Transform[8][];
        occupiedTiles = new int[8][];
        caves = new Transform[MaxCaves];
        cavesX = new int[MaxCaves];
        cavesY = new int[MaxCaves];
        for (int i = 0; i < 8; i++) {
            tiles[i] = new Transform[10];
            occupiedTiles[i] = new int[10];
        }
        GetMapTiles();


        //create player
        Instantiate(player, tiles[0][0].transform);
        occupiedTiles[0][0] = 1;
        occupiedTiles[0][1] = 2;

        //create enemy
        Instantiate(enemy, tiles[4][5].transform);
        occupiedTiles[4][5] = 2;

        //create trees
        CreateTrees();


        //create rocks
        CreateRocks();

        //create carrots
        CreateCarrots();

        //create caves
        CreateCaves();
        occupiedTiles[0][0] = 0;
        occupiedTiles[0][1] = 0;
        occupiedTiles[4][5] = 0;
    }

    private void CreateTrees() {
        for (int k = 0; k < maxTrees; k++)
        {
            int i = Random.Range(0, 8);
            int j = Random.Range(0, 10);
            while (occupiedTiles[i][j] != 0)
            {
                i = Random.Range(0, 8);
                j = Random.Range(0, 10);
            }
            Instantiate(tree, tiles[i][j]);
            occupiedTiles[i][j] = 1;
        }
    }

    private void CreateRocks() {
        for (int k = 0; k < maxRocks; k++)
        {
            int i = Random.Range(0, 8);
            int j = Random.Range(0, 10);
            while (occupiedTiles[i][j] != 0)
            {
                i = Random.Range(0, 8);
                j = Random.Range(0, 10);
            }
            Instantiate(rock, tiles[i][j]);
            occupiedTiles[i][j] = 1;
        }
    }
    private void CreateCarrots() {
        for (int k = 0; k < maxCarrots; k++)
        {
            int i = Random.Range(0, 8);
            int j = Random.Range(0, 10);
            while (occupiedTiles[i][j] != 0)
            {
                i = Random.Range(0, 8);
                j = Random.Range(0, 10);
            }
            Instantiate(carrot, tiles[i][j]);
            occupiedTiles[i][j] = 2;
        }
    }

    private int nr = 0;

    public int MaxCaves
    {
        get
        {
            return maxCaves;
        }

        set
        {
            maxCaves = value;
        }
    }

    private void CreateCaves() {
        for (int k = 0; k < MaxCaves; k++)
        {
            int i = Random.Range(0, 8);
            int j = Random.Range(0, 10);
            while (occupiedTiles[i][j] != 0)
            {
                i = Random.Range(0, 8);
                j = Random.Range(0, 10);
            }
            Instantiate(cave, tiles[i][j]);
            caves[nr] = tiles[i][j].transform;
            cavesX[nr] = i;
            cavesY[nr] = j;
            nr++;
            occupiedTiles[i][j] = 2;
        }
    }

    private void GetMapTiles() {
        int i = 0;
        int j = 0;
        int children = map.transform.childCount;
        for (int k = 0; k < children; k++) {
            tiles[i][j] = map.transform.GetChild(k);
            j++;
            if (j == 10) {
                i++;
                j = 0;
            }
        }
    }
}
