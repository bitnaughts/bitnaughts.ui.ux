using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotterButton : MonoBehaviour
{
    // Start is called before the first frame update
    PlotterController controller;
    StructureController ship;
    void Awake() {
        controller = GameObject.Find("PlotterOverlay").GetComponent<PlotterController>();
        ship = GameObject.Find("Ship").GetComponent<StructureController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseUp()
    {
        switch (this.name) {
            case "Rotate":
                ship.Rotate90(controller.Selected());
                break;
            case "Ok":
                controller.Deselect();
                break;
            case "Delete":
                ship.Remove(controller.Selected());
                controller.Deselect();
                break;
        }
    }
}
