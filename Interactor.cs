using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
/*

Font size 50
Character width 25
Text height 50

*/

// Mexico 19.419892, -99.088050


public class Interactor : MonoBehaviour
{
    public string Stage = "SplashScreen";
    public GameObject InputJoystick, InputUseWeapon;
    public GameObject[] GridLayers;
    public Sprite PixelSprite, OverlaySprite;
    public GameObject SplashScreen;
    public GameObject LoadingScreen;
    public GameObject TutorialAssets;
    public AudioClip IntroMusic;
    List<string> Narration = new List<string> {
        "Today, you will learn\n",
        "Today, you will learn\nto use the Map Interface",
        "to issue tactical\n",
        "to issue tactical\ncommands.",
        "Aim the crosshair using\n",
        "Aim the crosshair using\nthe mouse",
        "or joystick.\n",
        "When you're on target\n",
        "When you're on target\nthe crosshair",
        "alters shape to\n",
        "alters shape to\nindicate a",
        "lock-on.\n",
        "Press the Zoom key\n",
        "Press the Zoom key\nto zoom in.",
        "2",
        "3"
    };
    float NarrationTimer = 0;
    int NarrationIndex = 0;
    List<float> NarrationTiming = new List<float> {
        000, //"Map Interface"
        01.5f,
        03f,
        05f,
        07f, //"Aim the Crosshair"
        09f,
        010f,
        011,
        012,
        013,
        014,
        015,
        60, //*Click "Tutorial" Marker*
        61.5f,
        180,
        240
    };
    public AudioClip NarratorSwitchToMap, NarratorZoomInMap, NarratorZoomInDetails, NarratorAimTheCrosshair, NarratorWelcome;
    public AudioClip TutorialOpening, TutorialLockOn, TutorialPrint, TutorialPrinted, TutorialComponentsIcons, TutorialLook, TutorialUseWeapon, TutorialBinocular, TutorialHitTarget, TutorialRightOn, TutorialAttackTarget, TutorialCycle, TutorialHulkDestroyed, TutorialTorpedoFired, TutorialCannonFired, TutorialSensorFired;
    public GameObject CampaignNewtonsLaws, CampaignDopplerShift, CampaignDopplerEffect, CampaignPlanksLaw, CampaignHawkingRadiation, CampaignMoracevsParadox, CampaignDeBroglieTheory, CampaignFermiParadox, CampaignPascalsWager;
    public AudioClip HookNarration, SplashScreenNarration, CampaignRadioDaysNarration, CampaignNewtonsLawsNarration, CampaignTheAtomNarration, CampaignDopplerShiftNarration, CampaignTheElectronNarration, CampaignDopplerEffectNarration, CampaignModernWarNarration, CampaignPlanksLawNarration, CampaignTelevisionNarration, CampaignHawkingRadiationNarration, CampaignVideotapeRecordsNarration, CampaignMoracevsParadoxNarration, CampaignElectronicMusicNarration, CampaignDeBroglieTheoryNarration, CampaignRadioIsotopesNarration, CampaignFermiParadoxNarration, CampaignHardnessTestNarration, CampaignPascalsWagerNarration, CampaignConclusionNarration, CampaignCreditsNarration;
    public GameObject Content, InterpreterPanel, InterpreterPanelEdge, MapPanel, SubtitlesShadow, Subtitles; 
    public AudioClip TutorialIntro, TutorialLookAround, TutorialMapInterface, TutorialMapScreen, TutorialIssueOrders, TutorialTargetWindow, TutorialTargetWindowHelp, TutorialTargetWindowSelected, TutorialGood, TutorialGood2, TutorialGood3, TutorialTry, TutorialBetter, TutorialCancel, TutorialOther, TutorialMusic, TutorialComponents, TutorialGetMoving, TutorialThrottle, TutorialDogfight, TutorialOutro, TutorialLeftWindow, TutorialRightWindow, TutorialCursor, TutorialSelect;
    public AudioClip CannonFire, ThrusterThrottle, SonarScan, TorpedoFact, ProcessorPing, GimbalRotate, TorpedoLaunch;
    public AudioClip ThemeSong, Click, Click2;
    public AudioClip SoundBack, SoundClick, SoundError, SoundOnMouse, SoundStart, SoundToggle, SoundProcessor, SoundGimbal, SoundCannon1, SoundCannon2, SoundCannon3, SoundRadar, SoundThruster, SoundBooster, SoundTorpedo1, SoundTorpedo2, SoundWarning, SoundWarningOver;
    public GameObject Overlay, OverlayZoomIn;
    public GameObject Example;
    public GameObject PrinterPrint, PrinterRight, PrinterLeft;
    private string command = "";
    private string history = "";
    public StructureController Ship;
    public GameObject volume_slider;
    public string start_text = "$"; 
    public OverlayInteractor OverlayInteractor;
    public GameObject ClickableText;
    Text TabToggle;
    GameObject MapScreenPanOverlay;
    public Text InputField;
    public Text Timer, TimerShadow, SplitTimer, SplitTimerShadow;
    GameObject camera;
    public List<GameObject> ButtonsCache = new List<GameObject>();
    int cache_size = 125;
    string audio_queue = "Splash Screen";
    public AudioClip clip_queue;

    // example ship ui
    // public GameObject InputUp, InputLeft, InputRight, InputDown, InputA, InputB;
    // example ship objects
    public GameObject CannonL, Processor, Bulkhead, BoosterR, ThrusterL, BoosterL, Thruster, ThrusterR, CannonR, SensorL, SensorR, Printer;
    // Start is called before the first frame update
    void Start()
    {
        InputJoystick.SetActive(false);
        InputUseWeapon.SetActive(false);

        PrinterPrint = GameObject.Find("InputPrinterPrint");
        PrinterRight = GameObject.Find("InputPrinterRight");
        PrinterLeft = GameObject.Find("InputPrinterLeft");
        OverlayZoomIn = GameObject.Find("OverlayZoomIn");
        TabToggle = GameObject.Find("TabToggle").GetComponent<Text>();
        SplashScreen.SetActive(true);
        Subtitles = GameObject.Find("Subtitles");
        SubtitlesShadow = GameObject.Find("SubtitlesShadow");
        SubtitlesShadow.SetActive(false);
        Subtitles.SetActive(false);
        camera = GameObject.Find("Main Camera");
        volume_slider = GameObject.Find("VolumeSlider");
        for (int i = 0; i < cache_size; i++) {
            ButtonsCache.Add(Instantiate(ClickableText, Content.transform) as GameObject);
        } 
        OverlayInteractor = GameObject.Find("OverlayBorder")?.GetComponent<OverlayInteractor>();
        InterpreterPanelEdge = GameObject.Find("InterpreterPanelEdge");
        MapScreenPanOverlay = GameObject.Find("MapScreenPanOverlay");
        RenderText("$ <b>tutorial</b>\n$");

        Timer.text = "⛅";
        PlayVideo(audio_queue);
        OnMapView();
        PrinterLeft.SetActive(false);
        PrinterRight.SetActive(false);
        PrinterPrint.SetActive(false);
        Printer.SetActive(false);
    }
    public void HitSfx() {
        if (tutorial_clip_index == 6) {
            tutorial_clip_index = 7;
            tutorial_timer = 0;
            PlayAudio(TutorialHitTarget);
        }
    }
    public void PrinterLeftFx() {
        if (BoosterR.activeSelf)
        {
            Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◌")});
            BoosterL.SetActive(false);
            BoosterR.SetActive(false);
            SensorL.SetActive(true);
            SensorR.SetActive(true);
            ThrusterL.SetActive(true);
            ThrusterR.SetActive(true);
            Thruster.SetActive(false);
        } else if (CannonR.activeSelf) {
            Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◎")});
            CannonL.SetActive(false);
            CannonR.SetActive(false);
            BoosterL.SetActive(true);
            BoosterR.SetActive(true);
            Thruster.SetActive(true);
            ThrusterL.SetActive(false);
            ThrusterR.SetActive(false);
        } else {
            Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◍")});
            SensorL.SetActive(false);
            SensorR.SetActive(false);
            CannonL.SetActive(true);
            CannonR.SetActive(true);
        }

    }
    public void PrinterRightFx() {
        if (BoosterR.activeSelf)
        {
            Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◍")});
            BoosterL.SetActive(false);
            BoosterR.SetActive(false);
            CannonL.SetActive(true);
            CannonR.SetActive(true);
            ThrusterL.SetActive(true);
            ThrusterR.SetActive(true);
            Thruster.SetActive(false);
        } else if (CannonR.activeSelf) {
            Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◌")});
            CannonL.SetActive(false);
            CannonR.SetActive(false);
            SensorL.SetActive(true);
            SensorR.SetActive(true);
        } else {
            Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◎")});
            SensorL.SetActive(false);
            SensorR.SetActive(false);
            BoosterL.SetActive(true);
            BoosterR.SetActive(true);
            Thruster.SetActive(true);
            ThrusterL.SetActive(false);
            ThrusterR.SetActive(false);
        }
    }
    public void InputWFx() {
        if (Thruster.activeSelf) {
            GameObject.Find("Thruster").GetComponent<ComponentController>().Action(25);
        } else {
            GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(25);
            GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(25);
        }
    }
    public void InputDFx() {
        if (Thruster.activeSelf) {
            GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(25);
        } else {
            GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(25);
            GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-25);
        }

    }
    public void InputSFx() {
        if (Thruster.activeSelf) {
            GameObject.Find("Thruster").GetComponent<ComponentController>().Action(-25);
        } else {
            GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-25);
            GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-25);
        }
        
    }
    public void InputAFx() {
        if (Thruster.activeSelf) {
            GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(25);
        } else {
            GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(25);
            GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-25);
        }
    }
    public void InputXFx() {
        if (BoosterR.activeSelf)
        {
            GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-1);
        } else if (CannonR.activeSelf) {
            GameObject.Find("CannonL").GetComponent<ComponentController>().Action(-1);
        } else {
            GameObject.Find("SensorL").GetComponent<ComponentController>().Action(-1);
        }
    }
    public void InputYFx() {
        if (tutorial_clip_index < 7) { 
            tutorial_timer = 0; tutorial_clip_index = 7; 
            if (BoosterR.activeSelf) PlayAudio(TutorialTorpedoFired); 
            if (CannonR.activeSelf) PlayAudio(TutorialCannonFired);
            if (SensorR.activeSelf) PlayAudio(TutorialCannonFired);
            
        }
        if (BoosterR.activeSelf)
        {
            GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-1);
            GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-1);
        } else if (CannonR.activeSelf) {
            GameObject.Find("CannonL").GetComponent<ComponentController>().Action(-1);
            GameObject.Find("CannonR").GetComponent<ComponentController>().Action(-1);
        } else {
            GameObject.Find("SensorL").GetComponent<ComponentController>().Action(-1);
            GameObject.Find("SensorR").GetComponent<ComponentController>().Action(-1);
        }
        
    }
    bool printing = false;
    public void PrinterPrintFx() {
        if (InputField.text.Contains("Printer")) {
            printing = true;
            PrinterLeft.SetActive(false);
            PrinterRight.SetActive(false);
            PrinterPrint.SetActive(false);
        } else if (InputField.text.Contains("Processor")) {
            OnCodeView();
        } else if (InputField.text.Contains("Cannon")) {
            GameObject.Find("CannonL")?.GetComponent<ComponentController>().Action(-1);
            GameObject.Find("CannonR")?.GetComponent<ComponentController>().Action(-1);
            PrinterLeft.SetActive(false);
        } else if (InputField.text.Contains("Booster")) {
            GameObject.Find("BoosterL")?.GetComponent<ComponentController>().Action(-1);
            GameObject.Find("BoosterR")?.GetComponent<ComponentController>().Action(-1);
        } else if (InputField.text.Contains("Sensor")) {
            GameObject.Find("SensorL")?.GetComponent<ComponentController>().Action(-1);
            GameObject.Find("SensorR")?.GetComponent<ComponentController>().Action(-1);
        } else if (InputField.text.Contains("Thruster")) {
            GameObject.Find("ThrusterR")?.GetComponent<ComponentController>().Action(25);
            GameObject.Find("ThrusterL")?.GetComponent<ComponentController>().Action(25);
            GameObject.Find("Thruster")?.GetComponent<ComponentController>().Action(25);
        }
    }
    public static int Max(int val1, int val2) {
        return (val1>=val2)?val1:val2;
    }
    public static int Min(int val1, int val2) {
        return (val1<val2)?val1:val2;
    }
    float story_timer = -1f, start_timer = 0f;
    public void SetBackground(Color color) 
    {
        camera.GetComponent<Camera>().backgroundColor = color;
        for (int i = 0; i < GridLayers.Length; i++)
        {
            if (color.r == 0) {
                GridLayers[i].GetComponent<SpriteRenderer>().color = color;
                GridLayers[i].GetComponent<SpriteRenderer>().sprite = PixelSprite;
            }
            else {
                GridLayers[i].GetComponent<SpriteRenderer>().color = new Color(255/255f, 255/255f, 195/255f, .05f * (i + 2));//35f/255f, 95f/255f, 110f/255f, .66f);
                GridLayers[i].GetComponent<SpriteRenderer>().sprite = OverlaySprite;
            }
        }
    }
    public void SetVolume() 
    {
        camera.GetComponent<AudioSource>().volume = volume_slider.GetComponent<Slider>().value;
        GameObject.Find("Video Player").GetComponent<AudioSource>().volume = volume_slider.GetComponent<Slider>().value;
    }
    public void PlayMusic(AudioClip clip) {
        if (GameObject.Find("Video Player") != null) {
            GameObject.Find("Video Player").GetComponent<AudioSource>().clip = clip;
            GameObject.Find("Video Player").GetComponent<AudioSource>().Play();
            GameObject.Find("Video Player").GetComponent<AudioSource>().loop = false;
            GameObject.Find("Video Player").GetComponent<AudioSource>().volume = volume_slider.GetComponent<Slider>().value / 8;
        }
    }
    public void PlayAudio(AudioClip clip) {
        if (camera != null) {
            camera.GetComponent<AudioSource>().clip = clip;
            camera.GetComponent<AudioSource>().Play();
            camera.GetComponent<AudioSource>().loop = false;
        }
    }
    string queue_audio = "";
    public void PlayVideo(string url) 
    {
        LoadingScreen.SetActive(true);
        var trimmed_url = url.Replace(" ", "").Replace("'", "");
        queue_audio = trimmed_url;
        GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().enabled = true;
        // string asset_location = System.IO.Path.Combine (Application.streamingAssetsPath, "BitNaughts" + trimmed_url + "480p.mp4");
        // #if UNITY_WEBGL
        string asset_location = "https://raw.githubusercontent.com/bitnaughts/bitnaughts.assets/master/Videos/BitNaughts" + trimmed_url + "480p.mp4";
        // #endif
        // print (asset_location);
        GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().url = asset_location; 
        GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().Play();
        SetBackground(new Color(0f, 0f, 0f));
        MapScreenPanOverlay.SetActive(false);
        OverlayZoomIn.SetActive(false);
        volume_slider.SetActive(true);
        SubtitlesShadow.SetActive(true);
        Subtitles.SetActive(true);
        volume_slider.SetActive(true);
        if (url.Length < 15) {
            InputField.text = "⧆ " + url;
        }
        else 
        {
            InputField.text = url;
        }
    }

    public void StoryMode(int index) {
        story_timer = 0f;
        start_timer = -1f;
        global_timer = 0f;
        clip_index = index;
        PlayVideo(campaign_clips[clip_index]);
        clip_index++;
        OnMapView();
    }
    public AudioClip LookupNarration(string clip) 
    {
        switch (clip)
        {
            case "Hook":
                return HookNarration;
            case "SplashScreen":
                return SplashScreenNarration;
            case "RadioDays":
                return CampaignRadioDaysNarration;
            case "NewtonsLaws":
                return CampaignNewtonsLawsNarration;
            case "TheAtom":
                return CampaignTheAtomNarration;
            case "DeBroglieTheory":
                return CampaignDeBroglieTheoryNarration;
            case "TheElectron":
                return CampaignTheElectronNarration;
            case "DopplerShift":
                return CampaignDopplerShiftNarration;
            case "ModernWar":
                return CampaignModernWarNarration;
            case "DopplerEffect":
                return CampaignDopplerEffectNarration;
            case "Television":
                return CampaignTelevisionNarration;
            case "PlanksLaw":
                return CampaignPlanksLawNarration;
            case "VideotapeRecords":
                return CampaignVideotapeRecordsNarration;
            case "HawkingRadiation":
                return CampaignHawkingRadiationNarration;
            case "ElectronicMusic":
                return CampaignElectronicMusicNarration;
            case "MoravecsParadox":
                return CampaignMoracevsParadoxNarration;
            case "RadioIsotopes":
                return CampaignRadioIsotopesNarration;
            case "FermiParadox":
                return CampaignFermiParadoxNarration;
            case "HardnessTest":
                return CampaignHardnessTestNarration;
            case "PascalsWager":
                return CampaignPascalsWagerNarration;
            case "Conclusion":
                return CampaignConclusionNarration;
            case "Credits":
                return CampaignCreditsNarration;
        }
        return SoundError;
    }
    public void OnToggleView() {
        if (MapPanel.activeSelf)
        {
            OnCodeView();
        }
        else 
        {
            OnMapView();
        }
    }
    public void OnMapView() {
        MapPanel.SetActive(true);
        if (volume_slider.activeSelf == false) MapScreenPanOverlay.SetActive(true);
        InterpreterPanel.SetActive(false);
        InterpreterPanelEdge.SetActive(false);
        TabToggle.text = "▦ GUI";
    }
    public void OnCodeView() {
        MapPanel.SetActive(false);
        InterpreterPanel.SetActive(true);
        InterpreterPanelEdge.SetActive(true);
        TabToggle.text = "▤ TUI";
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
        // volume_slider.SetActive(false/);
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
        SetContentSize(max_line_length * 50f, lines.Length * 100f);
    }
    public string component_name = "";
    public string component_text = "";
    public void RenderComponent(string component) {
        var component_string = Ship.GetComponentToString(component);
        var component_header = component_string.IndexOf("\n");
        component_name = component_string.Substring(0, component_header);
        InputField.text = component_name;
        if (InputField.text == "Printer") {
            InputField.text = "▦ Printer";//" ⛴ Ship Select";
            if (GameObject.Find("MarkerTutorial") != null) GameObject.Find("MarkerTutorial").SetActive(false);
            PrinterLeft.SetActive(true);
            PrinterRight.SetActive(true);
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "Print";
            if (tutorial_clip_index < 2) {
                tutorial_timer = 0; tutorial_clip_index = 2; PlayAudio(TutorialComponentsIcons);
            }
            Processor.SetActive(true);
            Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◎")});
            Thruster.SetActive(true);
            BoosterL.SetActive(true);
            BoosterR.SetActive(true);
        }
        else if (InputField.text.Contains("Processor")) {
            InputField.text = "▩ " + InputField.text;
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "▤ TUI";
            PrinterRight.SetActive(false);
            PrinterLeft.SetActive(false);
        }
        else if (InputField.text.Contains("Cannon")) {
            InputField.text = "◍ " + InputField.text;
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "◍ Fire";
            PrinterRight.SetActive(false);
            PrinterLeft.SetActive(false);
        }
        else if (InputField.text.Contains("Booster")) {
            InputField.text = "◎ " + InputField.text;
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "◎ Fire";
            PrinterRight.SetActive(true);
            PrinterLeft.SetActive(true);
        }
        else if (InputField.text.Contains("Sensor")) {
            InputField.text = "◌ " + InputField.text;
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "◌ Fire";
            PrinterRight.SetActive(true);
            PrinterLeft.SetActive(true);
        }
        else if (InputField.text.Contains("Thruster")) {
            InputField.text = "◉ " + InputField.text;
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "◉ Fire";
            PrinterRight.SetActive(false);
            PrinterLeft.SetActive(false);
        }
        if (GameObject.Find("OverlayDropdownLabel") != null) GameObject.Find("OverlayDropdownLabel").GetComponent<Text>().text = component_name;
        component_text = component_string.Substring(component_header + 1);
        // RenderText(component_text);
        // RenderText(Ship.interpreter.ToString());
    }

    public string[] GetComponents() {
        return Ship.GetControllers();
    }
    void SetContentSize(float width,float height) {
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(width + 100, height + 200);
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
                // OverlayInteractor.OnDropdownChange(); 
            break;
            default:
                GameObject.Find(component_name).name = InputField.text;
                Ship.Start();
                OverlayInteractor.UpdateOptions();
                // OverlayInteractor.OnDropdownChange(); 
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
    public void UseWeapon() {
        Action("Cannon", -1);//GetInput(), -1);
        if (clip_index == 2 && campaign_stage == 2) { campaign_stage++; story_timer = 0f; }
    }
    public void CycleTutorial() {
        if (tutorial_clip_index == 10) {
            tutorial_clip_index = 11;
            tutorial_timer = 0;
            PlayAudio(TutorialGood2);
        } else if (tutorial_clip_index == 11) {
            tutorial_clip_index = 12;
            tutorial_timer = 0;
            PlayAudio(TutorialGood3);
        }
    }
    public void BinocularTutorial() {
        if (tutorial_clip_index == 9) {
            tutorial_clip_index = 10;
            tutorial_timer = 0;
            PlayAudio(TutorialCycle);
            // PlayAudio(TutorialGood2);
        }
    }
    public void PanTutorial() {
        if (tutorial_clip_index == 3) { tutorial_timer = 0; tutorial_clip_index = 4; PlayAudio(TutorialPrinted); InputJoystick.SetActive(true); }
        // if (clip_index == 2 && campaign_stage == 0) { campaign_stage++; story_timer = 0f; }
    }
    public void TargetTutorial() {
        if (clip_index == 2 && campaign_stage == 1) { campaign_stage++; story_timer = 0f; }
    }
    public void FireTutorial() {
        if (clip_index == 2 && campaign_stage == 2) { campaign_stage++; story_timer = 0f; }
    }
    public void CancelTutorial() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

    }
    public void ThrustTutorial() {
    }
    public void FinishTutorial() {
        Sound("Back");
        if (story_timer != -1) {
            campaign_splits[clip_index - 1] = story_timer;//999 ; in the future
            story_timer = 0f;
            if (clip_index < 20) {
                PlayVideo(campaign_clips[clip_index]);
                clip_index++;
            }
            else 
            {
                
                Application.Quit();
            }
        } else {
            Application.Quit();
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
        // camera.GetComponent<AudioSource>().clip = clip;
        // camera.GetComponent<AudioSource>().volume = .5f;
        // camera.GetComponent<AudioSource>().Play();
    }
    void MapSubtitlesAtTime(string text, float time, float timer) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            Subtitles.GetComponent<Text>().text = text;
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
    void MapSubtitles(string text) {
        Subtitles.GetComponent<Text>().text = text;
    }
    double click_duration = 0;
    public double GetClickDuration() {
        return click_duration;
    }
    float global_timer = 0, animation_timer = 0;
    string FloatToTime(float time) {
        var pt = ((int)((time*100) % 60)).ToString("00");
        var ss = ((int)(time % 60)).ToString("00");
        var mm = (Mathf.Floor(time / 60) % 60).ToString();//.TrimStart('0');
        // if (mm == "") return ss + "." + pt;
        return mm + ":" + ss + "." + pt[0];
    }
    void Update () {
        animation_timer += Time.deltaTime;
        if (Input.GetMouseButton(0)) {
            click_duration += Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0)) {
            click_duration = 0;
        }
        if (Input.GetKeyDown("x")) {
            InputYFx();
        }
        if (Input.GetKey("w")) {
            if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(0.5f);
            if (tutorial_clip_index < 5) { tutorial_timer = 0; tutorial_clip_index = 5; PlayAudio(TutorialAttackTarget); InputUseWeapon.SetActive(true); InputJoystick.SetActive(true); }
        }
        if (Input.GetKey("q")) {
            if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(0.5f);
            if (tutorial_clip_index < 5) { tutorial_timer = 0; tutorial_clip_index = 5; PlayAudio(TutorialAttackTarget); InputUseWeapon.SetActive(true); InputJoystick.SetActive(true); }
        }
        if (Input.GetKey("e")) {
            if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-0.5f);
            if (tutorial_clip_index < 5) { tutorial_timer = 0; tutorial_clip_index = 5; PlayAudio(TutorialAttackTarget); InputUseWeapon.SetActive(true); InputJoystick.SetActive(true); }
        }
        if (Input.GetKey("a")) {
            if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(0.5f);
        }
        if (Input.GetKey("d")) {
            if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-0.5f);
        }
        if (Input.GetKey("s")) {
            if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-0.5f);
        }
        if (Input.GetKey("z")) {
            if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(0.5f);
        }
        if (Input.GetKey("c")) {
            if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-0.5f);
        }
    }
    bool CheckInsideEdge() {
        return (Input.mousePosition.y > 100 && Input.mousePosition.y < Screen.height - 160 && Input.mousePosition.x > 100 && Input.mousePosition.x < Screen.width - 100);
    }
    string[] campaign_clips = new string[] { "Radio Days", "Newton's Laws", "The Atom", "De Broglie Theory", "The Electron",  "Doppler Effect", "Modern War", "Doppler Shift", "Television", "Plank's Law", "Videotape Records", "Hawking Radiation", "Electronic Music", "Moravec's Paradox", "Radio Isotopes", "Fermi Paradox", "Hardness Test", "Pascal's Wager", "Conclusion", "Credits", "" };
    string[] tutorial_clips = new string[] { "Tutorial Introduction", "Digital Computers", "Binary", "Components", "Morse Code", "☄ BitNaughts   " };
    int campaign_stage = -1, tutorial_stage = -1; 
    int[] campaign_clip_durations = new int[] {999, 81, 999, 79, 999, 64, 999, 46, 999, 79, 999, 74, 999, 107, 999, 95, 999, 116, 999, 51, 155, 999 };
    float[] campaign_splits = new float[20];
    float tutorial_timer = -1;
    float tutorial_save_time = 0;
    int[] tutorial_clip_durations = new int[] {999, 81, 999, 79, 999, 64, 999, 46, 999, 79, 999, 74, 999, 107, 999, 95, 999, 116, 999, 51, 999, 999, 999, 999 };
    int tutorial_clip_index = 0;
    int clip_index = 0;
    string credits_output = "";
    public void MapZoomed() {
        Stage = "MapZoomed";
        Printer.SetActive(true);
        
        // InputField.text = "? Tutorial";//GameObject.Find(component_name).name = 
        Ship.Start();
        OverlayInteractor.UpdateOptions();
        MapScreenPanOverlay.SetActive(true);
        GameObject.Find("OverlayPanDown")?.SetActive(false);
        GameObject.Find("BinocularToggle")?.SetActive(false);
        GameObject.Find("CycleToggle")?.SetActive(false);
        TutorialAssets.SetActive(true);
        GameObject.Find("OverlayZoomIn").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        PlayAudio(NarratorWelcome);
    }
    public void MapZoom() {
        if (GameObject.Find("MarkerCampaign") != null) GameObject.Find("MarkerCampaign").SetActive(false);
        if (GameObject.Find("MarkerMultiplay") != null) GameObject.Find("MarkerMultiplay").SetActive(false);
        Stage = "MapZoom";
        PlayAudio(NarratorZoomInDetails);
    }
    public void MapInteractor() {
        NarrationTimer = 60;
        NarrationIndex = 9;
        PlayAudio(NarratorZoomInMap);
        GameObject.Find("MarkerTutorial").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        OverlayZoomIn.SetActive(true);
        volume_slider.SetActive(false);
        // MapScreenPanOverlay.SetActive(true);
        // if (GameObject.Find("OverlayPanDown") != null) { GameObject.Find("OverlayPanDown").SetActive(false); }
        // if (GameObject.Find("OverlayPanUp") != null) { GameObject.Find("OverlayUp").SetActive(false); }
        // if (GameObject.Find("OverlayPanLeft") != null) { GameObject.Find("OverlayPanLeft").SetActive(false); }
        // if (GameObject.Find("OverlayPanRight") != null) { GameObject.Find("OverlayPanRight").SetActive(false); }
        // if (GameObject.Find("OverlayPanZoomOut") != null) { GameObject.Find("OverlayPanZoomOut").SetActive(false); }
    }
    void SpriteFlash(string name, float start) {
        if (tutorial_timer > start ) { 
            if (GameObject.Find(name) != null) GameObject.Find(name).GetComponent<SpriteRenderer>().color = new Color(.5f + (tutorial_timer * 2) % 1, .5f + (tutorial_timer * 2) % 1, 0, 1f);
        }
    }
    void Flash(string name, float start) {
        if (tutorial_timer > start) { 
            if (GameObject.Find(name) != null) GameObject.Find(name).GetComponent<Image>().color = new Color(.5f + (tutorial_timer * 2) % 1, .5f + (tutorial_timer * 2) % 1, 0, 1f);
        }
    }
    void ResetSpriteFlash(string name, float time) {
        if (tutorial_timer > time) { 
            if (GameObject.Find(name) != null) GameObject.Find(name).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
    void ResetFlash(string name, float time) {
        if (tutorial_timer > time) { 
            if (GameObject.Find(name) != null) GameObject.Find(name).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
    int print_index = 0;
    GameObject print_obj;
    void FixedUpdate()
    {
        global_timer += Time.deltaTime;
        Gamepad gamepad = Gamepad.current;
        if (gamepad != null)
        {
            Vector2 stickL = gamepad.leftStick.ReadValue(); 
            if (stickL.y < -1/5f) {
                if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(stickL.y * 5);
                if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(stickL.y * 5);
                if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(stickL.y * 5);
                if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(stickL.y * 5);
                if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(stickL.y * 5);
            }
            if (stickL.y > 1/5f) {
                if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(stickL.y * 5);
                if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(stickL.y * 5);
                if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(stickL.y * 5);
                if (tutorial_clip_index < 5) { tutorial_timer = 0; tutorial_clip_index = 5; PlayAudio(TutorialAttackTarget); InputUseWeapon.SetActive(true); }
            }
            if (stickL.x < -1/5f) {
                if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(stickL.x * 5);
                if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-stickL.x * 5);
                if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(stickL.x * 5);
                if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-stickL.x * 5);
            }
            if (stickL.x > 1/5f) {
                if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(stickL.x * 5);
                if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-stickL.x * 5);
                if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(stickL.x * 5);
                if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-stickL.x * 5);
            }
        }
        if (InputField.text.Contains("Processor") && TabToggle.text == "▤ TUI") { 
            RenderText(Processor.GetComponent<ProcessorController>().interpreter.ToString());
        }
        if (queue_audio != "") {
            if (GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().frame != -1) {
                LoadingScreen.SetActive(false);
                PlayAudio(LookupNarration(queue_audio));
                queue_audio = "";
                if (story_timer != -1) {
                    story_timer = 0;
                }
                if (start_timer != -1) {
                    start_timer = 0;
                }
            }
        } else if (printing) {
            if (print_index < GameObject.Find("Example").transform.GetChild(0).GetComponentsInChildren<ComponentController>().Length) {
                if (print_obj == null) print_obj = GameObject.Find("Example").transform.GetChild(0).GetComponentsInChildren<ComponentController>()[print_index++].gameObject;
                if (GameObject.Find("Printer").GetComponent<PrinterController>().GoTo(print_obj.transform.localPosition + new Vector3(0, 0f, 0))) {
                    print_obj.GetComponent<ComponentController>().Launch();
                    print_obj = null;
                }
                else {
                    return;
                }
            } else {
                Printer.SetActive(false);
                ClearText();
                PrinterLeft.SetActive(false);
                PrinterRight.SetActive(false);
                PrinterPrint.SetActive(false);                
                Ship.Start();
                OverlayInteractor.UpdateOptions();
                if (tutorial_clip_index < 3) {
                    tutorial_timer = 0; tutorial_clip_index = 3; PlayAudio(TutorialLookAround);
                }
                // Ship.Start();
                // OverlayInteractor.UpdateOptions();
                // OverlayInteractor.OnDropdownChange("Printer"); 
                OverlayInteractor.gameObject.SetActive(false);
                OverlayZoomIn.SetActive(true);
                MapScreenPanOverlay.SetActive(true);
                if (tutorial_timer == -1) {
                    Subtitles.SetActive(false);
                    SubtitlesShadow.SetActive(false);
                }
                else 
                {
                    // if (GameObject.Find("OverlayPanDown")) { GameObject.Find("OverlayPanDown").SetActive(false); }
                }
                printing = false;
            }
        } else if (Stage == "MapInterface" || Stage == "MapZoom" || Stage == "MapZoomed") {
            NarrationTimer += Time.deltaTime;              
            if (Stage == "MapZoom") {
                // if ((int)(global_timer / 4f) % 2 == 0) {

                    Timer.text = "SEA-TAC\nINTERNAT\nAIRPORT";
                // } else {
                //     Timer.text = FloatToTime(global_timer) + "\n" + System.DateTime.Now.AddYears(-54).ToString("MM/dd/yyyy");
                // }
            } else {
                Timer.text = FloatToTime(global_timer) + "\n" + NarrationIndex + ":" + FloatToTime(NarrationTimer) + "\n" + System.DateTime.Now.AddYears(-54).ToString("MM/dd/yyyy");
            } 
            if (NarrationTimer >= NarrationTiming[NarrationIndex]) {
                MapSubtitles(Narration[NarrationIndex]);
                NarrationIndex++;
                switch (NarrationIndex) {
                    case 5: 
                    PlayAudio(NarratorAimTheCrosshair);
                    break;
                }
            }
            if (NarrationIndex >= 5 && NarrationIndex <= 8) {
                GameObject.Find("MarkerTutorial").GetComponent<SpriteRenderer>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            }
            else if (NarrationIndex >= 9 && NarrationIndex < 14) {
                if (GameObject.Find("OverlayZoomIn") != null) GameObject.Find("OverlayZoomIn").GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            }
            // NarrationIndex
        } else if (start_timer != -1) {
            if ((start_timer / 5f ) % 2 < 1) {
                Timer.text = FloatToTime(start_timer) + "\n" + System.DateTime.Now.AddYears(-54).ToString("MM/dd/yyyy");
            } else {
                Timer.text = System.DateTime.Now.ToString("h:mm:ss.f") + "\n" + System.DateTime.Now.AddYears(-54).ToString("MM/dd/yyyy");
            }
            start_timer += Time.deltaTime;
            MapSubtitlesAtTime("We interrupt this\n", 0f, start_timer);
            MapSubtitlesAtTime("We interrupt this\nprogram", .75f, start_timer);
            MapSubtitlesAtTime("to bring you a special\n", 1.25f, start_timer);
            MapSubtitlesAtTime("to bring you a special\nnews bulletin.", 2f, start_timer);
            MapSubtitlesAtTime("A state of emergency\n", 2.75f, start_timer);
            MapSubtitlesAtTime("A state of emergency\nhas been declared", 3.5f, start_timer);
            MapSubtitlesAtTime("by the President of\n", 4.5f, start_timer);
            MapSubtitlesAtTime("by the President of\nthe United States!", 5.25f, start_timer);
            MapSubtitlesAtTime("We're switching live to\n", 6.25f, start_timer);
            MapSubtitlesAtTime("Wilsens Glenn,\n", 7.25f, start_timer);
            MapSubtitlesAtTime("Wilsens Glenn,\nNew Jersey", 8f, start_timer);
            MapSubtitlesAtTime("where the landing of\n", 8.75f, start_timer);
            MapSubtitlesAtTime("hundreds of unidentified\n", 9.5f, start_timer);
            MapSubtitlesAtTime("hundreds of unidentified\nspacecraft", 10.5f, start_timer);
            MapSubtitlesAtTime("have now been officially\n", 11.5f, start_timer);
            MapSubtitlesAtTime("have now been officially\nconfirmed as", 12.5f, start_timer);
            MapSubtitlesAtTime("a full scale invasion\n", 13.5f, start_timer);
            MapSubtitlesAtTime("a full scale invasion\nof the Earth", 14.5f, start_timer);
            MapSubtitlesAtTime("by Martians!\n", 15.25f, start_timer);
            MapSubtitlesAtTime("", 16f, start_timer);
            MapSubtitlesAtTime("We're seeing ...\n", 18.75f, start_timer);
            MapSubtitlesAtTime("We're seeing ...\nit's horrible", 19.25f, start_timer);
            MapSubtitlesAtTime("I can't believe\n", 20.25f, start_timer);
            MapSubtitlesAtTime("I can't believe\nmy eyes ...", 20.75f, start_timer);
            MapSubtitlesAtTime("People are dying ...\n", 21.5f, start_timer);
            MapSubtitlesAtTime("People are dying ...\nbeing trampled", 22.25f, start_timer);
            MapSubtitlesAtTime("in their efforts to\n", 23.25f, start_timer);
            MapSubtitlesAtTime("in their efforts to\nescape!", 24f, start_timer);
            MapSubtitlesAtTime("Power lines are down\n", 25.5f, start_timer);
            MapSubtitlesAtTime("Power lines are down\neverywhere!", 26.5f, start_timer);
            MapSubtitlesAtTime("We could be cut off at\n", 27.5f, start_timer);
            MapSubtitlesAtTime("We could be cut off at\nany minute!", 28.5f, start_timer);
            MapSubtitlesAtTime("", 29f, start_timer);
            MapSubtitlesAtTime("There's another group of\n", 30.25f, start_timer);
            MapSubtitlesAtTime("There's another group of\nspaceships", 31.25f, start_timer);
            MapSubtitlesAtTime("There's another group of\nalien ships", 32.25f, start_timer);
            MapSubtitlesAtTime("They're coming out of\n", 33f, start_timer);
            MapSubtitlesAtTime("They're coming out of\nthe sky!", 34f, start_timer);
            MapSubtitlesAtTime("⛈", 36f, start_timer);
            if (start_timer > 41 || (Input.GetMouseButton(0) && CheckInsideEdge())) 
            {
                Stage = "MapInterface";
                InputField.text = "☄ BitNaughts";
                SplashScreen.SetActive(false);
                start_timer = -1;
                
                PlayAudio(NarratorSwitchToMap);
                // tutorial_timer = -.5f;
                // Subtitles.SetActive(false);
                // SubtitlesShadow.SetActive(false);
                // OnMapView();
                // camera.GetComponent<AudioSource>().Stop();
                SetBackground(new Color(105/255f, 146/255f, 176/255f));
                // Overlay
                GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().enabled = false;
                // volume_slider.SetActive(false);
                // InputField.text = "☄ BitNaughts";
            }
        } else if (tutorial_timer != -1) {
            if (tutorial_timer == -.5f) { 
                PlayAudio(TutorialOpening);
                TutorialAssets.SetActive(true);
                Subtitles.SetActive(true);
                SubtitlesShadow.SetActive(true);
                tutorial_timer = 0;
                PlayMusic(IntroMusic);
            }
            tutorial_timer += Time.deltaTime;
            switch (tutorial_clip_index) {
                case 0:
                    MapSubtitlesAtTime("Welcome to the US Naval\n", 0f, tutorial_timer);
                    MapSubtitlesAtTime("Welcome to the US Naval\nAcademy!", 1.75f, tutorial_timer);
                    MapSubtitlesAtTime("Today, you will learn\n", 3f, tutorial_timer);
                    MapSubtitlesAtTime("Today, you will learn\nship control.", 4.5f, tutorial_timer);
                    if (tutorial_timer > 6) { tutorial_timer = 0; tutorial_clip_index++; PlayAudio(TutorialLockOn); }
                    break;
                case 1:
                    SpriteFlash("Printer", 0);
                    MapSubtitlesAtTime("First off, let's\n", 0f, tutorial_timer);
                    MapSubtitlesAtTime("First off, let's\nintroduce", 1.25f, tutorial_timer);
                    MapSubtitlesAtTime("the Target Window.\n", 2f, tutorial_timer);
                    MapSubtitlesAtTime("Now the Target Window\n", 4f, tutorial_timer);
                    MapSubtitlesAtTime("Now the Target Window\nappears whenever", 5.5f, tutorial_timer);
                    MapSubtitlesAtTime("you look at a unit\n", 6.5f, tutorial_timer);
                    MapSubtitlesAtTime("you look at a unit\nwith the crosshair", 7.75f, tutorial_timer);
                    MapSubtitlesAtTime("Have a go at this now!\n", 9.25f, tutorial_timer);
                    break;
                case 2:
                    ResetSpriteFlash("Printer", 0);
                    Flash("InputPrinterPrint", 0);
                    MapSubtitlesAtTime("Good, good.\n", 0f, tutorial_timer);
                    MapSubtitlesAtTime("You can also use the\n", 1f, tutorial_timer);
                    MapSubtitlesAtTime("You can also use the\ncrosshair", 1.5f, tutorial_timer);
                    MapSubtitlesAtTime("to issue orders to your\n", 2f, tutorial_timer);
                    MapSubtitlesAtTime("currently seleted unit.\n", 3f, tutorial_timer);
                    break;
                // case 3:
                //     ResetFlash("InputPrinterPrint", 0);
                //     MapSubtitlesAtTime("Outstanding!\n", 0f, tutorial_timer);
                //     MapSubtitlesAtTime("The Target Window\n", 1.5f, tutorial_timer);
                //     MapSubtitlesAtTime("The Target Window\ndisplays", 2.75f, tutorial_timer);
                //     MapSubtitlesAtTime("the Unit's name\n", 3.25f, tutorial_timer);
                //     MapSubtitlesAtTime("the Unit's name\nand what", 4.5f, tutorial_timer);
                //     MapSubtitlesAtTime("class it is.\n", 5.4f, tutorial_timer);
                //     MapSubtitlesAtTime("", 6.5f, tutorial_timer);
                //     break;
                case 3:
                    ResetFlash("InputPrinterPrint", 0);
                    ResetSpriteFlash("Printer", 0);
                    Flash("OverlayPanUp", 0);
                    Flash("OverlayPanDown", 0);
                    Flash("OverlayPanLeft", 0);
                    Flash("OverlayPanRight", 0);
                    Flash("OverlayZoomIn", 0);
                    Flash("OverlayZoomOut", 0);
                    MapSubtitlesAtTime("First off, try looking\n", 0f, tutorial_timer);
                    MapSubtitlesAtTime("First off, try looking\naround.", 1f, tutorial_timer);
                    MapSubtitlesAtTime("360° awareness is needed\n", 2f, tutorial_timer);
                    MapSubtitlesAtTime("360° awareness is needed\nfor dogfighting", 2.5f, tutorial_timer);
                    break;
                case 4:
                    ResetFlash("OverlayPanUp", 0);
                    ResetFlash("OverlayPanDown", 0);
                    ResetFlash("OverlayPanLeft", 0);
                    ResetFlash("OverlayPanRight", 0);
                    ResetFlash("OverlayZoomIn", 0);
                    ResetFlash("OverlayZoomOut", 0);
                    Flash("InputJoystick", 0);
                    MapSubtitlesAtTime("Great!\n", 0f, tutorial_timer);
                    MapSubtitlesAtTime("Time to get this baby\n", 1f, tutorial_timer);
                    MapSubtitlesAtTime("Time to get this baby\nairborn", 1.5f, tutorial_timer);
                    MapSubtitlesAtTime("Set your throttle to\n", 3f, tutorial_timer);
                    MapSubtitlesAtTime("Set your throttle to\nmaximum", 4f, tutorial_timer);
                    break;
                case 5:
                    ResetFlash("InputJoystick", 0);
                    ResetFlash("OverlayPanUp", 0);
                    ResetFlash("OverlayPanDown", 0);
                    ResetFlash("OverlayPanLeft", 0);
                    ResetFlash("OverlayPanRight", 0);
                    ResetFlash("OverlayZoomIn", 0);
                    ResetFlash("OverlayZoomOut", 0);
                    SpriteFlash("martian_hulk_1", 0);
                    Flash("InputUseWeapon", 0);
                    MapSubtitlesAtTime("Okay cadet, strafe that\n", 0f, tutorial_timer);
                    MapSubtitlesAtTime("Okay cadet, strafe that\ncargo ship", 1.5f, tutorial_timer);
                    MapSubtitlesAtTime("until she goes down!\n", 2.5f, tutorial_timer);
                    if (tutorial_timer > 5) { tutorial_timer = 0; tutorial_clip_index++; PlayAudio(TutorialUseWeapon); }
                    break;
                case 6:
                    ResetFlash("InputJoystick", 0);
                    ResetFlash("OverlayPanUp", 0);
                    ResetFlash("OverlayPanDown", 0);
                    ResetFlash("OverlayPanLeft", 0);
                    ResetFlash("OverlayPanRight", 0);
                    ResetFlash("OverlayZoomIn", 0);
                    ResetFlash("OverlayZoomOut", 0);
                    Flash("InputUseWeapon", 0);
                    SpriteFlash("martian_hulk_1", 0);
                    MapSubtitlesAtTime("Press the Use Weapon\n", 0f, tutorial_timer);
                    MapSubtitlesAtTime("Press the Use Weapon\ncontrol", 1.5f, tutorial_timer);
                    MapSubtitlesAtTime("to fire!\n", 2.5f, tutorial_timer);
                    MapSubtitlesAtTime("", 3.5f, tutorial_timer);
                    break;
                case 7:
                    ResetFlash("InputUseWeapon", 0);
                    SpriteFlash("martian_hulk_1", 0);
                    if (BoosterR.activeSelf) {
                        MapSubtitlesAtTime("Although torpedos are\n", 0f, tutorial_timer);
                        MapSubtitlesAtTime("Although torpedos are\npowerful,", 1.5f, tutorial_timer);
                        MapSubtitlesAtTime("they're slow!\n", 2.75f, tutorial_timer);
                        MapSubtitlesAtTime("Firing a spread makes\n", 3.75f, tutorial_timer);
                        MapSubtitlesAtTime("Firing a spread makes\nit harder", 4.75f, tutorial_timer);
                        MapSubtitlesAtTime("for your enemy to\n", 5.75f, tutorial_timer);
                        MapSubtitlesAtTime("for your enemy to\navoid them.", 6.75f, tutorial_timer);
                        MapSubtitlesAtTime("", 8f, tutorial_timer);
                    } else {
                        MapSubtitlesAtTime("Shoot that cargo hulk\n", 0f, tutorial_timer);
                        MapSubtitlesAtTime("Shoot that cargo hulk\nfive times!", 1.25f, tutorial_timer);
                        MapSubtitlesAtTime("", 2.5f, tutorial_timer);
                    }
                    if (TutorialAssets.transform.childCount < 3) {
                        PlayAudio(TutorialHulkDestroyed);
                        tutorial_clip_index = 8;
                        tutorial_timer = 0;
                    }
                    break;
                case 8:
                    MapSubtitlesAtTime("Ha! Great stuff!\n", 0f, tutorial_timer);
                    MapSubtitlesAtTime("Let's move on!\n", 2f, tutorial_timer);
                    if (tutorial_timer > 4) { tutorial_timer = 0; tutorial_clip_index++; PlayAudio(TutorialBinocular); }
                    break;
                case 9:
                    Flash("BinocularToggle", 0f);
                    MapSubtitlesAtTime("You can also shoot from\n", 0f, tutorial_timer);
                    MapSubtitlesAtTime("You can also shoot from\nBinocular View,", 2f, tutorial_timer);
                    MapSubtitlesAtTime("which can help you aim\n", 3f, tutorial_timer);
                    MapSubtitlesAtTime("which can help you aim\nmore accurately.", 4f, tutorial_timer);
                    MapSubtitlesAtTime("", 5f, tutorial_timer);
                    if (TutorialAssets.transform.childCount < 2) {
                        tutorial_clip_index = 13;
                        tutorial_timer = 0;
                        PlayAudio(TutorialRightOn);
                    }
                    break;
                case 10:
                    ResetFlash("BinocularToggle", 0);
                    Flash("CycleToggle", 0);
                    MapSubtitlesAtTime("You can also use the\n", 0f, tutorial_timer);
                    MapSubtitlesAtTime("You can also use the\nTarget Button", 2f, tutorial_timer);
                    MapSubtitlesAtTime("to cycle targets\n", 3.5f, tutorial_timer);
                    MapSubtitlesAtTime("to cycle targets\nbetween all", 4.5f, tutorial_timer);
                    MapSubtitlesAtTime("detected enemy units\n", 5.5f, tutorial_timer);
                    MapSubtitlesAtTime("", 6.5f, tutorial_timer);
                    SpriteFlash("martian_hulk_2", 0);
                    SpriteFlash("martian_hulk_3", 0);
                    if (TutorialAssets.transform.childCount < 2) {
                        tutorial_clip_index = 13;
                        tutorial_timer = 0;
                        PlayAudio(TutorialRightOn);
                    }
                    break;
                case 11:
                    Flash("CycleToggle", 0);
                    ResetFlash("BinocularToggle", 0);
                    MapSubtitlesAtTime("Good!\n", 0f, tutorial_timer);
                    MapSubtitlesAtTime("Good! One more time.\n", 1f, tutorial_timer);
                    if (TutorialAssets.transform.childCount < 2) {
                        tutorial_clip_index = 13;
                        tutorial_timer = 0;
                        PlayAudio(TutorialRightOn);
                    }
                    break;
                case 12:
                    ResetFlash("CycleToggle", 0);
                    ResetFlash("BinocularToggle", 0);
                    SpriteFlash("martian_hulk_2", 0);
                    SpriteFlash("martian_hulk_3", 0);
                    MapSubtitlesAtTime("Good!\n", 0f, tutorial_timer);
                    MapSubtitlesAtTime("\n", 0f, tutorial_timer);
                    if (TutorialAssets.transform.childCount < 2) {
                        tutorial_clip_index = 13;
                        tutorial_timer = 0;
                        PlayAudio(TutorialRightOn);
                    }
                    break;
                case 13:
                    ResetFlash("CycleToggle", 0);
                    ResetFlash("BinocularToggle", 0);
                    SpriteFlash("martian_hulk_2", 0);
                    SpriteFlash("martian_hulk_3", 0);
                    MapSubtitlesAtTime("That was right on the\n", 0f, tutorial_timer);
                    MapSubtitlesAtTime("That was right on the\nmoney!", 1.5f, tutorial_timer);
                    MapSubtitlesAtTime("Great stuff!\n", 2.25f, tutorial_timer);
                    MapSubtitlesAtTime("\n", 3f, tutorial_timer);
                    if (TutorialAssets.transform.childCount == 0) {
                        tutorial_clip_index = 14;
                        tutorial_timer = 0;
                        PlayAudio(TutorialOutro);
                    }
                    break;
                case 14:
                    ResetFlash("CycleToggle", 0);
                    ResetFlash("BinocularToggle", 0);
                    MapSubtitlesAtTime("Excellence work!\n", 0f, tutorial_timer);
                    MapSubtitlesAtTime("You have now completed\n", 1.5f, tutorial_timer);
                    MapSubtitlesAtTime("You have now completed\nthe tutorial", 2.5f, tutorial_timer);   
                    MapSubtitlesAtTime("I hope you never have\n", 4f, tutorial_timer);            
                    MapSubtitlesAtTime("I hope you never have\ncause to use", 5f, tutorial_timer);  
                    MapSubtitlesAtTime("the knowledge you have\n", 6f, tutorial_timer);           
                    MapSubtitlesAtTime("the knowledge you have\njust acquired.", 7f, tutorial_timer);  
                    MapSubtitlesAtTime("That is all for today!\n", 9f, tutorial_timer);          
                    MapSubtitlesAtTime("That is all for today!\nDismissed!", 10.5f, tutorial_timer);       
                    if (tutorial_timer > 12 ) { // || Input.GetMouseButton(0) && CheckInsideEdge()
                        AppendText("$");
                        // SetCommand("$ tutorial");
                        OnCodeView();
                        tutorial_timer = -1;
                        tutorial_clip_index = -1;
                        Subtitles.SetActive(false);
                        SubtitlesShadow.SetActive(false);
                    }  
                    break;
            }
            if ((animation_timer / 7.77f ) % 2 < 1) {
                Timer.text = FloatToTime(tutorial_timer) + "\n" + System.DateTime.Now.AddYears(-54).ToString("MM/dd/yyyy");
            } else {
                Timer.text = System.DateTime.Now.ToString("h:mm:ss.f") + "\n" + System.DateTime.Now.AddYears(-54).ToString("MM/dd/yyyy");
            } 
        } else if (story_timer > -1 && clip_index > -1) {
            // story_timer += Time.deltaTime;
            //             MapSubtitlesAtTime("⛈", 0, story_timer);
            //             MapSubtitlesAtTime("⛅", 2f, story_timer);
            //             MapSubtitlesAtTime("7 ...", 6.75f, story_timer);
            //             MapSubtitlesAtTime("6 ...", 7.75f, story_timer);
            //             MapSubtitlesAtTime("5 ...", 8.75f, story_timer);
            //             MapSubtitlesAtTime("4 ...", 9.75f, story_timer);
            //             MapSubtitlesAtTime("3 ...", 10.75f, story_timer);
            //             MapSubtitlesAtTime("2 ...", 11.75f, story_timer);
            //             MapSubtitlesAtTime("1 ...", 12.75f, story_timer);
            //             MapSubtitlesAtTime("☄ Tap to continue", 13.75f, story_timer);
            //             MapSubtitlesAtTime("Today, orbitting", 18.5f, story_timer);
            //             MapSubtitlesAtTime("satellites of the", 19.75f, story_timer);
            //             MapSubtitlesAtTime("Navy Navigation", 20.75f, story_timer);
            //             MapSubtitlesAtTime("Satellite System", 21.5f, story_timer);
            //             MapSubtitlesAtTime("provide around-the-", 23.5f, story_timer);
            //             MapSubtitlesAtTime("clock ultraprecise", 24.5f, story_timer);
            //             MapSubtitlesAtTime("position fixes", 26.25f, story_timer);
            //             MapSubtitlesAtTime("from space", 27.5f, story_timer);
            //             MapSubtitlesAtTime("to units of", 28.5f, story_timer);
            //             MapSubtitlesAtTime("the fleet,", 29.25f, story_timer);
            //             MapSubtitlesAtTime("everywhere,", 30f, story_timer);
            //             MapSubtitlesAtTime("in any kind", 31f, story_timer);
            //             MapSubtitlesAtTime("of weather.", 32.25f, story_timer);
            //             MapSubtitlesAtTime("⛈", 34.25f, story_timer);
            //             MapSubtitlesAtTime("Navigation", 39f, story_timer);
            //             MapSubtitlesAtTime("by satellite,", 40f, story_timer);
            //             MapSubtitlesAtTime("how and why", 42f, story_timer);
            //             MapSubtitlesAtTime("does it work?", 43.25f, story_timer);
            //             MapSubtitlesAtTime("First, a little", 44.75f, story_timer);
            //             MapSubtitlesAtTime("astrophysics", 45.5f, story_timer);
            //             MapSubtitlesAtTime("to answer why.", 46.75f, story_timer);
            //             MapSubtitlesAtTime("⛈", 48.25f, story_timer);
            //             MapSubtitlesAtTime("Any satellite, man-", 54.5f, story_timer);
            //             MapSubtitlesAtTime("made or not,", 56f, story_timer);
            //             MapSubtitlesAtTime("remains in orbit", 57f, story_timer);
            //             MapSubtitlesAtTime("because the force with", 58f, story_timer);
            //             MapSubtitlesAtTime("which it is trying to", 59.5f, story_timer);
            //             MapSubtitlesAtTime("fly away from Earth", 60.5f, story_timer);
            //             MapSubtitlesAtTime("is matched by the", 62.25f, story_timer);
            //             MapSubtitlesAtTime("gravitation pull", 63.25f, story_timer);
            //             MapSubtitlesAtTime("of Earth.", 64.75f, story_timer);
            //             MapSubtitlesAtTime("So it continues", 66.25f, story_timer);
            //             MapSubtitlesAtTime("moving around Earth", 67.25f, story_timer);
            //             MapSubtitlesAtTime("in an orbit whose", 68.75f, story_timer);
            //             MapSubtitlesAtTime("path conforms very", 70.25f, story_timer);
            //             MapSubtitlesAtTime("nearly to the", 71f, story_timer);
            //             MapSubtitlesAtTime("classic laws of Sir", 72f, story_timer);
            //             MapSubtitlesAtTime("Isaac Newton", 73f, story_timer);
            //             MapSubtitlesAtTime("and Johannes Kepler", 74f, story_timer);
            //             MapSubtitlesAtTime("Tap to continue ...", 76f, story_timer);

                //     MapSubtitlesAtTime("⛈", 0, story_timer);
                //     MapSubtitlesAtTime("⛅", 2f, story_timer);
                //     MapSubtitlesAtTime("Suppose we put", 3.5f, story_timer);
                //     MapSubtitlesAtTime("a radio transmitter", 4.5f, story_timer);
                //     MapSubtitlesAtTime("in a satellite.", 5.5f, story_timer);
                //     MapSubtitlesAtTime("You will detect", 9.25f, story_timer);
                //     MapSubtitlesAtTime("that the radio", 10.25f, story_timer);
                //     MapSubtitlesAtTime("frequency is", 11f, story_timer);
                //     MapSubtitlesAtTime("doppler shifted", 11.75f, story_timer);
                //     MapSubtitlesAtTime("while the satellite", 13f, story_timer);
                //     MapSubtitlesAtTime("passes by.", 14f, story_timer);
                //     MapSubtitlesAtTime("The doppler effect", 18.25f, story_timer);
                //     MapSubtitlesAtTime("shows up as an", 19.25f, story_timer);
                //     MapSubtitlesAtTime("apparent change in", 20.25f, story_timer);
                //     MapSubtitlesAtTime("frequency, and", 21.25f, story_timer);
                //     MapSubtitlesAtTime("is caused by the", 22.25f, story_timer);
                //     MapSubtitlesAtTime("relative motion", 23.25f, story_timer);
                //     MapSubtitlesAtTime("between the satellite", 24.5f, story_timer);
                //     MapSubtitlesAtTime("transmitter and the", 25.25f, story_timer);
                //     MapSubtitlesAtTime("receiving antenna", 27f, story_timer);
                //     MapSubtitlesAtTime("on Earth.", 29f, story_timer);
                //     MapSubtitlesAtTime("Tap to continue ...", 31, story_timer);
                //     break;

                //     MapSubtitlesAtTime("⛅", 2f, story_timer);
                //     MapSubtitlesAtTime("Doppler shift", 3.5f, story_timer);
                //     MapSubtitlesAtTime("can be plotted", 4.5f, story_timer);
                //     MapSubtitlesAtTime("frequency versus", 5.5f, story_timer);
                //     MapSubtitlesAtTime("time.", 7.25f, story_timer);
                //     MapSubtitlesAtTime("to produce a", 8.25f, story_timer);
                //     MapSubtitlesAtTime("unique curve.", 9.25f, story_timer);
                //     MapSubtitlesAtTime("Which can be", 10.75f, story_timer);
                //     MapSubtitlesAtTime(" received at only", 11.8f, story_timer);
                //     MapSubtitlesAtTime("one point on Earth", 11.8f, story_timer);
                //     MapSubtitlesAtTime("at a given instant.", 13.75f, story_timer);
                //     MapSubtitlesAtTime("Knowing your position", 18f, story_timer);
                //     MapSubtitlesAtTime("on Earth,", 18f, story_timer);
                //     MapSubtitlesAtTime("you can use the", 20f, story_timer);
                //     MapSubtitlesAtTime("Doppler Curve", 21.25f, story_timer);
                //     MapSubtitlesAtTime("to calculate", 22.25f, story_timer);
                //     MapSubtitlesAtTime("the exact orbit", 23f, story_timer);
                //     MapSubtitlesAtTime("of the satellite", 24f, story_timer);
                //     MapSubtitlesAtTime("And the reverse", 27f, story_timer);
                //     MapSubtitlesAtTime("is true,", 28f, story_timer);
                //     MapSubtitlesAtTime("if you start with", 30f, story_timer);
                //     MapSubtitlesAtTime("the satellite of", 31f, story_timer);
                //     MapSubtitlesAtTime("known orbit,", 31f, story_timer);
                //     MapSubtitlesAtTime("analysis of the", 33f, story_timer);
                //     MapSubtitlesAtTime("Doppler Shift as", 34f, story_timer);
                //     MapSubtitlesAtTime("the satellite passes,", 35f, story_timer);
                //     MapSubtitlesAtTime("will tell you exactly", 36.5f, story_timer);
                //     MapSubtitlesAtTime("where you are,", 37.75f, story_timer);
                //     MapSubtitlesAtTime("anywhere on Earth.", 39f, story_timer);
                //     MapSubtitlesAtTime("This fact", 41f, story_timer);
                //     MapSubtitlesAtTime("forms the basis", 42.5f, story_timer);
                //     MapSubtitlesAtTime("of the Navy", 43.5f, story_timer);
                //     MapSubtitlesAtTime("Navigation Satellite", 44.25f, story_timer);
                //     MapSubtitlesAtTime("System.", 45.5f, story_timer);
                //     MapSubtitlesAtTime("The system is more", 47.75f, story_timer);
                //     MapSubtitlesAtTime("accurate than", 48.75f, story_timer);
                //     MapSubtitlesAtTime("any other navigation", 50f, story_timer);
                //     MapSubtitlesAtTime("system known today.", 51f, story_timer);
                //     MapSubtitlesAtTime("Accuracy that can", 54.5f, story_timer);
                //     MapSubtitlesAtTime("establish a pinpoint of", 55.5f, story_timer);
                //     MapSubtitlesAtTime("latitude and longitude.", 57.5f, story_timer);
                //     MapSubtitlesAtTime("This precision", 59.5f, story_timer);
                //     MapSubtitlesAtTime("is the result", 60.5f, story_timer);
                //     MapSubtitlesAtTime("of new techiques", 61.5f, story_timer);
                //     MapSubtitlesAtTime("for fine control", 62.5f, story_timer);
                //     MapSubtitlesAtTime("and measurement", 63.5f, story_timer);
                //     MapSubtitlesAtTime("of time", 64.5f, story_timer);
                //     MapSubtitlesAtTime("in terms of", 64.5f, story_timer);
                //     MapSubtitlesAtTime("frequencies generated", 64.5f, story_timer);
                //     MapSubtitlesAtTime("by ultrastable", 64.5f, story_timer);
                //     MapSubtitlesAtTime("oscillators.", 64.5f, story_timer);
                //     MapSubtitlesAtTime("Of course,", 71.75f, story_timer);
                //     MapSubtitlesAtTime("There are many", 72.25f, story_timer);
                //     MapSubtitlesAtTime("astronautical", 73.25f, story_timer);
                //     MapSubtitlesAtTime("problems that", 74.25f, story_timer);
                //     MapSubtitlesAtTime("confront this new", 75.5f, story_timer);
                //     MapSubtitlesAtTime("system.", 76.5f, story_timer);
                //     MapSubtitlesAtTime("Tap to continue ...", 78.5f, story_timer);
                //     break;
                //     MapSubtitlesAtTime("⛈", 0, story_timer);
                //     MapSubtitlesAtTime("⛅", 2f, story_timer);
                //     MapSubtitlesAtTime("Series of experi-", 2.5f, story_timer);
                //     MapSubtitlesAtTime("mental satellites.", 3.5f, story_timer);
                //     MapSubtitlesAtTime("Beginning with the", 4.5f, story_timer);
                //     MapSubtitlesAtTime("launch of the", 5.5f, story_timer);
                //     MapSubtitlesAtTime("\"NAV-1B\" satellite", 6.25f, story_timer);
                //     MapSubtitlesAtTime("in April, nineteen sixty.", 7.5f, story_timer);
                //     MapSubtitlesAtTime("⛈", 9.5f, story_timer);
                //     MapSubtitlesAtTime("To cover the whole Earth", 11f, story_timer);
                //     MapSubtitlesAtTime("a satellite must be in", 12.5f, story_timer);
                //     MapSubtitlesAtTime("near-polar orbit.", 13.5f, story_timer);
                //     MapSubtitlesAtTime("That is, an", 15f, story_timer);
                //     MapSubtitlesAtTime("orbit passing", 16f, story_timer);
                //     MapSubtitlesAtTime("near the north and", 17f, story_timer);
                //     MapSubtitlesAtTime("south poles.", 18f, story_timer);
                //     MapSubtitlesAtTime("As the Earth", 20.5f, story_timer);
                //     MapSubtitlesAtTime("rotates beneath the", 21.5f, story_timer);
                //     MapSubtitlesAtTime("fixed plane of", 22.5f, story_timer);
                //     MapSubtitlesAtTime("the orbit,", 23.5f, story_timer);
                //     MapSubtitlesAtTime("which passes completely", 24.5f, story_timer);
                //     MapSubtitlesAtTime("around Earth,", 25.75f, story_timer);
                //     MapSubtitlesAtTime("Sooner or later", 28f, story_timer);
                //     MapSubtitlesAtTime("the satellite will", 29.5f, story_timer);
                //     MapSubtitlesAtTime("pass within", 30.5f, story_timer);
                //     MapSubtitlesAtTime("range of any", 31.25f, story_timer);
                //     MapSubtitlesAtTime("part of the globe.", 32.25f, story_timer);
                //     MapSubtitlesAtTime("With one satellite", 35f, story_timer);
                //     MapSubtitlesAtTime("in orbit,", 35.75f, story_timer);
                //     MapSubtitlesAtTime("a particular point", 37.25f, story_timer);
                //     MapSubtitlesAtTime("on the Earth", 38.25f, story_timer);
                //     MapSubtitlesAtTime("is within range", 39.25f, story_timer);
                //     MapSubtitlesAtTime("at least once", 40.25f, story_timer);
                //     MapSubtitlesAtTime("each twelve hours.", 41.75f, story_timer);
                //     MapSubtitlesAtTime("Ships and submarines", 46.5f, story_timer);
                //     MapSubtitlesAtTime("need to know", 47.75f, story_timer);
                //     MapSubtitlesAtTime("their position more", 48.25f, story_timer);
                //     MapSubtitlesAtTime("frequently that this", 49.25f, story_timer);
                //     MapSubtitlesAtTime("and therefore", 51.75f, story_timer);
                //     MapSubtitlesAtTime("the \"Navy Navigation\"", 52.5f, story_timer);
                //     MapSubtitlesAtTime("\"Satellite System\"", 53.75f, story_timer);
                //     MapSubtitlesAtTime("employs a constell-", 54.75f, story_timer);
                //     MapSubtitlesAtTime("ation of satellites,", 55.75f, story_timer);
                //     MapSubtitlesAtTime("each in a polar orbit.", 57.5f, story_timer);
                //     MapSubtitlesAtTime("Tap to continue ...", 59.5f, story_timer);
                //     break;

                //     MapSubtitlesAtTime("⛈", 0, story_timer);
                //     MapSubtitlesAtTime("⛅", 2f, story_timer);
                //     MapSubtitlesAtTime("The message is", 3.5f, story_timer);
                //     MapSubtitlesAtTime("injected into the", 4.25f, story_timer);
                //     MapSubtitlesAtTime("satellite by", 5.25f, story_timer);
                //     MapSubtitlesAtTime("high-power radio", 6.25f, story_timer);
                //     MapSubtitlesAtTime("radio transmission.", 7f, story_timer);
                //     MapSubtitlesAtTime("This updates", 11.5f, story_timer);
                //     MapSubtitlesAtTime("old information", 12.5f, story_timer);
                //     MapSubtitlesAtTime("stored in satellite", 14f, story_timer);
                //     MapSubtitlesAtTime("memory,", 15f, story_timer);
                //     MapSubtitlesAtTime("and extends the", 16.5f, story_timer);
                //     MapSubtitlesAtTime("navigational utility", 17.25f, story_timer);
                //     MapSubtitlesAtTime("of that satellite.", 18.5f, story_timer);
                //     MapSubtitlesAtTime("The system works", 20f, story_timer);
                //     MapSubtitlesAtTime("anywhere in the world", 21.25f, story_timer);
                //     MapSubtitlesAtTime("night or day, in", 23f, story_timer);
                //     MapSubtitlesAtTime("any kind of weather.", 25f, story_timer);
                //     MapSubtitlesAtTime("For every pound", 35f, story_timer);
                //     MapSubtitlesAtTime("of satellite in", 36f, story_timer);
                //     MapSubtitlesAtTime("orbit, there are", 37f, story_timer);
                //     MapSubtitlesAtTime("tons of equipment", 38f, story_timer);
                //     MapSubtitlesAtTime("on Earth", 39f, story_timer);
                //     MapSubtitlesAtTime("that make navigation", 40.25f, story_timer);
                //     MapSubtitlesAtTime("by satellite", 41.25f, story_timer);
                //     MapSubtitlesAtTime("possible.", 42.75f, story_timer);
                //     MapSubtitlesAtTime("Tap to continue ...", 44.75f, story_timer);
                //     break;
                //     MapSubtitlesAtTime("⛈", 0, story_timer);
                //     MapSubtitlesAtTime("⛅", 2f, story_timer);
                //     MapSubtitlesAtTime("We're inside the", 4.25f, story_timer);
                //     MapSubtitlesAtTime("\"Operation Center\"", 5.25f, story_timer);
                //     MapSubtitlesAtTime("command post for", 7.25f, story_timer);
                //     MapSubtitlesAtTime("the \"Operations Duty\"", 8.25f, story_timer);
                //     MapSubtitlesAtTime("\"Officer\".", 9.25f, story_timer);
                //     MapSubtitlesAtTime("The mission of", 13f, story_timer);
                //     MapSubtitlesAtTime("the group-", 13.5f, story_timer);
                //     MapSubtitlesAtTime("around the clock,", 15f, story_timer);
                //     MapSubtitlesAtTime("seven days a week,", 16f, story_timer);
                //     MapSubtitlesAtTime("is to make sure", 17.5f, story_timer);
                //     MapSubtitlesAtTime("that each navagation", 18.5f, story_timer);
                //     MapSubtitlesAtTime("satellite", 19.5f, story_timer);
                //     MapSubtitlesAtTime("always has", 20f, story_timer);
                //     MapSubtitlesAtTime("correct, up-to-date", 21.5f, story_timer);
                //     MapSubtitlesAtTime("information", 22.5f, story_timer);
                //     MapSubtitlesAtTime("stored in its", 23.5f, story_timer);
                //     MapSubtitlesAtTime("memory unit.", 24.5f, story_timer);
                //     MapSubtitlesAtTime("An informative array", 28f, story_timer);
                //     MapSubtitlesAtTime("of actuated displays", 29.25f, story_timer);
                //     MapSubtitlesAtTime("shows the performance,", 31.25f, story_timer);
                //     MapSubtitlesAtTime("memory,", 33.25f, story_timer);
                //     MapSubtitlesAtTime("and injection status", 34f, story_timer);
                //     MapSubtitlesAtTime("and helps the duty", 36f, story_timer);
                //     MapSubtitlesAtTime("officer coordinate", 37f, story_timer);
                //     MapSubtitlesAtTime("all network operations", 38.5f, story_timer);
                //     MapSubtitlesAtTime("that keep the satellites", 40.5f, story_timer);
                //     MapSubtitlesAtTime("broadcasting navigation", 41.5f, story_timer);
                //     MapSubtitlesAtTime("data.", 43.25f, story_timer);
                //     MapSubtitlesAtTime("Tap to continue ...", 45.24f, story_timer);

                //     MapSubtitlesAtTime("⛈", 0, story_timer);
                //     MapSubtitlesAtTime("⛅", 2f, story_timer);
                //     MapSubtitlesAtTime("Satellite memory", 4f, story_timer);
                //     MapSubtitlesAtTime("units and", 5f, story_timer);
                //     MapSubtitlesAtTime("control circuitry", 6f, story_timer);
                //     MapSubtitlesAtTime("can handle nearly", 7f, story_timer);
                //     MapSubtitlesAtTime("twenty-five thousand", 8f, story_timer);
                //     MapSubtitlesAtTime("separate bits", 9f, story_timer);
                //     MapSubtitlesAtTime("of modulated", 10f, story_timer);
                //     MapSubtitlesAtTime("information", 11f, story_timer);
                //     MapSubtitlesAtTime("⛈", 12.5f, story_timer);
                //     MapSubtitlesAtTime("The satellite", 16f, story_timer);
                //     MapSubtitlesAtTime("gets its power", 17f, story_timer);
                //     MapSubtitlesAtTime("from the sun.", 18f, story_timer);
                //     MapSubtitlesAtTime("Sixteen thousand", 20f, story_timer);
                //     MapSubtitlesAtTime("individual solar", 21f, story_timer);
                //     MapSubtitlesAtTime("cells convert", 22f, story_timer);
                //     MapSubtitlesAtTime("sunlight into", 23f, story_timer);
                //     MapSubtitlesAtTime("electrical energy", 24f, story_timer);
                //     MapSubtitlesAtTime("that is stored", 25f, story_timer);
                //     MapSubtitlesAtTime("in Nickle-", 25.75f, story_timer);
                //     MapSubtitlesAtTime("Cadium batteries", 26.75f, story_timer);
                //     MapSubtitlesAtTime("inside the satellite.", 28f, story_timer);
                //     MapSubtitlesAtTime("Tap to continue ...", 30, story_timer);
                //     break;

                //     MapSubtitlesAtTime("⛈", 0, story_timer);
                //     MapSubtitlesAtTime("⛅", 2f, story_timer);
                //     MapSubtitlesAtTime("A few years later", 3.25f, story_timer);
                //     MapSubtitlesAtTime("the French genius", 4.25f, story_timer);
                //     MapSubtitlesAtTime("Blaise Pascal", 5.5f, story_timer);
                //     MapSubtitlesAtTime("invented and built", 7.5f, story_timer);
                //     MapSubtitlesAtTime("the world's first", 8.5f, story_timer);
                //     MapSubtitlesAtTime("mechanical adding", 9.5f, story_timer);
                //     MapSubtitlesAtTime("machines.", 10.5f, story_timer);
                //     MapSubtitlesAtTime("The is one of them.", 11.5f, story_timer);
                //     MapSubtitlesAtTime("Made in the sixteen-", 13f, story_timer);
                //     MapSubtitlesAtTime("forties.", 14f, story_timer);
                //     MapSubtitlesAtTime("Pascal's acheievement", 16.5f, story_timer);
                //     MapSubtitlesAtTime("lay in the", 18f, story_timer);
                //     MapSubtitlesAtTime("gear mechanism", 18.5f, story_timer);
                //     MapSubtitlesAtTime("which automatically", 20f, story_timer);
                //     MapSubtitlesAtTime("took care of carry-", 20.75f, story_timer);
                //     MapSubtitlesAtTime("overs.", 21.75f, story_timer);
                //     MapSubtitlesAtTime("For example,", 23.25f, story_timer);
                //     MapSubtitlesAtTime("six", 25f, story_timer);
                //     MapSubtitlesAtTime("plus nine", 28.5f, story_timer);
                //     MapSubtitlesAtTime("and the one carried", 30.75f, story_timer);
                //     MapSubtitlesAtTime("over to the next place.", 32f, story_timer);
                //     MapSubtitlesAtTime("⛈", 34f, story_timer);
                //     MapSubtitlesAtTime("In every area", 36.75f, story_timer);
                //     MapSubtitlesAtTime("of defense,", 37f, story_timer);
                //     MapSubtitlesAtTime("science,", 38.5f, story_timer);
                //     MapSubtitlesAtTime("engineering and", 39.5f, story_timer);
                //     MapSubtitlesAtTime("business,", 40.5f, story_timer);
                //     MapSubtitlesAtTime("progress depends", 41.5f, story_timer);
                //     MapSubtitlesAtTime("on the availability", 42.5f, story_timer);
                //     MapSubtitlesAtTime("of fast, accurate", 43.75f, story_timer);
                //     MapSubtitlesAtTime("methods of calculation.", 45f, story_timer);
                //     MapSubtitlesAtTime("They've enabled us", 48.5f, story_timer);
                //     MapSubtitlesAtTime("to take giant", 49.75f, story_timer);
                //     MapSubtitlesAtTime("steps forward", 50.25f, story_timer);
                //     MapSubtitlesAtTime("in power,", 51f, story_timer);
                //     MapSubtitlesAtTime("in control,", 52.5f, story_timer);
                //     MapSubtitlesAtTime("in design,", 54.5f, story_timer);
                //     MapSubtitlesAtTime("in processing,", 55.5f, story_timer);
                //     MapSubtitlesAtTime("and in research.", 57f, story_timer);
                //     MapSubtitlesAtTime("Tap to continue ...", 59f, story_timer);
                //     break;
                // case 19:
                //     MapSubtitlesAtTime("⛈", 0, story_timer);
                //     MapSubtitlesAtTime("⛅", 2.4f, story_timer);
                //     MapSubtitlesAtTime("♩", 3.4f, story_timer);
                //     MapSubtitlesAtTime("♩♩", 3.9f, story_timer);
                //     MapSubtitlesAtTime("♪", 4.4f, story_timer);
                //     MapSubtitlesAtTime("♪♪", 4.9f, story_timer);
                //     MapSubtitlesAtTime("☄", 5.4f, story_timer);
                //     MapSubtitlesAtTime("♪", 5.9f, story_timer);
                //     MapSubtitlesAtTime("♫", 6.15f, story_timer);
                //     MapSubtitlesAtTime("♫♪", 6.4f, story_timer);
                //     MapSubtitlesAtTime("♫♫", 6.65f, story_timer);
                //     MapSubtitlesAtTime("♫♫♪", 6.9f, story_timer);
                //     MapSubtitlesAtTime("⛅", 8f, story_timer);
                //     MapSubtitlesAtTime("In the final", 9f, story_timer);
                //     MapSubtitlesAtTime("analysis however,", 10f, story_timer);
                //     MapSubtitlesAtTime("the key to the future", 11f, story_timer);
                //     MapSubtitlesAtTime("is not an aparatus", 12f, story_timer);
                //     MapSubtitlesAtTime("a machine", 13.75f, story_timer);
                //     MapSubtitlesAtTime("or an electronic cube,", 15f, story_timer);
                //     MapSubtitlesAtTime("but the brainpower", 16f, story_timer);
                //     MapSubtitlesAtTime("of man.", 17f, story_timer);
                //     MapSubtitlesAtTime("Nothing will", 18.5f, story_timer);
                //     MapSubtitlesAtTime("ever replace", 19f, story_timer);
                //     MapSubtitlesAtTime("creative intelligence.", 20f, story_timer);
                //     MapSubtitlesAtTime("In great laboratories,", 21.5f, story_timer);
                //     MapSubtitlesAtTime("in colleges", 23.25f, story_timer);
                //     MapSubtitlesAtTime("and universities,", 23.75f, story_timer);
                //     MapSubtitlesAtTime("in solitary quiet ...", 25f, story_timer);
                //     MapSubtitlesAtTime("Man thinks,", 26.35f, story_timer);
                //     MapSubtitlesAtTime("reasons,", 28f, story_timer);
                //     MapSubtitlesAtTime("experiments,", 29f, story_timer);
                //     MapSubtitlesAtTime("creates.", 30f, story_timer);
                //     MapSubtitlesAtTime("The mind", 31, story_timer);
                //     MapSubtitlesAtTime("strains to peer", 32f, story_timer);
                //     MapSubtitlesAtTime("beyond today's horizons", 33.25f, story_timer);
                //     MapSubtitlesAtTime("for a glimpse of", 35f, story_timer);
                //     MapSubtitlesAtTime("the wonders of tomorrow!", 36.25f, story_timer);
                //     MapSubtitlesAtTime("⛅", 39f, story_timer);
                //     MapSubtitlesAtTime("🔚", 43.5f, story_timer);
                //     MapSubtitlesAtTime("⛈", 47f, story_timer);
                //     break;
                // case 20:
                //     MapSubtitlesAtTime("╔═════════════════════╗\n║ BitNaughts Campaign ║\n║   * Report Card *   ║\n╠═════════════════════╣\n║ Time: " + FloatToTime(global_timer) + "\t\t  ║\n╚═════════════════════╝\n\nThanks for playing!", 0f, story_timer);
                //     MapSubtitlesAtTime("╔═════════════════════╗\n║ BitNaughts Campaign ║\n║   * Report Card *   ║\n╠═════════════════════╣\n║ Date: " + System.DateTime.Now.ToString("h:mm:ss.f") + "\t  ║\n╚═════════════════════╝\n\nThanks for playing!", 5f, story_timer);
                //     MapSubtitlesAtTime("╔═════════════════════╗\n║ BitNaughts Campaign ║\n║   * Report Card *   ║\n╠═════════════════════╣\n║ Date: " + System.DateTime.Now.ToString("MM/dd/yyyy") + "\t  ║\n╚═════════════════════╝\n\nThanks for playing!", 7.5f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ Woody Allen's      ║\n║ Radio Days         ║\n║             (1987) ║\n╚════════════════════╝\n\nTap to continue ...", 10f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ Jay Bonafield's    ║\n║ The Future Is Now  ║\n║             (1955) ║\n╚════════════════════╝\n\nTap to continue ...", 12.25f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ U.S. Navy's        ║\n║ Navigation Satel-  ║\n║ lite System (1955) ║\n╚════════════════════╝\n\nTap to continue ...", 14.5f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ U.S. Navy's        ║\n║ Digital Computer   ║\n║ Techniques  (1962) ║\n╚════════════════════╝\n\nTap to continue ...", 16.75f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ N.A.S.A's          ║\n║ Space Down to      ║\n║ Earth       (1970) ║\n╚════════════════════╝\n\nTap to continue ...", 19f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Sprite *     ║\n║ Alejandro Monge's  ║\n║ Modular Spaceships ║\n║             (2014) ║\n╚════════════════════╝\n\nTap to continue ...", 21.25f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Sound  *     ║\n║ Eidos Interactive  ║\n║ Battlestations     ║\n║ Pacific     (2009) ║\n╚════════════════════╝\n\nTap to continue ...", 23.5f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n║ Valentine          ║\n║             (2013) ║\n╚════════════════════╝\n\nTap to continue ...", 25.75f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n║ Valentine          ║\n║             (2013) ║\n╚════════════════════╝\n\nTap to continue ...", 28f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n║ Valentine          ║\n║             (2013) ║\n\n\nTap to continue ...", 33f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n║ Valentine          ║\n\n\n\nTap to continue ...", 33.5f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n\n\n\n\nTap to continue ...", 34f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n\n\n\n\n\nTap to continue ...", 34.5f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╚════════════════════╝\n\n\n\n\n\n\nTap to continue ...", 35f, story_timer);
                //     MapSubtitlesAtTime("These current tests", 35.25f, story_timer);
                //     MapSubtitlesAtTime("are enabling", 36.75f, story_timer);
                //     MapSubtitlesAtTime("N.A.S.A", 37.75f, story_timer);
                //     MapSubtitlesAtTime("and the broadcasting", 38.25f, story_timer);
                //     MapSubtitlesAtTime("community", 39.25f, story_timer);
                //     MapSubtitlesAtTime("to iron out", 40.25f, story_timer);
                //     MapSubtitlesAtTime("technical problems", 41.25f, story_timer);
                //     MapSubtitlesAtTime("that are involved", 42.25f, story_timer);
                //     MapSubtitlesAtTime("in this form of", 43.25f, story_timer);
                //     MapSubtitlesAtTime("transmission.", 44.25f, story_timer);
                //     MapSubtitlesAtTime("And to determine", 45.25f, story_timer);
                //     MapSubtitlesAtTime("the costs of such", 46.75f, story_timer);
                //     MapSubtitlesAtTime("future operations.", 47.75f, story_timer);
                //     MapSubtitlesAtTime("If these tests are", 50f, story_timer);
                //     MapSubtitlesAtTime("successful,", 51f, story_timer);
                //     MapSubtitlesAtTime("we have every reason", 52.5f, story_timer);
                //     MapSubtitlesAtTime("to believe that", 53.25f, story_timer);
                //     MapSubtitlesAtTime("they will be,", 53.75f, story_timer);
                //     MapSubtitlesAtTime("The American", 55.25f, story_timer);
                //     MapSubtitlesAtTime("people will", 55.75f, story_timer);
                //     MapSubtitlesAtTime("reap a major", 56.75f, story_timer);
                //     MapSubtitlesAtTime("domestic dividend", 57.75f, story_timer);
                //     MapSubtitlesAtTime("from the national", 59.25f, story_timer);
                //     MapSubtitlesAtTime("space efforts.", 60.25f, story_timer);
                //     MapSubtitlesAtTime("⛈", 61.25f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n║ Brian Hungerman    ║\n║ brianhungerman.com ║\n║             (2022) ║\n╚════════════════════╝\n\nTap to continue ...", 62.25f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n║ Brian Hungerman    ║\n║ brianhungerman.com ║\n║             (2022) ║\n\n\nTap to continue ...", 85f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n║ Brian Hungerman    ║\n║ brianhungerman.com ║\n\n\n\nTap to continue ...", 85.5f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n║ Brian Hungerman    ║\n\n\n\n\nTap to continue ...", 86f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n\n\n\n\n\nTap to continue ...", 86.5f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╚════════════════════╝\n\n\n\n\n\n\nTap to continue ...", 87f, story_timer);
                //     MapSubtitlesAtTime("The current goal", 87.25f, story_timer);
                //     MapSubtitlesAtTime("of satellite", 88.25f, story_timer);
                //     MapSubtitlesAtTime("geodesy", 89f, story_timer);
                //     MapSubtitlesAtTime("is to tie all", 89.5f, story_timer);
                //     MapSubtitlesAtTime("geodetic", 90.5f, story_timer);
                //     MapSubtitlesAtTime("grids together", 91.25f, story_timer);
                //     MapSubtitlesAtTime("within an", 92f, story_timer);
                //     MapSubtitlesAtTime("accuracy of", 92.5f, story_timer);
                //     MapSubtitlesAtTime("thirty feet.", 93.25f, story_timer);
                //     MapSubtitlesAtTime("Using high-flying", 95f, story_timer);
                //     MapSubtitlesAtTime("satellites as geo-", 96f, story_timer);
                //     MapSubtitlesAtTime("detic markers,", 97f, story_timer);
                //     MapSubtitlesAtTime("the world's", 98.5f, story_timer);
                //     MapSubtitlesAtTime("contentients", 99f, story_timer);
                //     MapSubtitlesAtTime("will eventually be", 99.5f, story_timer);
                //     MapSubtitlesAtTime("tied together", 100.5f, story_timer);
                //     MapSubtitlesAtTime("to one common", 101.25f, story_timer);
                //     MapSubtitlesAtTime("reference system.", 102.25f, story_timer);
                //     MapSubtitlesAtTime("Educational and", 104.5f, story_timer);
                //     MapSubtitlesAtTime("cultural programs", 105.5f, story_timer);
                //     MapSubtitlesAtTime("to populations of", 106.5f, story_timer);
                //     MapSubtitlesAtTime("entire nations", 107.75f, story_timer);
                //     MapSubtitlesAtTime("through inter-", 109.5f, story_timer);
                //     MapSubtitlesAtTime("contentiential", 110f, story_timer);
                //     MapSubtitlesAtTime("television!", 110.5f, story_timer);
                //     MapSubtitlesAtTime("⛈", 112.5f, story_timer);
                //     MapSubtitlesAtTime("As we develop", 114.4f, story_timer);
                //     MapSubtitlesAtTime("this potential in", 115.5f, story_timer);
                //     MapSubtitlesAtTime("the future,", 116.5f, story_timer);
                //     MapSubtitlesAtTime("applications from", 117.5f, story_timer);
                //     MapSubtitlesAtTime("space will have", 118.5f, story_timer);
                //     MapSubtitlesAtTime("continued", 119.25f, story_timer);
                //     MapSubtitlesAtTime("profound and", 120.25f, story_timer);
                //     MapSubtitlesAtTime("direct effects", 121f, story_timer);
                //     MapSubtitlesAtTime("on our", 122f, story_timer);
                //     MapSubtitlesAtTime("everyday lives", 122.75f, story_timer);
                //     MapSubtitlesAtTime("here on Earth.", 124f, story_timer);
                //     MapSubtitlesAtTime("⛅", 126f, story_timer);
                //     MapSubtitlesAtTime("⛈", 148, story_timer);
                //     break;
                // case 21:
                //     MapSubtitlesAtTime("⛈", 0, story_timer);
                //     break;
            // }
        } else {
            if ((animation_timer / 7.77f ) % 2 < 1) {
                Timer.text = tutorial_save_time + "\n" + System.DateTime.Now.AddYears(-54).ToString("MM/dd/yyyy");
            } else {
                Timer.text = System.DateTime.Now.ToString("h:mm:ss.f") + "\n" + System.DateTime.Now.AddYears(-54).ToString("MM/dd/yyyy");
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
