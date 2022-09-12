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
    public AudioClip TutorialIntro, TutorialLookAround, TutorialMapInterface, TutorialMapScreen, TutorialIssueOrders, TutorialGood, TutorialGood2, TutorialCancel, TutorialOther, TutorialMusic, TutorialComponents, TutorialGetMoving, TutorialOutro, TutorialLeftWindow, TutorialRightWindow, TutorialCursor, TutorialSelect;

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
        InputField.text = "☄ BitNaughts";
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
    public string component_name = "";
    public void RenderComponent(string component) {
        var component_string = Ship.GetComponentToString(component);
        var component_header = component_string.IndexOf("\n");
        component_name = component_string.Substring(0, component_header);
        InputField.text = component_name;
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
        InputField.text = placeholder;
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
                Ship.Start();
                OverlayInteractor.UpdateOptions();
                OverlayInteractor.OnDropdownChange(); 
            break;
            default:
                GameObject.Find(component_name).name = InputField.text;
                Ship.Start();
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

    float timer = 30f;
    bool tutorialIntro = false, tutorialPan = false, tutorialFire = false, tutorialCancel = false, tutorialThrust;

    public void StartTutorial() {
        if (tutorialIntro == false) {
            tutorialIntro = true;
            tutorialPan = false;
            tutorialFire = false;
            tutorialCancel = false;
            tutorialThrust = false;
            timer = 0;
        }
    }
    public void PanTutorial() {
        if (tutorialIntro && !tutorialPan) {
            tutorialIntro = false;
            tutorialPan = true;
            tutorialFire = false;
            tutorialCancel = false;
            tutorialThrust = false;
            timer = 0;
        }
    }
    public void FireTutorial() {
        if (tutorialPan && !tutorialFire) {
            tutorialIntro = false;
            tutorialPan = false;
            tutorialFire = true;
            tutorialCancel = false;
            tutorialThrust = false;
            timer = 0;
        }
    }
    public void CancelTutorial() {
        if (tutorialFire && !tutorialCancel) {
            tutorialIntro = false;
            tutorialPan = false;
            tutorialFire = false;
            tutorialCancel = true;
            tutorialThrust = false;
            timer = 0;
        }
    }
    public void ThrustTutorial() {
        if (tutorialCancel && !tutorialThrust) {
            tutorialIntro = false;
            tutorialPan = false;
            tutorialFire = false;
            tutorialCancel = false;
            tutorialThrust = true;
            timer = 0;
        }
    }
    public void Action(string name, int action) {
        print ("Fire" + name + action);
        GameObject.Find(name).GetComponent<ComponentController>().Action(action);
    }
    void PlayAtTime(AudioClip clip, float timer, float time) {
        if (timer > time && timer < time + (Time.deltaTime * 2f)) {
            GetComponent<AudioSource>().clip = clip;
            GetComponent<AudioSource>().volume = 1f;
            GetComponent<AudioSource>().Play();
        }
    }
    void SubtitlesAtTime(string text, float timer, float time) {
        if (timer > time && timer < time + (Time.deltaTime * 2f)) {
            // GameObject.Find("Subtitles").GetComponent<Text>().text = text + "\n";
            RenderText(text);
        }
    }
    void FixedUpdate()
    {
        if (tutorialIntro) {
            timer += Time.deltaTime;
            PlayAtTime(TutorialIntro, timer, 0.5f);
            SubtitlesAtTime("$ <b>tutorial</b>", timer, 0f);
            SubtitlesAtTime("⍰_Welcome_to_the_command_tutorial!", timer, 0.5f);
            SubtitlesAtTime("⍰_Welcome_to_the_command_tutorial!\n  Today_you_will_learn_ship_control.", timer, 3f);
            PlayAtTime(TutorialMapScreen, timer, 7f);
            if (timer > 7f  && timer < 11f) { 
                GameObject.Find("Overlay").GetComponent<Image>().color = new Color(.5f + (timer * 2) % 1, .5f + (timer * 2) % 1, 0, 1f);
            }
            if (timer > 11f && timer < 11f + (Time.deltaTime * 2f)) { GameObject.Find("Overlay").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f); }
            SubtitlesAtTime("▦_Map_screen_shows_you:", timer, 7f);
            SubtitlesAtTime("▦_Map_screen_shows_you:\n-_Mission_area", timer, 9f);
            SubtitlesAtTime("▦_Map_screen_shows_you:\n-_Mission_area\n-_Friendly_units", timer, 11f);
            SubtitlesAtTime("▦_Map_screen_shows_you:\n-_Mission_area\n-_Friendly_units\n-_Detected_enemy_units", timer, 12.5f);
            SubtitlesAtTime("▦_Map_screen_shows_you:\n-_Mission_area\n-_Friendly_units\n-_Detected_enemy_units\n-_Selected_unit_highlighted_yellow", timer, 15f);
            PlayAtTime(TutorialLookAround, timer, 21f);
            SubtitlesAtTime("⁜_First_off,_try_looking_around:", timer, 21f);
            SubtitlesAtTime("⁜_First_off,_try_looking_around:\n  360°_awareness_is_needed\n  for_dogfighting!", timer, 23f);
            if (timer > 21f) { 
                GameObject.Find("Overlay").GetComponent<Image>().color = new Color(.5f + (timer * 2) % 1, .5f + (timer * 2) % 1, 0, 1f);
            }
        }
        if (tutorialPan) {
            timer += Time.deltaTime;
            PlayAtTime(TutorialGood2, timer, 0.5f);
            if (timer > 0f && timer < 0f + (Time.deltaTime * 2f)) { GameObject.Find("Overlay").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f); }
            PlayAtTime(TutorialCursor, timer, 2f);
            SubtitlesAtTime("⁜_The_cursor_is_used_to\n  select_units_and_issue_orders.", timer, 2f);
            PlayAtTime(TutorialRightWindow, timer, 7f);
            if (timer > 7f && timer < 7f + (Time.deltaTime * 2f)) { 
                for (int i = 0; i < OverlayInteractor.OverlayDropdown.options.Count; i++) {
                    if (OverlayInteractor.OverlayDropdown.options[i].text == "Cannon") OverlayInteractor.OverlayDropdown.value = i; 
                }
                OverlayInteractor.gameObject.SetActive(true);
                OverlayInteractor.OnDropdownChange(); 
            }
            if (timer > 7f && timer < 11f) { 
                GameObject.Find("OverlayBorder").GetComponent<Image>().color = new Color(1f, 1f, 0, .35f + (timer * 2) % 1);
            }
            PlayAtTime(TutorialLeftWindow, timer, 11f);
            if (timer > 11f && timer < 15f) { 
                GameObject.Find("InterpreterPanel").GetComponent<Image>().color = new Color(.5f + (timer * 2) % 1, .5f + (timer * 2) % 1, 0, 1f);
            }
            if (timer > 15f && timer < 15f + (Time.deltaTime * 2f)) { GameObject.Find("InterpreterPanel").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f); }
            PlayAtTime(TutorialSelect, timer, 18f);
            if (timer > 18f) { 
                GameObject.Find("ClickableFire").GetComponent<Image>().color = new Color(.5f + (timer * 2) % 1, .5f + (timer * 2) % 1, 0, 1f);
            }
        }
        if (tutorialFire) {
            timer += Time.deltaTime;
            PlayAtTime(TutorialGood, timer, 0.5f);
            PlayAtTime(TutorialCancel, timer, 2f);
            SubtitlesAtTime("X Clear_a_target_at_any_time\n  by_pressing_Cancel_button.", timer, 2f);
            SubtitlesAtTime("X Clear_a_target_at_any_time\n  by_pressing_Cancel_button.\n  Do_this_now.", timer, 8f);
            if (timer > 2f) { 
                GameObject.Find("OverlayDelete").GetComponent<Image>().color = new Color(1f, 1f, 0, .35f + (timer * 2) % 1);
            }
        }
        if (tutorialCancel) {
            timer += Time.deltaTime;
            PlayAtTime(TutorialOther, timer, 0.5f);
            SubtitlesAtTime("▢ Other_instruments_are_detailed\n  in_later_tutorials.", timer, 0.5f);
            PlayAtTime(TutorialGetMoving, timer, 5f);
            if (timer > 5f && timer < 5f + (Time.deltaTime * 2f)) { 
                for (int i = 0; i < OverlayInteractor.OverlayDropdown.options.Count; i++) {
                    if (OverlayInteractor.OverlayDropdown.options[i].text == "Thruster") OverlayInteractor.OverlayDropdown.value = i; 
                }
                OverlayInteractor.gameObject.SetActive(true);
                OverlayInteractor.OnDropdownChange(); 
                GameObject.Find("OverlayDelete").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
            if (timer > 5f) { 
                GameObject.Find("ClickableThrust").GetComponent<Image>().color = new Color(.5f + (timer * 2) % 1, .5f + (timer * 2) % 1, 0, 1f);
            }
        }
        if (tutorialThrust) {
            timer += Time.deltaTime;
            PlayAtTime(TutorialOutro, timer, 0.5f);
            SubtitlesAtTime("☀ Excellent work!", timer, 0.5f);
            SubtitlesAtTime("☀ Excellent work!\n  You have completed the tutorial.", timer, 1.5f);
            SubtitlesAtTime("☔ I hope you never have cause\n  to use the knowledge\n  you just acquired.", timer, 5f);
            SubtitlesAtTime("☂ That is all for today: dismissed!", timer, 10f);
            SubtitlesAtTime("$", timer, 14f);
            if (timer > 14) {
                tutorialThrust = false;
            }
        }
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
