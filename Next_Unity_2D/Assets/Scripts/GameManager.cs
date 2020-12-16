using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private float targetTime = 1.0f;
    private string toolSelection;
    private Text selectionText;
    public TileManager tileManager;

    private void Start() {
        selectionText = GameObject.Find("SelectionText").GetComponent<Text>();
        toolSelection = "toolTilling";
        selectionText.text = "Selection: toolTilling";
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
            selectionText.text = "Selection: toolTilling";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            toolSelection = "toolSeed1";
            selectionText.text = "Selection: toolSeed1";
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
}
