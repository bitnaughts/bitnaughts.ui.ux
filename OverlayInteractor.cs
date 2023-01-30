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
    public GameObject OverlayOk, OverlayDelete, MapScreenPanOverlay, OverlayZoomIn, 
    OverlayMove, OverlayMoveUp, OverlayMoveLeft, OverlayMoveRight, OverlayMoveDown, OverlayMoveRotateCW, OverlayMoveRotateCCW, 
    OverlayResize, OverlayResizeExpandUp, OverlayResizeShrinkUp, OverlayResizeExpandLeft, OverlayResizeShrinkLeft, OverlayResizeExpandRight, OverlayResizeShrinkRight, OverlayResizeExpandDown, OverlayResizeShrinkDown;
    void Start()
    {
        last_position = new Vector2 (999,999);
        last_size = new Vector2 (999,999);
        Interactor = GameObject.Find("ScreenCanvas").GetComponent<Interactor>();
        MapScreenPanOverlay = GameObject.Find("MapScreenPanOverlay");
        OverlayZoomIn = GameObject.Find("OverlayZoomIn");
    }
    void Update()
    {
        if (!populated) {
            UpdateOptions();
            populated = true;
        }
    }
    public void UpdateOptions() 
    {
        OverlayDropdown.options = new List<Dropdown.OptionData>();
        if (Ship.components != null) {
            foreach (var key in Ship.components.Keys) {
                OverlayDropdown.options.Add(new Dropdown.OptionData(key));
            }
        }
        this.gameObject.SetActive(false);
    }
    public void Resize() 
    {
        Resize(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void Resize(string name) 
    {
        Vector3 component_position, component_size;
        float rotation;
        string option;
        print (name);
        option = OverlayDropdown.options[OverlayDropdown.value].text;
        component_position = Ship.GetLocalPosition(option);
        component_size = Ship.GetSize(option);
        rotation = Ship.GetRotation(option);
        print (component_position.ToString());
        print (option);
        // if (GameObject.Find(option) != null) {
        //     Camera.main.transform.SetParent(GameObject.Find(option).transform);
        //     Camera.main.transform.localPosition = new Vector3(0, 0, -200);
        // } else {
        //     Camera.main.transform.SetParent(GameObject.Find("Example").transform);
        Camera.main.transform.localPosition = new Vector3(component_position.x, component_position.z, -200);
        // }//
        Vector3 size_vector = new Vector3(component_size.x, 0, component_size.y);
        Vector3 component_screen_tr_position = Camera.main.WorldToScreenPoint(component_position + size_vector / 2);
        Vector3 component_screen_bl_position = Camera.main.WorldToScreenPoint(component_position - size_vector / 2);
        this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(component_screen_tr_position.x - component_screen_bl_position.x, component_screen_tr_position.y - component_screen_bl_position.y ); 
        if (rotation != (int)rotation) {
            this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(this.transform.GetComponent<RectTransform>().sizeDelta.y, this.transform.GetComponent<RectTransform>().sizeDelta.x);
        }
        this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(
            Mathf.Clamp(200+this.transform.GetComponent<RectTransform>().sizeDelta.x, 500f, (Screen.width - 150)), 
            Mathf.Clamp(240+this.transform.GetComponent<RectTransform>().sizeDelta.y, 500f, (Screen.height - 300)));
        var rectTransform = OverlayDropdown.gameObject.transform.GetChild(2).gameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2 (rectTransform.sizeDelta.x, this.transform.GetComponent<RectTransform>().sizeDelta.y);
    }
    public void OnDropdownChange(string name) 
    {
        MapScreenPanOverlay.SetActive(false);
        OverlayZoomIn.SetActive(false);
        last_position = new Vector2 (999,999);
        Resize(name);
        OverlayOk.gameObject.SetActive(true);
        OverlayMove.gameObject.SetActive(true);
        OverlayDelete.gameObject.SetActive(true);
        OverlayResize.gameObject.SetActive(true);
        OverlayMoveUp.gameObject.SetActive(false);
        OverlayMoveLeft.gameObject.SetActive(false);
        OverlayMoveDown.gameObject.SetActive(false);
        OverlayMoveRight.gameObject.SetActive(false);
        OverlayMoveRotateCW.gameObject.SetActive(false);
        OverlayMoveRotateCCW.gameObject.SetActive(false);
        OverlayResizeExpandUp.gameObject.SetActive(false);
        OverlayResizeShrinkUp.gameObject.SetActive(false);
        OverlayResizeExpandLeft.gameObject.SetActive(false);
        OverlayResizeShrinkLeft.gameObject.SetActive(false);
        OverlayResizeExpandDown.gameObject.SetActive(false);
        OverlayResizeShrinkDown.gameObject.SetActive(false);
        OverlayResizeExpandRight.gameObject.SetActive(false);
        OverlayResizeShrinkRight.gameObject.SetActive(false);
        Interactor.RenderComponent(name);
    }
    
    public void OnSubmit() {
        Interactor.Sound("Click");
        Interactor.CompleteTutorial();
        // Interactor.CancelTutorial();
        if (last_position.x == 999) {
            this.gameObject.SetActive(false);
            MapScreenPanOverlay.SetActive(true);
            OverlayZoomIn.SetActive(true);
            Interactor.ClearText();
        }
        else 
        {
            OnDropdownChange(OverlayDropdown.options[OverlayDropdown.value].text);
        }
    }
    public void OnExit() 
    {
        Interactor.Sound("Back");
        if (gameObject.activeSelf) {
            DeleteComponent(OverlayDropdown.options[OverlayDropdown.value].text);
            this.gameObject.SetActive(false);
            MapScreenPanOverlay.SetActive(true);
            OverlayZoomIn.SetActive(true);
        } else {
            Application.Quit();
        }
    }
    public void OnHelp()
    {
        Interactor.Sound("Warning");
        Interactor.StartTutorial();
    }
    public void OnReset() 
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnDelete() 
    {
        Interactor.Sound("Back");
        Interactor.CompleteTutorial();
        if (last_position.x == 999) {
            this.gameObject.SetActive(false);
            MapScreenPanOverlay.SetActive(true);
            OverlayZoomIn.SetActive(true);
            // Interactor.CancelTutorial();
            Interactor.ClearText();
            // 
        }
        else { 
            Ship.SetPosition(OverlayDropdown.options[OverlayDropdown.value].text, last_position);
            if (last_size.x != 999) {
                Ship.SetSize(OverlayDropdown.options[OverlayDropdown.value].text, last_size);
            }
            OnDropdownChange(OverlayDropdown.options[OverlayDropdown.value].text); 
        }
    }
    public void DeleteComponent(string component)
    {
        Ship.Remove(component);
        this.gameObject.SetActive(false);
        MapScreenPanOverlay.SetActive(true);
        OverlayZoomIn.SetActive(true);
        Interactor.ClearText();
        Interactor.SetCommand("rm");
        Interactor.AppendText("$ rm <b>" + component + "</b>");
    }
    public void OnMove() 
    {
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
    public void OnMoveUp() 
    {
        Ship.Move(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(0,1)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnMoveLeft() 
    {
        Ship.Move(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(-1,0)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnMoveRight() 
    {
        Ship.Move(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(1,0)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnMoveDown() {
        Ship.Move(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(0,-1)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnMoveRotateCW() 
    {
        Ship.Rotate90(OverlayDropdown.options[OverlayDropdown.value].text); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnMoveRotateCCW() 
    {
        Ship.RotateM90(OverlayDropdown.options[OverlayDropdown.value].text); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResizeExpandUp() 
    {
        Ship.Upsize(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(0,1)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResizeShrinkUp() 
    {
        Ship.Downsize(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(0,1)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResizeExpandLeft() 
    {
        Ship.Upsize(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(-1,0)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResizeShrinkLeft() 
    {
        Ship.Downsize(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(-1,0)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResizeExpandRight() 
    {
        Ship.Upsize(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(1,0)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResizeShrinkRight() 
    {
        Ship.Downsize(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(1,0)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResizeExpandDown() 
    {
        Ship.Upsize(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(0,-1)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResizeShrinkDown() 
    {
        Ship.Downsize(OverlayDropdown.options[OverlayDropdown.value].text, new Vector2(0,-1)); Resize(); Interactor.RenderComponent(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void OnResize() 
    {
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
