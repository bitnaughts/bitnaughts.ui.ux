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
        this.GetComponent<RectTransform>().localPosition = new Vector2(-25f + (pos - (text.Length-1)/2f) * 25f, -30f + line * -50f);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(text.Length * 25f, 50f);

        this.transform.GetChild(0).GetComponent<Text>().text = initialized_text.Replace("_", " ");
    }
    public void OnClick() {
        if (initialized_text.Contains("<a>") && initialized_text.Contains("</a>")) {
            if (initialized_text.Contains("https://")) { Application.OpenURL(initialized_text.Substring(3, initialized_text.Length - 7)); return; }
            if (initialized_text.Contains("$")) { 
                // Interactor.AppendText("  <a>" + _variable + "</a>\n  <a>" + _method +"</a>\n  <a>" + _flow_control + "</a>\n  <a>help</a>");
                return;
            }
        }
        if (initialized_text == "$") {
            Interactor.SetCommand("$");
            Interactor.AppendText("$ <b>make</b>\n  <b>nano</b>\n  <b>rm</b>\n  <b>git</b>\n  <b>clear</b>\n  <a>about</a>\n  <a>tutorial</a>\n  <a>back</a>");
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
            case "<b>help</b>": 
            case "<a>help</a>": 
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
                    case "rm":
                        Interactor.AppendText("$ " + Interactor.GetCommand() + " <b>help</b> \n  rm deletes files;\n  <a>https://en.wikipedia.org/wiki/Rm_(Unix)</a>\n$");
                    break;
                }
            break;
            case "git": 
            case "<b>git</b>": 
                Interactor.SetCommand("git");
                Interactor.AppendText("$ git <b>status</b>\n      <b>add</b>\n      <b>commit</b>\n      <b>pull</b>\n      <b>push</b>\n      <a>help</a>\n      <a>back</a>");
            break;
            case "<i>status</i>": 
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
            case "<a>about</a>": 
                Interactor.AppendText("$ <b>about</b>\n☄ BitNaughts is an educational\n  programming video-game;\n  <a>https://github.com/bitnaughts/</a>\n\n$");
            break;
            case "<a>tutorial</a>": 
                Interactor.AppendText("$ <b>tutorial</b>\n$ ");
                Interactor.StartTutorial();
        // timer += Time.deltaTime;
            break;
            case "<i>pull</i>": 
                Interactor.SetCommand("pull");
                Interactor.AppendText("$ git pull\n...\n$");
            break;
            case "<a>add</a>": 
                Interactor.SetCommand("add");
                output = "$ git add <a>" + components[0] + "</a>";
                for (int i = 1; i < components.Length; i++) {
                    output += "\n          <a>" + components[i] + "</a>";
                } 
                Interactor.AppendText(output);
            break;
            case "<i>commit</i>": 
                Interactor.SetCommand("commit");
                Interactor.AppendText("$ git commit");
                Interactor.SetInputPlaceholder("+ Commit Msg");
            break;
            case "<i>push</i>": 
                Interactor.SetCommand("push");
                Interactor.AppendText("$ git push\n$");
            break;
            case "rm":
            case "<b>rm</b>":
                Interactor.SetCommand("rm");
                output = "$ rm <b>" + components[0] + "</b>";
                for (int i = 1; i < components.Length; i++) {
                    output += "\n     <b>" + components[i] + "</b>";
                } 
                output += "\n     <a>help</a>\n     <a>back</a>";
                Interactor.AppendText(output);
            break;
            case "nano":
            case "<b>nano</b>":
                Interactor.SetCommand("nano");
                output = "$ nano <b>" + components[0] + "</b>";
                for (int i = 1; i < components.Length; i++) {
                    output += "\n       <b>" + components[i] + "</b>";
                } 
                output += "\n       <a>help</a>\n       <a>back</a>";
                Interactor.AppendText(output);
                Interactor.SetInputPlaceholder("+ File");
            break;
            case "make": 
            case "<b>make</b>": 
                Interactor.SetCommand("make");
                Interactor.AppendText("$ make <b>▥_Processor</b>\n       <b>▩_Bulkhead</b>\n       <b>▣_Gimbal</b>\n       <b>◍_Cannon</b>\n       <b>◌_Sensor</b>\n       <b>◉_Thruster</b>\n       <b>◎_Booster</b>\n       <a>help</a>\n       <a>back</a>");
            break;
            case "Print()": 
                Interactor.PrintMock();
            break;
            case "Fire":
                 
                // Interactor.PrintMock();
            break;
            case "Component": 
                Interactor.RenderText("abstract class Component : Object {\n  virtual Vector pos;\n  virtual Vector siz;\n  virtual double rot;\n}\n\n<a>Exit</a>");
            break;
            case "Object": 
                Interactor.RenderText("abstract class Object {\n  protected Object clone() { ... }\n  protected void finalize() { ... }\n  public class getClass() { ... }\n}\n\n<a>Exit</a>");
            break;
            case "Nozzle": 
                Interactor.RenderText("final class Nozzle : Component {\n  void GoTo (Vector position);\n  void Place (string type);\n  void Resize (Vector size);\n  void Rotate (double rotation);\n}\n\n<a>Exit</a>");
            break;
            case "Heap": 
                Interactor.RenderText("final class Heap : Object {\n\n  /* New allocates objects */\n  void New (Object obj);\n\n  /* Delete deallocates objects */\n  void Delete (Object obj);\n}\n\n<a>Exit</a>");
            break;
            case "Shell": 
                Interactor.RenderText("final class Shell : Component {\n  void OnCollision (Object other) {\n    delete other;\n    delete this;\n  }\n}\n\n<a>Exit</a>");
            break;
            case "Ray": 
                Interactor.RenderText("final class Ray : Object {\n  double length;\n  public Ray (Vector dir) {\n    length = Cast (dir);\n  }\n\n  public double Length() {\n    return length;\n  }\n\n  public double Cast (Vector dir) { ... }\n}\n\n<a>Exit</a>");
            break;
            case "Torpedo":
                Interactor.RenderText("final class Torpedo : Shell {\n  double thr;\n\n/*_Torpedo_constructor_*/\n  public Torpedo(double throttle) {\n    thr = throttle;\n  }\n  OnCollision (Object other) {\n    delete other;\n    delete this;\n  }\n}\n\n<a>Exit</a>");
            break;
            case "<b>clear</b>": 
                Interactor.ClearHistory();
            break;
            case "<a>back</a>": 
                Interactor.RenderText("$");
                Interactor.SetCommand("$");
                Interactor.AppendText("$");// <b>make</b>\n  <b>nano</b>\n  <b>rm</b>\n  <b>git</b>\n  <b>clear</b>\n  <a>help</a>");
            break;
            case "<a>Exit</a>": 
                Interactor.ClearText();
                OverlayInteractor.gameObject.SetActive(false);
            break;


        }
    }

}
