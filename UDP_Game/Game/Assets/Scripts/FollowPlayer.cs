using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Vector3 offset;
    public Transform player;
    static private bool firstPerson = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.V))
        {
            firstPerson = !firstPerson;
        }
        if (!firstPerson)
            transform.position = player.position + offset;
        else
            transform.position = player.position;

    }
}
