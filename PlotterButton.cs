using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotterButton : MonoBehaviour
{
    // Start is called before the first frame update
    PlotterController controller;
    StructureController ship;
    void Awake()
    {
        controller = GameObject.Find("PlotterOverlay").GetComponent<PlotterController>();
        ship = GameObject.Find("Ship").GetComponent<StructureController>();
    }
    void Start()
    {

    }

    void OnMouseUp()
    {
        if (controller.Selected() == "")
        {
            controller.Deselect();
        }
        switch (this.name)
        {
            /* Shift */
            case "Up^":
                ship.Move(controller.Selected(), new Vector2(0, 1));
                controller.Select();
                break;
            case "Down^":
                ship.Move(controller.Selected(), new Vector2(0, -1));
                controller.Select();
                break;
            case "Right^":
                ship.Move(controller.Selected(), new Vector2(1, 0));
                controller.Select();
                break;
            case "Left^":
                ship.Move(controller.Selected(), new Vector2(-1, 0));
                controller.Select();
                break;
            /* Upsize */
            case "Up+":
                ship.Upsize(controller.Selected(), new Vector2(0, 1));
                controller.Select();
                break;
            case "Down+":
                ship.Upsize(controller.Selected(), new Vector2(0, -1));
                controller.Select();
                break;
            case "Right+":
                ship.Upsize(controller.Selected(), new Vector2(1, 0));
                controller.Select();
                break;
            case "Left+":
                ship.Upsize(controller.Selected(), new Vector2(-1, 0));
                controller.Select();
                break;
            /* Downsize */
            case "Up-":
                ship.Downsize(controller.Selected(), new Vector2(0, 1));
                controller.Select();
                break;
            case "Down-":
                ship.Downsize(controller.Selected(), new Vector2(0, -1));
                controller.Select();
                break;
            case "Right-":
                ship.Downsize(controller.Selected(), new Vector2(1, 0));
                controller.Select();
                break;
            case "Left-":
                ship.Downsize(controller.Selected(), new Vector2(-1, 0));
                controller.Select();
                break;
            case "Rotate":
                ship.Rotate90(controller.Selected());
                controller.Select();
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
