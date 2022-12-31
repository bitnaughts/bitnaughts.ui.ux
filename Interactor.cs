using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*

Font size 50
Character width 25
Text height 50

*/


public class Interactor : MonoBehaviour
{
    public GameObject[] GridLayers;
    public Sprite PixelSprite, OverlaySprite;
    public GameObject SplashScreen;
    public GameObject CampaignNewtonsLaws, CampaignDopplerShift, CampaignDopplerEffect, CampaignPlanksLaw, CampaignHawkingRadiation, CampaignMoracevsParadox, CampaignDeBroglieTheory, CampaignFermiParadox, CampaignPascalsWager;
    public AudioClip SplashScreenNarration, CampaignRadioDaysNarration, CampaignNewtonsLawsNarration, CampaignTheAtomNarration, CampaignDopplerShiftNarration, CampaignTheElectronNarration, CampaignDopplerEffectNarration, CampaignModernWarNarration, CampaignPlanksLawNarration, CampaignTelevisionNarration, CampaignHawkingRadiationNarration, CampaignVideotapeRecordsNarration, CampaignMoracevsParadoxNarration, CampaignElectronicMusicNarration, CampaignDeBroglieTheoryNarration, CampaignRadioIsotopesNarration, CampaignFermiParadoxNarration, CampaignHardnessTestNarration, CampaignPascalsWagerNarration, CampaignConclusionNarration, CampaignCreditsNarration;
    public GameObject Content, InterpreterPanel, InterpreterPanelEdge, MapPanel, SubtitlesShadow, Subtitles; 
    public AudioClip TutorialIntro, TutorialLookAround, TutorialMapInterface, TutorialMapScreen, TutorialIssueOrders, TutorialTargetWindow, TutorialTargetWindowHelp, TutorialTargetWindowSelected, TutorialGood, TutorialGood2, TutorialGood3, TutorialTry, TutorialBetter, TutorialCancel, TutorialOther, TutorialMusic, TutorialComponents, TutorialGetMoving, TutorialThrottle, TutorialDogfight, TutorialOutro, TutorialLeftWindow, TutorialRightWindow, TutorialCursor, TutorialSelect;
    public AudioClip CannonFire, ThrusterThrottle, SonarScan, TorpedoFact, ProcessorPing, GimbalRotate, TorpedoLaunch;
    public AudioClip ThemeSong, Click, Click2;
    public AudioClip SoundBack, SoundClick, SoundError, SoundOnMouse, SoundStart, SoundToggle, SoundProcessor, SoundGimbal, SoundCannon1, SoundCannon2, SoundCannon3, SoundRadar, SoundThruster, SoundBooster, SoundTorpedo1, SoundTorpedo2, SoundWarning, SoundWarningOver;
    public GameObject Overlay;
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
    public GameObject InputUp, InputLeft, InputRight, InputDown, InputA, InputB;
    // example ship objects
    public GameObject CannonL, Processor, Bulkhead, BoosterR, ThrusterL, BoosterL, Thruster, ThrusterR, CannonR, SensorL, SensorR, Printer;
    // Start is called before the first frame update
    void Start()
    {
        PrinterPrint = GameObject.Find("InputPrinterPrint");
        PrinterRight = GameObject.Find("InputPrinterRight");
        PrinterLeft = GameObject.Find("InputPrinterLeft");
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
        RenderText("$ <b>about</b>\n  <b>campaign</b>\n  <b>clear</b>\n  <b>cp</b>\n  <b>make</b>\n  <b>nano</b>\n  <b>rm</b>\n  <b>tutorial</b>\n  <b>back</b>");

        Timer.text = "⛅";
        PlayVideo(audio_queue);
        OnMapView();
        PrinterLeft.SetActive(false);
        PrinterRight.SetActive(false);
        PrinterPrint.SetActive(false);
    }
    public void PrinterLeftFx() {
        if (BoosterR.activeSelf)
        {
            BoosterL.SetActive(false);
            BoosterR.SetActive(false);
            SensorL.SetActive(true);
            SensorR.SetActive(true);
            ThrusterL.SetActive(true);
            ThrusterR.SetActive(true);
            Thruster.SetActive(false);
        } else if (CannonR.activeSelf) {
            CannonL.SetActive(false);
            CannonR.SetActive(false);
            BoosterL.SetActive(true);
            BoosterR.SetActive(true);
            Thruster.SetActive(true);
            ThrusterL.SetActive(false);
            ThrusterR.SetActive(false);
        } else {
            SensorL.SetActive(false);
            SensorR.SetActive(false);
            CannonL.SetActive(true);
            CannonR.SetActive(true);
        }

    }
    public void PrinterRightFx() {
        if (BoosterR.activeSelf)
        {
            BoosterL.SetActive(false);
            BoosterR.SetActive(false);
            CannonL.SetActive(true);
            CannonR.SetActive(true);
            ThrusterL.SetActive(true);
            ThrusterR.SetActive(true);
            Thruster.SetActive(false);
        } else if (CannonR.activeSelf) {
            CannonL.SetActive(false);
            CannonR.SetActive(false);
            SensorL.SetActive(true);
            SensorR.SetActive(true);
        } else {
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
        if (BoosterR.activeSelf)
        {
            GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-1);
        } else if (CannonR.activeSelf) {
            GameObject.Find("CannonR").GetComponent<ComponentController>().Action(-1);
        } else {
            GameObject.Find("SensorR").GetComponent<ComponentController>().Action(-1);
        }
        
    }
    public void PrinterPrintFx() {
        InputUp.SetActive(true);
        InputDown.SetActive(true);
        InputLeft.SetActive(true);
        InputRight.SetActive(true);
        InputA.SetActive(true);
        InputB.SetActive(true);
        Printer.SetActive(false);
        ClearText();
        PrinterLeft.SetActive(false);
        PrinterRight.SetActive(false);
        PrinterPrint.SetActive(false);

        Ship.Start();
        OverlayInteractor.UpdateOptions();
        OverlayInteractor.OnDropdownChange(); 
        OverlayInteractor.gameObject.SetActive(false);
        MapScreenPanOverlay.SetActive(true);
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
    public void PlayAudio(AudioClip clip) 
    {
        camera.GetComponent<AudioSource>().clip = clip;
        camera.GetComponent<AudioSource>().Play();
        camera.GetComponent<AudioSource>().loop = false;
    }
    string queue_audio = "";
    public void PlayVideo(string url) 
    {
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
            InputField.text = " ⛴ Ship Select";
            PrinterLeft.SetActive(true);
            PrinterRight.SetActive(true);
            PrinterPrint.SetActive(true);

            Processor.SetActive(true);
            Bulkhead.SetActive(true);
            Thruster.SetActive(true);
            BoosterL.SetActive(true);
            BoosterR.SetActive(true);

            
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
        // print ("Content.sizeDelta(" + width + ", " + height + ")");
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
    public void UseWeapon() {
        Action("Cannon", -1);//GetInput(), -1);
        if (clip_index == 2 && campaign_stage == 2) { campaign_stage++; story_timer = 0f; }
    }
    public void PanTutorial() {
        if (clip_index == 2 && campaign_stage == 0) { campaign_stage++; story_timer = 0f; }
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
        camera.GetComponent<AudioSource>().clip = clip;
        camera.GetComponent<AudioSource>().volume = .5f;
        camera.GetComponent<AudioSource>().Play();
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
    bool CheckInsideEdge() {
        return (Input.mousePosition.y > 100 && Input.mousePosition.y < Screen.height - 160 && Input.mousePosition.x > 100 && Input.mousePosition.x < Screen.width - 100);
    }
    string[] campaign_clips = new string[] { "Radio Days", "Newton's Laws", "The Atom", "De Broglie Theory", "The Electron",  "Doppler Effect", "Modern War", "Doppler Shift", "Television", "Plank's Law", "Videotape Records", "Hawking Radiation", "Electronic Music", "Moravec's Paradox", "Radio Isotopes", "Fermi Paradox", "Hardness Test", "Pascal's Wager", "Conclusion", "Credits", "" };
    string[] tutorial_clips = new string[] { "Tutorial Introduction", "Digital Computers", "Binary", "Components", "Morse Code", "☄ BitNaughts   " };
    int campaign_stage = -1, tutorial_stage = -1; 
    int[] campaign_clip_durations = new int[] {999, 81, 999, 79, 999, 64, 999, 46, 999, 79, 999, 74, 999, 107, 999, 95, 999, 116, 999, 51, 155, 999 };
    float[] campaign_splits = new float[20];
    int[] tutorial_clip_durations = new int[] {999, 81, 999, 79, 999, 64, 999, 46, 999, 79, 999, 74, 999, 107, 999, 95, 999, 116, 999, 51, 999, 999, 999, 999 };
    int clip_index = 0;
    string credits_output = "";
    void FixedUpdate()
    {
        if (queue_audio != "") {
            if (GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().frame != -1) {
                PlayAudio(LookupNarration(queue_audio));
                queue_audio = "";
                if (story_timer != -1) {
                    story_timer = 0;
                }
                if (start_timer != -1) {
                    start_timer = 0;
                }
            }
        }
        if (start_timer > -1) 
        {
            if ((start_timer / 5f ) % 2 < 1) {
                Timer.text = System.DateTime.Now.AddYears(-54).ToString("MM/dd/yyyy") + "\n" +  FloatToTime(start_timer) ;
            } else {
                Timer.text = System.DateTime.Now.AddYears(-54).ToString("MM/dd/yyyy") + "\n" + System.DateTime.Now.ToString("h:mm:ss.f") ;
            }
            start_timer += Time.deltaTime;
            MapSubtitlesAtTime("We interrupt", 0f, start_timer);
            MapSubtitlesAtTime("this program", 0.5f, start_timer);
            MapSubtitlesAtTime("to bring you", 1f, start_timer);
            MapSubtitlesAtTime("a special", 1.5f, start_timer);
            MapSubtitlesAtTime("news bulletin!", 2f, start_timer);
            MapSubtitlesAtTime("⛅", 2.5f, start_timer);
            MapSubtitlesAtTime("A state of emergency", 2.75f, start_timer);
            MapSubtitlesAtTime("has been declared", 3.75f, start_timer);
            MapSubtitlesAtTime("by the President of", 4.5f, start_timer);
            MapSubtitlesAtTime("the United States!", 5.25f, start_timer);
            MapSubtitlesAtTime("We're switching live", 6.25f, start_timer);
            MapSubtitlesAtTime("to Wilsens Glenn,", 7.25f, start_timer);
            MapSubtitlesAtTime("New Jersey", 8f, start_timer);
            MapSubtitlesAtTime("where the landing of", 8.5f, start_timer);
            MapSubtitlesAtTime("hundreds of unidentified", 9.5f, start_timer);
            MapSubtitlesAtTime("spacecraft have", 10.5f, start_timer);
            MapSubtitlesAtTime("now been officially", 11.5f, start_timer);
            MapSubtitlesAtTime("confirmed as a", 12.5f, start_timer);
            MapSubtitlesAtTime("full-scale invasion", 13.25f, start_timer);
            MapSubtitlesAtTime("of the Earth", 14.25f, start_timer);
            MapSubtitlesAtTime("by Martians!", 15f, start_timer);
            MapSubtitlesAtTime("⛈", 17f, start_timer);
            MapSubtitlesAtTime("We're seeing ...", 18.75f, start_timer);
            MapSubtitlesAtTime("It's horrible ...", 19.25f, start_timer);
            MapSubtitlesAtTime("I can't believe", 20.25f, start_timer);
            MapSubtitlesAtTime("my eyes.", 20.75f, start_timer);
            MapSubtitlesAtTime("People are dying,", 21.5f, start_timer);
            MapSubtitlesAtTime("being trampled", 22.25f, start_timer);
            MapSubtitlesAtTime("in their efforts", 23.25f, start_timer);
            MapSubtitlesAtTime("to escape!", 24f, start_timer);
            MapSubtitlesAtTime("☄", 24.5f, start_timer);
            MapSubtitlesAtTime("Power lines", 25.5f, start_timer);
            MapSubtitlesAtTime("are down", 26.25f, start_timer);
            MapSubtitlesAtTime("everywhere", 27f, start_timer);
            MapSubtitlesAtTime("We could be", 27.75f, start_timer);
            MapSubtitlesAtTime("cut off at", 28.25f, start_timer);
            MapSubtitlesAtTime("any minute ...", 28.75f, start_timer);
            MapSubtitlesAtTime("⛈", 29.75f, start_timer);
            MapSubtitlesAtTime("\"Oh my God!\"", 30.25f, start_timer);
            MapSubtitlesAtTime("There's another group", 32.25f, start_timer);
            MapSubtitlesAtTime("of spaceships", 33f, start_timer);
            MapSubtitlesAtTime("of alien ships", 34f, start_timer);
            MapSubtitlesAtTime("They're coming out", 35f, start_timer);
            MapSubtitlesAtTime("of the sky!", 35.5f, start_timer);
            MapSubtitlesAtTime("⛈", 36f, start_timer);
            if (start_timer > 41 || (Input.GetMouseButton(0) && CheckInsideEdge())) 
            {
                SplashScreen.SetActive(false);
                start_timer = -1;
                Subtitles.SetActive(false);
                SubtitlesShadow.SetActive(false);
                OnCodeView();
                camera.GetComponent<AudioSource>().Stop();
                SetBackground(new Color(25/255f, 61/255f, 65/255f));
                // Overlay
                GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().enabled = false;
                volume_slider.SetActive(false);
                InputField.text = "☄ BitNaughts";
            }
        } else if (story_timer > -1 && clip_index > -1) {
            Timer.text = "";
            for (int i = 0; i < clip_index - 1; i++) {
                Timer.text += FloatToTime(campaign_splits[i]) + "\n";
            }
            if (clip_index == 20) Timer.text += "------\n" + FloatToTime(global_timer);
            else if (clip_index == 1) Timer.text = FloatToTime(story_timer);
            else if (SubtitlesShadow.activeSelf) {
                Timer.text += "------\n" + FloatToTime(story_timer);
            } 
            else {
                Timer.text = FloatToTime(story_timer) + "\n";
            }
            if (clip_index <= 19) {
                global_timer += Time.deltaTime;
            }
            if ((story_timer > 0.5f && Input.GetMouseButton(0) && CheckInsideEdge()) || (story_timer > campaign_clip_durations[clip_index] && story_timer < campaign_clip_durations[clip_index] + (Time.deltaTime * 2f))) {
                if (clip_index >= 20) {
                    story_timer = -1;
                    clip_index = -1;
                    Subtitles.SetActive(false);
                    SubtitlesShadow.SetActive(false);
                    volume_slider.SetActive(false);
                    OnCodeView();
                    RenderText("$ campaign\n$");
                    InputField.text = "☄ BitNaughts";
                    camera.GetComponent<AudioSource>().Stop();
                    GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().enabled = false;
                    SetBackground(new Color(25/255f, 61/255f, 65/255f));
                }
                else
                {
                    if (campaign_clip_durations[clip_index] == 999) //objective mission
                    {
                        switch (clip_index) {
                            case 2:
                                CampaignNewtonsLaws.SetActive(true);
                                break;
                            case 4:
                                CampaignNewtonsLaws.SetActive(false);
                                CampaignDeBroglieTheory.SetActive(true);
                                break;
                            case 6:
                                CampaignDeBroglieTheory.SetActive(false);
                                CampaignDopplerEffect.SetActive(true);
                                break;
                            case 8:
                                CampaignDopplerEffect.SetActive(false);
                                CampaignDopplerShift.SetActive(true);
                                break;
                            case 10:
                                CampaignDopplerShift.SetActive(false);
                                CampaignPlanksLaw.SetActive(true);
                                break;
                            case 12:
                                CampaignPlanksLaw.SetActive(false);
                                CampaignHawkingRadiation.SetActive(true);
                                break;
                            case 14:
                                CampaignHawkingRadiation.SetActive(false);
                                CampaignMoracevsParadox.SetActive(true);
                                break;
                            case 16:
                                CampaignMoracevsParadox.SetActive(false);
                                CampaignFermiParadox.SetActive(true);
                                break;
                            case 18:
                                CampaignFermiParadox.SetActive(false);
                                CampaignPascalsWager.SetActive(true);
                                break;

                        }
                        Subtitles.SetActive(false);
                        SubtitlesShadow.SetActive(false);
                        volume_slider.SetActive(false);
                        OnMapView();
                        SetBackground(new Color(25/255f, 61/255f, 65/255f));
                        camera.GetComponent<AudioSource>().Stop();
                        GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().enabled = false;
                        if (clip_index == 2 && campaign_stage == -1) { campaign_stage++; story_timer = 0f; }
                    }
                    else 
                    {
                        campaign_splits[clip_index - 1] = story_timer;
                        story_timer = 0f;
                        PlayVideo(campaign_clips[clip_index]);
                        clip_index++;
                    }
                }
            }
            story_timer += Time.deltaTime;
            switch (clip_index) { // max 21 characters ideally
                case 1:
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2.4f, story_timer);
                    MapSubtitlesAtTime("♩", 3.4f, story_timer);
                    MapSubtitlesAtTime("♩♩", 3.9f, story_timer);
                    MapSubtitlesAtTime("♪", 4.4f, story_timer);
                    MapSubtitlesAtTime("♪♪", 4.9f, story_timer);
                    MapSubtitlesAtTime("☄", 5.4f, story_timer);
                    MapSubtitlesAtTime("♪", 5.9f, story_timer);
                    MapSubtitlesAtTime("♫", 6.15f, story_timer);
                    MapSubtitlesAtTime("♫♪", 6.4f, story_timer);
                    MapSubtitlesAtTime("♫♫", 6.65f, story_timer);
                    // MapSubtitlesAtTime("♫♫♪", 6.9f, story_timer);
                    MapSubtitlesAtTime("The voices we used to", 7f, story_timer);
                    MapSubtitlesAtTime("hear on the radio ...", 8f, story_timer);
                    MapSubtitlesAtTime("☄", 10.25f, story_timer);
                    MapSubtitlesAtTime("Be sure and tune", 11.25f, story_timer);
                    MapSubtitlesAtTime("in tomorrow for", 12f, story_timer);
                    MapSubtitlesAtTime("another adventure", 13f, story_timer);
                    MapSubtitlesAtTime("of the", 13.75f, story_timer);
                    MapSubtitlesAtTime("\"Masked Avenger\"!", 14.25f, story_timer);
                    MapSubtitlesAtTime("When he flies over", 15.25f, story_timer);
                    MapSubtitlesAtTime("the city rooftops ...", 16f, story_timer);
                    MapSubtitlesAtTime("We all hear his cry:", 17.5f, story_timer);
                    MapSubtitlesAtTime("☄", 19, story_timer);
                    MapSubtitlesAtTime("\"Beware evildoers,\"", 20, story_timer);
                    MapSubtitlesAtTime("\"wherever you are!\"", 21.75f, story_timer);
                    MapSubtitlesAtTime("♪", 23f, story_timer);
                    MapSubtitlesAtTime("♪♪", 23.25f, story_timer);
                    MapSubtitlesAtTime("♪", 23.5f, story_timer);
                    MapSubtitlesAtTime("♪♪", 23.75f, story_timer);
                    MapSubtitlesAtTime("♪", 24.25f, story_timer);
                    MapSubtitlesAtTime("♪♩", 25f, story_timer);
                    MapSubtitlesAtTime("⛈", 27.5f, story_timer);
                    MapSubtitlesAtTime("I recall so many", 28.75f, story_timer);
                    MapSubtitlesAtTime("personal experiences", 29.5f, story_timer);
                    MapSubtitlesAtTime("from when I grew up", 30.75f, story_timer);
                    MapSubtitlesAtTime("and listened to one", 31.5f, story_timer);
                    MapSubtitlesAtTime("show after another ...", 32.5f, story_timer);
                    MapSubtitlesAtTime("This girl singing", 34.25f, story_timer);
                    MapSubtitlesAtTime("used to be a favorite", 35.5f, story_timer);
                    MapSubtitlesAtTime("at my house.", 36.25f, story_timer);
                    MapSubtitlesAtTime("One of many.", 37f, story_timer);
                    MapSubtitlesAtTime("Now it's all gone ...", 38.25f, story_timer);
                    MapSubtitlesAtTime("Except for", 40f, story_timer);
                    MapSubtitlesAtTime("the memories ...", 40.75f, story_timer);
                    MapSubtitlesAtTime("Pay more attention to ", 41.5f, story_timer);
                    MapSubtitlesAtTime("your school work and", 42.5f, story_timer);
                    MapSubtitlesAtTime("less to the radio!", 43.25f, story_timer);
                    MapSubtitlesAtTime("You always listen", 44.5f, story_timer);
                    MapSubtitlesAtTime("to the radio!", 45.25f, story_timer);
                    MapSubtitlesAtTime("It's different!", 46.5f, story_timer);
                    MapSubtitlesAtTime("Our lives", 47.25f, story_timer);
                    MapSubtitlesAtTime("are ruined already!", 47.75f, story_timer);
                    MapSubtitlesAtTime("⛅", 48.75f, story_timer);
                    MapSubtitlesAtTime("There are those who", 54.5f, story_timer);
                    MapSubtitlesAtTime("drink champagne", 55.25f, story_timer);
                    MapSubtitlesAtTime("at nightclubs", 56.1f, story_timer);
                    MapSubtitlesAtTime("and us who listen to", 57.1f, story_timer);
                    MapSubtitlesAtTime("them drink champagne", 58.1f, story_timer);
                    MapSubtitlesAtTime("on the radio!", 59.1f, story_timer);
                    MapSubtitlesAtTime("⛈", 59.5f, story_timer);
                    MapSubtitlesAtTime("We interrupt", 60f, story_timer);
                    MapSubtitlesAtTime("this program", 60.5f, story_timer);
                    MapSubtitlesAtTime("to bring you a", 61.1f, story_timer);
                    MapSubtitlesAtTime("special news bulletin!", 61.5f, story_timer);
                    MapSubtitlesAtTime("The landing of", 62.75f, story_timer);
                    MapSubtitlesAtTime("hundreds of", 63.5f, story_timer);
                    MapSubtitlesAtTime("unidentified space-", 64f, story_timer);
                    MapSubtitlesAtTime("craft have now been off-", 65f, story_timer);
                    MapSubtitlesAtTime("icially confirmed", 66f, story_timer);
                    MapSubtitlesAtTime("as a full-scale in-", 67f, story_timer);
                    MapSubtitlesAtTime("vasion of the Earth!", 68f, story_timer);
                    MapSubtitlesAtTime("⛈", 70f, story_timer);
                    MapSubtitlesAtTime("Now I love old", 71f, story_timer);
                    MapSubtitlesAtTime("radio stories ...", 72f, story_timer);
                    MapSubtitlesAtTime("There's another", 73.25f, story_timer);
                    MapSubtitlesAtTime("group of spaceships", 73.75f, story_timer);
                    MapSubtitlesAtTime("of alien ships", 74.75f, story_timer);
                    MapSubtitlesAtTime("they're coming out", 76f, story_timer);
                    MapSubtitlesAtTime("of the sky!", 76.5f, story_timer);
                    MapSubtitlesAtTime("♫", 78f, story_timer);
                    MapSubtitlesAtTime("♫♫", 78.5f, story_timer);
                    MapSubtitlesAtTime("♪", 79f, story_timer);
                    MapSubtitlesAtTime("⛈", 80f, story_timer);
                    break;
                case 2:
                    if (campaign_stage == -1) {
                        MapSubtitlesAtTime("⛈", 0, story_timer);
                        MapSubtitlesAtTime("⛅", 2f, story_timer);
                        MapSubtitlesAtTime("7 ...", 6.75f, story_timer);
                        MapSubtitlesAtTime("6 ...", 7.75f, story_timer);
                        MapSubtitlesAtTime("5 ...", 8.75f, story_timer);
                        MapSubtitlesAtTime("4 ...", 9.75f, story_timer);
                        MapSubtitlesAtTime("3 ...", 10.75f, story_timer);
                        MapSubtitlesAtTime("2 ...", 11.75f, story_timer);
                        MapSubtitlesAtTime("1 ...", 12.75f, story_timer);
                        MapSubtitlesAtTime("☄ Tap to continue", 13.75f, story_timer);
                        MapSubtitlesAtTime("Today, orbitting", 18.5f, story_timer);
                        MapSubtitlesAtTime("satellites of the", 19.75f, story_timer);
                        MapSubtitlesAtTime("Navy Navigation", 20.75f, story_timer);
                        MapSubtitlesAtTime("Satellite System", 21.5f, story_timer);
                        MapSubtitlesAtTime("provide around-the-", 23.5f, story_timer);
                        MapSubtitlesAtTime("clock ultraprecise", 24.5f, story_timer);
                        MapSubtitlesAtTime("position fixes", 26.25f, story_timer);
                        MapSubtitlesAtTime("from space", 27.5f, story_timer);
                        MapSubtitlesAtTime("to units of", 28.5f, story_timer);
                        MapSubtitlesAtTime("the fleet,", 29.25f, story_timer);
                        MapSubtitlesAtTime("everywhere,", 30f, story_timer);
                        MapSubtitlesAtTime("in any kind", 31f, story_timer);
                        MapSubtitlesAtTime("of weather.", 32.25f, story_timer);
                        MapSubtitlesAtTime("⛈", 34.25f, story_timer);
                        MapSubtitlesAtTime("Navigation", 39f, story_timer);
                        MapSubtitlesAtTime("by satellite,", 40f, story_timer);
                        MapSubtitlesAtTime("how and why", 42f, story_timer);
                        MapSubtitlesAtTime("does it work?", 43.25f, story_timer);
                        MapSubtitlesAtTime("First, a little", 44.75f, story_timer);
                        MapSubtitlesAtTime("astrophysics", 45.5f, story_timer);
                        MapSubtitlesAtTime("to answer why.", 46.75f, story_timer);
                        MapSubtitlesAtTime("⛈", 48.25f, story_timer);
                        MapSubtitlesAtTime("Any satellite, man-", 54.5f, story_timer);
                        MapSubtitlesAtTime("made or not,", 56f, story_timer);
                        MapSubtitlesAtTime("remains in orbit", 57f, story_timer);
                        MapSubtitlesAtTime("because the force with", 58f, story_timer);
                        MapSubtitlesAtTime("which it is trying to", 59.5f, story_timer);
                        MapSubtitlesAtTime("fly away from Earth", 60.5f, story_timer);
                        MapSubtitlesAtTime("is matched by the", 62.25f, story_timer);
                        MapSubtitlesAtTime("gravitation pull", 63.25f, story_timer);
                        MapSubtitlesAtTime("of Earth.", 64.75f, story_timer);
                        MapSubtitlesAtTime("So it continues", 66.25f, story_timer);
                        MapSubtitlesAtTime("moving around Earth", 67.25f, story_timer);
                        MapSubtitlesAtTime("in an orbit whose", 68.75f, story_timer);
                        MapSubtitlesAtTime("path conforms very", 70.25f, story_timer);
                        MapSubtitlesAtTime("nearly to the", 71f, story_timer);
                        MapSubtitlesAtTime("classic laws of Sir", 72f, story_timer);
                        MapSubtitlesAtTime("Isaac Newton", 73f, story_timer);
                        MapSubtitlesAtTime("and Johannes Kepler", 74f, story_timer);
                        MapSubtitlesAtTime("Tap to continue ...", 76f, story_timer);
                        // MapSubtitlesAtTime("Welcome to the", 2f, story_timer);
                        // MapSubtitlesAtTime("U.S. Naval Academy", 2.5f, story_timer);
                        // MapSubtitlesAtTime("Aerial Ordnance", 4.5f, story_timer);
                        // MapSubtitlesAtTime("Tutorial!", 5.5f, story_timer);
                        // MapSubtitlesAtTime("Here you will learn", 7f, story_timer);
                        // MapSubtitlesAtTime("the mains ways", 8.5f, story_timer);
                        // MapSubtitlesAtTime("of attacking", 9.5f, story_timer);
                        // MapSubtitlesAtTime("from the air.", 10.5f, story_timer);
                        // MapSubtitlesAtTime("Strike aircraft are", 12f, story_timer);
                        // MapSubtitlesAtTime("the longest ranging and", 13.25f, story_timer);
                        // MapSubtitlesAtTime("often most powerful way", 15f, story_timer);
                        // MapSubtitlesAtTime("to engage a target", 17f, story_timer);
                        // MapSubtitlesAtTime("So be sure", 18.75f, story_timer);
                        // MapSubtitlesAtTime("to pay attention!", 19.75f, story_timer);
                        // MapSubtitlesAtTime("Click/tap to continue ...", 21f, story_timer);
                    }
                    else if (campaign_stage == 0) {
                        // if (story_timer > 0f && story_timer < Time.deltaTime * 2f) { PlayAudio("CampaignNewtonsLawsLookAround.ogg"); }
                        MapSubtitlesAtTime("First off,", 0f, story_timer);
                        MapSubtitlesAtTime("Try looking around", 1f, story_timer);
                        MapSubtitlesAtTime("", 2f, story_timer);
                    }
                    else if (campaign_stage == 1) {
                        // if (story_timer > 0f && story_timer < Time.deltaTime * 2f) { PlayAudio("CampaignNewtonsLawsTarget.ogg"); }
                        MapSubtitlesAtTime("Target ,", 0f, story_timer);
                        MapSubtitlesAtTime("Try", 1f, story_timer);
                        MapSubtitlesAtTime("", 2f, story_timer);

                    }
                    else if (campaign_stage == 2) {
                        // if (story_timer > 0f && story_timer < Time.deltaTime * 2f) { PlayAudio("CampaignNewtonsLawsUse.ogg"); }
                        MapSubtitlesAtTime("Press the \"Use Weapon\"", 0f, story_timer);
                        MapSubtitlesAtTime("key to fire!", 1f, story_timer);
                        MapSubtitlesAtTime("", 2f, story_timer);

                    }
                    else if (campaign_stage == 3) {
                        // if (story_timer > 0f && story_timer < Time.deltaTime * 2f) { PlayAudio("CampaignNewtonsLawsFire.ogg"); }
                        MapSubtitlesAtTime("Well done!", 0f, story_timer);
                        MapSubtitlesAtTime("", 1f, story_timer);
                        MapSubtitlesAtTime("", 2f, story_timer);

                    }
                    else if (campaign_stage == 4) {
                        // if (story_timer > 0f && story_timer < Time.deltaTime * 2f) { PlayAudio("CampaignNewtonsLawsDone.ogg"); }
                        MapSubtitlesAtTime("Well done!", 0f, story_timer);
                        MapSubtitlesAtTime("", 1f, story_timer);
                        MapSubtitlesAtTime("", 2f, story_timer);

                    }
                    if (CampaignNewtonsLaws.transform.childCount == 0) 
                    {
                        campaign_splits[clip_index - 1] = story_timer;
                        story_timer = 0f;
                        PlayVideo(campaign_clips[clip_index]);
                        clip_index++;
                    }
                    break;
                case 3:
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2.4f, story_timer);
                    MapSubtitlesAtTime("♩", 3.4f, story_timer);
                    MapSubtitlesAtTime("♩♩", 3.9f, story_timer);
                    MapSubtitlesAtTime("♪", 4.4f, story_timer);
                    MapSubtitlesAtTime("♪♪", 4.9f, story_timer);
                    MapSubtitlesAtTime("☄", 5.4f, story_timer);
                    MapSubtitlesAtTime("♪", 5.9f, story_timer);
                    MapSubtitlesAtTime("♫", 6.15f, story_timer);
                    MapSubtitlesAtTime("♫♪", 6.4f, story_timer);
                    MapSubtitlesAtTime("♫♫", 6.65f, story_timer);
                    MapSubtitlesAtTime("♫♫♪", 6.9f, story_timer);
                    MapSubtitlesAtTime("⛅", 7.5f, story_timer);
                    MapSubtitlesAtTime("♩", 10f, story_timer);
                    MapSubtitlesAtTime("♩♩", 11.25f, story_timer);
                    MapSubtitlesAtTime("♩♩♪", 12.25f, story_timer);
                    MapSubtitlesAtTime("♩", 13f, story_timer);
                    MapSubtitlesAtTime("♩♪", 13.5f, story_timer);
                    MapSubtitlesAtTime("♩♫", 13.75f, story_timer);
                    MapSubtitlesAtTime("♪", 14f, story_timer);
                    MapSubtitlesAtTime("♪♪", 14.5f, story_timer);
                    MapSubtitlesAtTime("♪♪♪", 15f, story_timer);
                    MapSubtitlesAtTime("♪♪♪♪", 15.5f, story_timer);
                    MapSubtitlesAtTime("♪♪♪♪♪", 15.75f, story_timer);
                    MapSubtitlesAtTime("♩", 16f, story_timer);
                    MapSubtitlesAtTime("♩♩", 16.5f, story_timer);
                    MapSubtitlesAtTime("♩♩♩", 16.75f, story_timer);
                    MapSubtitlesAtTime("♩♩♩♩", 17.25f, story_timer);
                    MapSubtitlesAtTime("♩♩♩♩♩", 17.75f, story_timer);
                    MapSubtitlesAtTime("☄", 18f, story_timer);
                    MapSubtitlesAtTime("♪", 18.25f, story_timer);
                    MapSubtitlesAtTime("♪♪", 18.5f, story_timer);
                    MapSubtitlesAtTime("♪♫", 18.75f, story_timer);
                    MapSubtitlesAtTime("♪♫♩", 19f, story_timer);
                    MapSubtitlesAtTime("\"Neutron\"", 20f, story_timer);
                    MapSubtitlesAtTime("\"Gamma Rays\"", 21f, story_timer);
                    MapSubtitlesAtTime("\"Solar Power\"", 22.5f, story_timer);
                    MapSubtitlesAtTime("\"Transistor\"", 23.5f, story_timer);
                    MapSubtitlesAtTime("\"Automation\"", 24.75f, story_timer);
                    MapSubtitlesAtTime("A new language has", 26.25f, story_timer);
                    MapSubtitlesAtTime("come into currency.", 27.25f, story_timer);
                    MapSubtitlesAtTime("To the public,", 28.75f, story_timer);
                    MapSubtitlesAtTime("it is a language", 29.35f, story_timer);
                    MapSubtitlesAtTime("of the future.", 30, story_timer);
                    MapSubtitlesAtTime("To the scientist,", 31f, story_timer);
                    MapSubtitlesAtTime("a language", 32f, story_timer);
                    MapSubtitlesAtTime("of the present.", 32.5f, story_timer);
                    MapSubtitlesAtTime("This then is a report", 34f, story_timer);
                    MapSubtitlesAtTime("on our present future.", 35f, story_timer);
                    MapSubtitlesAtTime("Some of it profound.", 37f, story_timer);
                    MapSubtitlesAtTime("Some of it mere gadgetry.", 38.25f, story_timer);
                    MapSubtitlesAtTime("You are looking now", 40.5f, story_timer);
                    MapSubtitlesAtTime("at a nuclear reactor.", 41.5f, story_timer);
                    MapSubtitlesAtTime("It is not producing", 43.25f, story_timer);
                    MapSubtitlesAtTime("a bomb.", 44f, story_timer);
                    MapSubtitlesAtTime("It can produce", 45f, story_timer);
                    MapSubtitlesAtTime("electricity.", 46f, story_timer);
                    MapSubtitlesAtTime("From a pilot", 47.35f, story_timer);
                    MapSubtitlesAtTime("atomic power plant", 48f, story_timer);
                    MapSubtitlesAtTime("in the desert", 49.25f, story_timer);
                    MapSubtitlesAtTime("the lights go on!", 50.15f, story_timer);
                    MapSubtitlesAtTime("Nuclear energy", 51.75f, story_timer);
                    MapSubtitlesAtTime("goes to work!", 53f, story_timer);
                    MapSubtitlesAtTime("Not destroying but", 54f, story_timer);
                    MapSubtitlesAtTime("serving mankind!", 55.5f, story_timer);
                    MapSubtitlesAtTime("⛅", 70 - 12.25f, story_timer);
                    MapSubtitlesAtTime("♪", 58.5f, story_timer);
                    MapSubtitlesAtTime("♪♩", 59f, story_timer);
                    MapSubtitlesAtTime("♪♩♪", 59.5f, story_timer);
                    MapSubtitlesAtTime("♩", 60.5f, story_timer);
                    MapSubtitlesAtTime("♩♩", 61f, story_timer);
                    MapSubtitlesAtTime("The power lines of", 61.25f, story_timer);
                    MapSubtitlesAtTime("tomorrow may also", 62.15f, story_timer);
                    MapSubtitlesAtTime("derive their elect-", 63.25f, story_timer);
                    MapSubtitlesAtTime("ricity from that", 64.25f, story_timer);
                    MapSubtitlesAtTime("source of all power:", 65f, story_timer);
                    MapSubtitlesAtTime("the sun!", 66.25f, story_timer);
                    MapSubtitlesAtTime("☄", 78.5f, story_timer);
                    MapSubtitlesAtTime("♪", 70f, story_timer);
                    MapSubtitlesAtTime("♪♩", 70.5f, story_timer);
                    MapSubtitlesAtTime("⛈", 71.5f, story_timer);
                    break;
                case 4:
                    if (CampaignDeBroglieTheory.transform.childCount == 0) {
                        campaign_splits[clip_index - 1] = story_timer;
                        story_timer = 0f;
                        PlayVideo(campaign_clips[clip_index]);
                        clip_index++;
                    }
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2f, story_timer);
                    MapSubtitlesAtTime("The satellite", 5.25f, story_timer);
                    MapSubtitlesAtTime("is currently a", 6.25f, story_timer);
                    MapSubtitlesAtTime("compact octagonal", 7f, story_timer);
                    MapSubtitlesAtTime("spacecraft,", 8.25f, story_timer);
                    MapSubtitlesAtTime("weighing about one", 9.25f, story_timer);
                    MapSubtitlesAtTime("hundred thirty pounds.", 10.25f, story_timer);
                    MapSubtitlesAtTime("It is loaded with", 12.75f, story_timer);
                    MapSubtitlesAtTime("miniaturized", 13.75f, story_timer);
                    MapSubtitlesAtTime("electronic components", 14.75f, story_timer);
                    MapSubtitlesAtTime("of receiving,", 15.75f, story_timer);
                    MapSubtitlesAtTime("transmitting,", 17.25f, story_timer);
                    MapSubtitlesAtTime("telemetry,", 18.25f, story_timer);
                    MapSubtitlesAtTime("power and", 19.25f, story_timer);
                    MapSubtitlesAtTime("memory systems.", 20.25f, story_timer);
                    MapSubtitlesAtTime("⛈", 22.25f, story_timer);
                    MapSubtitlesAtTime("The  satellite", 25f, story_timer);
                    MapSubtitlesAtTime("clock is in actuality", 26.25f, story_timer);
                    MapSubtitlesAtTime("an ultrastable", 28f, story_timer);
                    MapSubtitlesAtTime("five megacycle", 29.25f, story_timer);
                    MapSubtitlesAtTime("oscillator", 30.5f, story_timer);
                    MapSubtitlesAtTime("like this one.", 31.25f, story_timer);
                    MapSubtitlesAtTime("Solid state elect-", 35.25f, story_timer);
                    MapSubtitlesAtTime("ronic components", 35.25f, story_timer);
                    MapSubtitlesAtTime("used in the satellite", 37.25f, story_timer);
                    MapSubtitlesAtTime("are so minute", 38.25f, story_timer);
                    MapSubtitlesAtTime("that a binocular", 40.75f, story_timer);
                    MapSubtitlesAtTime("microscope is used", 41.75f, story_timer);
                    MapSubtitlesAtTime("during assembly for", 42.75f, story_timer);
                    MapSubtitlesAtTime("the precision welding", 44.25f, story_timer);
                    MapSubtitlesAtTime("required to", 45.25f, story_timer);
                    MapSubtitlesAtTime("ensure absolute", 46.25f, story_timer);
                    MapSubtitlesAtTime("reliability", 47.25f, story_timer);
                    MapSubtitlesAtTime("under vibration and", 48.25f, story_timer);
                    MapSubtitlesAtTime("temperature stresses", 49.5f, story_timer);
                    MapSubtitlesAtTime("encountered during", 50.75f, story_timer);
                    MapSubtitlesAtTime("launch and orbit.", 51.75f, story_timer);
                    MapSubtitlesAtTime("Tap to continue ...", 53.75f, story_timer);
                    break;
                case 5:
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2.4f, story_timer);
                    MapSubtitlesAtTime("♩", 3.4f, story_timer);
                    MapSubtitlesAtTime("♩♩", 3.9f, story_timer);
                    MapSubtitlesAtTime("♪", 4.4f, story_timer);
                    MapSubtitlesAtTime("♪♪", 4.9f, story_timer);
                    MapSubtitlesAtTime("☄", 5.4f, story_timer);
                    MapSubtitlesAtTime("♪", 5.9f, story_timer);
                    MapSubtitlesAtTime("♫", 6.15f, story_timer);
                    MapSubtitlesAtTime("♫♪", 6.4f, story_timer);
                    MapSubtitlesAtTime("♫♫", 6.65f, story_timer);
                    MapSubtitlesAtTime("♫♫♪", 6.9f, story_timer);
                    MapSubtitlesAtTime("⛅", 8f, story_timer);
                    MapSubtitlesAtTime("Along with the atom", 8.5f, story_timer);
                    MapSubtitlesAtTime("and the sun", 9.5f, story_timer);
                    MapSubtitlesAtTime("the electron opens", 10.25f, story_timer);
                    MapSubtitlesAtTime("a major highway to", 11.5f, story_timer);
                    MapSubtitlesAtTime("this present future.", 12.5f, story_timer);
                    MapSubtitlesAtTime("In the electronics age", 14.5f, story_timer);
                    MapSubtitlesAtTime("the development of", 16f, story_timer);
                    MapSubtitlesAtTime("giant computers", 17f, story_timer);
                    MapSubtitlesAtTime("electronic brains,", 18f, story_timer);
                    MapSubtitlesAtTime("has been a key", 19.25f, story_timer);
                    MapSubtitlesAtTime("development.", 19.75f, story_timer);
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
                    MapSubtitlesAtTime("industrial revolution:", 32.25f, story_timer);
                    MapSubtitlesAtTime("automation ...", 33.5f, story_timer);
                    MapSubtitlesAtTime("the highly controversial", 34.5f, story_timer);
                    MapSubtitlesAtTime("automatic factory.", 36.25f, story_timer);
                    MapSubtitlesAtTime("In this engine", 37.75f, story_timer);
                    MapSubtitlesAtTime("block assembly", 38.75f, story_timer);
                    MapSubtitlesAtTime("thousands of precision", 40f, story_timer);
                    MapSubtitlesAtTime("operations are", 41f, story_timer);
                    MapSubtitlesAtTime("performed with", 42f, story_timer);
                    MapSubtitlesAtTime("electronic brainpower", 43f, story_timer);
                    MapSubtitlesAtTime("replacing manpower.", 44f, story_timer);
                    MapSubtitlesAtTime("Only a token", 45.5f, story_timer);
                    MapSubtitlesAtTime("workforce is needed", 46.5f, story_timer);
                    MapSubtitlesAtTime("for maintenance", 47.725f, story_timer);
                    MapSubtitlesAtTime("and supervision.", 48.5f, story_timer);
                    MapSubtitlesAtTime("⛅", 50.25f, story_timer);
                    MapSubtitlesAtTime("⛈", 54.5f, story_timer);
                    break;
                case 6:
                    if (CampaignDopplerEffect.transform.childCount == 0) {
                        campaign_splits[clip_index - 1] = story_timer;
                        story_timer = 0f;
                        PlayVideo(campaign_clips[clip_index]);
                        clip_index++;
                    }
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2f, story_timer);
                    MapSubtitlesAtTime("Suppose we put", 3.5f, story_timer);
                    MapSubtitlesAtTime("a radio transmitter", 4.5f, story_timer);
                    MapSubtitlesAtTime("in a satellite.", 5.5f, story_timer);
                    MapSubtitlesAtTime("You will detect", 9.25f, story_timer);
                    MapSubtitlesAtTime("that the radio", 10.25f, story_timer);
                    MapSubtitlesAtTime("frequency is", 11f, story_timer);
                    MapSubtitlesAtTime("doppler shifted", 11.75f, story_timer);
                    MapSubtitlesAtTime("while the satellite", 13f, story_timer);
                    MapSubtitlesAtTime("passes by.", 14f, story_timer);
                    MapSubtitlesAtTime("The doppler effect", 18.25f, story_timer);
                    MapSubtitlesAtTime("shows up as an", 19.25f, story_timer);
                    MapSubtitlesAtTime("apparent change in", 20.25f, story_timer);
                    MapSubtitlesAtTime("frequency, and", 21.25f, story_timer);
                    MapSubtitlesAtTime("is caused by the", 22.25f, story_timer);
                    MapSubtitlesAtTime("relative motion", 23.25f, story_timer);
                    MapSubtitlesAtTime("between the satellite", 24.5f, story_timer);
                    MapSubtitlesAtTime("transmitter and the", 25.25f, story_timer);
                    MapSubtitlesAtTime("receiving antenna", 27f, story_timer);
                    MapSubtitlesAtTime("on Earth.", 29f, story_timer);
                    MapSubtitlesAtTime("Tap to continue ...", 31, story_timer);
                    break;
                case 7:
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2.4f, story_timer);
                    MapSubtitlesAtTime("♩", 3.4f, story_timer);
                    MapSubtitlesAtTime("♩♩", 3.9f, story_timer);
                    MapSubtitlesAtTime("♪", 4.4f, story_timer);
                    MapSubtitlesAtTime("♪♪", 4.9f, story_timer);
                    MapSubtitlesAtTime("☄", 5.4f, story_timer);
                    MapSubtitlesAtTime("♪", 5.9f, story_timer);
                    MapSubtitlesAtTime("♫", 6.15f, story_timer);
                    MapSubtitlesAtTime("♫♪", 6.4f, story_timer);
                    MapSubtitlesAtTime("♫♫", 6.65f, story_timer);
                    MapSubtitlesAtTime("♫♫♪", 6.9f, story_timer);
                    MapSubtitlesAtTime("⛅", 8f, story_timer);
                    MapSubtitlesAtTime("Without electronic", 8.25f, story_timer);
                    MapSubtitlesAtTime("control systems", 9.25f, story_timer);
                    MapSubtitlesAtTime("No nation could", 10.5f, story_timer);
                    MapSubtitlesAtTime("defend itself in", 11.25f, story_timer);
                    MapSubtitlesAtTime("modern war.", 12f, story_timer);
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
                case 8:
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    if (CampaignDopplerShift.transform.childCount == 0) {
                        campaign_splits[clip_index - 1] = story_timer;
                        story_timer = 0f;
                        PlayVideo(campaign_clips[clip_index]);
                        clip_index++;
                    }
                    MapSubtitlesAtTime("⛅", 2f, story_timer);
                    MapSubtitlesAtTime("Doppler shift", 3.5f, story_timer);
                    MapSubtitlesAtTime("can be plotted", 4.5f, story_timer);
                    MapSubtitlesAtTime("frequency versus", 5.5f, story_timer);
                    MapSubtitlesAtTime("time.", 7.25f, story_timer);
                    MapSubtitlesAtTime("to produce a", 8.25f, story_timer);
                    MapSubtitlesAtTime("unique curve.", 9.25f, story_timer);
                    MapSubtitlesAtTime("Which can be", 10.75f, story_timer);
                    MapSubtitlesAtTime(" received at only", 11.8f, story_timer);
                    MapSubtitlesAtTime("one point on Earth", 11.8f, story_timer);
                    MapSubtitlesAtTime("at a given instant.", 13.75f, story_timer);
                    MapSubtitlesAtTime("Knowing your position", 18f, story_timer);
                    MapSubtitlesAtTime("on Earth,", 18f, story_timer);
                    MapSubtitlesAtTime("you can use the", 20f, story_timer);
                    MapSubtitlesAtTime("Doppler Curve", 21.25f, story_timer);
                    MapSubtitlesAtTime("to calculate", 22.25f, story_timer);
                    MapSubtitlesAtTime("the exact orbit", 23f, story_timer);
                    MapSubtitlesAtTime("of the satellite", 24f, story_timer);
                    MapSubtitlesAtTime("And the reverse", 27f, story_timer);
                    MapSubtitlesAtTime("is true,", 28f, story_timer);
                    MapSubtitlesAtTime("if you start with", 30f, story_timer);
                    MapSubtitlesAtTime("the satellite of", 31f, story_timer);
                    MapSubtitlesAtTime("known orbit,", 31f, story_timer);
                    MapSubtitlesAtTime("analysis of the", 33f, story_timer);
                    MapSubtitlesAtTime("Doppler Shift as", 34f, story_timer);
                    MapSubtitlesAtTime("the satellite passes,", 35f, story_timer);
                    MapSubtitlesAtTime("will tell you exactly", 36.5f, story_timer);
                    MapSubtitlesAtTime("where you are,", 37.75f, story_timer);
                    MapSubtitlesAtTime("anywhere on Earth.", 39f, story_timer);
                    MapSubtitlesAtTime("This fact", 41f, story_timer);
                    MapSubtitlesAtTime("forms the basis", 42.5f, story_timer);
                    MapSubtitlesAtTime("of the Navy", 43.5f, story_timer);
                    MapSubtitlesAtTime("Navigation Satellite", 44.25f, story_timer);
                    MapSubtitlesAtTime("System.", 45.5f, story_timer);
                    MapSubtitlesAtTime("The system is more", 47.75f, story_timer);
                    MapSubtitlesAtTime("accurate than", 48.75f, story_timer);
                    MapSubtitlesAtTime("any other navigation", 50f, story_timer);
                    MapSubtitlesAtTime("system known today.", 51f, story_timer);
                    MapSubtitlesAtTime("Accuracy that can", 54.5f, story_timer);
                    MapSubtitlesAtTime("establish a pinpoint of", 55.5f, story_timer);
                    MapSubtitlesAtTime("latitude and longitude.", 57.5f, story_timer);
                    MapSubtitlesAtTime("This precision", 59.5f, story_timer);
                    MapSubtitlesAtTime("is the result", 60.5f, story_timer);
                    MapSubtitlesAtTime("of new techiques", 61.5f, story_timer);
                    MapSubtitlesAtTime("for fine control", 62.5f, story_timer);
                    MapSubtitlesAtTime("and measurement", 63.5f, story_timer);
                    MapSubtitlesAtTime("of time", 64.5f, story_timer);
                    MapSubtitlesAtTime("in terms of", 64.5f, story_timer);
                    MapSubtitlesAtTime("frequencies generated", 64.5f, story_timer);
                    MapSubtitlesAtTime("by ultrastable", 64.5f, story_timer);
                    MapSubtitlesAtTime("oscillators.", 64.5f, story_timer);
                    MapSubtitlesAtTime("Of course,", 71.75f, story_timer);
                    MapSubtitlesAtTime("There are many", 72.25f, story_timer);
                    MapSubtitlesAtTime("astronautical", 73.25f, story_timer);
                    MapSubtitlesAtTime("problems that", 74.25f, story_timer);
                    MapSubtitlesAtTime("confront this new", 75.5f, story_timer);
                    MapSubtitlesAtTime("system.", 76.5f, story_timer);
                    MapSubtitlesAtTime("Tap to continue ...", 78.5f, story_timer);
                    break;
                case 9:
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2.4f, story_timer);
                    MapSubtitlesAtTime("♩", 3.4f, story_timer);
                    MapSubtitlesAtTime("♩♩", 3.9f, story_timer);
                    MapSubtitlesAtTime("♪", 4.4f, story_timer);
                    MapSubtitlesAtTime("♪♪", 4.9f, story_timer);
                    MapSubtitlesAtTime("☄", 5.4f, story_timer);
                    MapSubtitlesAtTime("♪", 5.9f, story_timer);
                    MapSubtitlesAtTime("♫", 6.15f, story_timer);
                    MapSubtitlesAtTime("♫♪", 6.4f, story_timer);
                    MapSubtitlesAtTime("♫♫", 6.65f, story_timer);
                    MapSubtitlesAtTime("♫♫♪", 6.9f, story_timer);
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
                case 10:
                    if (CampaignPlanksLaw.transform.childCount == 0) {
                        campaign_splits[clip_index - 1] = story_timer;
                        story_timer = 0f;
                        PlayVideo(campaign_clips[clip_index]);
                        clip_index++;
                    }
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2f, story_timer);
                    MapSubtitlesAtTime("Series of experi-", 2.5f, story_timer);
                    MapSubtitlesAtTime("mental satellites.", 3.5f, story_timer);
                    MapSubtitlesAtTime("Beginning with the", 4.5f, story_timer);
                    MapSubtitlesAtTime("launch of the", 5.5f, story_timer);
                    MapSubtitlesAtTime("\"NAV-1B\" satellite", 6.25f, story_timer);
                    MapSubtitlesAtTime("in April, nineteen sixty.", 7.5f, story_timer);
                    MapSubtitlesAtTime("⛈", 9.5f, story_timer);
                    MapSubtitlesAtTime("To cover the whole Earth", 11f, story_timer);
                    MapSubtitlesAtTime("a satellite must be in", 12.5f, story_timer);
                    MapSubtitlesAtTime("near-polar orbit.", 13.5f, story_timer);
                    MapSubtitlesAtTime("That is, an", 15f, story_timer);
                    MapSubtitlesAtTime("orbit passing", 16f, story_timer);
                    MapSubtitlesAtTime("near the north and", 17f, story_timer);
                    MapSubtitlesAtTime("south poles.", 18f, story_timer);
                    MapSubtitlesAtTime("As the Earth", 20.5f, story_timer);
                    MapSubtitlesAtTime("rotates beneath the", 21.5f, story_timer);
                    MapSubtitlesAtTime("fixed plane of", 22.5f, story_timer);
                    MapSubtitlesAtTime("the orbit,", 23.5f, story_timer);
                    MapSubtitlesAtTime("which passes completely", 24.5f, story_timer);
                    MapSubtitlesAtTime("around Earth,", 25.75f, story_timer);
                    MapSubtitlesAtTime("Sooner or later", 28f, story_timer);
                    MapSubtitlesAtTime("the satellite will", 29.5f, story_timer);
                    MapSubtitlesAtTime("pass within", 30.5f, story_timer);
                    MapSubtitlesAtTime("range of any", 31.25f, story_timer);
                    MapSubtitlesAtTime("part of the globe.", 32.25f, story_timer);
                    MapSubtitlesAtTime("With one satellite", 35f, story_timer);
                    MapSubtitlesAtTime("in orbit,", 35.75f, story_timer);
                    MapSubtitlesAtTime("a particular point", 37.25f, story_timer);
                    MapSubtitlesAtTime("on the Earth", 38.25f, story_timer);
                    MapSubtitlesAtTime("is within range", 39.25f, story_timer);
                    MapSubtitlesAtTime("at least once", 40.25f, story_timer);
                    MapSubtitlesAtTime("each twelve hours.", 41.75f, story_timer);
                    MapSubtitlesAtTime("Ships and submarines", 46.5f, story_timer);
                    MapSubtitlesAtTime("need to know", 47.75f, story_timer);
                    MapSubtitlesAtTime("their position more", 48.25f, story_timer);
                    MapSubtitlesAtTime("frequently that this", 49.25f, story_timer);
                    MapSubtitlesAtTime("and therefore", 51.75f, story_timer);
                    MapSubtitlesAtTime("the \"Navy Navigation\"", 52.5f, story_timer);
                    MapSubtitlesAtTime("\"Satellite System\"", 53.75f, story_timer);
                    MapSubtitlesAtTime("employs a constell-", 54.75f, story_timer);
                    MapSubtitlesAtTime("ation of satellites,", 55.75f, story_timer);
                    MapSubtitlesAtTime("each in a polar orbit.", 57.5f, story_timer);
                    MapSubtitlesAtTime("Tap to continue ...", 59.5f, story_timer);
                    break;
                case 11:
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2.4f, story_timer);
                    MapSubtitlesAtTime("♩", 3.4f, story_timer);
                    MapSubtitlesAtTime("♩♩", 3.9f, story_timer);
                    MapSubtitlesAtTime("♪", 4.4f, story_timer);
                    MapSubtitlesAtTime("♪♪", 4.9f, story_timer);
                    MapSubtitlesAtTime("☄", 5.4f, story_timer);
                    MapSubtitlesAtTime("♪", 5.9f, story_timer);
                    MapSubtitlesAtTime("♫", 6.15f, story_timer);
                    MapSubtitlesAtTime("♫♪", 6.4f, story_timer);
                    MapSubtitlesAtTime("♫♫", 6.65f, story_timer);
                    MapSubtitlesAtTime("♫♫♪", 6.9f, story_timer);
                    MapSubtitlesAtTime("In the laboratory a", 7.25f, story_timer);
                    MapSubtitlesAtTime("\"Television Camera\"", 8f, story_timer);
                    MapSubtitlesAtTime("rigged up to a", 9f, story_timer);
                    MapSubtitlesAtTime("\"Microscope\"", 10f, story_timer);
                    MapSubtitlesAtTime("allows a scientist", 11f, story_timer);
                    MapSubtitlesAtTime("to get a big screen", 12f, story_timer);
                    MapSubtitlesAtTime("picture of the", 13f, story_timer);
                    MapSubtitlesAtTime("highly magnified", 14f, story_timer);
                    MapSubtitlesAtTime("field without the", 14f, story_timer);
                    MapSubtitlesAtTime("usual squint into", 15.5f, story_timer);
                    MapSubtitlesAtTime("the eyepiece", 16.5f, story_timer);
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
                    MapSubtitlesAtTime("In full-compatible color", 36f, story_timer);
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
                case 12:
                    if (CampaignHawkingRadiation.transform.childCount == 0) {
                        campaign_splits[clip_index - 1] = story_timer;
                        story_timer = 0f;
                        PlayVideo(campaign_clips[clip_index]);
                        clip_index++;
                    }
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2f, story_timer);
                    MapSubtitlesAtTime("The message is", 3.5f, story_timer);
                    MapSubtitlesAtTime("injected into the", 4.25f, story_timer);
                    MapSubtitlesAtTime("satellite by", 5.25f, story_timer);
                    MapSubtitlesAtTime("high-power radio", 6.25f, story_timer);
                    MapSubtitlesAtTime("radio transmission.", 7f, story_timer);
                    MapSubtitlesAtTime("This updates", 11.5f, story_timer);
                    MapSubtitlesAtTime("old information", 12.5f, story_timer);
                    MapSubtitlesAtTime("stored in satellite", 14f, story_timer);
                    MapSubtitlesAtTime("memory,", 15f, story_timer);
                    MapSubtitlesAtTime("and extends the", 16.5f, story_timer);
                    MapSubtitlesAtTime("navigational utility", 17.25f, story_timer);
                    MapSubtitlesAtTime("of that satellite.", 18.5f, story_timer);
                    MapSubtitlesAtTime("The system works", 20f, story_timer);
                    MapSubtitlesAtTime("anywhere in the world", 21.25f, story_timer);
                    MapSubtitlesAtTime("night or day, in", 23f, story_timer);
                    MapSubtitlesAtTime("any kind of weather.", 25f, story_timer);
                    MapSubtitlesAtTime("For every pound", 35f, story_timer);
                    MapSubtitlesAtTime("of satellite in", 36f, story_timer);
                    MapSubtitlesAtTime("orbit, there are", 37f, story_timer);
                    MapSubtitlesAtTime("tons of equipment", 38f, story_timer);
                    MapSubtitlesAtTime("on Earth", 39f, story_timer);
                    MapSubtitlesAtTime("that make navigation", 40.25f, story_timer);
                    MapSubtitlesAtTime("by satellite", 41.25f, story_timer);
                    MapSubtitlesAtTime("possible.", 42.75f, story_timer);
                    MapSubtitlesAtTime("Tap to continue ...", 44.75f, story_timer);
                    break;
                case 13:
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2.4f, story_timer);
                    MapSubtitlesAtTime("♩", 3.4f, story_timer);
                    MapSubtitlesAtTime("♩♩", 3.9f, story_timer);
                    MapSubtitlesAtTime("♪", 4.4f, story_timer);
                    MapSubtitlesAtTime("♪♪", 4.9f, story_timer);
                    MapSubtitlesAtTime("☄", 5.4f, story_timer);
                    MapSubtitlesAtTime("♪", 5.9f, story_timer);
                    MapSubtitlesAtTime("♫", 6.15f, story_timer);
                    MapSubtitlesAtTime("♫♪", 6.4f, story_timer);
                    MapSubtitlesAtTime("♫♫", 6.65f, story_timer);
                    MapSubtitlesAtTime("♫♫♪", 6.9f, story_timer);
                    MapSubtitlesAtTime("⛅", 8f, story_timer);
                    MapSubtitlesAtTime("In another field,", 10f, story_timer);
                    MapSubtitlesAtTime("music can now be", 11f, story_timer);
                    MapSubtitlesAtTime("produced entirely", 12f, story_timer);
                    MapSubtitlesAtTime("by electronics!", 13f, story_timer);
                    MapSubtitlesAtTime("No known instruments", 14f, story_timer);
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
                    MapSubtitlesAtTime("permitting miniaturization", 77.5f, story_timer);
                    MapSubtitlesAtTime("making big things smaller", 79.5f, story_timer);
                    MapSubtitlesAtTime("things like:", 81.5f, story_timer);
                    MapSubtitlesAtTime("\"Pocket Radios\",", 82.5f, story_timer);
                    MapSubtitlesAtTime("⛅", 84f, story_timer);
                    MapSubtitlesAtTime("\"Wristwatch Radios\",", 87.5f, story_timer);
                    MapSubtitlesAtTime("and a coming attraction", 90f, story_timer);
                    MapSubtitlesAtTime("portable battery powered", 91.5f, story_timer);
                    MapSubtitlesAtTime("\"Television Sets\"!", 93.5f, story_timer);
                    MapSubtitlesAtTime("⛈", 94.5f, story_timer);
                    break;
                case 14:
                    if (CampaignMoracevsParadox.transform.childCount == 0) {
                        campaign_splits[clip_index - 1] = story_timer;
                        story_timer = 0f;
                        PlayVideo(campaign_clips[clip_index]);
                        clip_index++;
                    }
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2f, story_timer);
                    MapSubtitlesAtTime("We're inside the", 4.25f, story_timer);
                    MapSubtitlesAtTime("\"Operation Center\"", 5.25f, story_timer);
                    MapSubtitlesAtTime("command post for", 7.25f, story_timer);
                    MapSubtitlesAtTime("the \"Operations Duty\"", 8.25f, story_timer);
                    MapSubtitlesAtTime("\"Officer\".", 9.25f, story_timer);
                    MapSubtitlesAtTime("The mission of", 13f, story_timer);
                    MapSubtitlesAtTime("the group-", 13.5f, story_timer);
                    MapSubtitlesAtTime("around the clock,", 15f, story_timer);
                    MapSubtitlesAtTime("seven days a week,", 16f, story_timer);
                    MapSubtitlesAtTime("is to make sure", 17.5f, story_timer);
                    MapSubtitlesAtTime("that each navagation", 18.5f, story_timer);
                    MapSubtitlesAtTime("satellite", 19.5f, story_timer);
                    MapSubtitlesAtTime("always has", 20f, story_timer);
                    MapSubtitlesAtTime("correct, up-to-date", 21.5f, story_timer);
                    MapSubtitlesAtTime("information", 22.5f, story_timer);
                    MapSubtitlesAtTime("stored in its", 23.5f, story_timer);
                    MapSubtitlesAtTime("memory unit.", 24.5f, story_timer);
                    MapSubtitlesAtTime("An informative array", 28f, story_timer);
                    MapSubtitlesAtTime("of actuated displays", 29.25f, story_timer);
                    MapSubtitlesAtTime("shows the performance,", 31.25f, story_timer);
                    MapSubtitlesAtTime("memory,", 33.25f, story_timer);
                    MapSubtitlesAtTime("and injection status", 34f, story_timer);
                    MapSubtitlesAtTime("and helps the duty", 36f, story_timer);
                    MapSubtitlesAtTime("officer coordinate", 37f, story_timer);
                    MapSubtitlesAtTime("all network operations", 38.5f, story_timer);
                    MapSubtitlesAtTime("that keep the satellites", 40.5f, story_timer);
                    MapSubtitlesAtTime("broadcasting navigation", 41.5f, story_timer);
                    MapSubtitlesAtTime("data.", 43.25f, story_timer);
                    MapSubtitlesAtTime("Tap to continue ...", 45.24f, story_timer);
                    break;
                case 15:
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2.4f, story_timer);
                    MapSubtitlesAtTime("♩", 3.4f, story_timer);
                    MapSubtitlesAtTime("♩♩", 3.9f, story_timer);
                    MapSubtitlesAtTime("♪", 4.4f, story_timer);
                    MapSubtitlesAtTime("♪♪", 4.9f, story_timer);
                    MapSubtitlesAtTime("☄", 5.4f, story_timer);
                    MapSubtitlesAtTime("♪", 5.9f, story_timer);
                    MapSubtitlesAtTime("♫", 6.15f, story_timer);
                    MapSubtitlesAtTime("♫♪", 6.4f, story_timer);
                    MapSubtitlesAtTime("♫♫", 6.65f, story_timer);
                    MapSubtitlesAtTime("♫♫♪", 6.9f, story_timer);
                    MapSubtitlesAtTime("⛅", 8f, story_timer);
                    MapSubtitlesAtTime("This man is starting to", 8.25f, story_timer);
                    MapSubtitlesAtTime("make a delivery for the", 9.25f, story_timer);
                    MapSubtitlesAtTime("Atomic Drug Store", 10.25f, story_timer);
                    MapSubtitlesAtTime("in Oak Ridge, Tennessee.", 11.25f, story_timer);
                    MapSubtitlesAtTime("This is one pharmacy", 13f, story_timer);
                    MapSubtitlesAtTime("that carries no", 14.5f, story_timer);
                    MapSubtitlesAtTime("aspirins,", 15.25f, story_timer);
                    MapSubtitlesAtTime("soda mints", 16f, story_timer);
                    MapSubtitlesAtTime("or cough drops.", 17f, story_timer);
                    MapSubtitlesAtTime("The merchandise is", 18f, story_timer);
                    MapSubtitlesAtTime("\"Radio Active\"", 19f, story_timer);
                    MapSubtitlesAtTime("which explains the", 20f, story_timer);
                    MapSubtitlesAtTime("ingenious methods", 21.25f, story_timer);
                    MapSubtitlesAtTime("of handling.", 22.125f, story_timer);
                    MapSubtitlesAtTime("The prioprietor is the", 23.125f, story_timer);
                    MapSubtitlesAtTime("Atomic Energy Commission.", 24.125f, story_timer);
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
                    MapSubtitlesAtTime("every year,", 57.5f, story_timer);
                    MapSubtitlesAtTime("are advancing medical", 58.5f, story_timer);
                    MapSubtitlesAtTime("science, as well as", 59.5f, story_timer);
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
                case 16:
                    if (CampaignFermiParadox.transform.childCount == 0) {
                        campaign_splits[clip_index - 1] = story_timer;
                        story_timer = 0f;
                        PlayVideo(campaign_clips[clip_index]);
                        clip_index++;
                    }
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2f, story_timer);
                    MapSubtitlesAtTime("Satellite memory", 4f, story_timer);
                    MapSubtitlesAtTime("units and", 5f, story_timer);
                    MapSubtitlesAtTime("control circuitry", 6f, story_timer);
                    MapSubtitlesAtTime("can handle nearly", 7f, story_timer);
                    MapSubtitlesAtTime("twenty-five thousand", 8f, story_timer);
                    MapSubtitlesAtTime("separate bits", 9f, story_timer);
                    MapSubtitlesAtTime("of modulated", 10f, story_timer);
                    MapSubtitlesAtTime("information", 11f, story_timer);
                    MapSubtitlesAtTime("⛈", 12.5f, story_timer);
                    MapSubtitlesAtTime("The satellite", 16f, story_timer);
                    MapSubtitlesAtTime("gets its power", 17f, story_timer);
                    MapSubtitlesAtTime("from the sun.", 18f, story_timer);
                    MapSubtitlesAtTime("Sixteen thousand", 20f, story_timer);
                    MapSubtitlesAtTime("individual solar", 21f, story_timer);
                    MapSubtitlesAtTime("cells convert", 22f, story_timer);
                    MapSubtitlesAtTime("sunlight into", 23f, story_timer);
                    MapSubtitlesAtTime("electrical energy", 24f, story_timer);
                    MapSubtitlesAtTime("that is stored", 25f, story_timer);
                    MapSubtitlesAtTime("in Nickle-", 25.75f, story_timer);
                    MapSubtitlesAtTime("Cadium batteries", 26.75f, story_timer);
                    MapSubtitlesAtTime("inside the satellite.", 28f, story_timer);
                    MapSubtitlesAtTime("Tap to continue ...", 30, story_timer);
                    break;
                case 17:
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2.4f, story_timer);
                    MapSubtitlesAtTime("♩", 3.4f, story_timer);
                    MapSubtitlesAtTime("♩♩", 3.9f, story_timer);
                    MapSubtitlesAtTime("♪", 4.4f, story_timer);
                    MapSubtitlesAtTime("♪♪", 4.9f, story_timer);
                    MapSubtitlesAtTime("☄", 5.4f, story_timer);
                    MapSubtitlesAtTime("♪", 5.9f, story_timer);
                    MapSubtitlesAtTime("♫", 6.15f, story_timer);
                    MapSubtitlesAtTime("♫♪", 6.4f, story_timer);
                    MapSubtitlesAtTime("♫♫", 6.65f, story_timer);
                    MapSubtitlesAtTime("♫♫♪", 6.9f, story_timer);
                    MapSubtitlesAtTime("⛅", 8f, story_timer);
                    MapSubtitlesAtTime("The aparatus of", 8.75f, story_timer);
                    MapSubtitlesAtTime("the atomic age is", 9.75f, story_timer);
                    MapSubtitlesAtTime("enormously ingenious!", 10.5f, story_timer);
                    MapSubtitlesAtTime("At Argon National", 12f, story_timer);
                    MapSubtitlesAtTime("Laboratory in Chicago,", 13f, story_timer);
                    MapSubtitlesAtTime("metallurgists use a", 14.5f, story_timer);
                    MapSubtitlesAtTime("master-slave", 16f, story_timer);
                    MapSubtitlesAtTime("manipulator", 17f, story_timer);
                    MapSubtitlesAtTime("to work with", 17.75f, story_timer);
                    MapSubtitlesAtTime("radioactive metals.", 18.25f, story_timer);
                    MapSubtitlesAtTime("This manipulator", 19.75f, story_timer);
                    MapSubtitlesAtTime("can do anything", 21f, story_timer);
                    MapSubtitlesAtTime("human hands can do!", 22f, story_timer);
                    MapSubtitlesAtTime("As you watch,", 23.5f, story_timer);
                    MapSubtitlesAtTime("the operator", 24.5f, story_timer);
                    MapSubtitlesAtTime("shielded by three feet", 25.5f, story_timer);
                    MapSubtitlesAtTime("of glass and concrete,", 26.5f, story_timer);
                    MapSubtitlesAtTime("conducts an involved", 28f, story_timer);
                    MapSubtitlesAtTime("metal hardness test,", 29.35f, story_timer);
                    MapSubtitlesAtTime("relying entirely", 30.5f, story_timer);
                    MapSubtitlesAtTime("on his nimble", 32.25f, story_timer);
                    MapSubtitlesAtTime("mechanical fingers.", 33.15f, story_timer);
                    MapSubtitlesAtTime("⛅", 35f, story_timer);
                    MapSubtitlesAtTime("⛈", 105f, story_timer);
                    break;
                case 18:
                    if (CampaignPascalsWager.transform.childCount == 0) {
                        campaign_splits[clip_index - 1] = story_timer;
                        story_timer = 0f;
                        PlayVideo(campaign_clips[clip_index]);
                        clip_index++;
                    }
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2f, story_timer);
                    MapSubtitlesAtTime("A few years later", 3.25f, story_timer);
                    MapSubtitlesAtTime("the French genius", 4.25f, story_timer);
                    MapSubtitlesAtTime("Blaise Pascal", 5.5f, story_timer);
                    MapSubtitlesAtTime("invented and built", 7.5f, story_timer);
                    MapSubtitlesAtTime("the world's first", 8.5f, story_timer);
                    MapSubtitlesAtTime("mechanical adding", 9.5f, story_timer);
                    MapSubtitlesAtTime("machines.", 10.5f, story_timer);
                    MapSubtitlesAtTime("The is one of them.", 11.5f, story_timer);
                    MapSubtitlesAtTime("Made in the sixteen-", 13f, story_timer);
                    MapSubtitlesAtTime("forties.", 14f, story_timer);
                    MapSubtitlesAtTime("Pascal's acheievement", 16.5f, story_timer);
                    MapSubtitlesAtTime("lay in the", 18f, story_timer);
                    MapSubtitlesAtTime("gear mechanism", 18.5f, story_timer);
                    MapSubtitlesAtTime("which automatically", 20f, story_timer);
                    MapSubtitlesAtTime("took care of carry-", 20.75f, story_timer);
                    MapSubtitlesAtTime("overs.", 21.75f, story_timer);
                    MapSubtitlesAtTime("For example,", 23.25f, story_timer);
                    MapSubtitlesAtTime("six", 25f, story_timer);
                    MapSubtitlesAtTime("plus nine", 28.5f, story_timer);
                    MapSubtitlesAtTime("and the one carried", 30.75f, story_timer);
                    MapSubtitlesAtTime("over to the next place.", 32f, story_timer);
                    MapSubtitlesAtTime("⛈", 34f, story_timer);
                    MapSubtitlesAtTime("In every area", 36.75f, story_timer);
                    MapSubtitlesAtTime("of defense,", 37f, story_timer);
                    MapSubtitlesAtTime("science,", 38.5f, story_timer);
                    MapSubtitlesAtTime("engineering and", 39.5f, story_timer);
                    MapSubtitlesAtTime("business,", 40.5f, story_timer);
                    MapSubtitlesAtTime("progress depends", 41.5f, story_timer);
                    MapSubtitlesAtTime("on the availability", 42.5f, story_timer);
                    MapSubtitlesAtTime("of fast, accurate", 43.75f, story_timer);
                    MapSubtitlesAtTime("methods of calculation.", 45f, story_timer);
                    MapSubtitlesAtTime("They've enabled us", 48.5f, story_timer);
                    MapSubtitlesAtTime("to take giant", 49.75f, story_timer);
                    MapSubtitlesAtTime("steps forward", 50.25f, story_timer);
                    MapSubtitlesAtTime("in power,", 51f, story_timer);
                    MapSubtitlesAtTime("in control,", 52.5f, story_timer);
                    MapSubtitlesAtTime("in design,", 54.5f, story_timer);
                    MapSubtitlesAtTime("in processing,", 55.5f, story_timer);
                    MapSubtitlesAtTime("and in research.", 57f, story_timer);
                    MapSubtitlesAtTime("Tap to continue ...", 59f, story_timer);
                    break;
                case 19:
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    MapSubtitlesAtTime("⛅", 2.4f, story_timer);
                    MapSubtitlesAtTime("♩", 3.4f, story_timer);
                    MapSubtitlesAtTime("♩♩", 3.9f, story_timer);
                    MapSubtitlesAtTime("♪", 4.4f, story_timer);
                    MapSubtitlesAtTime("♪♪", 4.9f, story_timer);
                    MapSubtitlesAtTime("☄", 5.4f, story_timer);
                    MapSubtitlesAtTime("♪", 5.9f, story_timer);
                    MapSubtitlesAtTime("♫", 6.15f, story_timer);
                    MapSubtitlesAtTime("♫♪", 6.4f, story_timer);
                    MapSubtitlesAtTime("♫♫", 6.65f, story_timer);
                    MapSubtitlesAtTime("♫♫♪", 6.9f, story_timer);
                    MapSubtitlesAtTime("⛅", 8f, story_timer);
                    MapSubtitlesAtTime("In the final", 9f, story_timer);
                    MapSubtitlesAtTime("analysis however,", 10f, story_timer);
                    MapSubtitlesAtTime("the key to the future", 11f, story_timer);
                    MapSubtitlesAtTime("is not an aparatus", 12f, story_timer);
                    MapSubtitlesAtTime("a machine", 13.75f, story_timer);
                    MapSubtitlesAtTime("or an electronic cube,", 15f, story_timer);
                    MapSubtitlesAtTime("but the brainpower", 16f, story_timer);
                    MapSubtitlesAtTime("of man.", 17f, story_timer);
                    MapSubtitlesAtTime("Nothing will", 18.5f, story_timer);
                    MapSubtitlesAtTime("ever replace", 19f, story_timer);
                    MapSubtitlesAtTime("creative intelligence.", 20f, story_timer);
                    MapSubtitlesAtTime("In great laboratories,", 21.5f, story_timer);
                    MapSubtitlesAtTime("in colleges", 23.25f, story_timer);
                    MapSubtitlesAtTime("and universities,", 23.75f, story_timer);
                    MapSubtitlesAtTime("in solitary quiet ...", 25f, story_timer);
                    MapSubtitlesAtTime("Man thinks,", 26.35f, story_timer);
                    MapSubtitlesAtTime("reasons,", 28f, story_timer);
                    MapSubtitlesAtTime("experiments,", 29f, story_timer);
                    MapSubtitlesAtTime("creates.", 30f, story_timer);
                    MapSubtitlesAtTime("The mind", 31, story_timer);
                    MapSubtitlesAtTime("strains to peer", 32f, story_timer);
                    MapSubtitlesAtTime("beyond today's horizons", 33.25f, story_timer);
                    MapSubtitlesAtTime("for a glimpse of", 35f, story_timer);
                    MapSubtitlesAtTime("the wonders of tomorrow!", 36.25f, story_timer);
                    MapSubtitlesAtTime("⛅", 39f, story_timer);
                    MapSubtitlesAtTime("🔚", 43.5f, story_timer);
                    MapSubtitlesAtTime("⛈", 47f, story_timer);
                    break;
                case 20:
                    MapSubtitlesAtTime("╔═════════════════════╗\n║ BitNaughts Campaign ║\n║   * Report Card *   ║\n╠═════════════════════╣\n║ Time: " + FloatToTime(global_timer) + "\t\t  ║\n╚═════════════════════╝\n\nThanks for playing!", 0f, story_timer);
                    MapSubtitlesAtTime("╔═════════════════════╗\n║ BitNaughts Campaign ║\n║   * Report Card *   ║\n╠═════════════════════╣\n║ Date: " + System.DateTime.Now.ToString("h:mm:ss.f") + "\t  ║\n╚═════════════════════╝\n\nThanks for playing!", 5f, story_timer);
                    MapSubtitlesAtTime("╔═════════════════════╗\n║ BitNaughts Campaign ║\n║   * Report Card *   ║\n╠═════════════════════╣\n║ Date: " + System.DateTime.Now.ToString("MM/dd/yyyy") + "\t  ║\n╚═════════════════════╝\n\nThanks for playing!", 7.5f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ Woody Allen's      ║\n║ Radio Days         ║\n║             (1987) ║\n╚════════════════════╝\n\nTap to continue ...", 10f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ Jay Bonafield's    ║\n║ The Future Is Now  ║\n║             (1955) ║\n╚════════════════════╝\n\nTap to continue ...", 12.25f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ U.S. Navy's        ║\n║ Navigation Satel-  ║\n║ lite System (1955) ║\n╚════════════════════╝\n\nTap to continue ...", 14.5f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ U.S. Navy's        ║\n║ Digital Computer   ║\n║ Techniques  (1962) ║\n╚════════════════════╝\n\nTap to continue ...", 16.75f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ N.A.S.A's          ║\n║ Space Down to      ║\n║ Earth       (1970) ║\n╚════════════════════╝\n\nTap to continue ...", 19f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Sprite *     ║\n║ Alejandro Monge's  ║\n║ Modular Spaceships ║\n║             (2014) ║\n╚════════════════════╝\n\nTap to continue ...", 21.25f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Sound  *     ║\n║ Eidos Interactive  ║\n║ Battlestations     ║\n║ Pacific     (2009) ║\n╚════════════════════╝\n\nTap to continue ...", 23.5f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n║ Valentine          ║\n║             (2013) ║\n╚════════════════════╝\n\nTap to continue ...", 25.75f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n║ Valentine          ║\n║             (2013) ║\n╚════════════════════╝\n\nTap to continue ...", 28f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n║ Valentine          ║\n║             (2013) ║\n\n\nTap to continue ...", 33f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n║ Valentine          ║\n\n\n\nTap to continue ...", 33.5f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n\n\n\n\nTap to continue ...", 34f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n\n\n\n\n\nTap to continue ...", 34.5f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╚════════════════════╝\n\n\n\n\n\n\nTap to continue ...", 35f, story_timer);
                    MapSubtitlesAtTime("These current tests", 35.25f, story_timer);
                    MapSubtitlesAtTime("are enabling", 36.75f, story_timer);
                    MapSubtitlesAtTime("N.A.S.A", 37.75f, story_timer);
                    MapSubtitlesAtTime("and the broadcasting", 38.25f, story_timer);
                    MapSubtitlesAtTime("community", 39.25f, story_timer);
                    MapSubtitlesAtTime("to iron out", 40.25f, story_timer);
                    MapSubtitlesAtTime("technical problems", 41.25f, story_timer);
                    MapSubtitlesAtTime("that are involved", 42.25f, story_timer);
                    MapSubtitlesAtTime("in this form of", 43.25f, story_timer);
                    MapSubtitlesAtTime("transmission.", 44.25f, story_timer);
                    MapSubtitlesAtTime("And to determine", 45.25f, story_timer);
                    MapSubtitlesAtTime("the costs of such", 46.75f, story_timer);
                    MapSubtitlesAtTime("future operations.", 47.75f, story_timer);
                    MapSubtitlesAtTime("If these tests are", 50f, story_timer);
                    MapSubtitlesAtTime("successful,", 51f, story_timer);
                    MapSubtitlesAtTime("we have every reason", 52.5f, story_timer);
                    MapSubtitlesAtTime("to believe that", 53.25f, story_timer);
                    MapSubtitlesAtTime("they will be,", 53.75f, story_timer);
                    MapSubtitlesAtTime("The American", 55.25f, story_timer);
                    MapSubtitlesAtTime("people will", 55.75f, story_timer);
                    MapSubtitlesAtTime("reap a major", 56.75f, story_timer);
                    MapSubtitlesAtTime("domestic dividend", 57.75f, story_timer);
                    MapSubtitlesAtTime("from the national", 59.25f, story_timer);
                    MapSubtitlesAtTime("space efforts.", 60.25f, story_timer);
                    MapSubtitlesAtTime("⛈", 61.25f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n║ Brian Hungerman    ║\n║ brianhungerman.com ║\n║             (2022) ║\n╚════════════════════╝\n\nTap to continue ...", 62.25f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n║ Brian Hungerman    ║\n║ brianhungerman.com ║\n║             (2022) ║\n\n\nTap to continue ...", 85f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n║ Brian Hungerman    ║\n║ brianhungerman.com ║\n\n\n\nTap to continue ...", 85.5f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n║ Brian Hungerman    ║\n\n\n\n\nTap to continue ...", 86f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n\n\n\n\n\nTap to continue ...", 86.5f, story_timer);
                    MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╚════════════════════╝\n\n\n\n\n\n\nTap to continue ...", 87f, story_timer);
                    MapSubtitlesAtTime("The current goal", 87.25f, story_timer);
                    MapSubtitlesAtTime("of satellite", 88.25f, story_timer);
                    MapSubtitlesAtTime("geodesy", 89f, story_timer);
                    MapSubtitlesAtTime("is to tie all", 89.5f, story_timer);
                    MapSubtitlesAtTime("geodetic", 90.5f, story_timer);
                    MapSubtitlesAtTime("grids together", 91.25f, story_timer);
                    MapSubtitlesAtTime("within an", 92f, story_timer);
                    MapSubtitlesAtTime("accuracy of", 92.5f, story_timer);
                    MapSubtitlesAtTime("thirty feet.", 93.25f, story_timer);
                    MapSubtitlesAtTime("Using high-flying", 95f, story_timer);
                    MapSubtitlesAtTime("satellites as geo-", 96f, story_timer);
                    MapSubtitlesAtTime("detic markers,", 97f, story_timer);
                    MapSubtitlesAtTime("the world's", 98.5f, story_timer);
                    MapSubtitlesAtTime("contentients", 99f, story_timer);
                    MapSubtitlesAtTime("will eventually be", 99.5f, story_timer);
                    MapSubtitlesAtTime("tied together", 100.5f, story_timer);
                    MapSubtitlesAtTime("to one common", 101.25f, story_timer);
                    MapSubtitlesAtTime("reference system.", 102.25f, story_timer);
                    MapSubtitlesAtTime("Educational and", 104.5f, story_timer);
                    MapSubtitlesAtTime("cultural programs", 105.5f, story_timer);
                    MapSubtitlesAtTime("to populations of", 106.5f, story_timer);
                    MapSubtitlesAtTime("entire nations", 107.75f, story_timer);
                    MapSubtitlesAtTime("through inter-", 109.5f, story_timer);
                    MapSubtitlesAtTime("contentiential", 110f, story_timer);
                    MapSubtitlesAtTime("television!", 110.5f, story_timer);
                    MapSubtitlesAtTime("⛈", 112.5f, story_timer);
                    MapSubtitlesAtTime("As we develop", 114.4f, story_timer);
                    MapSubtitlesAtTime("this potential in", 115.5f, story_timer);
                    MapSubtitlesAtTime("the future,", 116.5f, story_timer);
                    MapSubtitlesAtTime("applications from", 117.5f, story_timer);
                    MapSubtitlesAtTime("space will have", 118.5f, story_timer);
                    MapSubtitlesAtTime("continued", 119.25f, story_timer);
                    MapSubtitlesAtTime("profound and", 120.25f, story_timer);
                    MapSubtitlesAtTime("direct effects", 121f, story_timer);
                    MapSubtitlesAtTime("on our", 122f, story_timer);
                    MapSubtitlesAtTime("everyday lives", 122.75f, story_timer);
                    MapSubtitlesAtTime("here on Earth.", 124f, story_timer);
                    MapSubtitlesAtTime("⛅", 126f, story_timer);
                    MapSubtitlesAtTime("⛈", 148, story_timer);
                    break;
                case 21:
                    MapSubtitlesAtTime("⛈", 0, story_timer);
                    break;
            }
        }
        else 
        {
            if ((animation_timer / 5f ) % 2 < 1) {
                Timer.text = System.DateTime.Now.AddYears(-54).ToString("MM/dd/yyyy") + "\n";
            } else {
                Timer.text = System.DateTime.Now.ToString("h:mm:ss.f") + "\n";
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
            // timer += Time.deltaTime;
            // Flash("Clickable$", 0f);
            // if (timer > 3) {
            //     onLoad = false;
            //     timer = 0;
            //     ResetFlash("Clickable$", 0f);
            // }
        } else if (tutorialIntro) {
            // timer += Time.deltaTime;
            // global_timer += Time.deltaTime;
            // PlayAtTime(TutorialIntro, 1f);
            // SubtitlesAtTime("  Welcome_to_the", 1f);
            // SubtitlesAtTime("  Welcome_to_the\n⍰_Command_Tutorial!", 2f);
            // SubtitlesAtTime("  Welcome_to_the\n⍰_Command_Tutorial!\n\n  Today_you_will_learn", 4f);
            // SubtitlesAtTime("  Welcome_to_the\n⍰_Command_Tutorial!\n\n  Today_you_will_learn\n  \"Ship_control\"", 5.5f);
            // PlayAtTime(TutorialLookAround, 7f);
            // MapSubtitlesAtTime("Click and drag\n▦ Map Screen", 7f);
            // Flash ("OverlayPanUp", 7f);
            // Flash ("OverlayPanDown", 7f);
            // Flash ("OverlayPanLeft", 7f);
            // Flash ("OverlayPanRight", 7f);
            // SubtitlesAtTime("  Welcome_to_the\n⍰_Command_Tutorial!\n\n  Today_you_will_learn\n  \"Ship_control\"\n\n  First_off,_try_looking_around!", 7f);
            // SubtitlesAtTime("  Welcome_to_the\n⍰_Command_Tutorial!\n\n  Today_you_will_learn\n  \"Ship_control\"\n\n  First_off,_try_looking_around!\n  360°_awareness_is_needed", 9f);
            // SubtitlesAtTime("  Welcome_to_the\n⍰_Command_Tutorial!\n\n  Today_you_will_learn\n  \"Ship_control\"\n\n  First_off,_try_looking_around!\n  360°_awareness_is_needed_for\n  dog_fighting!", 11f);
            // PlayAtTime(TutorialTry, 20f);
            // PlayAtTime(TutorialLookAround, 30f);
        } else if (tutorialPan) {
            // timer += Time.deltaTime;
            // global_timer += Time.deltaTime;
            // MapSubtitlesAtTime("", 0f);
            // ResetFlash ("OverlayPanUp", 0f);
            // ResetFlash ("OverlayPanDown", 0f);
            // ResetFlash ("OverlayPanLeft", 0f);
            // ResetFlash ("OverlayPanRight", 0f);
            // Flash ("MapScreenOverlay", 1.5f);
            // Flash ("MapScreenOverlayBitBottomLeft", 1.5f);
            // Flash ("MapScreenOverlayBitRightTop", 1.5f);
            // Flash ("MapScreenOverlayBitRightBottom", 1.5f);
            // Flash ("MapScreenOverlayBitUpLeft", 1.5f);
            // Flash ("MapScreenOverlayBitUpRight", 1.5f);
            // Flash ("MapScreenOverlayBitDownLeft", 1.5f);
            // Flash ("MapScreenOverlayBitDownRight", 1.5f);
            // Flash ("MapScreenOverlayBitExit", 1.5f);
            // Flash ("MapScreenOverlayBitReset", 1.5f);
            // PlayAtTime(TutorialGood3, 0.5f);
            // PlayAtTime(TutorialMapScreen, 1.5f);
            // SubtitlesAtTime("▦_Map_Screen_shows", 1.5f);
            // SubtitlesAtTime("▦_Map_Screen_shows\n┗_Mission_area", 3.5f);
            // SubtitlesAtTime("▦_Map_Screen_shows\n┠_Mission_area\n┗_Friendly_units", 5f);
            // SubtitlesAtTime("▦_Map_Screen_shows\n┠_Mission_area\n┠_Friendly_units\n┗_Detected_enemy_units", 7f);
            // SubtitlesAtTime("▦_Map_Screen_shows\n├_Mission_area\n├_Friendly_units\n└_Detected_enemy_units\n\n  Select_units_highlighted_yellow!", 9f);
            // SpriteFlash ("Cannon", 9f);
            // ResetFlash("MapScreenOverlay", 9f);
            // ResetFlash ("MapScreenOverlayBitBottomLeft", 9f);
            // ResetFlash ("MapScreenOverlayBitRightTop", 9f);
            // ResetFlash ("MapScreenOverlayBitRightBottom", 9f);
            // ResetFlash ("MapScreenOverlayBitUpLeft", 9f);
            // ResetFlash ("MapScreenOverlayBitUpRight", 9f);
            // ResetFlash ("MapScreenOverlayBitDownLeft", 9f);
            // ResetFlash ("MapScreenOverlayBitDownRight", 9f);
            // ResetFlash ("MapScreenOverlayBitExit", 9f);
            // ResetFlash ("MapScreenOverlayBitReset", 9f);
            // PlayAtTime(TutorialTargetWindowHelp, 14f);
            // SubtitlesAtTime("▦_Map_Screen_shows\n├_Mission_area\n├_Friendly_units\n└_Detected_enemy_units\n\n  Select_units_highlighted_yellow!\n\n  When_the_reticle_is\n  over_the_target_press", 14f);
            // SubtitlesAtTime("▦_Map_Screen_shows\n├_Mission_area\n├_Friendly_units\n└_Detected_enemy_units\n\n  Select_units_highlighted_yellow!\n\n  When_the_reticle_is\n  over_the_target_press\n  \"Use_weapon_control\"", 16.5f);
            // SubtitlesAtTime("▦_Map_Screen_shows\n├_Mission_area\n├_Friendly_units\n└_Detected_enemy_units\n\n  Select_units_highlighted_yellow!\n\n  When_the_reticle_is\n  over_the_target_press\n  \"Use_weapon_control\"\n\n  This_will_display\n⁜_Target Window", 19f);
            // PlayAtTime(TutorialTry, 25f);
            // MapSubtitlesAtTime("Press\n◍ Cannon", 9f);
            // PlayAtTime(TutorialTargetWindowHelp, 29f);
        } else  if (tutorialTarget) {
            // timer += Time.deltaTime;
            // global_timer += Time.deltaTime;
            // ResetSpriteFlash("Cannon", 0f);
            // MapSubtitlesAtTime("", 0f);
            // PlayAtTime(TutorialTargetWindowSelected, 0.5f);
            // MapSubtitlesAtTime("⁜ Target Window displays", 1f);
            // MapSubtitlesAtTime("unit name and class", 4f);
            // Flash("OverlayBorder", 1.2f);
            // PlayAtTime(TutorialLeftWindow, 8f);
            // ResetFlash("OverlayBorder", 8f);
            // Flash ("InterpreterPanel", 8f);
            // Flash ("UnitScreenBit", 8f);
            // MapSubtitlesAtTime("Left is ▤ Unit Window", 8f);
            // MapSubtitlesAtTime("displays information about class", 11f);
            // ResetFlash ("InterpreterPanel", 15f);
            // ResetFlash ("UnitScreenBit", 15f);
            // PlayAtTime(TutorialSelect, 15f);
            // MapSubtitlesAtTime("Press Fire or\n/* Weapon control */", 15f);
            // Flash("Clickable/*_Weapon_control_*/", 15f);
            // Flash ("ClickableFire_()", 15f);
            // PlayAtTime(TutorialTry, 22f);
            // PlayAtTime(TutorialSelect, 26f);
        } else  if (tutorialFire) {
            // RenderComponent(InputField.text);
            // timer += Time.deltaTime;
            // global_timer += Time.deltaTime;
            // PlayAtTime(TutorialGood, 0.5f);
            // MapSubtitlesAtTime("", 0f);
            // PlayAtTime(TutorialCancel, 2f);
            // MapSubtitlesAtTime("Press\n☒ Cancel", 2f);
            // PlayAtTime(TutorialTry, 15f);
            // PlayAtTime(TutorialCancel, 19f);
            // Flash ("OverlayDelete", 2f);
        } else if (tutorialCancel) {
            // timer += Time.deltaTime;
            // global_timer += Time.deltaTime;
            // PlayAtTime(TutorialGetMoving, 0.1f);
            // SpriteFlash ("Thruster", 0.1f);
            // MapSubtitlesAtTime("", 0.1f);
            // SubtitlesAtTime("  Now_it's_time_for_you\n  to_get_this_old_girl_moving!", 0.5f);
            // Flash ("OverlayPanDown", 0.5f);
            // PlayAtTime(TutorialTry, 6f);
            // MapSubtitlesAtTime("Press\n◉ Thruster", 0.5f);
            // PlayAtTime(TutorialGetMoving, 12f);
        } else if (tutorialThrust) {
            // RenderComponent(InputField.text);
            // timer += Time.deltaTime;
            // global_timer += Time.deltaTime;
            // MapSubtitlesAtTime("", 0f);
            // ResetSpriteFlash ("Thruster", 0.1f);
            // ResetFlash ("OverlayPanDown", 0.1f);
            // PlayAtTime(TutorialGood2, 0.5f);
            // Flash("ClickableThrustUp_()", 1.5f);
            // Flash("Clickable/*_Thrust_control_(+)_*/", 1.5f);
            // PlayAtTime(TutorialThrottle, 1.5f);
            // MapSubtitlesAtTime("Press ThrustUp or\n/* Thrust control (+) */", 1.5f);
            // PlayAtTime(TutorialTry, 8f);
            // PlayAtTime(TutorialThrottle, 14f);
        } else if (tutorialFinish) {
            // timer += Time.deltaTime;
            // global_timer += Time.deltaTime;
            // ResetFlash ("OverlayPanDown", 0.1f);
            // MapSubtitlesAtTime("", 0f);
            // PlayAtTime(TutorialBetter, 0.1f);
            // PlayAtTime(TutorialOutro, 2.5f);
            // SubtitlesAtTime("☑You_have_completed_the_tutorial", 3.5f);
            // SubtitlesAtTime("☑You_have_completed_the_tutorial\n\n  I_hope_you_never_have\n  cause_to_use_the_knowledge\n  you_just_acquired!", 7f);
            // SubtitlesAtTime("☑You_have_completed_the_tutorial\n\n  I_hope_you_never_have\n  cause_to_use_the_knowledge\n  you_just_acquired!\n\n  That_is_all_for_today!", 12f);
            // SubtitlesAtTime("☑You_have_completed_the_tutorial\n\n  I_hope_you_never_have\n  cause_to_use_the_knowledge\n  you_just_acquired!\n\n  That_is_all_for_today!\n\n  Dismissed!", 13.5f);
            // SubtitlesAtTime("☑You_have_completed_the_tutorial\n\n  I_hope_you_never_have\n  cause_to_use_the_knowledge\n  you_just_acquired!\n\n  That_is_all_for_today!\n\n  Dismissed!", 15f);
            // Flash ("OverlayOk", 3.5f);
            // MapSubtitlesAtTime("Press\n☑ Ok", 3.5f);
            // if (timer > 16) {
            //     CompleteTutorial();
            // }
        } else if (OverlayInteractor != null && OverlayInteractor.gameObject.activeSelf) {
            if (animation_timer % 1 > 1 - Time.deltaTime) {
                // RenderComponent(InputField.text);
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
