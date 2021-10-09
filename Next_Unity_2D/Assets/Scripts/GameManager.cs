using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour {

    private float targetTime = 1.0f;
    private string toolSelection;
    private Image selectionBar;
    public TileManager tileManager;

    public TileBase pinkSeed;
    public TileBase blueSeed;
    public TileBase yellowSeed;
    TileBase selectedSeed;

    //Toolbar Sprites
    public Sprite selectionHoe;
    public Sprite selectionPinkSeed;
    public Sprite selectionYellowSeed;
    public Sprite selectionBlueSeed;

    private void Start() {
        selectionBar = GameObject.Find("SelectionBar").GetComponent<Image>();
        toolSelection = "toolTilling";
        selectionBar.sprite = selectionHoe;
    }

    // Start is called before the first frame update
    void Update() {
        targetTime -= Time.deltaTime;

        if(targetTime <= 0.0f) {
            timerEnded();
        }

        checkSelection();
    }

    //Runs ever second
    void timerEnded() {
        tileManager.timerUpdate();
        targetTime = 1.0f;
    }

    //Changes what the user has selected
    void checkSelection() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            toolSelection = "toolTilling";
            selectionBar.sprite = selectionHoe;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            toolSelection = "toolSeed1";
            selectionBar.sprite = selectionBlueSeed;
            selectedSeed = blueSeed;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            toolSelection = "toolSeed1";
            selectionBar.sprite = selectionYellowSeed;
            selectedSeed = yellowSeed;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            toolSelection = "toolSeed1";
            selectionBar.sprite = selectionPinkSeed;
            selectedSeed = pinkSeed;
        }
    }

    public bool canTill() {
        if(toolSelection == "toolTilling") {
            return true;
        }
        else {
            return false;
        }
    }

    public bool canSeed() {
        if (toolSelection == "toolSeed1") {
            return true;
        }
        else {
            return false;
        }
    }

    public TileBase getSelectedSeed() {
        return selectedSeed;
    }
}
