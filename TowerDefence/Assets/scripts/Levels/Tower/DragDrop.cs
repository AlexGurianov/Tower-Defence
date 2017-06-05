using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour {

    bool beingDragged = false;

    public Camera cam;

    public GameObject IndicatorQuad;
    public Material redMaterial;
    public Material greenMaterial;

    LayerMask placeableMask = ~1;

    bool placeable = true;

    bool one_click = false;
    float timer_for_double_click;

    public float delay = 1;

    // Use this for initialization
    void Start () {
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        GameObject.Find("Cancel Tower Button").GetComponent<Button>().enabled = true;
        GameObject.Find("Cancel Tower Button").GetComponent<Image>().enabled = true;
        GameObject.Find("Cancel Tower Button").GetComponent<Button>().interactable = true;
        GameObject.Find("Cancel Tower Button").GetComponent<Image>().raycastTarget = true;
    }

    // Update is called once per frame
    void Update () {
        if (beingDragged)
        {            
            var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(transform.position).z);

            var curPosition = cam.ScreenToWorldPoint(curScreenSpace);
            curPosition.y = transform.position.y;

            transform.position = curPosition;

            if (Physics.CheckSphere(curPosition, GetComponent<Collider>().bounds.size.x/2, placeableMask))
            {
                if (placeable == true)
                {
                    placeable = false;
                    IndicatorQuad.GetComponent<Renderer>().material = redMaterial;
                }
            }
            else if (placeable == false)
            {
                placeable = true;
                IndicatorQuad.GetComponent<Renderer>().material = greenMaterial;
            }
        }
    }

    private void OnMouseDrag()
    {
        beingDragged = true;
    }

    private void OnMouseUp()
    {
        if (beingDragged && RectTransformUtility.RectangleContainsScreenPoint(
                 GameObject.Find("Cancel Tower Button").GetComponent<RectTransform>(), Input.mousePosition))
        {
            GameObject.Find("Cancel Tower Button").GetComponent<Button>().interactable = false;
            GameObject.Find("Cancel Tower Button").GetComponent<Image>().raycastTarget = false;
            GameObject.Find("Cancel Tower Button").GetComponent<Image>().enabled = false;
            GameObject.Find("Cancel Tower Button").GetComponent<Button>().enabled = false;
            DataStorage.dataStorage.isPlacingTower = false;
            Destroy(gameObject);
        }
        beingDragged = false;
    }

    private void OnMouseDown()
    {
        if (!one_click)
        {
            one_click = true;
            timer_for_double_click = Time.time;
        }
        else
        {
            if (Time.time - timer_for_double_click > delay)
                one_click = false;
            else if (placeable)
            {
                GameObject.Find("Cancel Tower Button").GetComponent<Button>().interactable = false;
                GameObject.Find("Cancel Tower Button").GetComponent<Image>().raycastTarget = false;
                GameObject.Find("Cancel Tower Button").GetComponent<Image>().enabled = false;
                GameObject.Find("Cancel Tower Button").GetComponent<Button>().enabled = false;
                GetComponent<TowerController>().placeTower();
            }
            else
                one_click = false;
        }
    }
}
