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
