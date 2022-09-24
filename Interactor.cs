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

    public List<GameObject> ButtonsCache = new List<GameObject>();
    int cache_size = 125;
    public OverlayInteractor OverlayInteractor;
    public GameObject ClickableText;
    GameObject MapScreenPanOverlay;
    public InputField InputField;
    public Text InputFieldPlaceholder;

    public Text Timer, TimerShadow, SplitTimer, SplitTimerShadow;

    GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        for (int i = 0; i < cache_size; i++) {
            ButtonsCache.Add(Instantiate(ClickableText, this.transform) as GameObject);
        } 
        OverlayInteractor = GameObject.Find("OverlayBorder").GetComponent<OverlayInteractor>();
        MapScreenPanOverlay = GameObject.Find("MapScreenPanOverlay");
        RenderText("$");
        Timer.text = "Welcome";
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
        InputField.interactable = true;
        component_text = component_string.Substring(component_header + 1);
        RenderText(component_text);
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
        InputFieldPlaceholder.text = "";
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
        aboutIntro = true;
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
        GetComponent<AudioSource>().volume = .75f;
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
    void SubtitlesAtTime(string text, float time) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            // GameObject.Find("Subtitles").GetComponent<Text>().text = text + "\n";
            RenderText(text);
        }
    }
    void MapSubtitlesAtTime(string text, float time) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            GameObject.Find("Subtitles").GetComponent<Text>().text = text + "\n";
            GameObject.Find("SubtitlesShadow").GetComponent<Text>().text = text + "\n";
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
                Timer.text = "Time " + System.DateTime.Now.ToString("h:mm:ss");
            } else {
                Timer.text = "Ping " + camera.GetComponent<MultiplayerController>().Delay.ToString("s\\.fffff");
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

    void FixedUpdate()
    {
        if (aboutIntro) {
            timer += Time.deltaTime;
            global_timer += Time.deltaTime;
            PlayAtTime(ThemeSong, .8f);
            SubtitlesAtTime("☄_BitNaughts_is_an_educational\n  programming_video-game!", 2f);
            SubtitlesAtTime("☄_BitNaughts_is_an_educational\n  programming_video-game!\n\n  It's code gamified!", 4f);
            SubtitlesAtTime("☄_BitNaughts_is_an_educational\n  programming_video-game!\n\n  It's code gamified!\n  Play @ <a>https://bitnaughts.io</a>", 6f);
            SubtitlesAtTime("☄_BitNaughts_is_an_educational\n  programming_video-game!\n\n  It's code gamified!\n  Play @ <a>https://bitnaughts.io</a>\n  Learn @ <a>https://github.com/bitnaughts</a>", 8f);
            SubtitlesAtTime("☄_BitNaughts_is_an_educational\n  programming_video-game!\n\n  It's code gamified!\n  Play @ <a>https://bitnaughts.io</a>\n  Learn @ <a>https://github.com/bitnaughts</a>\n  Vote @ <a>https://hackbox.microsoft.com/project/340</a>", 10f);
            SubtitlesAtTime("☄_BitNaughts_is_an_educational\n  programming_video-game!\n\n  It's code gamified!\n  Play @ <a>https://bitnaughts.io</a>\n  Learn @ <a>https://github.com/bitnaughts</a>\n  Vote @ <a>https://hackbox.microsoft.com/project/340</a>\n\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>", 12f);
            SubtitlesAtTime("☄_BitNaughts_is_an_educational\n  programming_video-game!\n\n  It's code gamified!\n  Play @ <a>https://bitnaughts.io</a>\n  Learn @ <a>https://github.com/bitnaughts</a>\n  Vote @ <a>https://hackbox.microsoft.com/project/340</a>\n\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>", 14f);
            SubtitlesAtTime("☄_BitNaughts_is_an_educational\n  programming_video-game!\n\n  It's code gamified!\n  Play @ <a>https://bitnaughts.io</a>\n  Learn @ <a>https://github.com/bitnaughts</a>\n  Vote @ <a>https://hackbox.microsoft.com/project/340</a>\n\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 16f);
            SubtitlesAtTime("  programming_video-game!\n\n  It's code gamified!\n  Play @ <a>https://bitnaughts.io</a>\n  Learn @ <a>https://github.com/bitnaughts</a>\n  Vote @ <a>https://hackbox.microsoft.com/project/340</a>\n\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 18f);
            SubtitlesAtTime("\n  It's code gamified!\n  Play @ <a>https://bitnaughts.io</a>\n  Learn @ <a>https://github.com/bitnaughts</a>\n  Vote @ <a>https://hackbox.microsoft.com/project/340</a>\n\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 19f);
            SubtitlesAtTime("  It's code gamified!\n  Play @ <a>https://bitnaughts.io</a>\n  Learn @ <a>https://github.com/bitnaughts</a>\n  Vote @ <a>https://hackbox.microsoft.com/project/340</a>\n\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 20f);
            SubtitlesAtTime("  Play @ <a>https://bitnaughts.io</a>\n  Learn @ <a>https://github.com/bitnaughts</a>\n  Vote @ <a>https://hackbox.microsoft.com/project/340</a>\n\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 21f);
            SubtitlesAtTime("  Learn @ <a>https://github.com/bitnaughts</a>\n  Vote @ <a>https://hackbox.microsoft.com/project/340</a>\n\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 22f);
            SubtitlesAtTime("  Vote @ <a>https://hackbox.microsoft.com/project/340</a>\n\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 23f);
            SubtitlesAtTime("\n  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 24f);
            SubtitlesAtTime("  Soundtrack:\n  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 25f);
            SubtitlesAtTime("  Wintergatan:_\"Sommarfågel\"\n  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 26f);
            SubtitlesAtTime("  <a>https://wintergatan.net</a>\n\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 27f);
            SubtitlesAtTime("\n  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 28f);
            SubtitlesAtTime("  Sound_Effects:\n  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 29f);
            SubtitlesAtTime("  \"Battlestations_Pacific\"\n  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 30f);
            SubtitlesAtTime("  <a>https://spritedatabase.net/game/3228</a>\n\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 31f);
            SubtitlesAtTime("\n  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 32f);
            SubtitlesAtTime("  Sprites:\n  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 33f);
            SubtitlesAtTime("  Alejandro_Monge:_\"Modular_Spaceships\"\n  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 34f);
            SubtitlesAtTime("  <a>https://www.behance.net/gallery/14146659/Modular-Spaceships</a>", 35f);
            SubtitlesAtTime("$", 36f);
            Flash("Clickable$", 35.1f);
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
            MapSubtitlesAtTime("Target Window displays", 1f);
            MapSubtitlesAtTime("Target Window displays unit name & class", 3.2f);
            Flash("OverlayBorder", 1.2f);
            PlayAtTime(TutorialLeftWindow, 8f);
            ResetFlash("OverlayBorder", 8f);
            Flash ("InterpreterPanel", 8f);
            Flash ("UnitScreenBit", 8f);
            MapSubtitlesAtTime("Left is Unit Window", 8f);
            MapSubtitlesAtTime("Displays information about the class", 11f);
            ResetFlash ("InterpreterPanel", 15f);
            ResetFlash ("UnitScreenBit", 15f);
            PlayAtTime(TutorialSelect, 15f);
            MapSubtitlesAtTime("Press Fire or\n/*Use weapon control*/", 15f);
            Flash("Clickable/*_Use_weapon_control_*/", 15f);
            Flash ("ClickableFire", 15f);
            PlayAtTime(TutorialTry, 22f);
            PlayAtTime(TutorialSelect, 26f);
        } else  if (tutorialFire) {
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
            timer += Time.deltaTime;
            global_timer += Time.deltaTime;
            MapSubtitlesAtTime("", 0f);
            ResetSpriteFlash ("Thruster", 0f);
            ResetFlash ("OverlayPanDown", 0f);
            PlayAtTime(TutorialGood2, 0.5f);
            Flash("ClickableThrottleMax", 1.5f);
            Flash("Clickable/*_Throttle_control_(max)_*/", 1.5f);
            PlayAtTime(TutorialThrottle, 1.5f);
            MapSubtitlesAtTime("Press ThrottleMax or\n/*Throttle Control (max)*/", 1.5f);
            PlayAtTime(TutorialTry, 8f);
            PlayAtTime(TutorialThrottle, 14f);
        } else if (tutorialFinish) {
            timer += Time.deltaTime;
            global_timer += Time.deltaTime;
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
        } else if (OverlayInteractor.gameObject.activeSelf) {
            RenderComponent(InputField.text);
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
