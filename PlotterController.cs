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

    InputField right_input;
    Text interpreter;

    CameraController camera_controller;

    public string default_content = "\n None... \n\n To add, tap\n plotter grid.";
    public Sprite Overlay, OverlaySelected;

    const string Components = "Components",
        Processor = "Processor", Girder = "Girder", Grid = "Grid", Bulkhead = "Bulkhead", Printer = "Printer", Gimbal = "Gimbal", Engine = "Engine", Thruster = "Thruster", Cannon = "Cannon", Sensor = "Sensor",
        Syntax = "Syntax",
        Arithmetic = "Arithmetic",
        Flow_Control = "Flow Control",
        Boolean = "Boolean",
        Trigonometry = "Trigonometry",
        Interactivity = "Interactivity",

        Scroll_Up = "Scroll Up", Scroll_Down = "Scroll Down", Add_Above = "Add Above", Add_Below = "Add Below", Back = "Back", Delete = "Delete",
        JumpLabel = "Jump Label", Jump = "Jump", Jump_If_Equal = "Jump If Equal", Jump_If_Not_Equal = "Jump If Not Equal", Jump_If_Greater = "Jump If Greater", Jump_If_Less = "Jump If Less",  
        Add = "Add", Subtract = "Subtract", Absolute = "Absolute", Multiply = "Multiply", Divide = "Divide", Modulo = "Modulo", Exponential = "Exponential", Root = "Root", 
        Sine = "Sine", Cosine = "Cosine", Tangent = "Tangent", Secant = "Secant", Cosecant = "Cosecant", Cotangent = "Cotangent", Arcsine = "Arcsine", Arccosine = "Arccosine", Arctangent = "Arctangent",
        Function = "Component", Plot = "Plot", Print = "Print", Ping = "Ping", Parse = "Parse", 
        Set = "Set", And = "And", Or = "Or", Not = "Not", Nand = "Nand", Nor = "Nor", Xnor = "Xnor", 
        UpCarat = "^\t", DownCarat = "v\t", MinusCarat = "-\t", PlusCarat = "+\t", Carat = ">\t";



    string[] components_menu = {
            Carat + Processor, Carat + Girder, Carat + Grid, Carat + Bulkhead, Carat + Printer, Carat + Gimbal, Carat + Engine, Carat + Thruster, Carat + Cannon, Carat + Sensor },
        syntax_init_menu = { 
            UpCarat + Scroll_Up, PlusCarat + Add_Above, Carat + Arithmetic, Carat + Flow_Control, Carat + Boolean, Carat + Trigonometry, Carat + Interactivity, MinusCarat + Delete, PlusCarat + Add_Below, DownCarat + Scroll_Down },
        syntax_arthimetic_menu = { 
            Carat + Set, Carat + Add, Carat + Subtract, Carat + Absolute, Carat + Multiply, Carat + Divide, Carat + Modulo, Carat + Exponential, Carat + Root, MinusCarat + Back },
        syntax_flow_control_menu = { 
            Carat + JumpLabel, Carat + Jump, Carat + Jump_If_Equal, Carat + Jump_If_Not_Equal, Carat + Jump_If_Greater, Carat + Jump_If_Less, Carat, Carat, Carat, MinusCarat + Back },
        syntax_boolean_menu = { 
            Carat + And, Carat + Or, Carat + Not, Carat + Nand, Carat + Nor, Carat + Xnor, Carat, Carat, Carat, MinusCarat + Back },
        syntax_trigonometry_menu = { 
            Carat + Sine, Carat + Cosine, Carat + Tangent, Carat + Secant, Carat + Cosecant, Carat + Cotangent, Carat + Arcsine, Carat + Arccosine, Carat + Arctangent, MinusCarat + Back },
        syntax_interactivity_menu = { 
            Carat + Function, Carat + Plot, Carat + Print, Carat + Ping, Carat + Parse, Carat, Carat, Carat, Carat, MinusCarat + Back };


    int num_toggles = 10;

    float transition_time = 0f;

    void Start()
    {
        toggle_labels = new Text[num_toggles];
        toggles = new Toggle[num_toggles];
        for (int i = 0; i < num_toggles; i++)
        {
            toggles[i] = GameObject.Find(i + "").GetComponent<Toggle>();
            toggle_labels[i] = GameObject.Find(i + "Label").GetComponent<Text>();
        }
        ship = GameObject.Find("Ship").GetComponent<StructureController>();
        placement_overlay = GameObject.Find("PlacementOverlay");
        toggle_group = GameObject.Find("Left").GetComponent<ToggleGroup>();
        right_input = GameObject.Find("RightInput").GetComponent<InputField>();
        interpreter = GameObject.Find("RightViewContent").GetComponent<Text>();
        left_title = GameObject.Find("LeftTitle").GetComponent<Text>();
        camera_controller = GameObject.Find("Main Camera").GetComponent<CameraController>();
        component_content = GameObject.Find("LeftViewContent");
        component_content.GetComponent<Text>().text = default_content;
        
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

        if (focused == "") { placement_overlay.transform.position = pos; 
            interpreter.text = "";
            interpreter.GetComponent<RectTransform>().sizeDelta = new Vector2(interpreter.GetComponent<RectTransform>().sizeDelta.x, interpreter.text.Split('\n').Length * 26);

        }
        if (focused != "") 
        {
            interpreter.text = ship.GetComponentToString(focused);
            interpreter.GetComponent<RectTransform>().sizeDelta = new Vector2(interpreter.GetComponent<RectTransform>().sizeDelta.x, interpreter.text.Split('\n').Length * 26);
        }

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
                toggles[GetActiveToggle()].isOn = false;
            }
            if (selected == "" && recently_unfocused < 0 && Input.mousePosition.x > Screen.width / 4 && Input.mousePosition.x < 3 * Screen.width / 4)
            {
                if (focused != "") Select(focused);
                else 
                {
                    GameObject object_reference = Prefabs[GetActiveToggle()];
                    
                    var component_gameObject = Instantiate(object_reference, pos, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                    //move this logic to structure controller, use IfKeyExists
                    int component_count = 1;
                    while (ship.transform.Find("Rotator").Find(object_reference.name + component_count) != null) component_count++;
                    component_gameObject.name = object_reference.name + component_count;
                    component_gameObject.GetComponent<SpriteRenderer>().size = object_reference.GetComponent<ComponentController>().GetMinimumSize();
                    component_gameObject.transform.SetParent(ship.transform.Find("Rotator"));
                    
                    // Focus(component_gameObject.name);
                }
            }
        }
    }
    public void Focus(string component) 
    { 
        Focus(component, false);
    }
    public void Focus(string component, bool force_focus)
    {
        if (this.selected == "" || force_focus)
        {
            right_input.text = component;
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
            right_input.text = "";
            focused = "";
            recently_unfocused = .25f;
            placement_overlay.GetComponent<SpriteRenderer>().sprite = Overlay;
            placement_overlay.GetComponent<SpriteRenderer>().size = Prefabs[GetActiveToggle()].GetComponent<ComponentController>().GetMinimumSize();
        }
    }

    public void Rename()
    {
        if (left_title.text == Interpreter.Jump_Label_Text)
        {
            ship.SetOperand(selected, right_input.text);
            return;
        }
        if (left_title.text == Interpreter.Set_Text)
        {
            ship.SetOperand(selected, Interpreter.Set + " " + right_input.text);
            return;
        }
        GameObject.Find(selected).name = right_input.text;
        selected = right_input.text;
        ship.Start();
        Select();
    }
    public string GetTitleRaw(string title_in)
    {
        if (title_in.StartsWith("<b>")) title_in = title_in.Remove(0, 3);
        if (title_in.EndsWith("</b>")) title_in = title_in.Remove(title_in.Length - 4, 4); 
        if (title_in.Contains("\t")) title_in = title_in.Split('\t')[1]; 
        return title_in;
    }
    public void SwitchLeftView(string title)
    {
        transition_time = .1f;
        left_title.text = GetTitleRaw(title);
        component_content.GetComponent<Text>().text = Interpreter.GetTextDescription(left_title.text);
        right_input.text = selected;
        switch (left_title.text)
        {
            case Components: 
                component_content.GetComponent<Text>().text = Prefabs[GetActiveToggle()].GetComponent<ComponentController>().GetDescription();
                SetActiveToggles(components_menu); break;
            case Syntax:
                SetActiveToggles(syntax_init_menu); break;
            case Arithmetic: 
                SetActiveToggles(syntax_arthimetic_menu); break;
            case Flow_Control: 
                SetActiveToggles(syntax_flow_control_menu); break;
            case Interpreter.Jump_Label_Text:
                SetActiveToggles(syntax_flow_control_menu);
                string label = ship.GetNextLabel(selected);
                ship.SetOperand(selected, label);
                right_input.text = label;
                break;
            case Interpreter.Set_Text:
                SetActiveToggles(syntax_arthimetic_menu);
                string variable = ship.GetNextVariable(selected);
                ship.SetOperand(selected, Interpreter.Set + " " + variable);
                right_input.text = variable;
                break;
            case Boolean: 
                SetActiveToggles(syntax_boolean_menu); break;
            case Trigonometry: 
                SetActiveToggles(syntax_trigonometry_menu); break;
            case Interactivity: 
                SetActiveToggles(syntax_interactivity_menu); break;
            default:
                // component_content.GetComponent<Text>().text = Interpreter.GetTextDescription(left_title.text);
                ship.SetOperand(selected, Interpreter.GetTextCode(left_title.text)); break;

        }
    }

    public void SetActiveToggles(string[] toggle_labels_text)
    {
        string category = ship.GetEditInstructionCategory(selected), text = ship.GetEditInstructionText(selected);
        for (int i = 0; i < num_toggles; i++)
        {
            toggle_labels[i].text = toggle_labels_text[i];
            if (category != "" && text != "" && (toggle_labels[i].text.EndsWith(category) || toggle_labels[i].text.EndsWith(text)))
            {
                toggle_labels[i].text = "<b>" + toggle_labels[i].text + "</b>";
            }
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
        int active = GetActiveToggle();
        if (transition_time < 0) switch (left_title.text)
        {
            case Components:
                Deselect(); break; 
            case Syntax:
                
                transition_time = .1f;
                toggles[active].isOn = false;
                switch (active)
                {
                    case 0:
                        ship.Scroll(selected, -1);
                        SwitchLeftView(Syntax);
                        break;
                    case 1:
                        ship.AddLine(selected, 0);
                        SwitchLeftView(Syntax);
                        break;
                    case 7:
                        ship.DeleteLine(selected);
                        SwitchLeftView(Syntax);
                        break;
                    case 8:
                        ship.AddLine(selected, 1);
                        SwitchLeftView(Syntax);
                        break;
                    case 9:
                        ship.Scroll(selected, 1);
                        SwitchLeftView(Syntax);
                        break;
                    default:
                        SwitchLeftView(toggle_labels[active].text);
                        break;
                }
                break;
            default:
                transition_time = .1f;
                toggles[active].isOn = false;
                switch (active)
                {
                    case 9:
                        SwitchLeftView(Syntax);
                        break;
                    default:
                        SwitchLeftView(toggle_labels[active].text);
                    break;
                }
                break;
        }
    }

    public void Deselect()
    {
        this.selected = "";
        this.focused = "";

        right_input.text = selected;
        right_input.interactable = false;
        // interpreter.interactable = false;
        component_content.GetComponent<Text>().text = Prefabs[GetActiveToggle()].GetComponent<ComponentController>().GetDescription();
        placement_overlay.GetComponent<SpriteRenderer>().sprite = Overlay;
        ship.EnableColliders();

        recently_unfocused = .5f;

        SwitchLeftView(Components);
        placement_overlay.GetComponent<SpriteRenderer>().size = Prefabs[GetActiveToggle()].GetComponent<ComponentController>().GetMinimumSize();
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

        right_input.text = selected;
        right_input.interactable = true;
        // interpreter.interactable = true;
        
        component_content.GetComponent<Text>().text = ship.ToString(selected);


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

    public void Printy()
    {
        // ship.SetInstructions(selected, interpreter.text);
    }
}
