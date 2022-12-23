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
    public GameObject Content, InterpreterPanel, MapPanel, SubtitlesShadow, Subtitles; 
    public AudioClip TutorialIntro, TutorialLookAround, TutorialMapInterface, TutorialMapScreen, TutorialIssueOrders, TutorialTargetWindow, TutorialTargetWindowHelp, TutorialTargetWindowSelected, TutorialGood, TutorialGood2, TutorialGood3, TutorialTry, TutorialBetter, TutorialCancel, TutorialOther, TutorialMusic, TutorialComponents, TutorialGetMoving, TutorialThrottle, TutorialDogfight, TutorialOutro, TutorialLeftWindow, TutorialRightWindow, TutorialCursor, TutorialSelect;
    public AudioClip CannonFire, ThrusterThrottle, SonarScan, TorpedoFact, ProcessorPing, GimbalRotate, TorpedoLaunch;
    public AudioClip ThemeSong, Click, Click2;
    public AudioClip SoundBack, SoundClick, SoundError, SoundOnMouse, SoundStart, SoundToggle, SoundProcessor, SoundGimbal, SoundCannon1, SoundCannon2, SoundCannon3, SoundRadar, SoundThruster, SoundBooster, SoundTorpedo1, SoundTorpedo2, SoundWarning, SoundWarningOver;
    public GameObject Overlay;
    public GameObject Example;
    private string command = "";
    private string history = "";
    public StructureController Ship;
    public string start_text = "$"; //git clone https://github.com/bitnaughts/bitnaughts.git\nCloning into 'bitnaughts'...\nremote: Enumerating objects: 3994, done.\nremote: Counting objects: 100% (96/96), done.\nremote: Compressing objects: 100% (67/67), done.\nremote: Total 3994 (delta 34), reused 82 (delta 28), pack-reused 3898\nReceiving objects: 100% (3994/3994), 31.20 MiB | 10.49 MiB/s, done.\nResolving deltas: 100% (2755/2755), done.\n" + 
    // "\n~ $ cd bitnaughts\n\n~/bitnaughts $ help\n☄ BitNaughts is an educational\nprogramming video-game.\n\n~/bitnaughts $";//\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n~ $ help\nBit Bash\nBitNaughts inline terminal\nA really long line would fit like this with the slider being able to scroll to see the full text\n~ $\n\npublic class Processor {\n void Start() {\n }\n}\n\n\ntest one two three\nfour five six\nseven eight nine";

    public OverlayInteractor OverlayInteractor;
    public GameObject ClickableText;
    GameObject MapScreenPanOverlay;
    public Text InputField;

    public Text Timer, TimerShadow, SplitTimer, SplitTimerShadow;

    GameObject camera;

    public List<GameObject> ButtonsCache = new List<GameObject>();
    int cache_size = 125;
    // Start is called before the first frame update
    void Start()
    {
        Subtitles = GameObject.Find("Subtitles");
        SubtitlesShadow = GameObject.Find("SubtitlesShadow");
        SubtitlesShadow.SetActive(false);
        Subtitles.SetActive(false);
        camera = GameObject.Find("Main Camera");
        for (int i = 0; i < cache_size; i++) {
            ButtonsCache.Add(Instantiate(ClickableText, Content.transform) as GameObject);
        } 
        OverlayInteractor = GameObject.Find("OverlayBorder")?.GetComponent<OverlayInteractor>();
        MapScreenPanOverlay = GameObject.Find("MapScreenPanOverlay");
        RenderText("$");
        Timer.text = "Hello World ";
    }
    float story_timer = -1f;
    public void StoryMode() {
        story_timer = 0f;
        OnMapView();
        GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().enabled = true;
        SubtitlesShadow.SetActive(true);
        Subtitles.SetActive(true);
    }
    public void OnMapView() {
        MapPanel.SetActive(true);
        InterpreterPanel.SetActive(false);
    }
    public void OnCodeView() {
        MapPanel.SetActive(false);
        InterpreterPanel.SetActive(true);
    }
    public void AppendText(string text) {
        if (history.LastIndexOf("$") != -1) history = history.Substring(0, history.LastIndexOf("$"));
        history += text;
        RenderText(history);
    }
    public void ClearText() {
        if (history == "") history = "$";
        InputField.text = "☄ BitNaughts";
        MapScreenPanOverlay.SetActive(true);
        RenderText(history);
    }
    public void ClearHistory() {
        history = "$";
        RenderText(history);
    }
    public void PrintMock() {
        Example.SetActive(true);
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
        SetContentSize(25f + max_line_length * 37.5f, 50f + lines.Length * 75f);
    }
    public string component_name = "";
    public string component_text = "";
    public void RenderComponent(string component) {
        var component_string = Ship.GetComponentToString(component);
        var component_header = component_string.IndexOf("\n");
        component_name = component_string.Substring(0, component_header);
        InputField.text = component_name;
        component_text = component_string.Substring(component_header + 1);
        // RenderText(component_text);
        RenderText(Ship.interpreter.ToString());
    }

    public string[] GetComponents() {
        return Ship.GetControllers();
    }
    void SetContentSize(float width,float height) {
        // print ("Content.sizeDelta(" + width + ", " + height + ")");
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }
    public void SetInputPlaceholder(string placeholder) {
        InputField.text = "";//placeholder;
    }
    public string GetInput() {
        return InputField.text;
    }
    public void OnInput() {
        //create new component...
        // InputField.interactable = false;
        switch (GetCommand()) {
            case "nano":
                var component_gameObject = Instantiate(Overlay, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                component_gameObject.name = InputField.text;//object_reference.name + component_count;
                component_gameObject.GetComponent<SpriteRenderer>().size = new Vector2(2,2);//object_reference.GetComponent<ComponentController>().GetMinimumSize();
                component_gameObject.transform.SetParent(Ship.transform.Find("Rotator"));
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
        // InputFieldPlaceholder.text = "";
    }
    public string GetCommand() {
        return command;
    }
    public void SetCommand(string command) {
        if (command == "$") {
            onLoad = false;
            ResetFlash("Clickable$", 0f);
        }
        this.command = command;
    }

    float timer = 0f;
    bool onLoad = true, aboutIntro = false, tutorialIntro = false, tutorialPan = false, tutorialTarget = false, tutorialFire = false, tutorialCancel = false, tutorialThrust = false, tutorialFinish = false, tutorialComplete = false;
    public bool TutorialRunning() {
        return tutorialIntro || tutorialPan || tutorialTarget || tutorialFire || tutorialCancel || tutorialThrust || tutorialFinish;
    }
    public void StartTutorial() {
        if (tutorialIntro == false) {
            onLoad = false;
            aboutIntro = false;
            tutorialIntro = true;
            tutorialPan = false;
            tutorialTarget = false;
            tutorialFire = false;
            tutorialCancel = false;
            tutorialThrust = false;
            tutorialFinish = false;
            timer = 0;
            global_timer = 0;
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
            onLoad = true;
            timer = 0;
            animation_timer = 0;
            MapSubtitlesAtTime("", 0f);
            SubtitlesAtTime("$ tutorial\n$", 0f);
            Sound("WarningOver");
            GetComponent<AudioSource>().Stop();
        }
    }
    public void Action(string name, int action) {
        GameObject.Find(name).GetComponent<ComponentController>().Action(action);
    }
    public void Sound(string clip) {
        switch (clip) {
            case "Back": Sound(SoundBack); break;
            case "Click": Sound(SoundClick); break;
            case "Error": Sound(SoundError); break;
            case "OnMouse": Sound(SoundOnMouse); break;
            case "Toggle": Sound(SoundToggle); break;
            case "Processor": Sound(SoundProcessor); break;
            case "Gimbal": Sound(SoundGimbal); break;
            case "Cannon1": Sound(SoundCannon1); break;
            case "Cannon2": Sound(SoundCannon2); break;
            case "Cannon3": Sound(SoundCannon3); break;
            case "Radar": Sound(SoundRadar); break;
            case "Booster": Sound(SoundBooster); break;
            case "Thruster": Sound(SoundThruster); break;
            case "Torpedo1": Sound(SoundTorpedo1); break;
            case "Torpedo2": Sound(SoundTorpedo2); break;
            case "Warning": Sound(SoundWarning); break;
            case "WarningOver": Sound(SoundWarningOver); break;
        }
    }

    public void PlayTheme() {
        timer = 0;
        global_timer = 0; 
        PlayThemeMusic();
        aboutIntro = true;
    }
    public void PlayThemeMusic() {
        GameObject.Find("World").GetComponent<AudioSource>().clip = ThemeSong;
        GameObject.Find("World").GetComponent<AudioSource>().volume = .05f;
        GameObject.Find("World").GetComponent<AudioSource>().Play();
    }
    public bool IsIntroCompleted() {
        return aboutIntro;
    }
    public void PlayGimbal() {
        Play(GimbalRotate);
    }
    public void PlayCannon() {
        Play(CannonFire);
    }
    public void PlayTorpedo() {
        Play(TorpedoLaunch);
    }
    public void PlayThruster() {
        Play(ThrusterThrottle);
    }
    public void PlayRadar() {
        Play(SonarScan);
    }
    public void PlayProcessor() {
        Play(ProcessorPing);
    }
    public void PlayClick() {
        Play(Click);
    }
    public void PlayClick2() {
        Play(Click2);
    }
    public void PlayAtTime(AudioClip clip, float time) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            Play(clip);
        }
    }
    public void Play(AudioClip clip) {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().volume = 1f;
        GetComponent<AudioSource>().Play();
    }
    public void Sound(AudioClip clip) {
        if (clip == null) {
            return;
        }
        if (camera == null) {
            camera = GameObject.Find("Main Camera");
        }
        camera.GetComponent<AudioSource>().clip = clip;
        camera.GetComponent<AudioSource>().volume = .5f;
        camera.GetComponent<AudioSource>().Play();
    }
    void MapSubtitlesAtTime(string text, float time, float timer) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            Subtitles.GetComponent<Text>().text = text + "\n";
        }
    }
    void SubtitlesAtTime(string text, float time) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            // GameObject.Find("Subtitles").GetComponent<Text>().text = text + "\n";
            RenderText(text);
        }
    }
    void MapSubtitlesAtTime(string text, float time) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            Subtitles.GetComponent<Text>().text = text + "\n";
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
        var mm = (Mathf.Floor(time / 60) % 60).ToString().TrimStart('0');
        if (mm == "") return ss + "." + pt;
        return mm + ":" + ss + "." + pt;
    }
    void Update () {
        animation_timer += Time.deltaTime;
        if (TutorialRunning()) {
            Timer.text = "" + FloatToTime(global_timer);
        } else if (aboutIntro) {
            Timer.text = "" + FloatToTime(global_timer);
        }  else {
            // text = "            " + System.DateTime.Now.ToString("hh:mm:ss tt") + "\n" + text;  
            // text = "            " + FloatToTime(global_timer) + "\n" + text; 
            // SplitTimer.text = FloatToTime(timer);
            // SplitTimerShadow.text = FloatToTime(timer);
            // SplitTimer.text = "";
            // SplitTimerShadow.text = "";
            // TimerShadow.text = FloatToTime(global_timer);
            if (animation_timer < 2f) {
                Timer.color = new Color(.5f + (animation_timer * 2) % 1, .5f + (animation_timer * 2) % 1, 0, 1f);
            } else if (animation_timer < 2.1f) {
                Timer.color = new Color(1, 1, 1, 1);
            }
            else if ((animation_timer / 10f ) % 2 < 1) {
                Timer.text = "Time " + System.DateTime.Now.ToString("hh:mm:ss");
            } else {
                Timer.text = "Ping " + camera.GetComponent<MultiplayerController>().Delay.ToString("ss\\.fffff");
            }
        }
        if (Input.GetMouseButton(0)) {
            click_duration += Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0)) {
            click_duration = 0;
        }
    }
    void SpriteFlash(string name, float start) {
        if (timer > start ) { 
            if (GameObject.Find(name) != null) GameObject.Find(name).GetComponent<SpriteRenderer>().color = new Color(.5f + (timer * 2) % 1, .5f + (timer * 2) % 1, 0, 1f);
        }
    }
    void Flash(string name, float start) {
        if (timer > start) { 
            if (GameObject.Find(name) != null) GameObject.Find(name).GetComponent<Image>().color = new Color(.5f + (timer * 2) % 1, .5f + (timer * 2) % 1, 0, 1f);
        }
    }
    void ResetSpriteFlash(string name, float time) {
        if (timer > time) { 
            if (GameObject.Find(name) != null) GameObject.Find(name).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
    void ResetFlash(string name, float time) {
        if (timer > time) { 
            if (GameObject.Find(name) != null) GameObject.Find(name).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
    string[] clips = new string[] { "RadioDays", "Nuclear", "Electricity", "Weapons", "Television", "Recording", "Music", "Medicine", "Hardness", "Finale", "" };
    int[] clip_durations = new int[] { 0, 81, 79, 64, 46, 79, 74, 107, 95, 116, 51, 1 };
    int clip_index = 0;
    void FixedUpdate()
    {
        if (story_timer > -1) {
            // if (story_timer == 0) story_timer = 59;
            //prefix + "RadioDays" + postfix
            if ((story_timer > 1 && Input.GetMouseButton(0)) || (story_timer > clip_durations[clip_index] && story_timer < clip_durations[clip_index] + (Time.deltaTime * 2f))) {
                GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().url = System.IO.Path.Combine (Application.streamingAssetsPath, "BitNaughts" + clips[clip_index] + "480p.mp4"); 
                GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().Play();
                story_timer = 0;
                clip_index++;
                if (clip_index >= 11) {
                    clip_index = 0;
                    Subtitles.SetActive(false);
                    SubtitlesShadow.SetActive(false);
                    OnCodeView();
                    RenderText("$ story\n$");
                    story_timer = -1f;
                    GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().enabled = false;
                }
            }
            story_timer += Time.deltaTime;
            MapSubtitlesAtTime("⛈", 0, story_timer);
            MapSubtitlesAtTime("⛅", 2.25f, story_timer);
            MapSubtitlesAtTime("♩", 3.5f, story_timer);
            MapSubtitlesAtTime("♩♩", 4f, story_timer);
            MapSubtitlesAtTime("♪", 4.5f, story_timer);
            MapSubtitlesAtTime("♪♪", 5f, story_timer);
            MapSubtitlesAtTime("♩", 5.5f, story_timer);
            MapSubtitlesAtTime("♩♫", 6f, story_timer);
            MapSubtitlesAtTime("♫♫", 6.5f, story_timer);
            MapSubtitlesAtTime("♫♪", 7f, story_timer);
            switch (clip_index) {
                case 1:
                    MapSubtitlesAtTime("The voices we used to", 7.5f, story_timer);
                    MapSubtitlesAtTime("hear on the radio:", 8.5f, story_timer);
                    MapSubtitlesAtTime("☄", 10f, story_timer);
                    MapSubtitlesAtTime("Be sure and", 11f, story_timer);
                    MapSubtitlesAtTime("tune in tomorrow", 12f, story_timer);
                    MapSubtitlesAtTime("for another adventure", 13f, story_timer);
                    MapSubtitlesAtTime("of the \"Masked Avenger\"!", 14f, story_timer);
                    MapSubtitlesAtTime("When he flies over", 15.5f, story_timer);
                    MapSubtitlesAtTime("the city rooftops", 16.5f, story_timer);
                    MapSubtitlesAtTime("we all hear his cry:", 17.75f, story_timer);
                    MapSubtitlesAtTime("\"Beware evildoers,\"", 20, story_timer);
                    MapSubtitlesAtTime("\"wherever you are!\"", 22f, story_timer);
                    MapSubtitlesAtTime("♪", 23.25f, story_timer);
                    MapSubtitlesAtTime("♪♪", 23.5f, story_timer);
                    MapSubtitlesAtTime("♪♪♪", 23.75f, story_timer);
                    MapSubtitlesAtTime("♪", 24.25f, story_timer);
                    MapSubtitlesAtTime("♪♩", 25f, story_timer);
                    MapSubtitlesAtTime("⛈", 27.5f, story_timer);
                    MapSubtitlesAtTime("I recall so many", 28.5f, story_timer);
                    MapSubtitlesAtTime("personal experiences", 29.5f, story_timer);
                    MapSubtitlesAtTime("from when I grew up", 30.5f, story_timer);
                    MapSubtitlesAtTime("listened to one", 31.5f, story_timer);
                    MapSubtitlesAtTime("show after another.", 32.5f, story_timer);
                    MapSubtitlesAtTime("⛅", 33.5f, story_timer);
                    MapSubtitlesAtTime("This girl singing", 34.25f, story_timer);
                    MapSubtitlesAtTime("used to be a favorite", 35.5f, story_timer);
                    MapSubtitlesAtTime("at my house.", 36.25f, story_timer);
                    MapSubtitlesAtTime("One of many.", 37.25f, story_timer);
                    MapSubtitlesAtTime("Now it's all gone.", 38.25f, story_timer);
                    MapSubtitlesAtTime("Except for the memories...", 39.75f, story_timer);
                    MapSubtitlesAtTime("Pay more attention", 41.25f, story_timer);
                    MapSubtitlesAtTime("to your school work", 42.25f, story_timer);
                    MapSubtitlesAtTime("and less to the radio!", 43.25f, story_timer);
                    MapSubtitlesAtTime("You always listen to the radio!", 44.25f, story_timer);
                    MapSubtitlesAtTime("It's different!", 46f, story_timer);
                    MapSubtitlesAtTime("Our lives are ruined already!", 47.5f, story_timer);
                    MapSubtitlesAtTime("⛅", 49f, story_timer);
                    // MapSubtitlesAtTime("♪", 50f, story_timer);
                    // MapSubtitlesAtTime("♫", 51f, story_timer);
                    MapSubtitlesAtTime("There are those who", 54f, story_timer);
                    MapSubtitlesAtTime("drink champange at nightclubs,", 55.5f, story_timer);
                    MapSubtitlesAtTime("and us who listen to them", 57.25f, story_timer);
                    MapSubtitlesAtTime("drink champange on the radio!", 58.5f, story_timer);
                    MapSubtitlesAtTime("⛈", 59.5f, story_timer);
                    MapSubtitlesAtTime("We interrupt this program", 60f, story_timer);
                    MapSubtitlesAtTime("To bring you a special news bulletin!", 61.25f, story_timer);
                    MapSubtitlesAtTime("The landing of hundreds", 62.75f, story_timer);
                    MapSubtitlesAtTime("of unidentified spacecraft", 64f, story_timer);
                    MapSubtitlesAtTime("have now been officially confirmed", 65.5f, story_timer);
                    MapSubtitlesAtTime("as a full scale invasion", 67.5f, story_timer);
                    MapSubtitlesAtTime("of the Earth by Martians!", 68.5f, story_timer);
                    MapSubtitlesAtTime("Power lines are down everywhere!", 70f, story_timer);
                    MapSubtitlesAtTime("We could be cut off at any minute!", 72f, story_timer);
                    MapSubtitlesAtTime("\"Oh my gosh!\"", 75f, story_timer);
                    MapSubtitlesAtTime("There's another group of spaceships!", 77.5f, story_timer);
                    MapSubtitlesAtTime("Of alien ships!", 78.825f, story_timer);
                    MapSubtitlesAtTime("They're coming out of the sky!", 79.75f, story_timer);
                    MapSubtitlesAtTime("⛈", 80.75f, story_timer);
                    break;
                case 2:
                    MapSubtitlesAtTime("⛅", 8f, story_timer);
                    // MapSubtitlesAtTime("♪", 8f, story_timer);
                    // MapSubtitlesAtTime("♫", 10f, story_timer);
                    MapSubtitlesAtTime("\"Neutron\",", 32.5f - 12.25f, story_timer);
                    MapSubtitlesAtTime("\"Gamma Rays\",", 33.5f  - 12.25f, story_timer);
                    MapSubtitlesAtTime("\"Solar Power\",", 34.5f  - 12.25f, story_timer);
                    MapSubtitlesAtTime("\"Transistor\",", 36f - 12.25f, story_timer);
                    MapSubtitlesAtTime("\"Automation\".", 37.5f  - 12.25f, story_timer);
                    MapSubtitlesAtTime("A new language", 39f - 12.25f, story_timer);
                    MapSubtitlesAtTime("has come into currency.", 39.75f - 12.25f, story_timer);
                    MapSubtitlesAtTime("To the public,", 41f - 12.25f, story_timer);
                    MapSubtitlesAtTime("it is a language of the future.", 42f - 12.25f, story_timer);
                    MapSubtitlesAtTime("To the scientist,", 43.5f - 12.25f, story_timer);
                    MapSubtitlesAtTime("a language of the present.", 44.5f - 12.25f, story_timer);
                    MapSubtitlesAtTime("This then is a report", 46f - 12.25f, story_timer);
                    MapSubtitlesAtTime("on our present future.", 47.5f - 12.25f, story_timer);
                    MapSubtitlesAtTime("Some of it profound...", 49.25f - 12.25f, story_timer);
                    MapSubtitlesAtTime("some of it mere gadgetry...", 51f - 12.25f, story_timer);
                    MapSubtitlesAtTime("You are looking now", 53f - 12.25f, story_timer);
                    MapSubtitlesAtTime("at a nuclear reactor.", 54f - 12.25f, story_timer);
                    MapSubtitlesAtTime("It is not producing a bomb.", 55.5f - 12.25f, story_timer);
                    MapSubtitlesAtTime("It can produce electricity.", 57f - 12.25f, story_timer);
                    MapSubtitlesAtTime("In a pilot", 60f - 12.25f, story_timer);
                    MapSubtitlesAtTime("\"Atomic Power Plant\"", 60.75f - 12.25f, story_timer);
                    MapSubtitlesAtTime("in the desert", 61.5f - 12.25f, story_timer);
                    MapSubtitlesAtTime("the lights go on!", 62.5f - 12.25f, story_timer);
                    MapSubtitlesAtTime("Nuclear energy", 64f - 12.25f, story_timer);
                    MapSubtitlesAtTime("goes to work!", 65.5f - 12.25f, story_timer);
                    MapSubtitlesAtTime("Not destroying", 66.5f - 12.25f, story_timer);
                    MapSubtitlesAtTime("but serving mankind!", 67.5f - 12.25f, story_timer);
                    MapSubtitlesAtTime("⛅", 70 - 12.25f, story_timer);
                    // MapSubtitlesAtTime("♫", 71f - 12.25f, story_timer);
                    MapSubtitlesAtTime("The power lines of tomorrow", 74f - 12.25f, story_timer);
                    MapSubtitlesAtTime("may also derive their electricity", 75.5f - 12.25f, story_timer);
                    MapSubtitlesAtTime("from that source of all power", 76.75f - 12.25f, story_timer);
                    MapSubtitlesAtTime("the sun!", 79f - 12.25f, story_timer);
                    MapSubtitlesAtTime("⛅", 81f - 12.25f, story_timer);
                    // MapSubtitlesAtTime("♫", 82f - 12.25f, story_timer);
                    MapSubtitlesAtTime("⛈", 82f - 12.25f, story_timer);
                    break;
                case 3:
                    MapSubtitlesAtTime("⛅", 8f, story_timer);
                    MapSubtitlesAtTime("Along with the \"Atom\",", 8.5f, story_timer);
                    MapSubtitlesAtTime("and the \"Sun\",", 9.5f, story_timer);
                    MapSubtitlesAtTime("The \"Electron\" opens", 10.25f, story_timer);
                    MapSubtitlesAtTime("a major highway", 11.5f, story_timer);
                    MapSubtitlesAtTime("to this present future.", 12.5f, story_timer);
                    MapSubtitlesAtTime("In the \"Electronics Age\",", 14.5f, story_timer);
                    MapSubtitlesAtTime("the development of", 16f, story_timer);
                    MapSubtitlesAtTime("giant computers:", 17f, story_timer);
                    MapSubtitlesAtTime("\"Electronic Brains\",", 18f, story_timer);
                    MapSubtitlesAtTime("has been a key development.", 19.25f, story_timer);
                    MapSubtitlesAtTime("These incredibly", 21f, story_timer);
                    MapSubtitlesAtTime("complex machines", 22f, story_timer);
                    MapSubtitlesAtTime("are the mechanized", 23.25f, story_timer);
                    MapSubtitlesAtTime("geniuses of the", 24.5f, story_timer);
                    MapSubtitlesAtTime("twentieth century.", 25.35f, story_timer);
                    MapSubtitlesAtTime("They store information.", 26.5f, story_timer);
                    MapSubtitlesAtTime("Their memory is", 28f, story_timer);
                    MapSubtitlesAtTime("infallable.", 29f, story_timer);
                    MapSubtitlesAtTime("This ability has", 30.25f, story_timer);
                    MapSubtitlesAtTime("started a second", 31.25f, story_timer);
                    MapSubtitlesAtTime("Industrial Revolution:", 32.25f, story_timer);
                    MapSubtitlesAtTime("\"Automation\".", 33.5f, story_timer);
                    MapSubtitlesAtTime("The highly controversial", 34.5f, story_timer);
                    MapSubtitlesAtTime("\"Automatic Factory\".", 36.25f, story_timer);
                    MapSubtitlesAtTime("In this engine", 37.75f, story_timer);
                    MapSubtitlesAtTime("block assembly,", 38.75f, story_timer);
                    MapSubtitlesAtTime("thousands of precision", 40f, story_timer);
                    MapSubtitlesAtTime("operations are", 41f, story_timer);
                    MapSubtitlesAtTime("performed with", 42f, story_timer);
                    MapSubtitlesAtTime("\"Electronic Brainpower\"", 43f, story_timer);
                    MapSubtitlesAtTime("replacing manpower.", 44f, story_timer);
                    MapSubtitlesAtTime("Only a token", 45.5f, story_timer);
                    MapSubtitlesAtTime("workforce is needed:", 46.5f, story_timer);
                    MapSubtitlesAtTime("For maintenance", 47.725f, story_timer);
                    MapSubtitlesAtTime("and supervision.", 48.5f, story_timer);
                    MapSubtitlesAtTime("⛅", 50.25f, story_timer);
                    MapSubtitlesAtTime("⛈", 54.5f, story_timer);
                    break;
                case 4:
                    MapSubtitlesAtTime("⛅", 8f, story_timer);
                    MapSubtitlesAtTime("Without electronic", 8.25f, story_timer);
                    MapSubtitlesAtTime("control systems", 9.25f, story_timer);
                    MapSubtitlesAtTime("No nation could", 10.5f, story_timer);
                    MapSubtitlesAtTime("defend itself", 11.25f, story_timer);
                    MapSubtitlesAtTime("in modern war.", 12f, story_timer);
                    MapSubtitlesAtTime("Here is a new and", 13.25f, story_timer);
                    MapSubtitlesAtTime("striking example!", 14.25f, story_timer);
                    MapSubtitlesAtTime("A guided missile", 15.75f, story_timer);
                    MapSubtitlesAtTime("with a brain capable", 16.5f, story_timer);
                    MapSubtitlesAtTime("of outwitting", 17.5f, story_timer);
                    MapSubtitlesAtTime("any enemy bomber", 18.25f, story_timer);
                    MapSubtitlesAtTime("as shown in this", 19.5f, story_timer);
                    MapSubtitlesAtTime("graphic representation.", 20.5f, story_timer);
                    MapSubtitlesAtTime("Once the target has", 22.25f, story_timer);
                    MapSubtitlesAtTime("been pointed out to it,", 23.25f, story_timer);
                    MapSubtitlesAtTime("The missile's electronic", 24.25f, story_timer);
                    MapSubtitlesAtTime("intelligence", 25.5f, story_timer);
                    MapSubtitlesAtTime("will steer it", 26.325f, story_timer);
                    MapSubtitlesAtTime("to that target", 27f, story_timer);
                    MapSubtitlesAtTime("no matter how", 28f, story_timer);
                    MapSubtitlesAtTime("the bomber maneuvers.", 29f, story_timer);
                    MapSubtitlesAtTime("⛅", 31f, story_timer);
                    MapSubtitlesAtTime("⛈", 36.5f, story_timer);
                    break;
                case 5:
                    MapSubtitlesAtTime("⛅", 7.5f, story_timer);
                    MapSubtitlesAtTime("Here is an actual", 7.75f, story_timer);
                    MapSubtitlesAtTime("shot destroying", 8.75f, story_timer);
                    MapSubtitlesAtTime("a drone plane!", 9.75f, story_timer);
                    MapSubtitlesAtTime("⛅", 11f, story_timer);
                    MapSubtitlesAtTime("A major electronics", 62.75f, story_timer);
                    MapSubtitlesAtTime("development has been", 63.75f, story_timer);
                    MapSubtitlesAtTime("\"Television\"!", 64.75f, story_timer);
                    MapSubtitlesAtTime("For science.", 65.5f, story_timer);
                    MapSubtitlesAtTime("As well as", 66.25f, story_timer);
                    MapSubtitlesAtTime("entertainment.", 66.75f, story_timer);
                    MapSubtitlesAtTime("Through T.V.", 68.25f, story_timer);
                    MapSubtitlesAtTime("⛈", 68.825f, story_timer);
                    break;
                case 6:
                    MapSubtitlesAtTime("⛅", 7.25f, story_timer);
                    MapSubtitlesAtTime("In the laboratory,", 7.5f, story_timer);
                    MapSubtitlesAtTime("a \"Television Camera\"", 8.5f, story_timer);
                    MapSubtitlesAtTime("rigged up to", 9.5f, story_timer);
                    MapSubtitlesAtTime("a microscope", 10.25f, story_timer);
                    MapSubtitlesAtTime("allows a scientist", 11f, story_timer);
                    MapSubtitlesAtTime("to get a big screen", 12f, story_timer);
                    MapSubtitlesAtTime("picture of the", 13f, story_timer);
                    MapSubtitlesAtTime("highly magnified field", 14f, story_timer);
                    MapSubtitlesAtTime("without the usual", 15.5f, story_timer);
                    MapSubtitlesAtTime("squint into the eyepiece", 16.5f, story_timer);
                    MapSubtitlesAtTime("⛅", 19.75f, story_timer);
                    MapSubtitlesAtTime("⛈", 24f, story_timer);
                    MapSubtitlesAtTime("For entertainment", 25f, story_timer);
                    MapSubtitlesAtTime("television,", 26f, story_timer);
                    MapSubtitlesAtTime("\"Magnetic Videotape\"", 27f, story_timer);
                    MapSubtitlesAtTime("prmoises great things!", 28.5f, story_timer);
                    MapSubtitlesAtTime("Most people are familiar", 30.5f, story_timer);
                    MapSubtitlesAtTime("with sound recording", 31.5f, story_timer);
                    MapSubtitlesAtTime("on tape.", 32.5f, story_timer);
                    MapSubtitlesAtTime("This device records", 33.5f, story_timer);
                    MapSubtitlesAtTime("pictures on tape!", 34.5f, story_timer);
                    MapSubtitlesAtTime("In full compatible color", 36f, story_timer);
                    MapSubtitlesAtTime("or in black and white.", 37.5f, story_timer);
                    MapSubtitlesAtTime("As well as the", 38.75f, story_timer);
                    MapSubtitlesAtTime("programmed sound.", 39.5f, story_timer);
                    MapSubtitlesAtTime("The magnetic tape is", 41f, story_timer);
                    MapSubtitlesAtTime("half an inch wide", 42f, story_timer);
                    MapSubtitlesAtTime("and runs at", 43f, story_timer);
                    MapSubtitlesAtTime("twenty feet a second.", 44f, story_timer);
                    MapSubtitlesAtTime("A program can be", 45.25f, story_timer);
                    MapSubtitlesAtTime("recorded and played", 46.5f, story_timer);
                    MapSubtitlesAtTime("back at any time.", 47.5f, story_timer);
                    MapSubtitlesAtTime("Immediately,", 48.75f, story_timer);
                    MapSubtitlesAtTime("if desired.", 49.5f, story_timer);
                    MapSubtitlesAtTime("Without any laboratory", 50.5f, story_timer);
                    MapSubtitlesAtTime("processing.", 51.5f, story_timer);
                    MapSubtitlesAtTime("In motion pictures", 53f, story_timer);
                    MapSubtitlesAtTime("or video production,", 54f, story_timer);
                    MapSubtitlesAtTime("a director will be", 55f, story_timer);
                    MapSubtitlesAtTime("able to shoot", 55.75f, story_timer);
                    MapSubtitlesAtTime("a scene and", 56.25f, story_timer);
                    MapSubtitlesAtTime("play it back at once.", 57f, story_timer);
                    MapSubtitlesAtTime("Right on the set!", 58f, story_timer);
                    MapSubtitlesAtTime("⛅", 60f, story_timer);
                    MapSubtitlesAtTime("⛈", 64f, story_timer);
                    break;
                case 7:
                    MapSubtitlesAtTime("⛅", 8f, story_timer);
                    MapSubtitlesAtTime("In another field,", 10f, story_timer);
                    MapSubtitlesAtTime("Music can now be", 11f, story_timer);
                    MapSubtitlesAtTime("produced entirely", 12f, story_timer);
                    MapSubtitlesAtTime("by electronics!", 13f, story_timer);
                    MapSubtitlesAtTime("No known instruments!", 14f, story_timer);
                    MapSubtitlesAtTime("are involved.", 15.5f, story_timer);
                    MapSubtitlesAtTime("Coded information", 16.5f, story_timer);
                    MapSubtitlesAtTime("is punched out.", 17.5f, story_timer);
                    MapSubtitlesAtTime("An electronic", 18.5f, story_timer);
                    MapSubtitlesAtTime("\"Music Synthesizer\"", 19.5f, story_timer);
                    MapSubtitlesAtTime("does the rest!", 20.5f, story_timer);
                    MapSubtitlesAtTime("⛅", 21.5f, story_timer);
                    MapSubtitlesAtTime("This is music", 32.5f, story_timer);
                    MapSubtitlesAtTime("with a strictly", 33.5f, story_timer);
                    MapSubtitlesAtTime("electronic beat!", 34.5f, story_timer);
                    MapSubtitlesAtTime("⛅", 35.5f, story_timer);
                    MapSubtitlesAtTime("This is a transistor.", 61.75f, story_timer);
                    MapSubtitlesAtTime("It is the tiny bombshell", 63.25f, story_timer);
                    MapSubtitlesAtTime("of the electronics", 64.75f, story_timer);
                    MapSubtitlesAtTime("revolution.", 65.75f, story_timer);
                    MapSubtitlesAtTime("What it does,", 66.25f, story_timer);
                    MapSubtitlesAtTime("simply stated,", 67.25f, story_timer);
                    MapSubtitlesAtTime("is to replace", 68f, story_timer);
                    MapSubtitlesAtTime("vacuum tubes", 69f, story_timer);
                    MapSubtitlesAtTime("in many applications.", 70f, story_timer);
                    MapSubtitlesAtTime("It is an essential", 71.5f, story_timer);
                    MapSubtitlesAtTime("to modern electronic", 72.5f, story_timer);
                    MapSubtitlesAtTime("circuitry.", 73.5f, story_timer);
                    MapSubtitlesAtTime("It has many advantages:", 74.5f, story_timer);
                    MapSubtitlesAtTime("small size for one,", 76f, story_timer);
                    MapSubtitlesAtTime("permitting miniaturization,", 77.5f, story_timer);
                    MapSubtitlesAtTime("making big things smaller.", 79.5f, story_timer);
                    MapSubtitlesAtTime("Things like:", 81.5f, story_timer);
                    MapSubtitlesAtTime("\"Pocket Radios\",", 82.5f, story_timer);
                    MapSubtitlesAtTime("⛅", 84f, story_timer);
                    MapSubtitlesAtTime("\"Wristwatch Radios\",", 87.5f, story_timer);
                    MapSubtitlesAtTime("and a coming attraction:", 90f, story_timer);
                    MapSubtitlesAtTime("portable battery powered", 91.5f, story_timer);
                    MapSubtitlesAtTime("\"Television Sets\"!", 93.5f, story_timer);
                    MapSubtitlesAtTime("⛅", 86f, story_timer);
                    MapSubtitlesAtTime("⛈", 87f, story_timer);
                    break;
                case 8:
                    MapSubtitlesAtTime("⛅", 8f, story_timer);
                    MapSubtitlesAtTime("This man is starting to", 8.25f, story_timer);
                    MapSubtitlesAtTime("make a delivery for the", 9.25f, story_timer);
                    MapSubtitlesAtTime("\"Atomic Drug Store\"", 10.25f, story_timer);
                    MapSubtitlesAtTime("in Oak Ridge, Tennessee.", 11.25f, story_timer);
                    MapSubtitlesAtTime("This is one pharmacy", 13f, story_timer);
                    MapSubtitlesAtTime("that carries no", 14.5f, story_timer);
                    MapSubtitlesAtTime("aspirins,", 15.25f, story_timer);
                    MapSubtitlesAtTime("soda mints", 16f, story_timer);
                    MapSubtitlesAtTime("or cough drops.", 17f, story_timer);
                    MapSubtitlesAtTime("The merchandise", 18f, story_timer);
                    MapSubtitlesAtTime("is radioactive", 19f, story_timer);
                    MapSubtitlesAtTime("which explains the", 20f, story_timer);
                    MapSubtitlesAtTime("ingenious methods", 21.25f, story_timer);
                    MapSubtitlesAtTime("of handling.", 22.125f, story_timer);
                    MapSubtitlesAtTime("The prioprietor is the", 23.125f, story_timer);
                    MapSubtitlesAtTime("\"Atomic Energy Commission.\"", 24.125f, story_timer);
                    MapSubtitlesAtTime("These drugs and", 26f, story_timer);
                    MapSubtitlesAtTime("chemicals are known as", 27f, story_timer);
                    MapSubtitlesAtTime("\"Radio Isotopes\".", 28.5f, story_timer);
                    MapSubtitlesAtTime("At the end of the line,", 29.5f, story_timer);
                    MapSubtitlesAtTime("they will be shipped", 31f, story_timer);
                    MapSubtitlesAtTime("out of here in lead-", 32f, story_timer);
                    MapSubtitlesAtTime("shielded crates to", 33f, story_timer);
                    MapSubtitlesAtTime("doctors and research", 34f, story_timer);
                    MapSubtitlesAtTime("scientists in many", 35f, story_timer);
                    MapSubtitlesAtTime("parts of the world.", 36f, story_timer);
                    MapSubtitlesAtTime("The labels are strange:", 37.5f, story_timer);
                    MapSubtitlesAtTime("\"Radio Iron\",", 39.5f, story_timer);
                    MapSubtitlesAtTime("\"Radio Gold\",", 40.5f, story_timer);
                    MapSubtitlesAtTime("\"Radio Phosphorous\",", 41.75f, story_timer);
                    MapSubtitlesAtTime("\"Radio Iodine\",", 43f, story_timer);
                    MapSubtitlesAtTime("\"Radio Strontium\",", 44.5f, story_timer);
                    MapSubtitlesAtTime("\"Radio Yttrium\".", 45.5f, story_timer);
                    MapSubtitlesAtTime("You measure them", 47f, story_timer);
                    MapSubtitlesAtTime("not by ounces or grams,", 48f, story_timer);
                    MapSubtitlesAtTime("but by millicurries.", 49.5f, story_timer);
                    MapSubtitlesAtTime("How many millions of", 51.5f, story_timer);
                    MapSubtitlesAtTime("atoms disintegrate", 52.5f, story_timer);
                    MapSubtitlesAtTime("within one second.", 53.5f, story_timer);
                    MapSubtitlesAtTime("They are helping to", 55f, story_timer);
                    MapSubtitlesAtTime("save thousands of lives", 56f, story_timer);
                    MapSubtitlesAtTime("every year,", 57.35f, story_timer);
                    MapSubtitlesAtTime("are advancing medical", 58.5f, story_timer);
                    MapSubtitlesAtTime("sciencies, as well as", 59.75f, story_timer);
                    MapSubtitlesAtTime("research in biology,", 60.5f, story_timer);
                    MapSubtitlesAtTime("industry and agriculture.", 62f, story_timer);
                    MapSubtitlesAtTime("The most effective", 64f, story_timer);
                    MapSubtitlesAtTime("research tools since the", 65f, story_timer);
                    MapSubtitlesAtTime("invention of the microscope,", 66f, story_timer);
                    MapSubtitlesAtTime("they hold great promise", 67.5f, story_timer);
                    MapSubtitlesAtTime("for the future!", 68.75f, story_timer);
                    MapSubtitlesAtTime("⛅", 70f, story_timer);
                    MapSubtitlesAtTime("⛈", 84f, story_timer);
                    break;
                case 9:
                    MapSubtitlesAtTime("⛅", 8f, story_timer);
                    MapSubtitlesAtTime("The aparatus of", 9f, story_timer);
                    MapSubtitlesAtTime("the \"Atomic Age\"", 9.75f, story_timer);
                    MapSubtitlesAtTime("is enormously ingenious!", 10.5f, story_timer);
                    MapSubtitlesAtTime("At Argon National", 12f, story_timer);
                    MapSubtitlesAtTime("Laboratory in Chicago,", 13f, story_timer);
                    MapSubtitlesAtTime("metallurgists use a", 14.5f, story_timer);
                    MapSubtitlesAtTime("\"Master Slave Manipulator\"", 16f, story_timer);
                    MapSubtitlesAtTime("to work with radioactive", 17.75f, story_timer);
                    MapSubtitlesAtTime("metals. This manipulator", 19.25f, story_timer);
                    MapSubtitlesAtTime("can do anything", 21f, story_timer);
                    MapSubtitlesAtTime("human hands can do!", 22f, story_timer);
                    MapSubtitlesAtTime("As you watch,", 23.5f, story_timer);
                    MapSubtitlesAtTime("the operator,", 24.5f, story_timer);
                    MapSubtitlesAtTime("shielded by three feet", 25.5f, story_timer);
                    MapSubtitlesAtTime("of glass and concrete,", 26.5f, story_timer);
                    MapSubtitlesAtTime("conducts an involved,", 28f, story_timer);
                    MapSubtitlesAtTime("\"Metal Hardness Test\",", 29.35f, story_timer);
                    MapSubtitlesAtTime("relying entirely", 30.5f, story_timer);
                    MapSubtitlesAtTime("on his nimble", 32.25f, story_timer);
                    MapSubtitlesAtTime("mechanical fingers.", 33.15f, story_timer);
                    MapSubtitlesAtTime("⛅", 35f, story_timer);
                    MapSubtitlesAtTime("⛈", 105f, story_timer);
                    break;
                case 10:
                    MapSubtitlesAtTime("⛅", 8f, story_timer);
                    MapSubtitlesAtTime("In the final", 9f, story_timer);
                    MapSubtitlesAtTime("analysis however", 10f, story_timer);
                    MapSubtitlesAtTime("the key to the future", 11f, story_timer);
                    MapSubtitlesAtTime("is not an aparatus,", 12f, story_timer);
                    MapSubtitlesAtTime("a machine or an", 13.75f, story_timer);
                    MapSubtitlesAtTime("electronic cube", 15f, story_timer);
                    MapSubtitlesAtTime("but the brainpower", 16f, story_timer);
                    MapSubtitlesAtTime("of man.", 17f, story_timer);
                    MapSubtitlesAtTime("Nothing will ever replace", 18.5f, story_timer);
                    MapSubtitlesAtTime("\"Creative Intelligence\".", 20f, story_timer);
                    MapSubtitlesAtTime("In great laboratories,", 21.5f, story_timer);
                    MapSubtitlesAtTime("in colleges and universities,", 23.25f, story_timer);
                    MapSubtitlesAtTime("in solitary quiet,", 25f, story_timer);
                    MapSubtitlesAtTime("man thinks,", 26.35f, story_timer);
                    MapSubtitlesAtTime("reasons,", 28f, story_timer);
                    MapSubtitlesAtTime("experiments,", 29f, story_timer);
                    MapSubtitlesAtTime("creates.", 30f, story_timer);
                    MapSubtitlesAtTime("The mind", 31, story_timer);
                    MapSubtitlesAtTime("strains to peer", 32f, story_timer);
                    MapSubtitlesAtTime("beyond today's horizons", 33.25f, story_timer);
                    MapSubtitlesAtTime("for a glimpse of", 35f, story_timer);
                    MapSubtitlesAtTime("the wonders of tomorrow!", 37.5f, story_timer);
                    MapSubtitlesAtTime("⛅", 39f, story_timer);
                    MapSubtitlesAtTime("🔚", 43.5f, story_timer);
                    MapSubtitlesAtTime("⛈", 47f, story_timer);
                    break;
            }
        }
        if (aboutIntro) {
            timer += Time.deltaTime;
            global_timer += Time.deltaTime;
            if (timer < .8f + Time.deltaTime * 2 && timer > .8f) {
                GameObject.Find("World").GetComponent<AudioSource>().clip = ThemeSong;
                GameObject.Find("World").GetComponent<AudioSource>().volume = .2f;
                GameObject.Find("World").GetComponent<AudioSource>().Play();
            }
            SubtitlesAtTime("☄_BitNaughts_is_an_educational\n  programming_video-game!", 2f);
            SubtitlesAtTime("☄_BitNaughts_is_an_educational\n  programming_video-game!\n\n☈ It's_code_gamified!", 4f);
            SubtitlesAtTime("☄_BitNaughts_is_an_educational\n  programming_video-game!\n\n☈ It's_code_gamified!\n  Play: <a>https://bitnaughts.io</a>", 6f);
            SubtitlesAtTime("☄_BitNaughts_is_an_educational\n  programming_video-game!\n\n☈ It's_code_gamified!\n  Play: <a>https://bitnaughts.io</a>\n  Learn: <a>https://github.com/bitnaughts</a>", 8f);
            SubtitlesAtTime("☄_BitNaughts_is_an_educational\n  programming_video-game!\n\n☈ It's_code_gamified!\n  Play: <a>https://bitnaughts.io</a>\n  Learn: <a>https://github.com/bitnaughts</a>\n  Vote: <a>https://hackbox.microsoft.com/project/340</a>", 10f);
            SubtitlesAtTime("☄_BitNaughts_is_an_educational\n  programming_video-game!\n\n☈ It's_code_gamified!\n  Play: <a>https://bitnaughts.io</a>\n  Learn: <a>https://github.com/bitnaughts</a>\n  Vote: <a>https://hackbox.microsoft.com/project/340</a>\n\n  Music:_\"Wintergatan:_Sommarfågel\"\n  <a>https://wintergatan.net</a>", 12f);
            SubtitlesAtTime("☄_BitNaughts_is_an_educational\n  programming_video-game!\n\n☈ It's_code_gamified!\n  Play: <a>https://bitnaughts.io</a>\n  Learn: <a>https://github.com/bitnaughts</a>\n  Vote: <a>https://hackbox.microsoft.com/project/340</a>\n\n  Music:_\"Wintergatan:_Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  SFX:_\"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>", 14f);
            SubtitlesAtTime("☄_BitNaughts_is_an_educational\n  programming_video-game!\n\n☈ It's_code_gamified!\n  Play: <a>https://bitnaughts.io</a>\n  Learn: <a>https://github.com/bitnaughts</a>\n  Vote: <a>https://hackbox.microsoft.com/project/340</a>\n\n  Music:_\"Wintergatan:_Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  SFX:_\"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites: Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 16f);
            // SubtitlesAtTime("  programming_video-game!\n\n  It's code gamified!\n  Play:  <a>https://bitnaughts.io</a>\n  Learn: <a>https://github.com/bitnaughts</a>\n  Vote:  <a>https://hackbox.microsoft.com/project/340</a>\n\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 22f);
            // SubtitlesAtTime("\n  It's code gamified!\n  Play:  <a>https://bitnaughts.io</a>\n  Learn: <a>https://github.com/bitnaughts</a>\n  Vote:  <a>https://hackbox.microsoft.com/project/340</a>\n\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 22.5f);
            // SubtitlesAtTime("  It's code gamified!\n  Play:  <a>https://bitnaughts.io</a>\n  Learn: <a>https://github.com/bitnaughts</a>\n  Vote:  <a>https://hackbox.microsoft.com/project/340</a>\n\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 23f);
            // SubtitlesAtTime("  Play:  <a>https://bitnaughts.io</a>\n  Learn: <a>https://github.com/bitnaughts</a>\n  Vote:  <a>https://hackbox.microsoft.com/project/340</a>\n\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 23.5f);
            // SubtitlesAtTime("  Learn: <a>https://github.com/bitnaughts</a>\n  Vote:  <a>https://hackbox.microsoft.com/project/340</a>\n\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 24f);
            // SubtitlesAtTime("  Vote:  <a>https://hackbox.microsoft.com/project/340</a>\n\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 24.5f);
            // SubtitlesAtTime("\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 25f);
            // SubtitlesAtTime("  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 25.5f);
            // SubtitlesAtTime("  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 26f);
            // SubtitlesAtTime("  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 26.5f);
            // SubtitlesAtTime("\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 27f);
            // SubtitlesAtTime("  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 27.5f);
            // SubtitlesAtTime("  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 28f);
            // SubtitlesAtTime("  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 28.5f);
            // SubtitlesAtTime("\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 29f);
            // SubtitlesAtTime("  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 29.5f);
            // SubtitlesAtTime("  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 30f);
            // SubtitlesAtTime("  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 30.5f);
            if (global_timer > 92) {
                Sound("WarningOver");
                aboutIntro = false;
                timer = 0;
                global_timer = 0;
                RenderText("$ about\n$");
            }
        } else if (onLoad) {
            timer += Time.deltaTime;
            Flash("Clickable$", 0f);
            if (timer > 3) {
                onLoad = false;
                timer = 0;
                ResetFlash("Clickable$", 0f);
            }
        } else if (tutorialIntro) {
            timer += Time.deltaTime;
            global_timer += Time.deltaTime;
            PlayAtTime(TutorialIntro, 1f);
            SubtitlesAtTime("  Welcome_to_the", 1f);
            SubtitlesAtTime("  Welcome_to_the\n⍰_Command_Tutorial!", 2f);
            SubtitlesAtTime("  Welcome_to_the\n⍰_Command_Tutorial!\n\n  Today_you_will_learn", 4f);
            SubtitlesAtTime("  Welcome_to_the\n⍰_Command_Tutorial!\n\n  Today_you_will_learn\n  \"Ship_control\"", 5.5f);
            PlayAtTime(TutorialLookAround, 7f);
            MapSubtitlesAtTime("Click and drag\n▦ Map Screen", 7f);
            Flash ("OverlayPanUp", 7f);
            Flash ("OverlayPanDown", 7f);
            Flash ("OverlayPanLeft", 7f);
            Flash ("OverlayPanRight", 7f);
            SubtitlesAtTime("  Welcome_to_the\n⍰_Command_Tutorial!\n\n  Today_you_will_learn\n  \"Ship_control\"\n\n  First_off,_try_looking_around!", 7f);
            SubtitlesAtTime("  Welcome_to_the\n⍰_Command_Tutorial!\n\n  Today_you_will_learn\n  \"Ship_control\"\n\n  First_off,_try_looking_around!\n  360°_awareness_is_needed", 9f);
            SubtitlesAtTime("  Welcome_to_the\n⍰_Command_Tutorial!\n\n  Today_you_will_learn\n  \"Ship_control\"\n\n  First_off,_try_looking_around!\n  360°_awareness_is_needed_for\n  dog_fighting!", 11f);
            PlayAtTime(TutorialTry, 20f);
            PlayAtTime(TutorialLookAround, 30f);
        } else if (tutorialPan) {
            timer += Time.deltaTime;
            global_timer += Time.deltaTime;
            MapSubtitlesAtTime("", 0f);
            ResetFlash ("OverlayPanUp", 0f);
            ResetFlash ("OverlayPanDown", 0f);
            ResetFlash ("OverlayPanLeft", 0f);
            ResetFlash ("OverlayPanRight", 0f);
            Flash ("MapScreenOverlay", 1.5f);
            Flash ("MapScreenOverlayBitBottomLeft", 1.5f);
            Flash ("MapScreenOverlayBitRightTop", 1.5f);
            Flash ("MapScreenOverlayBitRightBottom", 1.5f);
            Flash ("MapScreenOverlayBitUpLeft", 1.5f);
            Flash ("MapScreenOverlayBitUpRight", 1.5f);
            Flash ("MapScreenOverlayBitDownLeft", 1.5f);
            Flash ("MapScreenOverlayBitDownRight", 1.5f);
            Flash ("MapScreenOverlayBitExit", 1.5f);
            Flash ("MapScreenOverlayBitReset", 1.5f);
            PlayAtTime(TutorialGood3, 0.5f);
            PlayAtTime(TutorialMapScreen, 1.5f);
            SubtitlesAtTime("▦_Map_Screen_shows", 1.5f);
            SubtitlesAtTime("▦_Map_Screen_shows\n┗_Mission_area", 3.5f);
            SubtitlesAtTime("▦_Map_Screen_shows\n┠_Mission_area\n┗_Friendly_units", 5f);
            SubtitlesAtTime("▦_Map_Screen_shows\n┠_Mission_area\n┠_Friendly_units\n┗_Detected_enemy_units", 7f);
            SubtitlesAtTime("▦_Map_Screen_shows\n├_Mission_area\n├_Friendly_units\n└_Detected_enemy_units\n\n  Select_units_highlighted_yellow!", 9f);
            SpriteFlash ("Cannon", 9f);
            ResetFlash("MapScreenOverlay", 9f);
            ResetFlash ("MapScreenOverlayBitBottomLeft", 9f);
            ResetFlash ("MapScreenOverlayBitRightTop", 9f);
            ResetFlash ("MapScreenOverlayBitRightBottom", 9f);
            ResetFlash ("MapScreenOverlayBitUpLeft", 9f);
            ResetFlash ("MapScreenOverlayBitUpRight", 9f);
            ResetFlash ("MapScreenOverlayBitDownLeft", 9f);
            ResetFlash ("MapScreenOverlayBitDownRight", 9f);
            ResetFlash ("MapScreenOverlayBitExit", 9f);
            ResetFlash ("MapScreenOverlayBitReset", 9f);
            PlayAtTime(TutorialTargetWindowHelp, 14f);
            SubtitlesAtTime("▦_Map_Screen_shows\n├_Mission_area\n├_Friendly_units\n└_Detected_enemy_units\n\n  Select_units_highlighted_yellow!\n\n  When_the_reticle_is\n  over_the_target_press", 14f);
            SubtitlesAtTime("▦_Map_Screen_shows\n├_Mission_area\n├_Friendly_units\n└_Detected_enemy_units\n\n  Select_units_highlighted_yellow!\n\n  When_the_reticle_is\n  over_the_target_press\n  \"Use_weapon_control\"", 16.5f);
            SubtitlesAtTime("▦_Map_Screen_shows\n├_Mission_area\n├_Friendly_units\n└_Detected_enemy_units\n\n  Select_units_highlighted_yellow!\n\n  When_the_reticle_is\n  over_the_target_press\n  \"Use_weapon_control\"\n\n  This_will_display\n⁜_Target Window", 19f);
            PlayAtTime(TutorialTry, 25f);
            MapSubtitlesAtTime("Press\n◍ Cannon", 9f);
            PlayAtTime(TutorialTargetWindowHelp, 29f);
        } else  if (tutorialTarget) {
            timer += Time.deltaTime;
            global_timer += Time.deltaTime;
            ResetSpriteFlash("Cannon", 0f);
            MapSubtitlesAtTime("", 0f);
            PlayAtTime(TutorialTargetWindowSelected, 0.5f);
            MapSubtitlesAtTime("⁜ Target Window displays", 1f);
            MapSubtitlesAtTime("unit name and class", 4f);
            Flash("OverlayBorder", 1.2f);
            PlayAtTime(TutorialLeftWindow, 8f);
            ResetFlash("OverlayBorder", 8f);
            Flash ("InterpreterPanel", 8f);
            Flash ("UnitScreenBit", 8f);
            MapSubtitlesAtTime("Left is ▤ Unit Window", 8f);
            MapSubtitlesAtTime("displays information about class", 11f);
            ResetFlash ("InterpreterPanel", 15f);
            ResetFlash ("UnitScreenBit", 15f);
            PlayAtTime(TutorialSelect, 15f);
            MapSubtitlesAtTime("Press Fire or\n/* Weapon control */", 15f);
            Flash("Clickable/*_Weapon_control_*/", 15f);
            Flash ("ClickableFire_()", 15f);
            PlayAtTime(TutorialTry, 22f);
            PlayAtTime(TutorialSelect, 26f);
        } else  if (tutorialFire) {
            RenderComponent(InputField.text);
            timer += Time.deltaTime;
            global_timer += Time.deltaTime;
            PlayAtTime(TutorialGood, 0.5f);
            MapSubtitlesAtTime("", 0f);
            PlayAtTime(TutorialCancel, 2f);
            MapSubtitlesAtTime("Press\n☒ Cancel", 2f);
            PlayAtTime(TutorialTry, 15f);
            PlayAtTime(TutorialCancel, 19f);
            Flash ("OverlayDelete", 2f);
        } else if (tutorialCancel) {
            timer += Time.deltaTime;
            global_timer += Time.deltaTime;
            PlayAtTime(TutorialGetMoving, 0.1f);
            SpriteFlash ("Thruster", 0.1f);
            MapSubtitlesAtTime("", 0.1f);
            SubtitlesAtTime("  Now_it's_time_for_you\n  to_get_this_old_girl_moving!", 0.5f);
            Flash ("OverlayPanDown", 0.5f);
            PlayAtTime(TutorialTry, 6f);
            MapSubtitlesAtTime("Press\n◉ Thruster", 0.5f);
            PlayAtTime(TutorialGetMoving, 12f);
        } else if (tutorialThrust) {
            RenderComponent(InputField.text);
            timer += Time.deltaTime;
            global_timer += Time.deltaTime;
            MapSubtitlesAtTime("", 0f);
            ResetSpriteFlash ("Thruster", 0.1f);
            ResetFlash ("OverlayPanDown", 0.1f);
            PlayAtTime(TutorialGood2, 0.5f);
            Flash("ClickableThrustUp_()", 1.5f);
            Flash("Clickable/*_Thrust_control_(+)_*/", 1.5f);
            PlayAtTime(TutorialThrottle, 1.5f);
            MapSubtitlesAtTime("Press ThrustUp or\n/* Thrust control (+) */", 1.5f);
            PlayAtTime(TutorialTry, 8f);
            PlayAtTime(TutorialThrottle, 14f);
        } else if (tutorialFinish) {
            timer += Time.deltaTime;
            global_timer += Time.deltaTime;
            ResetFlash ("OverlayPanDown", 0.1f);
            MapSubtitlesAtTime("", 0f);
            PlayAtTime(TutorialBetter, 0.1f);
            PlayAtTime(TutorialOutro, 2.5f);
            SubtitlesAtTime("☑You_have_completed_the_tutorial", 3.5f);
            SubtitlesAtTime("☑You_have_completed_the_tutorial\n\n  I_hope_you_never_have\n  cause_to_use_the_knowledge\n  you_just_acquired!", 7f);
            SubtitlesAtTime("☑You_have_completed_the_tutorial\n\n  I_hope_you_never_have\n  cause_to_use_the_knowledge\n  you_just_acquired!\n\n  That_is_all_for_today!", 12f);
            SubtitlesAtTime("☑You_have_completed_the_tutorial\n\n  I_hope_you_never_have\n  cause_to_use_the_knowledge\n  you_just_acquired!\n\n  That_is_all_for_today!\n\n  Dismissed!", 13.5f);
            SubtitlesAtTime("☑You_have_completed_the_tutorial\n\n  I_hope_you_never_have\n  cause_to_use_the_knowledge\n  you_just_acquired!\n\n  That_is_all_for_today!\n\n  Dismissed!", 15f);
            Flash ("OverlayOk", 3.5f);
            MapSubtitlesAtTime("Press\n☑ Ok", 3.5f);
            if (timer > 16) {
                CompleteTutorial();
            }
        } else if (OverlayInteractor != null && OverlayInteractor.gameObject.activeSelf) {
            if (animation_timer % 1 > 1 - Time.deltaTime) {
                RenderComponent(InputField.text);
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
