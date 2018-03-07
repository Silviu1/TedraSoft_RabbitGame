using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float speed = 1.5f;

    private GameManager gameManager;
    private Vector3 target;
    private int rabbitPosX = 0;
    private int rabbitPosY = 0;
    private float timp;
    private Transform[] cavesSpawned;
    private int[] cavesSpawnedX;
    private int[] cavesSpawnedY;
    private bool sw = false;
    private int tpSw = 0;
    void Start()
    {
        target = transform.position;
        timp = 1 / speed;
        gameManager = FindObjectOfType<GameManager>();
        cavesSpawned = gameManager.getCaves();
        cavesSpawnedX = gameManager.getCavesX();
        cavesSpawnedY = gameManager.getCavesY();
    }

    void Update()
    {
        if (gameManager.getTile(rabbitPosX,rabbitPosY).position==gameObject.transform.position)
        {
            if (tpSw == 2)
            {
                tpSw = 3;
            }
            sw = false;
            if (Input.GetMouseButtonDown(0))
            {
                sw = true;
                //movement right
                if ((Mathf.Abs(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x) >= Mathf.Abs(transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y)) && (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x) > 0 && rabbitPosY < 9)
                {

                    if (gameManager.GetOccupiedTile(rabbitPosX, rabbitPosY + 1) != 1)
                    {
                        rabbitPosY++;
                        target.x = gameManager.getTile(rabbitPosX, rabbitPosY).position.x;
                        target.y = transform.position.y;
                        target.z = transform.position.z;
                    }
                }
                //movement down
                else if (Mathf.Abs(transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y) >= Mathf.Abs(transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x) && (transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y) > 0 && rabbitPosX < 7)
                {
                    if (gameManager.GetOccupiedTile(rabbitPosX + 1, rabbitPosY) != 1)
                    {
                        rabbitPosX++;
                        target.x = transform.position.x;
                        target.y = gameManager.getTile(rabbitPosX, rabbitPosY).position.y;
                        target.z = transform.position.z;
                    }
                }
                //movement left
                else if (Mathf.Abs(transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x) >= Mathf.Abs(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y) && (transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x) > 0 && rabbitPosY > 0)
                {
                    if (gameManager.GetOccupiedTile(rabbitPosX, rabbitPosY - 1) != 1)
                    {
                        rabbitPosY--;
                        target.x = gameManager.getTile(rabbitPosX, rabbitPosY).position.x;
                        target.y = transform.position.y;
                        target.z = transform.position.z;
                    }
                }
                //movement up
                else if (gameManager.GetOccupiedTile(rabbitPosX - 1, rabbitPosY) != 1 && rabbitPosX > 0)
                {
                    rabbitPosX--;
                    target.x = transform.position.x;
                    target.y = gameManager.getTile(rabbitPosX, rabbitPosY).position.y;
                    target.z = transform.position.z;
                }
                if (tpSw == 1) {
                    tpSw = 2;
                }
            }
        }
        else
        {  
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        if (tpSw == 3) {
            tpSw = 0;
        }      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "cave"&&tpSw==0)
        {
            tpSw = 1;
            
            sw = false;
            int i = Random.Range(0, gameManager.MaxCaves);
            while (collision.transform.position == cavesSpawned[i].transform.position)
            {
                i = Random.Range(0, gameManager.MaxCaves);
            }
            target.x = cavesSpawned[i].transform.position.x;
            target.y = cavesSpawned[i].transform.position.y;
            target.z = cavesSpawned[i].transform.position.z;
            gameObject.transform.position = cavesSpawned[i].transform.position;
            
            rabbitPosX = cavesSpawnedX[i];
            rabbitPosY = cavesSpawnedY[i];
        }
        else if (collision.gameObject.tag == "enemy") {
            Destroy(gameObject);
        }
    }
}
