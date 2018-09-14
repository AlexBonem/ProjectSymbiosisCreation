using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionSetup : MonoBehaviour {

    [SerializeField] private Collider2D stickmanHead;
    [SerializeField] private Collider2D stickmanLeftTorso;
    [SerializeField] private Collider2D stickmanLeftArm;
    [SerializeField] private Collider2D stickmanLeftForeArm;

    // Use this for initialization
    void Awake () {
        Physics2D.IgnoreCollision(stickmanHead, stickmanLeftForeArm);
        Physics2D.IgnoreCollision(stickmanHead, stickmanLeftArm);
        Physics2D.IgnoreCollision(stickmanLeftTorso, stickmanLeftForeArm);
        Physics2D.IgnoreCollision(stickmanLeftTorso, stickmanHead);
    }
	
	// Update is called once per frame
	void Update () {
	}
}
