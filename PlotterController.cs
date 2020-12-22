using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlotterController : MonoBehaviour
{
    public GameObject Processor, Beam, Grid, Engine, Thruster, Gimbal, Hardpoint, Cannon, Sensor, Printer, Bulkhead;

    GameObject placement_overlay;
    ToggleGroup component_toggle_group;

    StructureController ship;
    InputField component_namer;

    public Sprite Overlay, OverlaySelected;


    void Start()
    {
        ship = GameObject.Find("Ship").GetComponent<StructureController>();
        placement_overlay = GameObject.Find("PlacementOverlay");
        component_toggle_group = GameObject.Find("ComponentsBody").GetComponent<ToggleGroup>();
        component_namer = GameObject.Find("ComponentNamer").GetComponent<InputField>();
        Deselect();
    }
    string selected = "";
    bool clicked = false;
    string focused = "";
    float click_duration;
    void Update()
    {
        if (selected == "" && Input.mousePosition.x > Screen.width / 4 && Input.mousePosition.x < 3 * Screen.width / 4)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.x = Mathf.Round(pos.x);
            pos.y = Mathf.Round(pos.y);

            foreach (var component in component_toggle_group.ActiveToggles())
            {
                Vector2 min_size = GetMinimumSize(component.name);
                
                if (focused == "") placement_overlay.GetComponent<SpriteRenderer>().size = min_size;

                if (min_size.x % 2 == 1) pos.x += .5f;
                if (min_size.y % 2 == 1) pos.y += .5f;

                if (Input.GetMouseButtonDown(0))
                {
                    clicked = true;
                    click_duration = Time.time;
                }
                if (Input.GetMouseButtonUp(0) && Time.time - click_duration < .25f)
                {
                    if (focused != "") Select(focused);
                    else
                    {
                        bool valid = true;
                        Rect new_component = new Rect(pos.x, pos.y, min_size.x, min_size.y);
                        foreach (var ship_component in ship.components.Values)
                        {
                            if (new_component.Overlaps(ship_component.rectangle))
                            {
                                valid = false;
                            }
                        }
                        if (valid)
                        {
                            var component_gameObject = Instantiate(GetObject(component.name), pos, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                            int component_count = 1;
                            while (ship.transform.Find("Rotator").Find(component.name + component_count) != null) component_count++;
                            component_gameObject.name = component.name + component_count;
                            component_gameObject.GetComponent<SpriteRenderer>().size = min_size;
                            component_gameObject.transform.SetParent(ship.transform.Find("Rotator"));
                            component_gameObject.GetComponent<ComponentController>().rectangle = new_component;

                            // selected = component_gameObject.name;
                            Select(component_gameObject.name);
                        }
                    }
                }
            }


            if (focused == "") placement_overlay.transform.position = pos;
        }
    }

    public void Focus(string component, Vector2 position, Vector2 size)
    {
        if (this.selected == "")
        {
            
            component_namer.text = component;
            focused = component;
            placement_overlay.transform.position = position;
            placement_overlay.GetComponent<SpriteRenderer>().size = size;
            placement_overlay.GetComponent<SpriteRenderer>().sprite = OverlaySelected;
        }
    }
    public void Unfocus()
    {
        if (this.selected == "")
        {
            component_namer.text = selected;
            focused = "";
            placement_overlay.GetComponent<SpriteRenderer>().sprite = Overlay;
        }
    }

    public void Rename() 
    {
        print(selected);
        print(component_namer.text);
        GameObject.Find(selected).name = component_namer.text;
        selected = component_namer.text;
        ship.Start();
    }

    public void Deselect()
    {
        this.selected = "";
        this.focused = "";

        component_namer.text = selected;
        component_namer.interactable = false;
        placement_overlay.GetComponent<SpriteRenderer>().sprite = Overlay;
        ship.EnableColliders();
        foreach (Transform child in transform.Find("PlacementOverlay"))
        {
            child.gameObject.SetActive(false);
        }
    }
    public void Select(string component)
    {
        this.selected = component;

        component_namer.text = selected;
        component_namer.interactable = true;
        placement_overlay.GetComponent<SpriteRenderer>().sprite = OverlaySelected;
        ship.DisableColliders();

        foreach (Transform child in transform.Find("PlacementOverlay"))
        {
            child.gameObject.SetActive(true);
        }
    }
    public string Selected()
    {
        return selected;
    }
    private Vector2 GetMinimumSize(string component)
    {
        switch (component)
        {
            case "Processor":
                return new Vector2(4, 4);
            case "Engine":
                return new Vector2(5, 2);
            case "Printer":
                return new Vector2(3, 3);
            case "Bulkhead":
                return new Vector2(2, 6);
            case "Beam":
            case "Grid":
            case "Thruster":
            case "Gimbal":
            case "Hardpoint":
            case "Cannon":
            case "Sensor":
            default:
                return new Vector2(2, 2);
        }
    }
    private GameObject GetObject(string component)
    {
        switch (component)
        {
            case "Processor":
                return Processor;
            case "Beam":
                return Beam;
            case "Grid":
                return Grid;
            case "Engine":
                return Engine;
            case "Thruster":
                return Thruster;
            case "Gimbal":
                return Gimbal;
            case "Hardpoint":
                return Hardpoint;
            case "Cannon":
                return Cannon;
            case "Sensor":
                return Sensor;
            case "Printer":
                return Printer;
            case "Bulkhead":
                return Bulkhead;
            default:
                return null;

        }
    }

}
