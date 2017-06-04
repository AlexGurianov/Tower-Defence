using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct TowerAtLevel
{
    public float damage_coef;
    public int nextUpdateCost;
    public string featureText;
    public string valueText;
    public Material material;

    public TowerAtLevel(float d_c, int nUC, string fT, string vT, Material mt)
    {
        damage_coef = d_c;
        nextUpdateCost = nUC;
        featureText = fT;
        valueText = vT;
        material = mt;
    }
}

public class UpgradeController : MonoBehaviour {

    public Dictionary<TowerType, List<TowerAtLevel>> towersLevelInfo;

    public Text nextGenText;
    public Text featureText;
    public Text valueText;
    public Text costText;

    public Material Material1;
    public Material Material2;
    public Material Material3;

    // Use this for initialization
    void Awake () {
        towersLevelInfo = new Dictionary<TowerType, List<TowerAtLevel>> {
            { TowerType.tower1, new List<TowerAtLevel> { new TowerAtLevel(1f, 40, "damage", "X 1.2", Material1), new TowerAtLevel(1.2f, 50, "damage", "X 1.3", Material2), new TowerAtLevel(1.2f*1.3f, -1, "", "", Material3) } },
            { TowerType.tower2, new List<TowerAtLevel> { new TowerAtLevel(1f, 30, "damage", "X 1.5", Material1), new TowerAtLevel(1.5f, 40, "damage", "X 1.6", Material2), new TowerAtLevel(1.5f*1.6f, -1, "", "", Material3) } },
            { TowerType.tower3, new List<TowerAtLevel> { new TowerAtLevel(1f, 20, "damage", "X 2", Material1), new TowerAtLevel(2f, 30, "damage", "X 2.1", Material2), new TowerAtLevel(2f*2.1f, -1, "", "", Material3) } }
        };

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateUpgradeInfo(TowerType towerType, int level)
    {
        nextGenText.text = (level + 1).ToString();
        featureText.text = towersLevelInfo[towerType][level - 1].featureText;
        valueText.text = towersLevelInfo[towerType][level - 1].valueText;
        costText.text = towersLevelInfo[towerType][level - 1].nextUpdateCost.ToString();
    }
}
