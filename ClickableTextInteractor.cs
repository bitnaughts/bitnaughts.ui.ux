using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickableTextInteractor : MonoBehaviour
{   
    Interactor Interactor;
    OverlayInteractor OverlayInteractor;
    public string initialized_text;
    private string _method = "⊡_Method",  _variable = "◬_Variable", _arithmetic = "∓_Arithmetic", _flow_control = "☈_Flow_Control", _boolean = "-_Boolean", _trigonometry = "∡_Trigonometry"; //⋚
    public void Initialize(Interactor interactor, OverlayInteractor overlayInteractor, string text, int line, int pos) {
        this.gameObject.SetActive(true);
        Interactor = interactor;
        OverlayInteractor = overlayInteractor;
        initialized_text = text;
        if (text.StartsWith("<") && text.EndsWith(">")) text = text.Substring(3, text.Length - 7);
        // print (text + " " + line + " " + pos);

    //    if (text.Contains("━") || text.Contains("║"))
    //    {
    //        button.GetComponent<Button>().interactable = false;
    //    }
    //    else
    //    {
    //        button.GetComponent<Button>().interactable = true;
    //        button.GetComponent<Button>().onClick.AddListener ( delegate { Click (button.name); });
    //    }



        this.GetComponent<RectTransform>().localPosition = new Vector2(-((Interactor.fontSize - 5) / 1.5f) + (pos - (text.Length - 1)/2f) * Interactor.fontSize / 2, line * -Interactor.fontSize) + new Vector2(25f, -100f);// + (pos - (text.Length-1)/2f) * 50f, -125f + line * -100f);//new Vector2(-50f + (pos - (text.Length-1)/2f) * 37.5f, -83f + line * -75f);
        var text_length = text.Length * 50f;
        if (text.StartsWith("<color=#")) text_length -= 23 * 50f;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(text.Length * Interactor.fontSize / 2, Interactor.fontSize);//new Vector2(text_length, 100f);//new Vector2(text.Length * 37.5f, 75f);
        this.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        this.transform.Find("Text").GetComponent<Text>().fontSize = (int)Interactor.fontSize;

        if (initialized_text.Contains("/*") && initialized_text.Contains("*/")) {
            initialized_text = Formatter.comment(initialized_text);
        }
        if (initialized_text.StartsWith("\'") && initialized_text.EndsWith("\'")) {
            initialized_text = Formatter.stringify(initialized_text);
        }
        if (initialized_text.StartsWith("\"") && initialized_text.EndsWith("\"")) {
            initialized_text = Formatter.stringify(initialized_text);
        }
        switch (initialized_text) {
            case "final":
            case "class":
            case "static":
            case "abstract":
            case "virtual":
            case "new":
            case "delete":
            case "void":
            case "while":
            case "public":
            case "protected":
            case "private":
            case "for":
            case "if":
            case "var":
            case "switch":
            case "case":
            case "return":
                initialized_text = Formatter.keyword(initialized_text);
                break;
            case "string":
            case "int":
            case "double":
            case "char":
            case "bool":
            case "Component":
            case "Booster":
            case "Thruster":
            case "Bulkhead":
            case "Processor":
            case "Sensor":
            case "Cannon":
            case "Gimbal":
            case "Object":
            case "Vector":
            case "Shell":
            case "Torpedo":
            case "Ray":
            case "Heap":
            case "LatLong":
            case "SF":
            case "LA":
            case "NY":
            case "DC":
            // case "Processor":
            // case "Cannon":
            // case "Gimbal":
            // case "Bulkhead":
                initialized_text = Formatter.ColorObject(initialized_text);
                break;
            // case "Main":
            // case "Rotate":
            // case "Fire":
            //     initialized_text = Formatter.keyword(initialized_text);
            //     break;
        }
        if (GetComponent<InputField>() != null) {
            this.transform.GetComponent<InputField>().text = "";
            this.transform.GetComponent<InputField>().placeholder.GetComponent<Text>().text = initialized_text.Replace("_", " ");
        }
        else {
            this.transform.GetChild(0).GetComponent<Text>().text = initialized_text.Replace("_", " ");
        }
        this.name = "Clickable" + text;
    }
    public void OnClick() {
        if (initialized_text.Contains("<a>") && initialized_text.Contains("</a>")) {
            if (initialized_text.Contains("https://")) { Application.OpenURL(initialized_text.Substring(3, initialized_text.Length - 7)); return; }
            if (initialized_text.Contains("$")) { 
                // Interactor.AppendText("  <a>" + _variable + "</a>\n  <a>" + _method +"</a>\n  <a>" + _flow_control + "</a>\n  <a>help</a>");
                return;
            }
        }
        else if (initialized_text.StartsWith("<") && initialized_text.EndsWith(">")) {
            initialized_text = initialized_text.Split('>')[1].Split('<')[0];
        } 
        if (initialized_text == "$") {
            // If code editor:
            // print (Interactor.component_name);
            if (Interactor.component_name != "") 
            {
               // OverlayInteractor.gameObject.SetActive(true);
                //OverlayInteractor.OnCodeEditor();
           // }
          //  else {
                // else, TUI menu:
                Interactor.AppendText("$ <b>...____________________</b>\n  <b>about</b>\n  <b>copilot</b>\n  <b>campaign</b>\n  <b>settings</b>\n  <b>clear</b>\n  <b>back</b>\n  <b>restart</b>\n");
            }
        }
        foreach (var component in Interactor.GetComponents()) {
            if (initialized_text.Contains(component)) {
                switch (Interactor.GetCommand()) {
                    case "rm":
                        Interactor.AppendText("$ rm " + component + "\n$");
                        OverlayInteractor.DeleteComponent(component);
                        break;
                    // case "add":
                    //     Interactor.AppendText("$ git add " + component + "\n$");
                    //     // OverlayInteractor.CreateComponent(component);
                    //     break;
                    case "nano":
                        Interactor.AppendText("$ nano " + component + "\n$");
                        // Interactor.RenderText("class " + component + " : Component {\n void Start() {\n }\n}\n\n<a>Exit</a>");
                        for (int i = 0; i < OverlayInteractor.OverlayDropdown.options.Count; i++) {
                            if (OverlayInteractor.OverlayDropdown.options[i].text == component.Split('_')[1]) OverlayInteractor.OverlayDropdown.value = i; 
                        }
                        GameObject.Find("MapScreenPanOverlay").SetActive(false);
                        // OverlayInteractor.gameObject.SetActive(true);
                        OverlayInteractor.OnDropdownChange(0); 
                        break;
                }
                break;
            }
        }
        string output = "";
        var components = Interactor.GetComponents();
        switch (initialized_text) {
            case "SF":
            case "(37º_46'_N,_122º_25'_W);":
                Interactor.TargetLocation = new Mapbox.Utils.Vector2d(37.77903f, -122.41991f);
                Interactor.Stage = "MapZoom";
                Interactor.RenderText("$ new LatLong (37º_46'_N,_122º_25'_W);");
                break;
            case "LA":
            case "(34º_05'_N,_118º_18'_W);":
                Interactor.TargetLocation = new Mapbox.Utils.Vector2d(34.05369f,-118.24277f);
                Interactor.Stage = "MapZoom";
                Interactor.RenderText("$ new LatLong (34º_05'_N,_118º_18'_W);");
                break;
            case "NY":
            case "(40º_42'_N,_74º_00'_W);":
                Interactor.TargetLocation = new Mapbox.Utils.Vector2d(40.71275f, -74.00599f);
                Interactor.Stage = "MapZoom";
                Interactor.RenderText("$ new LatLong (40º_42'_N,_74º_00'_W);");
                break;
            case "DC":
            case "(38º_53'_N,_77º_02'_W);":
                Interactor.TargetLocation = new Mapbox.Utils.Vector2d(38.89500f, -77.03667f);
                Interactor.Stage = "MapZoom";
                Interactor.RenderText("$ new LatLong (38º_53'_N,_77º_02'_W);");
                break;
            case "help": 
                switch (Interactor.GetCommand()) {
                    // case "git":
                    //     Interactor.AppendText("$ " + Interactor.GetCommand() + " <b>help</b> \n↯ git versions files;\n  <a>https://git-scm.com/</a>\n$");
                    // break;
                    case "nano":
                        Interactor.AppendText("$ " + Interactor.GetCommand() + " <b>help</b> \nη nano edits files;\n  <a>https://nano-editor.org/</a>\n$");
                    break;
                    case "make":
                        Interactor.AppendText("$ " + Interactor.GetCommand() + " <b>help</b> \n  make builds files;\n  <a>https://en.wikipedia.org/wiki/Make_(software)</a>\n$");
                    break;
                    case "cp":
                        Interactor.AppendText("$ " + Interactor.GetCommand() + " <b>help</b> \n  cp copies files;\n  <a>https://en.wikipedia.org/wiki/Cp_(Unix)</a>\n$");
                    break;
                    case "rm":
                        Interactor.AppendText("$ " + Interactor.GetCommand() + " <b>help</b> \n  rm deletes files;\n  <a>https://en.wikipedia.org/wiki/Rm_(Unix)</a>\n$");
                    break;
                }
            break;
            // case "git": 
            //     Interactor.SetCommand("git");
            //     Interactor.AppendText("$ git <b>status</b>\n      <b>add</b>\n      <b>commit</b>\n      <b>pull</b>\n      <b>push</b>\n      <b>help</b>\n      <b>back</b>");
            // break;
            case "status": 
                Interactor.SetCommand("clone");
                Interactor.AppendText("$ git clone <i>bitnaughts.db</i>\n            <i>bitnaughts.components</i>\n            <i>bitnaughts.ui.ux</i>\n            <i>bitnaughts.assets</i>\n            <i>bitnaughts.github.io</i>\n            <i>bitnaughts.interpreter</i>");
                break;
            case "Edit_()":
            case "/*_Modify_Ship_*/":
                Interactor.PrinterEditFx();
                break;
            case "Print_()":
            case "/*_Construct_Ship_*/":
                Interactor.PrinterPrintFx();
                break;
            // case "<i>bitnaughts.db</i>": 
            //     Interactor.SetCommand("bitnaughts.db");
            //     Interactor.AppendText("$ git clone bitnaughts.db\n...\n$");
            // break;
            // case "<i>bitnaughts.components</i>": 
            //     Interactor.SetCommand("bitnaughts.components");
            //     Interactor.AppendText("$ git clone bitnaughts.components\n...\n$");
            // break;
            // case "<i>bitnaughts.ui.ux</i>": 
            //     Interactor.SetCommand("bitnaughts.ui.ux");
            //     Interactor.AppendText("$ git clone bitnaughts.ui.ux\n...\n$");
            // break;
            // case "<i>bitnaughts.assets</i>": 
            //     Interactor.SetCommand("bitnaughts.assets");
            //     Interactor.AppendText("$ git clone bitnaughts.assets\n...\n$");
            // break;
            // case "<i>bitnaughts.github.io</i>": 
            //     Interactor.SetCommand("bitnaughts.github.io");
            //     Interactor.AppendText("$ git clone bitnaughts.github.io\n...\n$");
            // break;
            // case "<i>bitnaughts.interpreter</i>": 
            //     Interactor.SetCommand("bitnaughts.interpreter");
            //     Interactor.AppendText("$ git clone bitnaughts.interpreter\n...\n$");
            // break;
            case "gui":
                Interactor.OnMapView();
                break;
            case "start":
                Interactor.OnMapView();
                Interactor.MapInteractor("0");
                break;
            case "campaign":
                // Interactor.ResetVideo();
                Interactor.PlayVideo("WarOfTheWorldsCredit");
                // Interactor.PlayAudio(Interactor.LookupNarration("WarOfTheWorldsStinger"));
                // Interactor.OnMapView();
                // Interactor.MapInteractor((Interactor.CampaignIndex + 1).ToString());
                break;
            case "copilot":
                // Interactor.ResetVideo();
                // Interactor.PlayVideo("WarOfTheWorldsStinger");
                Interactor.PlayVideo("WarOfTheWorldsBitBot");//Interactor.LookupNarration());
                // Interactor.OnMapView();
                // Interactor.MapInteractor((Interactor.CampaignIndex + 1).ToString());
                break;
            case "multiplay":
                Interactor.OnMapView();
                Interactor.MapInteractor("6"); //7,8
                break;
            case "about": 
                Interactor.Sound("About");
                Interactor.AppendText("$ <b>about</b>\n\n☄ BitNaughts is an educational\n  programming video-game;\n  It's code gamified!\n  <a>https://bitnaughts.io/</a>\n  <a>https://github.com/bitnaughts/</a>\n  <a>https://twitter.com/BitNaughts</a>\n  <a>https://www.youtube.com/@bitnaughts6237</a>\n  <a>https://www.twitch.tv/bitnaughts</a>\n\n$");
                // Interactor.PlayTheme();
                break;
            case "settings": 
                Interactor.Sound("Warning");
                Interactor.AppendText("$ <b>settings</b>\n\n  Font_Size:_75 (+) (-)\n  Audio:_65% (+) (-)\n\n$");
                // Interactor.PlayTheme();
                break;
            // case "campaign":
                // Interactor.AppendText("$ <b>about</b>\n☄ BitNaughts is an educational\n  programming video-game;\n  It's code gamified!\n$");
                // Interactor.StoryMode();
                // GameObject.Find("Panel").SetActive(true);
                // break;
                
            // case "campaign":
            //     Interactor.AppendText("$ campaign <b>Radio_Days</b>\n           <b>Newton's_Laws</b>\n           <b>The_Atom</b>\n           <b>Doppler_Effect</b>\n           <b>The_Electron</b>\n           <b>Doppler_Shift</b>\n           <b>Modern_War</b>\n           <b>Plank's_Law</b>\n           <b>Television</b>\n           <b>Hawking_Radiation</b>\n           <b>Videotape_Records</b>\n           <b>Moravec's_Paradox</b>\n           <b>Electronic_Music</b>\n           <b>De_Broglie_Theory</b>\n           <b>Radio_Isotopes</b>\n           <b>Fermi_Paradox</b>\n           <b>Hardness_Test</b>\n           <b>Pascal's_Wager</b>\n           <b>Conclusion</b>\n           <b>back</b>");
            //     break;
            case "Radio_Days":
                Interactor.StoryMode(0);
                break;
            case "Newton's_Laws":
                Interactor.StoryMode(1);
                break;
            case "The_Atom":
                Interactor.StoryMode(2);
                break;
            case "Doppler_Effect":
                Interactor.StoryMode(3);
                break;
            case "The_Electron":
                Interactor.StoryMode(4);
                break;
            case "Doppler_Shift":
                Interactor.StoryMode(5);
                break;
            case "Modern_War":
                Interactor.StoryMode(6);
                break;
            case "Plank's_Law":
                Interactor.StoryMode(7);
                break;
            case "Television":
                Interactor.StoryMode(8);
                break;
            case "Hawking_Radiation":
                Interactor.StoryMode(9);
                break;
            case "Videotape_Records":
                Interactor.StoryMode(10);
                break;
            case "Moravec's_Paradox":
                Interactor.StoryMode(11);
                break;
            case "Electronic_Music":
                Interactor.StoryMode(12);
                break;
            case "De_Broglie_Theory":
                Interactor.StoryMode(13);
                break;
            case "Radio_Isotopes":
                Interactor.StoryMode(14);
                break;
            case "Fermi_Paradox":
                Interactor.StoryMode(15);
                break;
            case "Hardness_Test":
                Interactor.StoryMode(16);
                break;
            case "Pascal's_Wager":
                Interactor.StoryMode(17);
                break;
            case "Conclusion":
                Interactor.StoryMode(18);
                break;
            case "restart":
            // case "⍰⍰_Help":
                
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                // Interactor.Sound("Warning");
                // Interactor.AppendText("$ tutorial <b>Binary</b>\n           <b>Morse_Code</b>\n           <b>Decimals</b>\n           <b>Arithmetic</b>\n           <b>Ship_Control</b>\n           <b>back</b>");
                // Interactor.StartTutorial();
                break;
            break;
            case "pull<": 
                Interactor.SetCommand("pull");
                Interactor.AppendText("$ git pull\n...\n$");
                break;
            case "add": 
                Interactor.SetCommand("add");
                output = "$ git add <a>" + components[0] + "</a>";
                for (int i = 1; i < components.Length; i++) {
                    output += "\n          <a>" + components[i] + "</a>";
                } 
                Interactor.AppendText(output);
                break;
            case "commit": 
                Interactor.SetCommand("commit");
                Interactor.AppendText("$ git commit");
                Interactor.SetInputPlaceholder("+ Commit Msg");
                break;
            case "push": 
                Interactor.SetCommand("push");
                Interactor.AppendText("$ git push\n$");
                break;
            case "cp":
                Interactor.SetCommand("cp");
                output = "$ cp <b>" + components[0] + "</b>";
                for (int i = 1; i < components.Length; i++) {
                    output += "\n     <b>" + components[i] + "</b>";
                } 
                output += "\n     <b>help</b>\n     <b>back</b>";
                Interactor.AppendText(output);
                break;
            case "rm":
                Interactor.SetCommand("rm");
                output = "$ rm <b>" + components[0] + "</b>";
                for (int i = 1; i < components.Length; i++) {
                    output += "\n     <b>" + components[i] + "</b>";
                } 
                output += "\n     <b>help</b>\n     <b>back</b>";
                Interactor.AppendText(output);
                break;
            case "nano":
                Interactor.SetCommand("nano");
                output = "$ nano <b>" + components[0] + "</b>";
                for (int i = 1; i < components.Length; i++) {
                    output += "\n       <b>" + components[i] + "</b>";
                } 
                output += "\n       <b>help</b>\n       <b>back</b>";
                Interactor.AppendText(output);
                break;
            case "make": 
                Interactor.SetCommand("make");
                Interactor.AppendText("$ make <b>▥_Processor</b>\n       <b>▩_Bulkhead</b>\n       <b>▣_Gimbal</b>\n       <b>◍_Cannon</b>\n       <b>◌_Sensor</b>\n       <b>◉_Thruster</b>\n       <b>◎_Booster</b>\n       <b>help</b>\n       <b>back</b>");
                break;
            case "▥_Processor": 
                Interactor.AppendText("$ make <b>▥_Processor</b>\n$ ");
                Interactor.SetInputPlaceholder("Processor");
                break;
            case "Main":
            case "Main_()":
            case "/*_Main_method_*/":
            case "/*_Entry_point_*/":
                Interactor.Sound("Processor");
                Interactor.ResetProcessor();
                break;
            case "▩_Bulkhead": 
                Interactor.AppendText("$ make <b>▩_Bulkhead</b>\n$ ");
                Interactor.SetInputPlaceholder("Bulkhead");
                break;
            case "▣_Gimbal": 
                Interactor.AppendText("$ make <b>▣_Gimbal</b>\n$ ");
                Interactor.SetInputPlaceholder("Gimbal");
                break;
            case "/*_Gimbal_Control_*/":
            case "Rotate":
                Interactor.Sound("Gimbal");
                Interactor.Action(Interactor.GetInput(), 15);
                break;
            case "◍_Cannon": 
                Interactor.AppendText("$ make <b>◍_Cannon</b>\n$ ");
                Interactor.SetInputPlaceholder("Cannon");
                break;
            case "Fire_()":
            case "/*_Weapon_control_*/":
                // Interactor.FireTutorial();
                Interactor.Action(Interactor.GetInput(), -1);
                // Interactor.Sound("Cannon");
                break;
            case "◌_Sensor": 
                Interactor.AppendText("$ make <b>◌_Sensor</b>\n$ ");
                Interactor.SetInputPlaceholder("Sensor");
                break;
            case "Scan_()":
            case "/*_Cast_Ray_*/":
                Interactor.Sound("Radar");
                Interactor.Action(Interactor.GetInput(), 0);
                break;
            case "◉_Thruster": 
                Interactor.AppendText("$ make <b>◉_Thruster</b>\n$ ");
                Interactor.SetInputPlaceholder("Thruster");
                break;
            case "Throttle":
            case "Throttle_()":
            case "/*_Throttle_Control_*/":
                // Interactor.FinishTutorial();
                Interactor.Action(Interactor.GetInput(), 25);
                Interactor.Sound("Thruster");
                break;
            case "ThrustDown_()":
            case "/*_Thrust_control_(-)_*/":
                Interactor.Action(Interactor.GetInput(), -25);
                Interactor.Sound("Gimbal");
                break;
            case "◎_Booster": 
                Interactor.AppendText("$ make <b>◎_Booster</b>\n$ ");
                Interactor.SetInputPlaceholder("Booster");
                break;
            case "Launch_()":
            case "/*_Torpedo_Control_*/":
                Interactor.Action(Interactor.GetInput(), -1);
                break;
            case "/*_Boost_Control_*/":
            case "Boost_()":
            case "Boost":
                Interactor.Action(Interactor.GetInput(), 25);
                Interactor.Sound("Booster");
                // Interactor.Action("Thruster", 100);
                break;
            case "Component": 
                Interactor.RenderText("abstract class Component : Object {\n virtual Vector pos;\n virtual Vector siz;\n virtual double rot;\n}\n<b>Exit</b>");
                break;
            case "Object": 
                Interactor.RenderText("abstract class Object {\n protected Object clone () { ... }\n protected void finalize () { ... }\n public class getClass () { ... }\n}\n<b>Exit</b>");
                break;
            case "Nozzle": 
                Interactor.RenderText("final class Nozzle : Component {\n void GoTo (Vector position)\n void Place (string type)\n void Resize (Vector size)\n void Rotate (double rotation)\n}\n<b>Exit</b>");
                break;
            case "Heap": 
                Interactor.RenderText("final class Heap : Object {\n /*_New_allocates_objects_*/\n void New (Object obj)\n /*_Delete_deallocates_objects_*/\n void Delete (Object obj);\n}\n<b>Exit</b>");
                break;
            case "Shell": 
                Interactor.RenderText("final class Shell : Component {\n void OnCollision (Object other) {\n  delete other;\n  delete this;\n }\n}\n\n<b>Exit</b>");
                break;
            case "Ray": 
                Interactor.RenderText("final class Ray : Object {\n double length;\n public Ray (Vector dir) {\n  length = Cast (dir);\n }\n\n  public double Length() {\n    return length;\n  }\n\n  public double Cast (Vector dir) { ... }\n}\n<b>Exit</b>");
                break;
            case "Torpedo":
                Interactor.RenderText("final class Torpedo : Shell {\n double thr;\n /*_Torpedo_constructor_*/\n public Torpedo(double throttle) {\n  thr = throttle;\n }\n OnCollision (Object other) {\n  delete other;\n  delete this;\n }\n}\n<b>Exit</b>");
                break;
            case "clear": 
                Interactor.ClearHistory();
                break;
            case "back": 
                Interactor.RenderText("$");
                Interactor.SetCommand("$");
                Interactor.AppendText("$");// <b>make</b>\n  <b>nano</b>\n  <b>rm</b>\n  <b>git</b>\n  <b>clear</b>\n  <a>help</a>");
                break;
            //☑_Ok\n☒_Cancel\n☒_Delete\n⍰⍰_Help
            case "☑_Ok":
                Interactor.Sound("Click");
                Interactor.ClearText();
                OverlayInteractor.gameObject.SetActive(false);
                break;
            case "☒_Cancel":
                Interactor.Sound("Back");
                Interactor.ClearText();
                OverlayInteractor.gameObject.SetActive(false);
                break;
            case "Exit": 
                Interactor.ClearText();
                OverlayInteractor.gameObject.SetActive(false);
                break;
            case "☒_Delete": 
                Interactor.Sound("Back");
                OverlayInteractor.gameObject.SetActive(false);
                Destroy(GameObject.Find(Interactor.component_name));
                Interactor.ClearText();
                // Interactor.SetCommand("rm");
                // Interactor.AppendText("$ rm <b>" + component + "</b>");
                break;
        }
    }

}
