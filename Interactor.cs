using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*

Font size 50
Character width 25
Text height 50

*/


public class Interactor : MonoBehaviour
{
    public GameObject Overlay;
    public GameObject Example;
    private string command = "";
    private string history = "";
    public StructureController Ship;
    public string start_text = "$"; //git clone https://github.com/bitnaughts/bitnaughts.git\nCloning into 'bitnaughts'...\nremote: Enumerating objects: 3994, done.\nremote: Counting objects: 100% (96/96), done.\nremote: Compressing objects: 100% (67/67), done.\nremote: Total 3994 (delta 34), reused 82 (delta 28), pack-reused 3898\nReceiving objects: 100% (3994/3994), 31.20 MiB | 10.49 MiB/s, done.\nResolving deltas: 100% (2755/2755), done.\n" + 
    // "\n~ $ cd bitnaughts\n\n~/bitnaughts $ help\n☄ BitNaughts is an educational\nprogramming video-game.\n\n~/bitnaughts $";//\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n~ $ help\nBit Bash\nBitNaughts inline terminal\nA really long line would fit like this with the slider being able to scroll to see the full text\n~ $\n\npublic class Processor {\n void Start() {\n }\n}\n\n\ntest one two three\nfour five six\nseven eight nine";

    public List<GameObject> ButtonsCache = new List<GameObject>();
    int cache_size = 200;
    public OverlayInteractor OverlayInteractor;
    public GameObject ClickableText;
    public InputField InputField;
    public Text InputFieldPlaceholder;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < cache_size; i++) {
            ButtonsCache.Add(Instantiate(ClickableText, this.transform) as GameObject);
        } 
        OverlayInteractor = GameObject.Find("OverlayBorder").GetComponent<OverlayInteractor>();
        RenderText("$");
    }

    public void AppendText(string text) {
        if (history.LastIndexOf("$") != -1) history = history.Substring(0, history.LastIndexOf("$"));
        history += text;
        RenderText(history);
    }
    public void ClearText() {
        if (history == "") history = "$";
        RenderText(history);
    }
    public void ClearHistory() {
        history = "$";
        RenderText(history);
    }
    public void PrintMock() {
        Example.SetActive(true);
    }
    public void FireMock() {
        // Example.GetComponent<StructureController>();
    }

    public void RenderText(string text) {
        foreach (var button in ButtonsCache) {
            button.SetActive(false);
        }
        float max_line_length = -1;
        string[] lines = text.Split('\n');
        for(int line = 0; line < lines.Length; line++) {
            string[] words = lines[line].Split(' ');
            int character_count = 0;
            for(int word = 0; word < words.Length; word++) {
                if (words[word].Length == 0) {
                    character_count++;
                    continue;
                } 
                character_count += words[word].Length + 1;
                if (words[word].StartsWith("<") && words[word].EndsWith(">")) character_count -= 7;
                InitializeClickableText(words[word], line, character_count);
            }
            if (character_count > max_line_length) {
                max_line_length = character_count;
            }
        }
        SetContentSize(25f + max_line_length * 25f, 50f + lines.Length * 50f);
    }
    public void RenderComponent(string component) {
        var component_string = Ship.GetComponentToString(component);
        var component_header = component_string.IndexOf("\n");
        InputField.text = component_string.Substring(0, component_header);
        InputField.interactable = true;
        RenderText(component_string.Substring(component_header + 1));
    }

    public string[] GetComponents() {
        return Ship.GetControllers();
    }
    void SetContentSize(float width,float height) {
        // print ("Content.sizeDelta(" + width + ", " + height + ")");
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }
    public void SetInputPlaceholder(string placeholder) {
        InputField.interactable = true;
        // InputFieldPlaceholder.text = placeholder;
        InputField.text = "";
    }
    public void OnInput() {
        //create new component...
        InputField.interactable = false;
        switch (GetCommand()) {
            case "nano":
                
                // GameObject object_reference = //Prefabs[GetActiveToggle()];
                    
                    var component_gameObject = Instantiate(Overlay, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                    //move this logic to structure controller, use IfKeyExists
                    // int component_count = 1;
                    // while (Ship.IsComponent(object_reference.name + component_count)) component_count++;
                    component_gameObject.name = InputField.text;//object_reference.name + component_count;
                    component_gameObject.GetComponent<SpriteRenderer>().size = new Vector2(2,2);//object_reference.GetComponent<ComponentController>().GetMinimumSize();
                    
                    // if (focused_type == "Gimbal" && !GetActiveText().Contains(focused_type)) {
                        // Transform gimbal_grid = Ship.transform.Find("Rotator").Find(focused).GetChild(0);
                        // component_gameObject.transform.SetParent(gimbal_grid);
                        
                    //     component_gameObject.transform.localPosition = new Vector2(pos.x - gimbal_grid.transform.position.x, pos.z  - gimbal_grid.transform.position.z);
                    // }
                    // else {
                        component_gameObject.transform.SetParent(Ship.transform.Find("Rotator"));
                        // component_gameObject.transform.localPosition = new Vector2(pos.x, pos.z);
                    // }
                    
                    // component_gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);

                    OverlayInteractor.UpdateOptions();
                    OverlayInteractor.OnDropdownChange(); 

            break;
        }
        InputFieldPlaceholder.text = "";
    }

    public string GetCommand() {
        return command;
    }
    public void SetCommand(string command) {
        this.command = command;
    }

    float time = 0f;
    void FixedUpdate()
    {
        // foreach (var button in ButtonsCache) {
        //     if (button.activeSelf == true) {
                
        //     }
        // }
        time += Time.deltaTime;
        // if ((int)time % 2 == 0) {
        //     RenderText("$");
        // }
        // else {
        //     RenderText("<a>$</a>");
        // }
        // switch ((int)(time*10f)) {
        //     case 00: RenderText("$"); break;
        //     case 05: RenderText("<a>$</a>"); break;
        //     case 10: RenderText("$"); break;
            // case 15: RenderText("<a>$</a>"); break;
            // case 20: RenderText("$"); break;
            // case 25: RenderText("<a>$</a>"); break;
            // case 30: RenderText("$"); break;
        // }
            // case 05: RenderText("$ git"); break;
            // case 10: RenderText("$ git clone"); break;
            // case 15: RenderText("$ git clone https://github.com/bitnaughts/bitnaughts.git"); break;
            // case 25: RenderText("$ git clone https://github.com/bitnaughts/bitnaughts.git\n<i>Cloning</i> <i>into</i> <i>'bitnaughts'</i>"); break;
            // case 26: RenderText("$ git clone https://github.com/bitnaughts/bitnaughts.git\n<i>Cloning</i> <i>into</i> <i>'bitnaughts'</i>\n<i>remote:</i> <i>Enumerating</i> <i>objects</i>"); break;
            // case 27: RenderText("$ git clone https://github.com/bitnaughts/bitnaughts.git\n<i>Cloning</i> <i>into</i> <i>'bitnaughts'</i>\n<i>remote:</i> <i>Enumerating</i> <i>objects</i>\n<i>remote:</i> <i>Counting</i> <i>objects</i>"); break;
            // case 28: RenderText("$ git clone https://github.com/bitnaughts/bitnaughts.git\n<i>Cloning</i> <i>into</i> <i>'bitnaughts'</i>\n<i>remote:</i> <i>Enumerating</i> <i>objects</i>\n<i>remote:</i> <i>Counting</i> <i>objects</i>\n<i>remote:</i> <i>Compressing</i> <i>objects</i>"); break;
            // case 29: RenderText("$ git clone https://github.com/bitnaughts/bitnaughts.git\n<i>Cloning</i> <i>into</i> <i>'bitnaughts'</i>\n<i>remote:</i> <i>Enumerating</i> <i>objects</i>\n<i>remote:</i> <i>Counting</i> <i>objects</i>\n<i>remote:</i> <i>Compressing</i> <i>objects</i>\n<i>Receiving</i> <i>objects</i>"); break;
            // case 30: RenderText("$ git clone https://github.com/bitnaughts/bitnaughts.git\n<i>Cloning</i> <i>into</i> <i>'bitnaughts'</i>\n<i>remote:</i> <i>Enumerating</i> <i>objects</i>\n<i>remote:</i> <i>Counting</i> <i>objects</i>\n<i>remote:</i> <i>Compressing</i> <i>objects</i>\n<i>Receiving</i> <i>objects</i>\n<i>Resolving</i> <i>deltas</i>"); break;
            // case 50: RenderText("$"); break;
            // case 55: RenderText("$ help"); break;
            // case 60: RenderText("$ help\n☄ BitNaughts is an educational\n  programming video-game;"); break;
            // case 80: RenderText("$"); break;
            // <i>git</i>\n  <i>nano</i>\n  <i>help</i>
        // }
    }

    void InitializeClickableText(string text, int line, int pos) {
        foreach (var button in ButtonsCache) {
            if (button.activeSelf == false) {
                button.GetComponent<ClickableTextInteractor>().Initialize(this, OverlayInteractor, text, line, pos);
                break;
            }
        }
    }
}
