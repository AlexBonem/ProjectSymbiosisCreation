using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Information about the position in the grid
/// </summary>
public class InfoIndexGrid : MonoBehaviour {
    private int index;
    private int row;
    private int col;
    private int page;
    private Text display;

    public void Create(int index, int row, int col, int page, Text display)
    {
        this.index = index;
        this.row = row;
        this.col = col;
        this.page = page;
        this.display = display;
    }

    private void OnMouseEnter()
    {
        if (page == 0)
            display.text = index.ToString() + " is " + row.ToString() + "x" + col.ToString();
        else
            display.text = index.ToString() + " is " + row.ToString() + "x" + col.ToString() + "x" + page.ToString();
        display.text += "\n" + gameObject.transform.position.x + "x " + gameObject.transform.position.y + "y " + gameObject.transform.position.z + "z";
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void OnMouseExit()
    {
        display.text = "";
        gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
    }
}
