using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Grid Sphere
/// </summary>
public class GridSphere : MonoBehaviour {
    public int index;
    private int row;
    private int col;
    private int page;
    private Text display;
    private MyGrid myGrid;
    private GameObject mainGameObject;

    private MeshRenderer meshRenderer;

    public bool isSelected;

    private static Vector3 dragMouseStartPosition; // posição inicial do mouse quando começar a arrastar as esferas
    public static bool isDragMode = false; // drag mode
    public bool isThisSphereSentToDragList = false; // se enviou a lista de esferas selecionadas

    private IEnumerator MyWaitForEndOfFrame() //executa no final do frame
    {
        yield return new WaitForEndOfFrame();

        myGrid.spheresUISelected.Clear();

        for (int i = 0; i < myGrid.spheres.Count; i++) // remove todas as definições de filhos
        {
            if (myGrid.spheres[i].transform.parent) myGrid.spheres[i].transform.SetParent(null);
        }

        print("before spheres: " + myGrid.spheresUISelected.Count);
        for (int i = 0; i < myGrid.spheres.Count; i++)
        {
            if (myGrid.spheresGridSphere[i].isSelected)
            {
                myGrid.spheresUISelected.Add(myGrid.spheresGridSphere[i].transform);

                if (myGrid.spheresUISelected.Count > 0)
                    myGrid.spheresUISelected[myGrid.spheresUISelected.Count - 1].SetParent(myGrid.spheresUISelected[0].transform);
            }
        }
        myGrid.dragMouseStartPosition = dragMouseStartPosition;
        myGrid.isDragMode = true;
        print("after spheres: " + myGrid.spheresUISelected.Count);
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        mainGameObject = GameObject.FindGameObjectWithTag("MainGameObject");
        myGrid = mainGameObject.GetComponent<MyGrid>();
    }

    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl) && isDragMode == false)
        {
            if (isSelected)
            {
                SelectSphere(false); // deseleciona a esfera
                isThisSphereSentToDragList = false; // seta todas as esfera como não enviada para lista
            }
        }
    }

    public void Create(int index, int row, int col, int page, Text display)
    {
        this.index = index;
        this.row = row;
        this.col = col;
        this.page = page;
        this.display = display;

        isSelected = false;
        myGrid.spheres.Add(gameObject);
        myGrid.spheresGridSphere.Add(gameObject.GetComponent<GridSphere>());
    }

    //OnMouaseEvents executa antes do update e lateupdate

    private void OnMouseDown() // seleciona a esfera usando o control
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isSelected = !isSelected;
        }
        else
        {
            isSelected = true;
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                isDragMode = true;
                dragMouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                StartCoroutine(MyWaitForEndOfFrame());
            }
        }
    }

    private void OnMouseEnter()
    {
        if (!Input.GetMouseButton(0)) // quando o botão esquerdo do mouse não estiver sendo apertado
        { 
            if (page == 0)
                display.text = index.ToString() + " is " + row.ToString() + "x" + col.ToString() + " sel: " + isSelected;
            else
                display.text = index.ToString() + " is " + row.ToString() + "x" + col.ToString() + "x" + page.ToString() + " sel: " + isSelected; ;
            display.text += "\n" + gameObject.transform.position.x + "x " + gameObject.transform.position.y + "y " + gameObject.transform.position.z + "z";
            meshRenderer.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        if (!Input.GetMouseButton(0) || isDragMode) // quando o botão esquerdo do mouse não estiver sendo apertado
        { 
            display.text = "";
            if (isSelected) meshRenderer.material.color = Color.green;
            else meshRenderer.material.color = Color.cyan;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Selector")
        {
            if (!Input.GetKey(KeyCode.LeftControl)) // se Control não estiver sendo apertado
            {
                SelectSphere(true);
            }
            else 
            {
                if (Input.GetKey(KeyCode.LeftShift)) // inverte a seleção
                {
                    SelectSphere(false);
                }
                else
                {
                    if (isSelected)
                    {
                        SelectSphere(false);
                    }
                    else
                    {
                        SelectSphere(true);
                    }
                }
            }
        }

        SelectionHighLight(other);
    }

    private void OnTriggerStay(Collider other) // altera a cor das esferas de acordo com o shift e control
    {
        SelectionHighLight(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SelectorHighLight")
        {
            if (isSelected) meshRenderer.material.color = Color.green;
            else meshRenderer.material.color = Color.cyan;
        }
    }

    //refactorization

    private void SelectionHighLight(Collider other)
    {
        if (other.tag == "SelectorHighLight")
        {
            if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
                if (isSelected) meshRenderer.material.color = Color.cyan;
                else meshRenderer.material.color = new Color(0.25f, 1, 0.5f);
            else
                if (Input.GetKey(KeyCode.LeftControl)) meshRenderer.material.color = Color.cyan;
                else
                    if (meshRenderer.material.color != new Color(0.25f, 1, 0.5f)) meshRenderer.material.color = new Color(0.25f, 1, 0.5f);
        }
    }

    private void SelectSphere(bool selection) // seleciona ou deseleciona uma esfera
    {
        isSelected = selection;
        if (selection) meshRenderer.material.color = Color.green;
        else meshRenderer.material.color = Color.cyan;
    }
}