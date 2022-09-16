using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverlayInteractor : MonoBehaviour
{
    bool populated = false;
    public Vector2 last_position, last_size;
    public Dropdown OverlayDropdown;
    public StructureController Ship;    
    public Interactor Interactor;

    public GameObject OverlayOk, OverlayDelete, 
    OverlayMove, OverlayMoveUp, OverlayMoveLeft, OverlayMoveRight, OverlayMoveDown, OverlayMoveRotateCW, OverlayMoveRotateCCW, 
    OverlayResize, OverlayResizeExpandUp, OverlayResizeShrinkUp, OverlayResizeExpandLeft, OverlayResizeShrinkLeft, OverlayResizeExpandRight, OverlayResizeShrinkRight, OverlayResizeExpandDown, OverlayResizeShrinkDown;

    // public GameObject ClickableText;

    void Start()
    {
        last_position = new Vector2 (999,999);
        last_size = new Vector2 (999,999);
        Interactor = GameObject.Find("Content").GetComponent<Interactor>();
    }



    void Update()
    {
        if (!populated) {
            UpdateOptions();
            populated = true;
        }
    }
    public void UpdateOptions() {
        OverlayDropdown.options = new List<Dropdown.OptionData>();
        foreach (var key in Ship.components.Keys) {
            OverlayDropdown.options.Add(new Dropdown.OptionData(key));
        }
        this.gameObject.SetActive(false);
    }
    public void Resize() {
        string option = OverlayDropdown.options[OverlayDropdown.value].text;
        Vector2 component_position = Ship.GetPosition(option);
        Camera.main.transform.position = new Vector3(component_position.x, component_position.y, -200f);
        Vector3 component_screen_tr_position = Camera.main.WorldToScreenPoint(component_position + Ship.GetSize(option) / 2);
        Vector3 component_screen_bl_position = Camera.main.WorldToScreenPoint(component_position - Ship.GetSize(option) / 2);
        this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(component_screen_tr_position.x - component_screen_bl_position.x, component_screen_tr_position.y - component_screen_bl_position.y); 
        print (Ship.GetRotation(option)); if (Ship.GetRotation(option) != (int)Ship.GetRotation(option)) {
            this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(this.transform.GetComponent<RectTransform>().sizeDelta.y, this.transform.GetComponent<RectTransform>().sizeDelta.x);
        }
        this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Clamp(100+this.transform.GetComponent<RectTransform>().sizeDelta.x, 180f, Screen.width / 2), Mathf.Clamp(100+this.transform.GetComponent<RectTransform>().sizeDelta.y, 180f, Screen.height));
        var rectTransform = OverlayDropdown.gameObject.transform.GetChild(2).gameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2 (rectTransform.sizeDelta.x, this.transform.GetComponent<RectTransform>().sizeDelta.y - 60f);
    }
    public void OnDropdownChange() {
        // print (OverlayDropdown.options[OverlayDropdown.value].text);
        last_position = new Vector2 (999,999);
        Resize();

        OverlayOk.gameObject.SetActive(true);
        OverlayDelete.gameObject.SetActive(true);
        OverlayMove.gameObject.SetActive(true);
        OverlayResize.gameObject.SetActive(true);
        
        OverlayMoveUp.gameObject.SetActive(false);
        OverlayMoveLeft.gameObject.SetActive(false);
        OverlayMoveRight.gameObject.SetActive(false);
        OverlayMoveDown.gameObject.SetActive(false);
        OverlayMoveRotateCW.gameObject.SetActive(false);
        OverlayMoveRotateCCW.gameObject.SetActive(false);

        OverlayResizeExpandUp.gameObject.SetActive(false);
        OverlayResizeShrinkUp.gameObject.SetActive(false);
        OverlayResizeExpandLeft.gameObject.SetActive(false);
        OverlayResizeShrinkLeft.gameObject.SetActive(false);
        OverlayResizeExpandRight.gameObject.SetActive(false);
        OverlayResizeShrinkRight.gameObject.SetActive(false);
        OverlayResizeExpandDown.gameObject.SetActive(false);
        OverlayResizeShrinkDown.gameObject.SetActive(false);
        
        // Interactor.AppendText("$ nano " + OverlayDropdown.options[OverlayDropdown.value].text + "\n$");
        // Interactor.RenderText("class " + OverlayDropdown.options[OverlayDropdown.value].text + " : Component {\n void Start() {\n }\n}\n\n<i>Exit</i>");
        Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
        // Interactor.RenderText("public class " + OverlayDropdown.options[OverlayDropdown.value].text + " : Component {\n public void Start() {\n }\n}");
    }
    
    public void OnSubmit() {
        if (last_position.x == 999) {
            Interactor.PlayClick2();
            this.gameObject.SetActive(false);
            Interactor.ClearText();
            Interactor.CompleteTutorial();
        }
        else {
            Interactor.PlayClick2();
            Interactor.ClearText();
            OnDropdownChange();

        }
    }
    public void OnExit() {
        Interactor.PlayClick();
        Application.Quit();
    }
    public void OnHelp() {
        Interactor.PlayClick2();
        Interactor.StartTutorial();
    }
    public void OnReset() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnDelete() {
        if (last_position.x == 999) {
            Interactor.PlayClick();
            this.gameObject.SetActive(false);
            Interactor.CancelTutorial();
            Interactor.ClearText();
            // DeleteComponent(OverlayDropdown.options[OverlayDropdown.value].text);
        }
        else { 
            Interactor.PlayClick();
            Ship.SetPosition(OverlayDropdown.options[OverlayDropdown.value].text, last_position);
            if (last_size.x != 999) {
                Ship.SetSize(OverlayDropdown.options[OverlayDropdown.value].text, last_size);
            }
            OnDropdownChange(); 
        }
    }
    public void DeleteComponent(string component) {
        Ship.Remove(component);
        this.gameObject.SetActive(false);
        Interactor.ClearText();
        Interactor.SetCommand("rm");
        Interactor.AppendText("$ rm <b>" + component + "</b>");
    }
    public void OnMove() {
        last_position = Ship.GetPosition(OverlayDropdown.options[OverlayDropdown.value].text);
        OverlayDelete.gameObject.SetActive(true);
        OverlayMove.gameObject.SetActive(false);
        OverlayResize.gameObject.SetActive(false);
        
        OverlayMoveUp.gameObject.SetActive(true);
        OverlayMoveLeft.gameObject.SetActive(true);
        OverlayMoveRight.gameObject.SetActive(true);
        OverlayMoveDown.gameObject.SetActive(true);
        OverlayMoveRotateCW.gameObject.SetActive(true);
        OverlayMoveRotateCCW.gameObject.SetActive(true);
    }
    
    public void OnMoveUp() {
        Ship.Move(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(0,1)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnMoveLeft() {
        Ship.Move(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(-1,0)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnMoveRight() {
        Ship.Move(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(1,0)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnMoveDown() {
        Ship.Move(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(0,-1)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnMoveRotateCW() {
        Ship.Rotate90(OverlayDropdown.options[OverlayDropdown.value].text); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnMoveRotateCCW() {
        Ship.RotateM90(OverlayDropdown.options[OverlayDropdown.value].text); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResizeExpandUp() {
        Ship.Upsize(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(0,1)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResizeShrinkUp() {
        Ship.Downsize(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(0,1)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResizeExpandLeft() {
        Ship.Upsize(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(-1,0)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResizeShrinkLeft() {
        Ship.Downsize(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(-1,0)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResizeExpandRight() {
        Ship.Upsize(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(1,0)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResizeShrinkRight() {
        Ship.Downsize(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(1,0)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResizeExpandDown() {
        Ship.Upsize(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(0,-1)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResizeShrinkDown() {
        Ship.Downsize(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(0,-1)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResize() {
        OverlayMove.gameObject.SetActive(false);
        OverlayResize.gameObject.SetActive(false);
        last_position = Ship.GetPosition(OverlayDropdown.options[OverlayDropdown.value].text);
        last_size = Ship.GetSize(OverlayDropdown.options[OverlayDropdown.value].text);

        OverlayResizeExpandUp.gameObject.SetActive(true);
        OverlayResizeShrinkUp.gameObject.SetActive(true);
        OverlayResizeExpandLeft.gameObject.SetActive(true);
        OverlayResizeShrinkLeft.gameObject.SetActive(true);
        OverlayResizeExpandRight.gameObject.SetActive(true);
        OverlayResizeShrinkRight.gameObject.SetActive(true);
        OverlayResizeExpandDown.gameObject.SetActive(true);
        OverlayResizeShrinkDown.gameObject.SetActive(true);
    }
}
