using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButtonsController : MonoBehaviour {

    public Camera cam;

    public PauseMenuController pauseMenuController;

    public GameObject deleteTowerButton;

    public GameObject tower1;

    bool towerSelectionStays = false;

    public int selectedTowerID = -1;

    // Use this for initialization
    void Start () {
        deleteTowerButton.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (selectedTowerID != -1 && Input.GetMouseButton(0) && !towerSelectionStays && !RectTransformUtility.RectangleContainsScreenPoint(
                 deleteTowerButton.GetComponent<RectTransform>(), Input.mousePosition))
        {
            DeselectTower();
        }
    }

    public void PauseButtonClicked()
    {
        pauseMenuController.CallPauseMenu();
    }

    public void AddTower1ButtonClicked()
    {
        Instantiate(tower1, new Vector3(0f, 0f, 0f), Quaternion.Euler(-90, 0, 0));
    }

    public void DeleteTowerButtonClicked()
    {
        if (selectedTowerID != -1)
        {
            //Destroy(DataStorage.dataStorage.towersDictionary[selectedTowerID].gameObject);
            DataStorage.dataStorage.towersDictionary[selectedTowerID].DeleteTower();
            //DataStorage.dataStorage.towersDictionary.Remove(selectedTowerID);
            selectedTowerID = -1;
            deleteTowerButton.SetActive(false);
        }
    }

    public void DeselectTower()
    {
        selectedTowerID = -1;
        deleteTowerButton.SetActive(false);
    } 

    public IEnumerator SelectTower(int TowerID)
    {
        if (TowerID != selectedTowerID)
        {
            towerSelectionStays = false;
            DeselectTower();
            deleteTowerButton.SetActive(true);
            Vector3 towerPos = DataStorage.dataStorage.towersDictionary[TowerID].transform.position;
            towerPos.x -= DataStorage.dataStorage.towersDictionary[TowerID].GetComponent<Collider>().bounds.size.x * 0.2f;
            towerPos.y += DataStorage.dataStorage.towersDictionary[TowerID].GetComponent<Collider>().bounds.size.y;
            Vector3 crossPos = cam.WorldToScreenPoint(towerPos);
            deleteTowerButton.transform.position = crossPos;
            while (Input.GetMouseButton(0))
                yield return null;
            selectedTowerID = TowerID;
        }
        else
            towerSelectionStays = true;
    }
}
