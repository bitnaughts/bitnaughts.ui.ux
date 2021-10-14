using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotterButton : MonoBehaviour
{
    // Start is called before the first frame update
    PlotterController controller;
    void Awake()
    {
        controller = GameObject.Find("PlotterOverlay").GetComponent<PlotterController>();
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
                controller.Ship.Move(controller.Selected(), new Vector2(0, 1));
                controller.Select();
                break;
            case "Down^":
                controller.Ship.Move(controller.Selected(), new Vector2(0, -1));
                controller.Select();
                break;
            case "Right^":
                controller.Ship.Move(controller.Selected(), new Vector2(1, 0));
                controller.Select();
                break;
            case "Left^":
                controller.Ship.Move(controller.Selected(), new Vector2(-1, 0));
                controller.Select();
                break;
            /* Upsize */
            case "Up+":
                controller.Ship.Upsize(controller.Selected(), new Vector2(0, 1));
                controller.Select();
                break;
            case "Down+":
                controller.Ship.Upsize(controller.Selected(), new Vector2(0, -1));
                controller.Select();
                break;
            case "Right+":
                controller.Ship.Upsize(controller.Selected(), new Vector2(1, 0));
                controller.Select();
                break;
            case "Left+":
                controller.Ship.Upsize(controller.Selected(), new Vector2(-1, 0));
                controller.Select();
                break;
            /* Downsize */
            case "Up-":
                controller.Ship.Downsize(controller.Selected(), new Vector2(0, 1));
                controller.Select();
                break;
            case "Down-":
                controller.Ship.Downsize(controller.Selected(), new Vector2(0, -1));
                controller.Select();
                break;
            case "Right-":
                controller.Ship.Downsize(controller.Selected(), new Vector2(1, 0));
                controller.Select();
                break;
            case "Left-":
                controller.Ship.Downsize(controller.Selected(), new Vector2(-1, 0));
                controller.Select();
                break;
            case "Rotate":
                controller.Ship.Rotate90(controller.Selected());
                controller.Select();
                break;
            case "Ok":
                controller.Deselect();
                break;
            case "Delete":
                controller.Ship.Remove(controller.Selected());
                controller.Deselect();
                break;
        }
    }
}
