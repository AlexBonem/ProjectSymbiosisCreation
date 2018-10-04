using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyMouse : MonoBehaviour
{
    [SerializeField]
    private MyGrid myGrid;
    private Rect rect;
    private GameObject planeUI;
    private GameObject planeSelector;
    private float distanceMoved;
    private float distanceMovedMin;
    private Vector2 startPosition;

    private bool doStartSelection;
    private bool doSelection;

    private MeshRenderer planeUIMeshRenderer;

    // Use this for initialization
    void Awake()
    {
        myGrid = gameObject.GetComponent<MyGrid>();
        distanceMoved = 0f;
        distanceMovedMin = 0.1f;
        doStartSelection = false;
        doSelection = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // acontece quando o botão esquerdo do mouse é apertado (1 frame apenas, e acontece ao mesmo tempo que o if de baixo)
        {
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) // acontece quando segurar o botão esquerdo do mouse
        {
            if (GridSphere.isDragMode == false) distanceMoved = Vector2.Distance(startPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (distanceMoved > distanceMovedMin && doSelection == false) doStartSelection = true; // permita começar o método de seleção Selection
        }

        // fim da seleção
        if (Input.GetMouseButtonUp(0)) // reseta a distancia movida quando soltar o botão esquerdo do mouse
        {
            //print(distanceMoved);
            distanceMoved = 0;
            doStartSelection = false;
            if (doSelection)
            { 
                rect.position += new Vector2(rect.width / 2, rect.height / 2); //configuration, make rect position starts at mouse position from the edges not from the center
                myGrid.SelectIndexes(rect); // seleciona os índices da grid que estão dentro da area da seleção
                Destroy(planeUI); //removes the selection object
                Destroy(planeSelector); //removes the selector
            }
            doSelection = false;
        }

        if (doStartSelection) StartSelection();
        if (doSelection) Selection();
    }

    private void StartSelection()
    {
        // cria box de seleção
        rect = new Rect(startPosition.x, startPosition.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).x - startPosition.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - startPosition.y); //AXB last 2
        //print(rect);

        //cria o um retangulo na tela
        planeUI = GameObject.CreatePrimitive(PrimitiveType.Quad);
        planeUIMeshRenderer = planeUI.GetComponent<MeshRenderer>();
        planeUIMeshRenderer.material = new Material(Shader.Find("Standard"))
        {
            color = Color.red - new Color(0, 0, 0, 0.8f)
        };
        //validation size
        if (rect.width == 0) rect.width = 0.01f;
        if (rect.height == 0) rect.height = 0.01f;

        planeUI.transform.localScale = new Vector3(rect.width, rect.height, 1);
        planeUI.transform.localPosition = new Vector3(rect.x + rect.width / 2, rect.y + rect.height / 2, -1);
        //AXB Start, Gambiara para setar o rendering mode para transaparente
        planeUIMeshRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        planeUIMeshRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        planeUIMeshRenderer.material.SetInt("_ZWrite", 0);
        planeUIMeshRenderer.material.DisableKeyword("_ALPHATEST_ON");
        planeUIMeshRenderer.material.DisableKeyword("_ALPHABLEND_ON");
        planeUIMeshRenderer.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        planeUIMeshRenderer.material.renderQueue = 3000;
        //AXB End

        planeSelector = GameObject.CreatePrimitive(PrimitiveType.Quad);
        planeSelector.tag = "SelectorHighLight";
        planeSelector.GetComponent<MeshRenderer>().enabled = false; // makes the object invisible

        doSelection = true;
    }

    private void Selection()
    {
        //redimencionar box de seleção até que soltar o botão
        rect.width = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - startPosition.x;
        rect.height = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - startPosition.y - 0.03f; //0.03f is the offset
        //validation size
        if (rect.width == 0) rect.width = 0.01f;
        if (rect.height == 0) rect.height = 0.01f;
        planeUI.transform.localScale = new Vector3(rect.width, rect.height, 1);
        planeUI.transform.localPosition = new Vector3(rect.x + rect.width / 2, rect.y + rect.height / 2, -1);
        //planeSelector é usado porque não consegui fazer o planeUI ficar na frente das esferas e colidir com elas ao mesmo tempo;

        planeSelector.transform.localScale = planeUI.transform.localScale;
        planeSelector.transform.localPosition = new Vector3(planeUI.transform.localPosition.x, planeUI.transform.localPosition.y, 0);
        
        doStartSelection = false;
    }
}
