using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {

    public bool selected = false;
    int team = 0;


	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (selected) 
        {
            transform.Find("Plane").GetComponent<MeshRenderer>();
            enabled = true;
        }
        else 
        {
            transform.Find("Plane").GetComponent <MeshRenderer>();
            enabled = false;
        }
	}
}
