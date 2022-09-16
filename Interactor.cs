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
    public AudioClip TutorialIntro, TutorialLookAround, TutorialMapInterface, TutorialMapScreen, TutorialIssueOrders, TutorialTargetWindow, TutorialTargetWindowHelp, TutorialTargetWindowSelected, TutorialGood, TutorialGood2, TutorialGood3, TutorialTry, TutorialBetter, TutorialCancel, TutorialOther, TutorialMusic, TutorialComponents, TutorialGetMoving, TutorialThrottle, TutorialDogfight, TutorialOutro, TutorialLeftWindow, TutorialRightWindow, TutorialCursor, TutorialSelect;
    public AudioClip CannonFire, ThrusterThrottle, SonarScan, TorpedoFact, ProcessorPing, GimbalRotate;
    public AudioClip ThemeSong, Click, Click2;
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

    public Text Timer, TimerShadow, SplitTimer, SplitTimerShadow;

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
        InputFieldPlaceholder.text = placeholder;
        InputField.text = "";//placeholder;
    }
    public string GetInput() {
        return InputField.text;
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

    float timer = 0f;
    bool tutorialIntro = false, tutorialPan = false, tutorialTarget = false, tutorialFire = false, tutorialCancel = false, tutorialThrust = false, tutorialFinish = false, tutorialComplete = false;
    public bool TutorialRunning() {
        return tutorialIntro || tutorialPan || tutorialTarget || tutorialFire || tutorialCancel || tutorialThrust || tutorialFinish;
    }
    public void StartTutorial() {
        if (tutorialIntro == false) {
            tutorialIntro = true;
            tutorialPan = false;
            tutorialTarget = false;
            tutorialFire = false;
            tutorialCancel = false;
            tutorialThrust = false;
            tutorialFinish = false;
            timer = 0;
        }
    }
    public void PanTutorial() {
        if (tutorialIntro && !tutorialPan && timer > 0.1f) {
            tutorialIntro = false;
            tutorialPan = true;
            tutorialTarget = false;
            tutorialFire = false;
            tutorialCancel = false;
            tutorialThrust = false;
            tutorialFinish = false;
            timer = 0;
        }
    }
    public void TargetTutorial() {
        if (tutorialPan && !tutorialTarget) {
            tutorialIntro = false;
            tutorialPan = false;
            tutorialTarget = true;
            tutorialFire = false;
            tutorialCancel = false;
            tutorialThrust = false;
            tutorialFinish = false;
            timer = 0;
        }
    }
    public void FireTutorial() {
        if (tutorialTarget && !tutorialFire) {
            tutorialIntro = false;
            tutorialPan = false;
            tutorialTarget = false;
            tutorialFire = true;
            tutorialCancel = false;
            tutorialThrust = false;
            tutorialFinish = false;
            timer = 0;
        }
    }
    public void CancelTutorial() {
        if (tutorialFire && !tutorialCancel) {
            tutorialIntro = false;
            tutorialPan = false;
            tutorialTarget = false;
            tutorialFire = false;
            tutorialCancel = true;
            tutorialThrust = false;
            tutorialFinish = false;
            timer = 0;
        }
    }
    public void ThrustTutorial() {
        if (tutorialCancel && !tutorialThrust) {
            tutorialIntro = false;
            tutorialPan = false;
            tutorialTarget = false;
            tutorialFire = false;
            tutorialCancel = false;
            tutorialThrust = true;
            tutorialFinish = false;
            timer = 0;
        }
    }
    public void FinishTutorial() {
        if (tutorialThrust && !tutorialFinish) {
            tutorialIntro = false;
            tutorialPan = false;
            tutorialTarget = false;
            tutorialFire = false;
            tutorialCancel = false;
            tutorialThrust = false;
            tutorialFinish = true;
            timer = 0;
        }
    }
    public void CompleteTutorial() {
        if (tutorialFinish) {
            tutorialComplete = true;
            tutorialIntro = false;
            tutorialPan = false;
            tutorialTarget = false;
            tutorialFire = false;
            tutorialCancel = false;
            tutorialThrust = false;
            tutorialFinish = false;
            timer = 0;
        }
    }
    public void Action(string name, int action) {
        print ("Fire" + name + action);
        GameObject.Find(name).GetComponent<ComponentController>().Action(action);
    }
    public void PlayTheme() {
        PlayAtTime(ThemeSong, 0f, 0f);
    }
    public void PlayGimbal() {
        PlayAtTime(GimbalRotate, 0f, 0f);
    }
    public void PlayCannon() {
        PlayAtTime(CannonFire, 0f, 0f);
    }
    public void PlayThruster() {
        PlayAtTime(ThrusterThrottle, 0f, 0f);
    }
    public void PlayRadar() {
        PlayAtTime(SonarScan, 0f, 0f);
    }
    public void PlayProcessor() {
        PlayAtTime(ProcessorPing, 0f, 0f);
    }
    public void PlayClick() {
        PlayAtTime(Click, 0f, 0f);
    }
    public void PlayClick2() {
        PlayAtTime(Click2, 0f, 0f);
    }
    public void PlayAtTime(AudioClip clip, float timer, float time) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            GetComponent<AudioSource>().clip = clip;
            GetComponent<AudioSource>().volume = 0.66f;
            GetComponent<AudioSource>().Play();
        }
    }
    void SubtitlesAtTime(string text, float timer, float time) {
        if (timer > time && timer < time + (Time.deltaTime * 2f)) {
            // GameObject.Find("Subtitles").GetComponent<Text>().text = text + "\n";
            RenderText(text);
        }
    }
    double click_duration = 0;
    public double GetClickDuration() {
        return click_duration;
    }
    float global_timer = 0, animation_timer = 0;
    string FloatToTime(float time) {
        var pt = ((int)((time*100) % 60)).ToString("00");
        var ss = ((int)(time % 60)).ToString("00");
        var mm = (Mathf.Floor(time / 60) % 60).ToString("00");
        return mm + ":" + ss + "." + pt;
    }
    void Update () {
        animation_timer += Time.deltaTime;
        if (TutorialRunning()) {
            SplitTimer.text = FloatToTime(timer);
            SplitTimerShadow.text = FloatToTime(timer);
        }
        else {
            SplitTimer.text = "";
            SplitTimerShadow.text = "";
        }
        if (!tutorialComplete) {
            global_timer += Time.deltaTime;
            Timer.text = FloatToTime(global_timer);
            TimerShadow.text = FloatToTime(global_timer);
        }
        else {
            Timer.color = new Color(.5f + (animation_timer * 2) % 1, .5f + (animation_timer * 2) % 1, 0, 1f);
            SplitTimer.text = "Tutorial";
            SplitTimerShadow.text = "Tutorial";
        }
        if (Input.GetMouseButton(0)) {
            click_duration += Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0)) {
            click_duration = 0;
        }
    }
    void SpriteFlash(string name, float start, float end) {
        if (timer > start  && timer < end) { 
            GameObject.Find(name).GetComponent<SpriteRenderer>().color = new Color(.5f + (timer * 2) % 1, .5f + (timer * 2) % 1, 0, 1f);
        }
        else if (timer >= end && timer < end + (Time.deltaTime * 2f)) {
            ResetSpriteFlash(name, end);
        }
    }
    void Flash(string name, float start, float end) {
        if (timer > start  && timer < end) { 
            GameObject.Find(name).GetComponent<Image>().color = new Color(.5f + (timer * 2) % 1, .5f + (timer * 2) % 1, 0, 1f);
        }
        else if (timer >= end && timer < end + (Time.deltaTime * 2f)) {
            ResetFlash(name, end);
        }
    }
    void ResetSpriteFlash(string name, float time) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            GameObject.Find(name).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
    void ResetFlash(string name, float time) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            GameObject.Find(name).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
    void FixedUpdate()
    {
        if (tutorialIntro) {
            timer += Time.deltaTime;
            PlayAtTime(TutorialIntro, timer, 0.5f);
            SubtitlesAtTime("$ <b>tutorial</b>", timer, 0f);
            SubtitlesAtTime("⍰_Welcome_to_the_command_tutorial!", timer, 0.5f);
            SubtitlesAtTime("⍰_Welcome_to_the_command_tutorial!\n  Today_you_will_learn_ship_control", timer, 3f);
            PlayAtTime(TutorialLookAround, timer, 7f);
            Flash ("MapScreenOverlay", 7f, 30f);
            Flash ("MapScreenOverlayBit", 7f, 30f);
            SubtitlesAtTime("⍰_Welcome_to_the_command_tutorial!\n  Today_you_will_learn_ship_control\n\n  First_off,_try_looking_around", timer, 7f);
            SubtitlesAtTime("⍰_Welcome_to_the_command_tutorial!\n  Today_you_will_learn_ship_control\n\n  First_off,_try_looking_around\n  360°_awareness_is_needed_for\n  dog_fighting!", timer, 9f);
            PlayAtTime(TutorialTry, timer, 15f);
            SubtitlesAtTime("⍰_Welcome_to_the_command_tutorial!\n  Today_you_will_learn_ship_control\n\n  First_off,_try_looking_around\n  360°_awareness_is_needed_for\n  dog_fighting!\n\n  Hint:_click_&_drag_the\n▦_Map_Screen", timer, 15f);
        }
        if (tutorialPan) {
            timer += Time.deltaTime;
            ResetFlash("MapScreenOverlay", 0);
            ResetFlash("MapScreenOverlayBit", 0);
            PlayAtTime(TutorialGood3, timer, 0.5f);
            PlayAtTime(TutorialMapScreen, timer, 1.5f);
            SubtitlesAtTime("▦_Map_Screen_shows:", timer, 1.5f);
            SubtitlesAtTime("▦_Map_Screen_shows:\n-_Mission_area", timer, 3.5f);
            SubtitlesAtTime("▦_Map_Screen_shows:\n-_Mission_area\n-_Friendly_units", timer, 5f);
            SubtitlesAtTime("▦_Map_Screen_shows:\n-_Mission_area\n-_Friendly_units\n-_Detected_enemy_units", timer, 7f);
            SubtitlesAtTime("▦_Map_Screen_shows:\n-_Mission_area\n-_Friendly_units\n-_Detected_enemy_units\n-_Select_units_highlighted_yellow", timer, 9f);
            PlayAtTime(TutorialTargetWindowHelp, timer, 14f);
            SubtitlesAtTime("▦_Map_Screen_shows:\n-_Mission_area\n-_Friendly_units\n-_Detected_enemy_units\n-_Select_units_highlighted_yellow\n\n  When_the_reticle_is_over\n  the_target", timer, 14f);
            SubtitlesAtTime("▦_Map_Screen_shows:\n-_Mission_area\n-_Friendly_units\n-_Detected_enemy_units\n-_Select_units_highlighted_yellow\n\n  When_the_reticle_is_over\n  the_target,_press_the_\n  \"use_weapon\"_control", timer, 15.5f);
            SubtitlesAtTime("▦_Map_Screen_shows:\n-_Mission_area\n-_Friendly_units\n-_Detected_enemy_units\n-_Select_units_highlighted_yellow\n\n  When_the_reticle_is_over\n  the_target,_press_the_\n  \"use_weapon\"_control.\n\n  This_will_display_the\n⁜_Target Window,_have_a_go.", timer, 19f);
            SpriteFlash ("Cannon", 9f, 41f);
            PlayAtTime(TutorialTry, timer, 25f);
            SubtitlesAtTime("▦_Map_Screen_shows:\n-_Mission_area\n-_Friendly_units\n-_Detected_enemy_units\n-_Select_units_highlighted_yellow\n\n  When_the_reticle_is_over\n  the_target,_press_the_\n  \"use_weapon\"_control.\n\n  This_will_display_the\n⁜_Target Window,_have_a_go.\n\n  Hint:_click_\"Cannon\"!", timer, 25f);
        }
        if (tutorialTarget) {
            timer += Time.deltaTime;
            ResetSpriteFlash("Cannon", 0f);

            // PlayAtTime(TutorialTargetWindow, timer, 12f);
            // SubtitlesAtTime("First_off,_let's_introduce_the\n⁜_Target_Window", timer, 12f);
            // SubtitlesAtTime("⁜_Target_Window_appears_when\n  looking_at_a_unit.", timer, 14f);
            // SubtitlesAtTime("⁜_Target_Window_appears_when\n  looking_at_a_unit.\n  Have_a_go_at_this_now.", timer, 16f);

            PlayAtTime(TutorialTargetWindowSelected, timer, 0.5f);
            Flash("OverlayBorder", 0.5f, 8f);



            PlayAtTime(TutorialLeftWindow, timer, 8f);
            Flash ("InterpreterPanel", 8f, 15f);
            Flash ("UnitScreenBit", 8f, 15f);
            PlayAtTime(TutorialSelect, timer, 15f);
            Flash("Clickable/*_\"Use_weapon\"_control_*/", 15f, 16f);
            PlayAtTime(TutorialTry, timer, 31f);
            Flash ("ClickableFire", 16f, 41f);
        }
        if (tutorialFire) {
            timer += Time.deltaTime;
            PlayAtTime(TutorialGood, timer, 0.5f);
            PlayAtTime(TutorialCancel, timer, 2f);
            SubtitlesAtTime("X Clear_a_target_at_any_time\n  by_pressing_\"Cancel\"_button.", timer, 2f);
            SubtitlesAtTime("X Clear_a_target_at_any_time\n  by_pressing_\"Cancel\"_button.\n\n  Do_this_now.", timer, 8f);
            PlayAtTime(TutorialTry, timer, 15f);
            SubtitlesAtTime("X Clear_a_target_at_any_time\n  by_pressing_\"Cancel\"_button.\n\n  Do_this_now.\n\n  Hint: click \"☒\"!", timer, 15f);
            Flash ("OverlayDelete", 2f, 41f);
        }
        if (tutorialCancel) {
            timer += Time.deltaTime;
            PlayAtTime(TutorialDogfight, timer, 0.5f);
            SubtitlesAtTime("  Well_done!_It's_time_\n  to_learn_to_dog_fight!", timer, 0.5f);
            PlayAtTime(TutorialGetMoving, timer, 5f);
            SpriteFlash ("Thruster", 5f, 41f);
            SubtitlesAtTime("  Well_done!_It's_time_\n  to_learn_to_dog_fight!\n\n  Now_it's_time_to_get\n  this_old_girl_moving!", timer, 5f);
            PlayAtTime(TutorialOther, timer, 10f);
            SubtitlesAtTime("  Well_done!_It's_time_\n  to_learn_to_dog_fight!\n\n  Now_it's_time_to_get\n  this_old_girl_moving!\n\n  Other_instruments_are_detailed\n  in_later_tutorials.", timer, 10f);
            PlayAtTime(TutorialTry, timer, 15f);
            SubtitlesAtTime("  Well_done!_It's_time_\n  to_learn_to_dog_fight!\n\n  Now_it's_time_to_get\n  this_old_girl_moving!\n\n  Other_instruments_are_detailed\n  in_later_tutorials.\n\n  Hint:_click_\"Thruster\"!", timer, 15f);
        }
        if (tutorialThrust) {
            timer += Time.deltaTime;
            ResetSpriteFlash ("Thruster", 0f);
            PlayAtTime(TutorialGood2, timer, 0.5f);
            Flash("ClickableThrottleMax", 1.5f, 41f);
            PlayAtTime(TutorialThrottle, timer, 1.5f);
            PlayAtTime(TutorialTry, timer, 8f);
        }
        if (tutorialFinish) {
            timer += Time.deltaTime;
            PlayAtTime(TutorialBetter, timer, 0.5f);
            PlayAtTime(TutorialOutro, timer, 2.5f);
            SubtitlesAtTime("☀_Excellent_work!", timer, 2.5f);
            SubtitlesAtTime("☀_Excellent_work!\n  You_have_completed_the_tutorial.", timer, 3.5f);
            SubtitlesAtTime("☀_Excellent_work!\n  You_have_completed_the_tutorial.\n\n☔_I_hope_you_never_have_cause\n  to_use_the_knowledge\n  you_just_acquired.", timer, 7f);
            SubtitlesAtTime("☀_Excellent_work!\n  You_have_completed_the_tutorial.\n\n☔_I_hope_you_never_have_cause\n  to_use_the_knowledge\n  you_just_acquired\n\n☂_That_is_all_for_today!", timer, 12f);
            SubtitlesAtTime("☀_Excellent_work!\n  You_have_completed_the_tutorial.\n\n☔_I_hope_you_never_have_cause\n  to_use_the_knowledge\n  you_just_acquired\n\n☂_That_is_all_for_today!\n  Dismissed!", timer, 13.5f);
            SubtitlesAtTime("☀_Excellent_work!\n  You_have_completed_the_tutorial.\n\n☔_I_hope_you_never_have_cause\n  to_use_the_knowledge\n  you_just_acquired\n\n☂_That_is_all_for_today!\n  Dismissed!\n\n  Hint:_click_\"☑\"!", timer, 15f);
            Flash ("OverlayOk", 3.5f, 20f);
            SubtitlesAtTime("$ tutorial\n$", timer, 21f);
            if (timer > 21) {
                tutorialFinish = false;
                tutorialComplete = true;
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
