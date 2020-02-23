using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    // a set of all game objects
    // 0. Heart         1. Born
    // 2. Barrier       3. Wall
    // 4. River         5. Grass
    // 6. AirWall
    public GameObject[] items;

    // store positions that has been created
    private List<Vector3> itemPositionList = new List<Vector3> ();

    private void Awake ()
    {
        // Heart
        CreateItem (items[0], new Vector3 (0, -8, 0), Quaternion.identity);
        // walls that surround heart
        CreateItem (items[3], new Vector3 (1, -8, 0), Quaternion.identity);
        CreateItem (items[3], new Vector3 (-1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem (items[3], new Vector3 (i, -7, 0), Quaternion.identity);
        }
        // AirWalls
        for (int i = -11; i < 12; i++)
        {
            CreateItem (items[6], new Vector3 (i, 9, 0), Quaternion.identity);
            CreateItem (items[6], new Vector3 (i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem (items[6], new Vector3 (-11, i, 0), Quaternion.identity);
            CreateItem (items[6], new Vector3 (11, i, 0), Quaternion.identity);
        }

        // player
        GameObject go = Instantiate (items[1], new Vector3 (-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born> ().createPlayer = true;
        // first enemies
        CreateItem (items[1], new Vector3 (-10, 8, 0), Quaternion.identity);
        CreateItem (items[1], new Vector3 (0, 8, 0), Quaternion.identity);
        CreateItem (items[1], new Vector3 (10, 8, 0), Quaternion.identity);
        // following enemies
        InvokeRepeating ("CreateEnemy", 4, 4);
        // others
        for (int i = 0; i < 20; i++)
        {
            for (int j = 2; j < 6; j++)
            {
                CreateItem (items[j], CreateRandomPosition (), Quaternion.identity);
            }
            CreateItem (items[3], CreateRandomPosition (), Quaternion.identity);
        }
    }

    private void CreateItem (GameObject createGameObject,
        Vector3 createPosition, Quaternion createRotation)
    {
        GameObject itemGo = Instantiate (createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent (gameObject.transform);
        itemPositionList.Add (createPosition);
    }

    private Vector3 CreateRandomPosition ()
    {
        while (true)
        {
            Vector3 createPosition = new Vector3 (
                Random.Range (-9, 10),
                Random.Range (-7, 8), 0);
            if (!positionColides (createPosition))
            {
                return createPosition;
            }

        }
    }
    private bool positionColides (Vector3 createPos)
    {
        for (int i = 0; i < itemPositionList.Count; i++)
        {
            if (createPos == itemPositionList[i])
            {
                return true;
            }
        }
        return false;
    }

    private void CreateEnemy ()
    {
        int num = Random.Range (0, 3);
        Vector3 EnemyPos = new Vector3 ();
        switch (num)
        {
            case 0:
                EnemyPos = new Vector3 (-10, 8, 0);
                break;
            case 1:
                EnemyPos = new Vector3 (0, 8, 0);
                break;
            case 2:
                EnemyPos = new Vector3 (10, 8, 0);
                break;
        }
        CreateItem (items[1], EnemyPos, Quaternion.identity);
    }
}