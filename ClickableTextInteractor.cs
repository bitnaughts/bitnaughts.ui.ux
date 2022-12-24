using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

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
        this.GetComponent<RectTransform>().localPosition = new Vector2(-40f + (pos - (text.Length-1)/2f) * 30f, -53f + line * -60f);//new Vector2(-50f + (pos - (text.Length-1)/2f) * 37.5f, -83f + line * -75f);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(text.Length * 30f, 60f);//new Vector2(text.Length * 37.5f, 75f);
        this.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

        if (initialized_text.Contains("/*") && initialized_text.Contains("*/")) {
            initialized_text = Formatter.comment(initialized_text);
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
            case "int":
            case "double":
            case "public":
            case "protected":
            case "private":
            case "for":
            case "if":
            case "return":
                initialized_text = Formatter.keyword(initialized_text);
                break;
            case "Component":
            case "Object":
            case "Vector":
            case "Shell":
            case "Torpedo":
            case "Ray":
            case "Heap":
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

        this.transform.GetChild(0).GetComponent<Text>().text = initialized_text.Replace("_", " ");
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
            Interactor.SetCommand("$");
            Interactor.AppendText("$ <b>back</b>\n  <b>campaign</b>\n  <b>clear</b>\n  <b>cp</b>\n  <b>git</b>\n  <b>make</b>\n  <b>nano</b>\n  <b>rm</b>");
        }
        foreach (var component in Interactor.GetComponents()) {
            if (initialized_text.Contains(component)) {
                switch (Interactor.GetCommand()) {
                    case "rm":
                        Interactor.AppendText("$ rm " + component + "\n$");
                        OverlayInteractor.DeleteComponent(component);
                        break;
                    case "add":
                        Interactor.AppendText("$ git add " + component + "\n$");
                        // OverlayInteractor.CreateComponent(component);
                        break;
                    case "nano":
                        Interactor.AppendText("$ nano " + component + "\n$");
                        // Interactor.RenderText("class " + component + " : Component {\n void Start() {\n }\n}\n\n<a>Exit</a>");
                        for (int i = 0; i < OverlayInteractor.OverlayDropdown.options.Count; i++) {
                            if (OverlayInteractor.OverlayDropdown.options[i].text == component.Split('_')[1]) OverlayInteractor.OverlayDropdown.value = i; 
                        }
                        GameObject.Find("MapScreenPanOverlay").SetActive(false);
                        OverlayInteractor.gameObject.SetActive(true);
                        OverlayInteractor.OnDropdownChange(); 
                        break;
                }
                break;
            }
        }
        string output = "";
        var components = Interactor.GetComponents();
        switch (initialized_text) {
            case "help": 
                switch (Interactor.GetCommand()) {
                    case "git":
                        Interactor.AppendText("$ " + Interactor.GetCommand() + " <b>help</b> \n↯ git versions files;\n  <a>https://git-scm.com/</a>\n$");
                    break;
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
            case "git": 
                Interactor.SetCommand("git");
                Interactor.AppendText("$ git <b>status</b>\n      <b>add</b>\n      <b>commit</b>\n      <b>pull</b>\n      <b>push</b>\n      <b>help</b>\n      <b>back</b>");
            break;
            case "status": 
                Interactor.SetCommand("clone");
                Interactor.AppendText("$ git clone <i>bitnaughts.db</i>\n            <i>bitnaughts.components</i>\n            <i>bitnaughts.ui.ux</i>\n            <i>bitnaughts.assets</i>\n            <i>bitnaughts.github.io</i>\n            <i>bitnaughts.interpreter</i>");
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
            case "about": 
                Interactor.Sound("Warning");
                Interactor.AppendText("$ <b>about</b>");
                Interactor.PlayTheme();
                break;
            case "campaign":
                Interactor.StoryMode();
                GameObject.Find("Panel").SetActive(true);
                break;
            case "tutorial":
            case "⍰⍰_Help":
                Interactor.Sound("Warning");
                Interactor.AppendText("$ <b>tutorial</b>");
                Interactor.StartTutorial();
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
            case "Main_()":
            case "/*_Main_method_*/":
                Interactor.Sound("Processor");
                break;
            case "▩_Bulkhead": 
                Interactor.AppendText("$ make <b>▩_Bulkhead</b>\n$ ");
                Interactor.SetInputPlaceholder("Bulkhead");
                break;
            case "▣_Gimbal": 
                Interactor.AppendText("$ make <b>▣_Gimbal</b>\n$ ");
                Interactor.SetInputPlaceholder("Gimbal");
                break;
            case "RotateCW":
            case "/*_Rotates_units_(CW)_*/":
                Interactor.Sound("Gimbal");
                Interactor.Action(Interactor.GetInput(), -15);
                break;
            case "RotateCCW":
            case "/*_Rotates_units_(CCW)_*/":
                Interactor.Sound("Gimbal");
                Interactor.Action(Interactor.GetInput(), 15);
                break;
            case "/*_Rotation_control_*/":
            case "Rotate":
                Interactor.Sound("Gimbal");
                Interactor.Action(Interactor.GetInput(), 1);
                break;
            case "◍_Cannon": 
                Interactor.AppendText("$ make <b>◍_Cannon</b>\n$ ");
                Interactor.SetInputPlaceholder("Cannon");
                break;
            case "Fire_()":
            case "/*_Weapon_control_*/":
                Interactor.FireTutorial();
                Interactor.Action(Interactor.GetInput(), -1);
                // Interactor.Sound("Cannon");
                break;
            case "◌_Sensor": 
                Interactor.AppendText("$ make <b>◌_Sensor</b>\n$ ");
                Interactor.SetInputPlaceholder("Sensor");
                break;
            case "Scan_()":
            case "/*_Ray_caster_*/":
                Interactor.Sound("Radar");
                break;
            case "◉_Thruster": 
                Interactor.AppendText("$ make <b>◉_Thruster</b>\n$ ");
                Interactor.SetInputPlaceholder("Thruster");
                break;
            case "ThrustUp_()":
            case "/*_Thrust_control_(+)_*/":
                Interactor.FinishTutorial();
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
            case "/*_Torpedo_control_*/":
                Interactor.Action(Interactor.GetInput(), -1);
                break;
            case "/*_Throttle_control_*/":
            case "Boost_()":
                Interactor.Action(Interactor.GetInput(), 25);
                Interactor.Sound("Booster");
                // Interactor.Action("Thruster", 100);
                break;
            case "Component": 
                Interactor.RenderText("abstract class Component : Object {\n  virtual Vector pos;\n  virtual Vector siz;\n  virtual double rot;\n}\n\n<b>Exit</b>");
                break;
            case "Object": 
                Interactor.RenderText("abstract class Object {\n  protected Object clone () { ... }\n  protected void finalize () { ... }\n  public class getClass () { ... }\n}\n\n<b>Exit</b>");
                break;
            case "Nozzle": 
                Interactor.RenderText("final class Nozzle : Component {\n  void GoTo (Vector position);\n  void Place (string type);\n  void Resize (Vector size);\n  void Rotate (double rotation);\n}\n\n<b>Exit</b>");
                break;
            case "Heap": 
                Interactor.RenderText("final class Heap : Object {\n\n  /*_New_allocates_objects */\n  void New (Object obj);\n\n  /*_Delete_deallocates_objects */\n  void Delete (Object obj);\n}\n\n<b>Exit</b>");
                break;
            case "Shell": 
                Interactor.RenderText("final class Shell : Component {\n  void OnCollision (Object other) {\n    delete other;\n    delete this;\n  }\n}\n\n<b>Exit</b>");
                break;
            case "Ray": 
                Interactor.RenderText("final class Ray : Object {\n  double length;\n  public Ray (Vector dir) {\n    length = Cast (dir);\n  }\n\n  public double Length() {\n    return length;\n  }\n\n  public double Cast (Vector dir) { ... }\n}\n\n<b>Exit</b>");
                break;
            case "Torpedo":
                Interactor.RenderText("final class Torpedo : Shell {\n  double thr;\n\n/*_Torpedo_constructor_*/\n  public Torpedo(double throttle) {\n    thr = throttle;\n  }\n  OnCollision (Object other) {\n    delete other;\n    delete this;\n  }\n}\n\n<b>Exit</b>");
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
