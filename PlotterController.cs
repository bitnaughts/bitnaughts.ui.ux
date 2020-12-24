using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlotterController : MonoBehaviour
{
    public GameObject[] Prefabs;
    GameObject placement_overlay, component_content;
    ToggleGroup toggle_group;
    Toggle[] toggles;
    Text[] toggle_labels;

    StructureController ship;

    Text left_title, center_title, right_title;

    InputField component_namer, interpreter;

    CameraController camera_controller;

    public string default_content = "\n None... \n\n To add, tap\n plotter grid.";
    public Sprite Overlay, OverlaySelected;

    const string Components = "Components",
        Syntax = "Syntax",
        Arithmetic = "Arithmetic",
        Flow_Control = "Flow Control",
        Boolean = "Boolean",
        Trigonometry = "Trigonometry",
        Interactivity = "Interactivity";



    string[] components_menu = { ">\tProcessor", ">\tGirder", ">\tGrid", ">\tBulkhead", ">\tPrinter", ">\tGimbal", ">\tEngine", ">\tThruster", ">\tCannon", ">\tSensor" },
        syntax_init_menu = { "^\tScroll Up", "+\tAdd Above", ">\t" + Arithmetic, ">\t" + Flow_Control, ">\t" + Boolean, ">\t" + Trigonometry, ">\t" + Interactivity, "-\tDelete", "+\tAdd Below", "v\tScroll Down" },
        syntax_arthimetic_menu = { ">\tSet", ">\tAdd", ">\tSubtract", ">\tAbsolute", ">\tMultiply", ">\tDivide", ">\tModulo", ">\tExponential", ">\tRoot", "-\tBack" },
        syntax_flow_control_menu = { ">\tJump Label", ">\tJump", ">\tJump if equal", ">\tJump if not equal", ">\tJump if greater", ">\tJump if less", ">\t...", ">\t...", ">\t...", "-\tBack" },
        syntax_boolean_menu = { ">\tSet", ">\tAnd", ">\tOr", ">\tNot", ">\tNand", ">\tNor", ">\tXnor", ">\t...", ">\t...", "-\tBack" },
        syntax_trigonometry_menu = { ">\tSine", ">\tCosine", ">\tTangent", ">\tSecant", ">\tCosecant", ">\tCotangent", ">\tArcsine", ">\tArccosine", ">\tArctangent", "-\tBack" },
        syntax_interactivity_menu = { ">\tFunction", ">\tPlot", ">\tPrint", ">\tPing", ">\tParse", ">\t...", ">\t...", ">\t...", ">\t...", "-\tBack" };


    int num_toggles = 10;

    float transition_time = 0f;

    void Start()
    {
        ship = GameObject.Find("Ship").GetComponent<StructureController>();
        placement_overlay = GameObject.Find("PlacementOverlay");
        toggle_group = GameObject.Find("Left").GetComponent<ToggleGroup>();
        component_namer = GameObject.Find("RightInput").GetComponent<InputField>();
        interpreter = GameObject.Find("RightView").GetComponent<InputField>();

        left_title = GameObject.Find("LeftTitle").GetComponent<Text>();

        camera_controller = GameObject.Find("Main Camera").GetComponent<CameraController>();

        component_content = GameObject.Find("LeftViewContent");
        component_content.GetComponent<Text>().text = default_content;

        toggle_labels = new Text[num_toggles];
        toggles = new Toggle[num_toggles];
        for (int i = 0; i < num_toggles; i++)
        {
            toggles[i] = GameObject.Find(i + "").GetComponent<Toggle>();
            toggle_labels[i] = GameObject.Find(i + "Label").GetComponent<Text>();
        }

        Deselect();
    }
    string selected = "";
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

        if (focused == "") placement_overlay.transform.position = pos;

        if (Input.GetMouseButtonDown(0))
        {
            clicked = true;
            click_duration = Time.time;
        }
        if (Input.GetMouseButtonUp(0) && Time.time - click_duration < .5f)
        {
            if (selected != "" && Input.mousePosition.x > 3 * Screen.width / 4 && Input.mousePosition.y < 9 * Screen.height / 10)
            {
                SwitchLeftView(Syntax);
            }
            if (selected == "" && recently_unfocused < 0 && Input.mousePosition.x > Screen.width / 4 && Input.mousePosition.x < 3 * Screen.width / 4)
            {
                if (focused != "") Select(focused);
                else 
                {
                    foreach (var toggle in toggle_group.ActiveToggles())
                    {
                        GameObject object_reference = GetObject(toggle.name);
                        
                        var component_gameObject = Instantiate(object_reference, pos, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                        int component_count = 1;
                        while (ship.transform.Find("Rotator").Find(object_reference.name + component_count) != null) component_count++;
                        component_gameObject.name = object_reference.name + component_count;
                        component_gameObject.GetComponent<SpriteRenderer>().size = object_reference.GetComponent<ComponentController>().GetMinimumSize();
                        component_gameObject.transform.SetParent(ship.transform.Find("Rotator"));
                        Focus(component_gameObject.name);
                    }
                }
            }
        }
    }
    public void Focus(string component) { Focus(component, false); }
    public void Focus(string component, bool force_focus)
    {
        if (this.selected == "" || force_focus)
        {
            component_namer.text = component;
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
            component_namer.text = "";
            focused = "";
            recently_unfocused = .25f;
            placement_overlay.GetComponent<SpriteRenderer>().sprite = Overlay;

            foreach (var toggle in toggle_group.ActiveToggles())
            {
                placement_overlay.GetComponent<SpriteRenderer>().size = GetObject(toggle.name).GetComponent<ComponentController>().GetMinimumSize();
            }
        }
    }

    public void Rename()
    {
        print(selected);
        print(component_namer.text);
        GameObject.Find(selected).name = component_namer.text;
        selected = component_namer.text;
        ship.Start();
        Select();
    }

    public void SwitchLeftView(string title)
    {
        left_title.text = title;
        transition_time = 1f;
        switch (title)
        {
            case Syntax: 
                SetActiveToggles(syntax_init_menu); break;
            case Components: 
                SetActiveToggles(components_menu); break;
            case Arithmetic: 
                SetActiveToggles(syntax_arthimetic_menu); break;
            case Flow_Control: 
                SetActiveToggles(syntax_flow_control_menu); break;
            case Boolean: 
                SetActiveToggles(syntax_boolean_menu); break;
            case Trigonometry: 
                SetActiveToggles(syntax_trigonometry_menu); break;
            case Interactivity: 
                SetActiveToggles(syntax_interactivity_menu); break;
            default:
                //Interpreter operand selection...

                break;
        }
    }

    public void SetActiveToggles(string[] toggle_labels_text)
    {
        for (int i = 0; i < num_toggles; i++)
        {
            toggles[i].isOn = false;
            toggle_labels[i].text = toggle_labels_text[i];
        }
    }

    public int GetActiveToggle()
    {
        foreach (var toggle in toggle_group.ActiveToggles())
        {
            return int.Parse(toggle.name);
        }
        return 0;
    }
    public void ToggleSwitched()
    {
        if (transition_time < 0) switch (left_title.text)
        {
            case Components:
                Deselect(); break; 
            case Syntax:
                switch (GetActiveToggle())
                {
                    case 0:
                        print("up");
                        SwitchLeftView(Syntax);
                        break;
                    case 1:
                        print("add up");
                        SwitchLeftView(Syntax);
                        break;
                    case 7:
                        print("delete");
                        SwitchLeftView(Syntax);
                        break;
                    case 8:
                        print("add down");
                        SwitchLeftView(Syntax);
                        break;
                    case 9:
                        print("down");
                        SwitchLeftView(Syntax);
                        break;
                    default:
                        SwitchLeftView(toggle_labels[GetActiveToggle()].text);
                        break;
                }
                break;
            default:
                switch (GetActiveToggle())
                {
                    case 9:
                        SwitchLeftView(Syntax);
                        break;
                    default:
                        SwitchLeftView(toggle_labels[GetActiveToggle()].text);
                    break;
                }
                break;
        }
    }

    public void Deselect()
    {
        this.selected = "";
        this.focused = "";

        component_namer.text = selected;
        component_namer.interactable = false;
        interpreter.interactable = false;
        component_content.GetComponent<Text>().text = "\n Description of current component...\n Description of current component...\n Description of current component...";
        placement_overlay.GetComponent<SpriteRenderer>().sprite = Overlay;
        ship.EnableColliders();

        recently_unfocused = .5f;

        SwitchLeftView(Components);
        foreach (var toggle in toggle_group.ActiveToggles())
        {
            placement_overlay.GetComponent<SpriteRenderer>().size = GetObject(toggle.name).GetComponent<ComponentController>().GetMinimumSize();
        }
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

        component_namer.text = selected;
        component_namer.interactable = true;
        interpreter.interactable = true;
        component_content.GetComponent<Text>().text = ship.ToString();
        component_content.GetComponent<RectTransform>().sizeDelta = new Vector2(component_content.GetComponent<RectTransform>().sizeDelta.x, component_content.GetComponent<Text>().text.Split('\n').Length * 26);
        placement_overlay.transform.position = ship.GetPosition(component);
        placement_overlay.GetComponent<SpriteRenderer>().size = component_size;
        placement_overlay.GetComponent<SpriteRenderer>().sprite = OverlaySelected;
        ship.DisableColliders();

        SetPlacementOverlay(component_size, component_min_size);

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
    private GameObject GetObject(string component)
    {
        return Prefabs[int.Parse(component)];
    }

    public void Print()
    {
    }
}
