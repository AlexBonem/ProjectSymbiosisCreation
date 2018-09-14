using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGrid : MonoBehaviour {

    private MyGrid myGrid;
    public Text display;
    //private MyGrid myGrid00;

	// Use this for initialization
	void Start () {
        //MyGrid myGrid;
        this.gameObject.AddComponent<MyGrid>();
        myGrid = this.gameObject.GetComponent<MyGrid>();
        //this.gameObject.AddComponent<MyGrid>();
        //myGrid00 = this.gameObject.GetComponents<MyGrid>()[1];
        //myGrid00.Create(2, 4, 1, 1, new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y -0.2f, 0));
        //myGrid00.Create(3, 3, 1, 1, new Vector3(-5, 0, 0));

        myGrid.Create(4, 6, 1, 1, Vector3.zero, display);

        //myGrid.DislocateRows(3, 1, 0.4f);
        //myGrid.DislocateRows(0, 1, -0.4f);
        myGrid.DislocateCols(0, 2, -0.4f);
        myGrid.DislocateCols(4, 2, 0.4f);

        myGrid.ShowIndex();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
