using UnityEngine;
using System.Collections;

public class TerrainGeneration : MonoBehaviour


{


    public Transform brick;
    


    void Start()
    {
        for (int y = -60; y < 60; y = y + 2)
        {
            for (int x = -60; x < 60; x = x + 2)
            {
                Transform myBrick = Instantiate(brick, new Vector3(x, y, 10), Quaternion.identity) as Transform;
                myBrick.parent = transform;
            }
        }         
    }

}