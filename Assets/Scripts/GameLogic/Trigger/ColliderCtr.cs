using UnityEngine;
using System.Collections;

public class ColliderCtr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D collider2d)
    {
        Debug.Log(collider2d.gameObject.name);
    }
    void OnCollisionEnter2D(Collision2D collision2d)
    {
        Debug.Log(collision2d.gameObject.name);
    }
}
