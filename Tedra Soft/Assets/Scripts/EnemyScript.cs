using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    private GameManager gameManager;
    private int[] posX;
    private int[] posY;
    private int length = 0;
    private Vector3 initialPos;
    // Use this for initialization
    void Start () {
        gameManager = FindObjectOfType<GameManager>();
        
        posX = new int[4];
        posY = new int[4];
        
        if (gameManager.GetOccupiedTile(4, 6) != 1) {
            posX[length] = 4;
            posY[length] = 6;
            length++;
        }
        if (gameManager.GetOccupiedTile(4, 4) != 1)
        {
            posX[length] = 4;
            posY[length] = 4;
            length++;
        }
        if (gameManager.GetOccupiedTile(3, 5) != 1)
        {
            posX[length] = 3;
            posY[length] = 5;
            length++;
        }
        if (gameManager.GetOccupiedTile(5, 5) != 1)
        {
            posX[length] = 5;
            posY[length] = 5;
            length++;
        }
        initialPos = transform.position;
        StartCoroutine(EnemyMovement(2f));
    }
    private IEnumerator EnemyMovement(float waitTime)
    {
        while (true)
        {
            int x = Random.Range(0, length);
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,  initialPos,1);
            
            yield return new WaitForSeconds(waitTime);
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameManager.getTile(posX[x],posY[x]).transform.position,1);
            yield return new WaitForSeconds(waitTime);
            
        }
       
    }
}
