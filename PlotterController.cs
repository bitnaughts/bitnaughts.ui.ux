
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class PlotterController : MonoBehaviour
{
    public GameObject[] Prefabs;
    GameObject placement_overlay, component_content;
    StructureController ship;

    Text left_title, center_title, right_title;
    public InputField input;
    Text interpreter;

    public string default_content = "\n None... \n\n To add, tap\n plotter grid.";
    public Sprite Overlay, OverlaySelected;

    public Launch launcher;

    string focused_type = "";

    int num_toggles = 10;

    float transition_time = 0f;

    void Start()
    {
        ship = GameObject.Find("Ship").GetComponent<StructureController>();
        placement_overlay = GameObject.Find("PlacementOverlay");
        // input = GameObject.Find("Input").GetComponent<InputField>();
        interpreter = GameObject.Find("ViewContent").GetComponent<Text>();
        left_title = GameObject.Find("LeftTitle").GetComponent<Text>();
    }
    string selected = "";
    ProcessorController selected_processor;
    bool clicked = false;
    string focused = "";
    float recently_unfocused = 0f;
    float click_duration;

    Vector2 selected_size, selected_min_size, pos;

    void Update()
    {
        recently_unfocused -= Time.deltaTime;
        transition_time -= Time.deltaTime;
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);

        if (selected != "") 
        {
            interpreter.text = ship.GetComponentToString(selected);
        }
        else if (focused != "") 
        {
        }
        else {
            placement_overlay.transform.position = pos; 
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            clicked = true;
            click_duration = Time.time;
        }
        if (Input.GetMouseButtonUp(0) && Time.time - click_duration < .5f)
        {
            // Needs generalizing to different screen formats
            if (selected == "" && recently_unfocused < 0 && Input.mousePosition.x > Screen.width / 2)
            {
                if (focused != "" && GetActiveText().Contains(focused_type)) Select(focused);
                else if (GetActiveToggle() != -1)
                {
                    GameObject object_reference = Prefabs[GetActiveToggle()];
                    
                    var component_gameObject = Instantiate(object_reference, pos, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                    //move this logic to structure controller, use IfKeyExists
                    int component_count = 1;
                    while (ship.IsComponent(object_reference.name + component_count)) component_count++;
                    component_gameObject.name = object_reference.name + component_count;
                    component_gameObject.GetComponent<SpriteRenderer>().size = object_reference.GetComponent<ComponentController>().GetMinimumSize();
                    
                    if (focused_type == "Gimbal" && !GetActiveText().Contains(focused_type)) {
                        component_gameObject.transform.SetParent(ship.transform.Find("Rotator").Find(focused).GetChild(0));
                    }
                    else component_gameObject.transform.SetParent(ship.transform.Find("Rotator"));
                    
                    component_gameObject.transform.localPosition = new Vector2(
                        Mathf.Round(component_gameObject.transform.localPosition.x),
                        Mathf.Round(component_gameObject.transform.localPosition.y)
                    );
                    component_gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);

                    
                    // Focus(component_gameObject.name);
                }
            }
        }
    }
    public void Focus(string component, Type type) 
    { 
        focused_type = type.ToString().Replace("Controller", "");
        Focus(component, false);
    }
    public void Focus(string component, bool force_focus)
    {
        if (this.selected == "" || force_focus)
        {
            input.text = component;
            focused = component;
            placement_overlay.transform.position = ship.GetPosition(component);
            placement_overlay.GetComponent<SpriteRenderer>().size = ship.GetSize(component);
            placement_overlay.GetComponent<SpriteRenderer>().sprite = OverlaySelected;
        }
    }
    public void Unfocus()
    {
        if (this.selected == "")
        {
            input.text = "";
            focused = "";
            recently_unfocused = .25f;
            placement_overlay.GetComponent<SpriteRenderer>().sprite = Overlay;
            if (GetActiveToggle() != -1) placement_overlay.GetComponent<SpriteRenderer>().size = Prefabs[GetActiveToggle()].GetComponent<ComponentController>().GetMinimumSize();
        }
    }

    // public void Rename()
    // {
    //     if (transition_time < 0)
    //     {
    //         if (left_title.text == Interpreter.Jump_Label_Text)
    //         {
    //             ship.SetOperand(selected, input.text);
    //             return;
    //         }
    //         if (left_title.text == Interpreter.Set_Text)
    //         {
    //             ship.SetOperand(selected, Interpreter.Set + " " + input.text);
    //             return;
    //         }
    //         GameObject.Find(selected).name = input.text;
    //         selected = input.text;
    //         ship.Start();
    //         Select(selected);
    //         // Will want to update scripts that contain old component references with new component references... TODO
    //     }
    // }

    public int GetActiveToggle()
    {
        return launcher.GetActiveButton();
    }
    public string GetActiveText()
    {
        return launcher.GetActiveText();
    }

    public void Deselect()
    {
        this.selected = "";
        this.focused = "";

        placement_overlay.GetComponent<SpriteRenderer>().sprite = Overlay;
        ship.EnableColliders();
        recently_unfocused = .5f;
        
        launcher.OnSelect("", "");

        foreach (Transform child in transform.Find("PlacementOverlay"))
        {
            child.gameObject.SetActive(false);
        }
    }
    public void Select()
    {
        Select(selected, ship.GetSize(selected), ship.GetMinimumSize(selected));
    }

    public void Select(string focused) 
    {
        Select(focused, ship.GetSize(focused), ship.GetMinimumSize(focused));
    }
    public void Select(string component, Vector2 component_size, Vector2 component_min_size)
    {
        this.selected = component;

        input.text = selected;
        input.interactable = true;
        placement_overlay.transform.position = ship.GetPosition(component);
        placement_overlay.GetComponent<SpriteRenderer>().size = component_size;
        placement_overlay.GetComponent<SpriteRenderer>().sprite = OverlaySelected;
        ship.DisableColliders();
        SetPlacementOverlay(component_size, component_min_size);

        launcher.OnSelect(focused_type, selected);
    }
    public string Selected()
    {
        return selected;
    }

    public void SetPlacementOverlay(Vector2 component_size, Vector2 component_min_size)
    {
        foreach (Transform child in transform.Find("PlacementOverlay"))
        {
            child.gameObject.SetActive(true);
            switch (child.name)
            {
                case "Up":
                    child.localPosition = new Vector2(0, component_size.y / 2);
                    break;
                case "Down":
                    child.localPosition = new Vector2(0, -component_size.y / 2);
                    break;
                case "Right":
                    child.localPosition = new Vector2(component_size.x / 2, 0);
                    break;
                case "Left":
                    child.localPosition = new Vector2(-component_size.x / 2, 0);
                    break;
                case "Rotate":
                    child.localPosition = new Vector2(-component_size.x / 2 - 1, component_size.y / 2 + 1);
                    break;
                case "Ok":
                    child.localPosition = new Vector2(component_size.x / 2 + 1, component_size.y / 2 + 1);
                    break;
                case "Delete":
                    child.localPosition = new Vector2(component_size.x / 2 + 1, -component_size.y / 2 - 1);
                    break;
            }
            foreach (Transform grandchild in child)
            {
                if ((grandchild.name == "Left-" || grandchild.name == "Right-") && component_size.x == component_min_size.x) grandchild.gameObject.SetActive(false);
                else if ((grandchild.name == "Up-" || grandchild.name == "Down-") && component_size.y == component_min_size.y) grandchild.gameObject.SetActive(false);
                else grandchild.gameObject.SetActive(true);
            }

        }
    }
}
