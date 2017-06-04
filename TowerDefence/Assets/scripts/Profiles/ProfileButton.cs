using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileButton : MonoBehaviour {

    public Button button;
    public Text ProfileName;

    public Image LeftFrame;
    public Image RightFrame;
    public Image UpFrame;
    public Image DownFrame;

    public Transform CheckPanel;

    void Start()
    {
        StartCoroutine(ChangeSize());
    }

    IEnumerator ChangeSize()
    {
        yield return null;
        GetComponent<RectTransform>().sizeDelta = new Vector2(0.6f * GetComponent<RectTransform>().sizeDelta.y, GetComponent<RectTransform>().sizeDelta.y);
    }

    public void Select()
    {
        Color tempColor = LeftFrame.color;
        tempColor.a = 1f;
        LeftFrame.color = tempColor;
        RightFrame.color = tempColor;
        UpFrame.color = tempColor;
        DownFrame.color = tempColor;
    }

    public void Deselect()
    {
        Color tempColor = LeftFrame.color;
        tempColor.a = 0f;
        LeftFrame.color = tempColor;
        RightFrame.color = tempColor;
        UpFrame.color = tempColor;
        DownFrame.color = tempColor;
    }

    public void SetProfile()
    {
        CheckPanel.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void UnsetProfile()
    {
        CheckPanel.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

}
