using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsController : MonoBehaviour {

    public Text CoinsText;

    public static Button tower1Button;
    public static Button tower2Button;
    public static Button tower3Button;
    public static Button wallButton;
    public static Button treeButton;
    public static Button tombButton;

    Dictionary<TowerType, Button> towerButtons;

    // Use this for initialization
    void Awake () {
        tower1Button = GameObject.Find("New Tower1 Button").GetComponent<Button>();
        tower2Button = GameObject.Find("New Tower2 Button").GetComponent<Button>();
        tower3Button = GameObject.Find("New Tower3 Button").GetComponent<Button>();
        wallButton = GameObject.Find("New Wall Button").GetComponent<Button>();
        treeButton = GameObject.Find("New Tree Button").GetComponent<Button>();
        tombButton = GameObject.Find("New Tomb Button").GetComponent<Button>();

        towerButtons = new Dictionary<TowerType, Button>
        {
            { TowerType.tower1, tower1Button }, { TowerType.tower2, tower2Button }, { TowerType.tower3, tower3Button }, { TowerType.wall_down, wallButton }, { TowerType.wall_up, wallButton },
            { TowerType.wall_left, wallButton }, { TowerType.wall_right, wallButton }, { TowerType.tree, treeButton }, { TowerType.tomb, tombButton }
        };
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /*Dictionary<TowerType, Button> towerButtons = new Dictionary<TowerType, Button>
        {
            { TowerType.tower1, tower1Button }, { TowerType.tower2, tower2Button }, { TowerType.tower3, tower3Button }, { TowerType.wall_down, wallButton }, { TowerType.wall_up, wallButton },
            { TowerType.wall_left, wallButton }, { TowerType.wall_right, wallButton }, { TowerType.tree, treeButton }, { TowerType.tomb, tombButton }
        };*/

    Dictionary<TowerType, int> towerCosts = new Dictionary<TowerType, int>
        {
            { TowerType.tower1, 100 }, { TowerType.tower2, 80 }, { TowerType.tower3, 70 }, { TowerType.wall_down, 75 }, { TowerType.wall_up, 75 },
            { TowerType.wall_left, 75 }, { TowerType.wall_right, 75 }, { TowerType.tree, 60 }, { TowerType.tomb, 50 }
        };

    public void AddCoinsForMonster()
    {
        DataStorage.dataStorage.coins += 10;
        UpdateCoinsText();
    }

    public void SubtractCoinsForTower(TowerType towerType)
    {
        DataStorage.dataStorage.coins -= towerCosts[towerType];
        UpdateCoinsText();
    }

    public void RefundCoinsForTower(TowerType towerType)
    {
        DataStorage.dataStorage.coins += Mathf.FloorToInt(towerCosts[towerType] * 0.5f);
        UpdateCoinsText();
    }

    public void UpdateCoinsText()
    {
        CoinsText.text = DataStorage.dataStorage.coins.ToString();
        foreach (KeyValuePair<TowerType, Button> pair in towerButtons)
        {
            if(towerCosts[pair.Key] <= DataStorage.dataStorage.coins)
                pair.Value.enabled = true;
            else
                pair.Value.enabled = false;
        }
    }
}
