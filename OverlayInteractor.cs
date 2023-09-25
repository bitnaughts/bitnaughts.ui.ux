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
    public string State = "";
    public GameObject InputPrinterPrint, OverlayOk, OverlayDelete, MapScreenPanOverlay, OverlayZoomIn, 
    OverlayMove, OverlayMoveUp, OverlayMoveLeft, OverlayMoveRight, OverlayMoveDown, OverlayMoveRotateCW, OverlayMoveRotateCCW, 
    OverlayResize, OverlayResizeExpandUp, OverlayResizeShrinkUp, OverlayResizeExpandLeft, OverlayResizeShrinkLeft, OverlayResizeExpandRight, OverlayResizeShrinkRight, OverlayResizeExpandDown, OverlayResizeShrinkDown,
    OverlayCodePrimitive, OverlayCodeObject, OverlayCodeFlowControl, OverlayCodeComment, OverlayCodeInput,
    OverlayCodePrimitiveBoolean, OverlayCodePrimitiveInteger, OverlayCodePrimitiveDouble, OverlayCodePrimitiveCharacter,
    OverlayCodeObjectThis, OverlayCodeObjectString, OverlayCodeObjectComponent,
    OverlayCodeFlowControlIf, OverlayCodeFlowControlWhile, OverlayCodeFlowControlFor, OverlayCodeFlowControlForEach,
    OverlayCodeNameX, OverlayCodeNameY, OverlayCodeNameZ, OverlayCodeBooleanTrue, OverlayCodeBooleanFalse,
    OverlayCodeComponentProcessor, OverlayCodeComponentBulkhead, OverlayCodeComponentGimbal, OverlayCodeComponentThruster, OverlayCodeComponentBooster, OverlayCodeComponentSensor, OverlayCodeComponentCannon,
    OverlayCodeCustomMethod, OverlayCodeSystem, OverlayCodeSystemInX, OverlayCodeSystemInY, OverlayCodeSystemInButton, OverlayCodeOperator,
    OverlayCodeAdd, OverlayCodeSubtract, OverlayCodeMultiply, OverlayCodeDivide, OverlayCodeModulus,
    OverlayCodeConstant, OverlayCodeConstantAddPointOne, OverlayCodeConstantAddOne, OverlayCodeConstantAddTen, OverlayCodeConstantAddHundred, OverlayCodeConstantMinusPointOne, OverlayCodeConstantMinusOne, OverlayCodeConstantMinusTen, OverlayCodeConstantMinusHundred,
    OverlayCodePrint, OverlayCodePrintLine;
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
            return;
        }
        Resize();
    }
    public void UpdateOptions() 
    {
        OverlayDropdown.options = new List<Dropdown.OptionData>();
        if (Ship.components != null) {
            foreach (var key in Ship.GetControllers()) {
                // print ("Adding dropdown option " + key);
                OverlayDropdown.options.Add(new Dropdown.OptionData(key));
            }
        }
        else {
            print ("unexpected null components");
        }
        this.gameObject.SetActive(false);
    }
    public void Resize() 
    {
        // print (OverlayDropdown.value);
        // print (OverlayDropdown.options[OverlayDropdown.value].text);
        Resize(OverlayDropdown.options[OverlayDropdown.value].text);
    }
    public void Resize(string name) 
    {
        Vector3 component_position, component_abs_position, component_size;
        float rotation;
        string option;
        option = name;
        component_position = Ship.GetLocalPosition(option);
        component_abs_position = Ship.GetPosition(option);
        component_size = Ship.GetSize(option);
        rotation = Ship.GetRotation(option);
        Camera.main.transform.position = new Vector3(component_abs_position.x, 200, component_abs_position.z);
        if (Interactor.GetBinocular() == "off") {
            Vector2 rotated_vector = new Vector2(
                map(Mathf.Cos(rotation * 2 * Mathf.Deg2Rad), -1, 1, component_size.y * 625 / Camera.main.orthographicSize, component_size.x * 625 / Camera.main.orthographicSize),
                map(Mathf.Cos(rotation * 2 * Mathf.Deg2Rad), -1, 1, component_size.x * 625 / Camera.main.orthographicSize, component_size.y * 625 / Camera.main.orthographicSize)
            );
            if (Camera.main.GetComponent<CameraController>().orientation == "Horizontal") {
                this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(
                    Mathf.Clamp(200f+Mathf.Abs(rotated_vector.x), 350f, (Screen.width / 2 - 260)), 
                    Mathf.Clamp(200f+Mathf.Abs(rotated_vector.y), 350f, (Screen.height - 260)));
            } else {
                this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(
                    Mathf.Clamp(200f+Mathf.Abs(rotated_vector.x), 350f, (Screen.width - 260)), 
                    Mathf.Clamp(200f+Mathf.Abs(rotated_vector.y), 350f, (Screen.height / 2 - 260)));
            }
        } else {
            if (Camera.main.GetComponent<CameraController>().orientation == "Horizontal") {
                this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(
                    Mathf.Clamp(200f+Mathf.Abs(component_size.x * 625 / Camera.main.orthographicSize), 350f, (Screen.width / 2 - 260)), 
                    Mathf.Clamp(200f+Mathf.Abs(component_size.y * 625 / Camera.main.orthographicSize), 350f, (Screen.height - 260)));
            } else {
                this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(
                    Mathf.Clamp(200f+Mathf.Abs(component_size.x * 625 / Camera.main.orthographicSize), 350f, (Screen.width - 260)), 
                    Mathf.Clamp(200f+Mathf.Abs(component_size.y * 625 / Camera.main.orthographicSize), 350f, (Screen.height / 2 - 260)));
            }
        }
        var rectTransform = OverlayDropdown.gameObject.transform.Find("Dropdown List")?.gameObject.GetComponent<RectTransform>();
        if (rectTransform != null) rectTransform.sizeDelta = new Vector2 (rectTransform.sizeDelta.x, this.transform.GetComponent<RectTransform>().sizeDelta.y - 90f);
    }
    
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s-a1)*(b2-b1)/(a2-a1);
    }
    string function_parameter_type = "";
    public void OnDropdownChange(int index) 
    {
        var option = OverlayDropdown.options[OverlayDropdown.value].text;//.Substring(1);
        MapScreenPanOverlay.SetActive(false);
        last_position = new Vector2 (999,999);
        Resize(option);
        if (State != "") {
            // print ("Code mode dropdown " + name);
            OverlayDropdown.gameObject.SetActive(false);
            OverlayCodeInput.SetActive(true);
            OverlayCodeInput.GetComponent<InputField>().text += option.Substring(2) + ".";
            OverlayCodeCustomMethod.gameObject.SetActive(true);
            OverlayCodeCustomMethod.transform.GetChild(0).GetComponent<Text>().text = Interactor.Processor.GetComponent<ProcessorController>().interpreter.GetMethods(option.Substring(2))[0];
            function_parameter_type = Interactor.Processor.GetComponent<ProcessorController>().interpreter.GetMethodParameter(option.Substring(2), Interactor.Processor.GetComponent<ProcessorController>().interpreter.GetMethods(option.Substring(2))[0]);

        } else {
            OverlayDropdown.gameObject.SetActive(true);
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

            OverlayCodePrimitive.gameObject.SetActive(false); OverlayCodeObject.gameObject.SetActive(false); OverlayCodeFlowControl.gameObject.SetActive(false);// OverlayCodeComment.gameObject.SetActive(true); 
            Interactor.RenderComponent(option);
        }

    }
    
    public void OnSubmit() {
        Interactor.Sound("Click");
        if (State != "") {
            var injection = OverlayCodeInput.GetComponent<InputField>().text;
            if (injection == "") {
                this.gameObject.SetActive(false);
                MapScreenPanOverlay.SetActive(true);
                OverlayZoomIn.SetActive(true);
                Interactor.ClearText();
                OverlayCodeInput.SetActive(false);
                OverlayCodeInput.GetComponent<InputField>().text = "";
                return;
            }
            if (State == "If" || State == "While" || State == "For") {
                injection += ")\n{\n}";
            }
            if (State == "Function") {
                injection += ");";
            }
            Interactor.Processor.GetComponent<ProcessorController>().SetInstructions("Process", "Main", injection);
            Interactor.RenderComponent("Process");
            CloseAllOverlays();
        } else {
            
            // Interactor.CancelTutorial();
            if (last_position.x == 999) {
                this.gameObject.SetActive(false);
                MapScreenPanOverlay.SetActive(true);
                OverlayZoomIn.SetActive(true);
                Interactor.ClearText();
                OverlayCodeInput.SetActive(false);
                OverlayCodeInput.GetComponent<InputField>().text = "";
            }
            else 
            {
                OnDropdownChange(0);
            }
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
            OverlayCodeInput.SetActive(false);
            OverlayCodeInput.GetComponent<InputField>().text = "";
            State = "";
        } else {
            Application.Quit();
        }
    }
    public void OnHelp()
    {
        Interactor.Sound("Warning");
    }
    public void OnReset() 
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnDelete() 
    {
        Interactor.Sound("Back");
        if (last_position.x == 999) {
            this.gameObject.SetActive(false);
            MapScreenPanOverlay.SetActive(true);
            OverlayZoomIn.SetActive(true);
            Interactor.ClearText();
        }
        else { 
            Ship.SetPosition(OverlayDropdown.options[OverlayDropdown.value].text, last_position);
            if (last_size.x != 999) {
                Ship.SetSize(OverlayDropdown.options[OverlayDropdown.value].text, last_size);
            }
            OnDropdownChange(0); 
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
    public void OnCodeEditor() 
    {
        CloseAllOverlays();
        InputPrinterPrint.SetActive(false);
        Interactor.InputField.text = "≜ Syntax";
        State = "Code";
        OverlayCodePrimitive.gameObject.SetActive(true);
        OverlayCodeObject.gameObject.SetActive(true);
        OverlayCodeFlowControl.gameObject.SetActive(true); 
        OverlayCodeInput.gameObject.SetActive(true);
        OverlayMove.gameObject.SetActive(false);
        OverlayResize.gameObject.SetActive(false);


    }
    public void CloseAllOverlays() {
     

        OverlayDropdown.gameObject.SetActive(true);
        OverlayCodePrimitive.gameObject.SetActive(false);
        OverlayCodeObject.gameObject.SetActive(false);
        OverlayCodeFlowControl.gameObject.SetActive(false); // OverlayCodeComment.gameObject.SetActive(false);
        OverlayCodeInput.gameObject.SetActive(false);
        OverlayCodeInput.GetComponent<InputField>().text = "";
        OverlayCodePrimitiveBoolean.gameObject.SetActive(false); 
        OverlayCodePrimitiveInteger.gameObject.SetActive(false); 
        OverlayCodePrimitiveDouble.gameObject.SetActive(false); 
        OverlayCodePrimitiveCharacter.gameObject.SetActive(false);
        OverlayCodeObjectThis.gameObject.SetActive(false); 
        OverlayCodeObjectString.gameObject.SetActive(false); 
        OverlayCodeObjectComponent.gameObject.SetActive(false); 
        OverlayCodeFlowControlIf.gameObject.SetActive(false); 
        OverlayCodeFlowControlWhile.gameObject.SetActive(false); 
        OverlayCodeFlowControlFor.gameObject.SetActive(false); 
        OverlayCodeFlowControlForEach.gameObject.SetActive(false);
        OverlayCodeNameX.gameObject.SetActive(false);
        OverlayCodeNameY.gameObject.SetActive(false);
        OverlayCodeNameZ.gameObject.SetActive(false);
        OverlayCodeBooleanTrue.SetActive(false);
        OverlayCodeBooleanFalse.SetActive(false);
        OverlayCodeComponentProcessor.SetActive(false);
        OverlayCodeComponentBulkhead.SetActive(false);
        OverlayCodeComponentGimbal.SetActive(false);
        OverlayCodeComponentThruster.SetActive(false);
        OverlayCodeComponentBooster.SetActive(false);
        OverlayCodeComponentSensor.SetActive(false);
        OverlayCodeComponentCannon.SetActive(false);
        OverlayCodeOperator.SetActive(false);
        OverlayCodeAdd.SetActive(false);
        OverlayCodeSubtract.SetActive(false);
        OverlayCodeMultiply.SetActive(false);
        OverlayCodeDivide.SetActive(false);
        OverlayCodeModulus.SetActive(false);
        OverlayCodeCustomMethod.gameObject.SetActive(false);
        OverlayCodeSystem.SetActive(false);
        OverlayCodeConstantAddPointOne.SetActive(false);
        OverlayCodeConstantAddOne.SetActive(false);
        OverlayCodeConstantAddTen.SetActive(false);
        OverlayCodeConstantMinusPointOne.SetActive(false);
        OverlayCodeConstantMinusOne.SetActive(false);
        OverlayCodeConstantMinusTen.SetActive(false);
        OverlayCodeSystemInX.SetActive(false);
        OverlayCodeSystemInY.SetActive(false);
    }
    public void OnCodeNameX() 
    {
        OverlayCodeNameX.gameObject.SetActive(false);
        OverlayCodeNameY.gameObject.SetActive(false);
        OverlayCodeNameZ.gameObject.SetActive(false);
        OverlayCodeBooleanTrue.SetActive(true);
        OverlayCodeBooleanFalse.SetActive(true);

        OverlayCodeInput.GetComponent<InputField>().text += "x";
    }
    public void OnCodeNameY() 
    {
        OverlayCodeNameX.gameObject.SetActive(false);
        OverlayCodeNameY.gameObject.SetActive(false);
        OverlayCodeNameZ.gameObject.SetActive(false);
        OverlayCodeBooleanTrue.SetActive(true);
        OverlayCodeBooleanFalse.SetActive(true);
        
        OverlayCodeInput.GetComponent<InputField>().text += "y";
    }

    public void OnCodeNameZ() 
    {
        OverlayCodeNameX.gameObject.SetActive(false);
        OverlayCodeNameY.gameObject.SetActive(false);
        OverlayCodeNameZ.gameObject.SetActive(false);
        OverlayCodeBooleanTrue.SetActive(true);
        OverlayCodeBooleanFalse.SetActive(true);
        
        OverlayCodeInput.GetComponent<InputField>().text += "z";
    }

    public void OnCodePrimitive() 
    {
        Interactor.InputField.text = "≐ Primitive";
        OverlayCodePrimitiveBoolean.gameObject.SetActive(true); 
        OverlayCodePrimitiveInteger.gameObject.SetActive(true); 
        OverlayCodePrimitiveDouble.gameObject.SetActive(true); 
        OverlayCodePrimitiveCharacter.gameObject.SetActive(true);

        OverlayCodePrimitive.gameObject.SetActive(false);
        OverlayCodeObject.gameObject.SetActive(false);
        OverlayCodeFlowControl.gameObject.SetActive(false); // OverlayCodeComment.gameObject.SetActive(true);
        OverlayCodeInput.gameObject.SetActive(true);
    }
    public void OnCodePrimitiveBoolean() 
    {
        OverlayCodePrimitiveBoolean.gameObject.SetActive(false); 
        OverlayCodePrimitiveInteger.gameObject.SetActive(false); 
        OverlayCodePrimitiveDouble.gameObject.SetActive(false); 
        OverlayCodePrimitiveCharacter.gameObject.SetActive(false);
        if (State == "Code"){
            Interactor.InputField.text = "≐ Boolean";
            State = "Boolean";
            OverlayCodeInput.GetComponent<InputField>().text = "boolean ";
            OverlayCodeNameX.gameObject.SetActive(true);
            OverlayCodeNameY.gameObject.SetActive(true);
            OverlayCodeNameZ.gameObject.SetActive(true);
        }
        else {
            //values/constants included
            OverlayCodeBooleanTrue.SetActive(true);
            OverlayCodeBooleanFalse.SetActive(true);
        }

    }
    public void OnCodePrimitiveBooleanTrue() 
    {
        OverlayCodeInput.GetComponent<InputField>().text += "true";
        OverlayCodeBooleanTrue.SetActive(false);
        OverlayCodeBooleanFalse.SetActive(false);
        OverlayCodeOperator.SetActive(true);
    }
    public void OnCodePrimitiveBooleanFalse() 
    {
        OverlayCodeInput.GetComponent<InputField>().text += "false";
        OverlayCodeBooleanTrue.SetActive(false);
        OverlayCodeBooleanFalse.SetActive(false);
        OverlayCodeOperator.SetActive(true);
    }

    public void OnCodePrimitiveInteger() 
    {
        Interactor.InputField.text = "≐ Integer";
        OverlayCodeInput.GetComponent<InputField>().text = "int ";
        OverlayCodePrimitiveBoolean.gameObject.SetActive(false); 
        OverlayCodePrimitiveInteger.gameObject.SetActive(false); 
        OverlayCodePrimitiveDouble.gameObject.SetActive(false); 
        OverlayCodePrimitiveCharacter.gameObject.SetActive(false);
        OverlayCodeNameX.gameObject.SetActive(true);
        OverlayCodeNameY.gameObject.SetActive(true);
        OverlayCodeNameZ.gameObject.SetActive(true);
    }
    public void OnCodePrimitiveDouble() 
    {
        if (State == "Function") {
            OverlayCodeSystemInX.gameObject.SetActive(true);
            OverlayCodeSystemInY.gameObject.SetActive(true);
            OverlayCodeConstant.SetActive(true);
        } else {
            Interactor.InputField.text = "≐ Double";
            OverlayCodeInput.GetComponent<InputField>().text = "double ";
            OverlayCodePrimitiveBoolean.gameObject.SetActive(false); 
            OverlayCodePrimitiveInteger.gameObject.SetActive(false); 
            OverlayCodePrimitiveDouble.gameObject.SetActive(false); 
            OverlayCodePrimitiveCharacter.gameObject.SetActive(false);
            OverlayCodeNameX.gameObject.SetActive(true);
            OverlayCodeNameY.gameObject.SetActive(true);
            OverlayCodeNameZ.gameObject.SetActive(true);   
        }
    }
    public void OnCodePrimitiveCharacter() 
    {
        Interactor.InputField.text = "≐ Character";
        OverlayCodeInput.GetComponent<InputField>().text = "char ";
        OverlayCodePrimitiveBoolean.gameObject.SetActive(false); 
        OverlayCodePrimitiveInteger.gameObject.SetActive(false); 
        OverlayCodePrimitiveDouble.gameObject.SetActive(false); 
        OverlayCodePrimitiveCharacter.gameObject.SetActive(false);
        OverlayCodeNameX.gameObject.SetActive(true);
        OverlayCodeNameY.gameObject.SetActive(true);
        OverlayCodeNameZ.gameObject.SetActive(true);
    }

    public void OnCodeObject() 
    {
        Interactor.InputField.text = "≗ Object";
        OverlayCodeObjectThis.gameObject.SetActive(true); 
        OverlayCodeObjectString.gameObject.SetActive(true); 
        OverlayCodeObjectComponent.gameObject.SetActive(true); 
        OverlayCodeSystem.gameObject.SetActive(true); 
        // OverlayCodePrimitiveCharacter.gameObject.SetActive(true);

        OverlayCodePrimitive.gameObject.SetActive(false);
        OverlayCodeObject.gameObject.SetActive(false);
        OverlayCodeFlowControl.gameObject.SetActive(false); // OverlayCodeComment.gameObject.SetActive(true);
        OverlayCodeInput.gameObject.SetActive(true);
    }


    public void OnCodeObjectThis() 
    {
        Interactor.InputField.text = "≗ This";
        OverlayCodeObjectThis.gameObject.SetActive(false);
        OverlayCodeObjectString.gameObject.SetActive(false);
        OverlayCodeObjectComponent.gameObject.SetActive(false);

    }

    public void OnCodeObjectString() 
    {
        Interactor.InputField.text = "≗ String";
        OverlayCodeInput.GetComponent<InputField>().text = "String ";
        OverlayCodeObjectThis.gameObject.SetActive(false); 
        OverlayCodeObjectString.gameObject.SetActive(false); 
        OverlayCodeObjectComponent.gameObject.SetActive(false); 
        OverlayCodeNameX.gameObject.SetActive(true);
        OverlayCodeNameY.gameObject.SetActive(true);
        OverlayCodeNameZ.gameObject.SetActive(true);
    }
    public void OnCodeObjectComponent() 
    {
        Interactor.InputField.text = "≗ Component";
        State = "Function";
        // OverlayCodeInput.GetComponent<InputField>().text = "Component ";
        OverlayCodeObjectThis.gameObject.SetActive(false); 
        OverlayCodeObjectString.gameObject.SetActive(false); 
        OverlayCodeObjectComponent.gameObject.SetActive(false); 
        OverlayCodeInput.gameObject.SetActive(false);
        OverlayDropdown.gameObject.SetActive(true);
        OverlayDropdown.transform.Find("OverlayDropdownLabel").GetComponent<Text>().text = "Reference";
        OverlayDropdown.Show();
        
        OverlayCodeSystem.SetActive(false);
        // OverlayCodeComponentProcessor.SetActive(true);
        // OverlayCodeComponentBulkhead.SetActive(true);
        // OverlayCodeComponentGimbal.SetActive(true);
        // OverlayCodeComponentThruster.SetActive(true);
        // OverlayCodeComponentBooster.SetActive(true);
        // OverlayCodeComponentSensor.SetActive(true);
        // OverlayCodeComponentCannon.SetActive(true);

    }
    public void OnCodeObjectComponentProcessor() 
    {
        OverlayCodeInput.gameObject.SetActive(true);
        OverlayCodeInput.GetComponent<InputField>().text = "Processor ";
        OverlayCodeComponentProcessor.SetActive(false);
        OverlayCodeComponentBulkhead.SetActive(false);
        OverlayCodeComponentGimbal.SetActive(false);
        OverlayCodeComponentThruster.SetActive(false);
        OverlayCodeComponentBooster.SetActive(false);
        OverlayCodeComponentSensor.SetActive(false);
        OverlayCodeComponentCannon.SetActive(false);
        OverlayDropdown.gameObject.SetActive(false);
        OverlayCodeNameX.gameObject.SetActive(true);
        OverlayCodeNameY.gameObject.SetActive(true);
        OverlayCodeNameZ.gameObject.SetActive(true);
    }
    public void OnCodeObjectComponentBulkhead() 
    {
        OverlayCodeInput.gameObject.SetActive(true);
        OverlayCodeInput.GetComponent<InputField>().text = "Bulkhead ";
        OverlayCodeComponentProcessor.SetActive(false);
        OverlayCodeComponentBulkhead.SetActive(false);
        OverlayCodeComponentGimbal.SetActive(false);
        OverlayCodeComponentThruster.SetActive(false);
        OverlayCodeComponentBooster.SetActive(false);
        OverlayCodeComponentSensor.SetActive(false);
        OverlayCodeComponentCannon.SetActive(false);
        OverlayDropdown.gameObject.SetActive(false);
        OverlayCodeNameX.gameObject.SetActive(true);
        OverlayCodeNameY.gameObject.SetActive(true);
        OverlayCodeNameZ.gameObject.SetActive(true);
    }
    public void OnCodeObjectComponentGimbal() 
    {
        OverlayCodeInput.gameObject.SetActive(true);
        OverlayCodeInput.GetComponent<InputField>().text = "Gimbal ";
        OverlayCodeComponentProcessor.SetActive(false);
        OverlayCodeComponentBulkhead.SetActive(false);
        OverlayCodeComponentGimbal.SetActive(false);
        OverlayCodeComponentThruster.SetActive(false);
        OverlayCodeComponentBooster.SetActive(false);
        OverlayCodeComponentSensor.SetActive(false);
        OverlayCodeComponentCannon.SetActive(false);
        OverlayDropdown.gameObject.SetActive(false);
        OverlayCodeNameX.gameObject.SetActive(true);
        OverlayCodeNameY.gameObject.SetActive(true);
        OverlayCodeNameZ.gameObject.SetActive(true);
    }
    public void OnCodeObjectComponentThruster() 
    {
        OverlayCodeInput.gameObject.SetActive(true);
        OverlayCodeInput.GetComponent<InputField>().text = "Thruster ";
        OverlayCodeComponentProcessor.SetActive(false);
        OverlayCodeComponentBulkhead.SetActive(false);
        OverlayCodeComponentGimbal.SetActive(false);
        OverlayCodeComponentThruster.SetActive(false);
        OverlayCodeComponentBooster.SetActive(false);
        OverlayCodeComponentSensor.SetActive(false);
        OverlayCodeComponentCannon.SetActive(false);
        OverlayDropdown.gameObject.SetActive(false);
        OverlayCodeNameX.gameObject.SetActive(true);
        OverlayCodeNameY.gameObject.SetActive(true);
        OverlayCodeNameZ.gameObject.SetActive(true);
    }
    public void OnCodeObjectComponentBooster() 
    {
        OverlayCodeInput.gameObject.SetActive(true);
        OverlayCodeInput.GetComponent<InputField>().text = "Booster ";
        OverlayCodeComponentProcessor.SetActive(false);
        OverlayCodeComponentBulkhead.SetActive(false);
        OverlayCodeComponentGimbal.SetActive(false);
        OverlayCodeComponentThruster.SetActive(false);
        OverlayCodeComponentBooster.SetActive(false);
        OverlayCodeComponentSensor.SetActive(false);
        OverlayCodeComponentCannon.SetActive(false);
        OverlayDropdown.gameObject.SetActive(false);
        OverlayCodeNameX.gameObject.SetActive(true);
        OverlayCodeNameY.gameObject.SetActive(true);
        OverlayCodeNameZ.gameObject.SetActive(true);
    }
    public void OnCodeObjectComponentSensor() 
    {
        OverlayCodeInput.gameObject.SetActive(true);
        OverlayCodeInput.GetComponent<InputField>().text = "Sensor ";
        OverlayCodeComponentProcessor.SetActive(false);
        OverlayCodeComponentBulkhead.SetActive(false);
        OverlayCodeComponentGimbal.SetActive(false);
        OverlayCodeComponentThruster.SetActive(false);
        OverlayCodeComponentBooster.SetActive(false);
        OverlayCodeComponentSensor.SetActive(false);
        OverlayCodeComponentCannon.SetActive(false);
        OverlayDropdown.gameObject.SetActive(false);
        OverlayCodeNameX.gameObject.SetActive(true);
        OverlayCodeNameY.gameObject.SetActive(true);
        OverlayCodeNameZ.gameObject.SetActive(true);
    }
    public void OnCodeObjectComponentCannon() 
    {
        OverlayCodeInput.gameObject.SetActive(true);
        OverlayCodeInput.GetComponent<InputField>().text = "Cannon ";
        OverlayCodeComponentProcessor.SetActive(false);
        OverlayCodeComponentBulkhead.SetActive(false);
        OverlayCodeComponentGimbal.SetActive(false);
        OverlayCodeComponentThruster.SetActive(false);
        OverlayCodeComponentBooster.SetActive(false);
        OverlayCodeComponentSensor.SetActive(false);
        OverlayCodeComponentCannon.SetActive(false);
        OverlayDropdown.gameObject.SetActive(false);
        OverlayCodeNameX.gameObject.SetActive(true);
        OverlayCodeNameY.gameObject.SetActive(true);
        OverlayCodeNameZ.gameObject.SetActive(true);
    }

    public void OnCodeFlow()
    {
        Interactor.InputField.text = "≘ Flow";
        OverlayCodeFlowControlIf.gameObject.SetActive(true); 
        OverlayCodeFlowControlWhile.gameObject.SetActive(true); 
        OverlayCodeFlowControlFor.gameObject.SetActive(true); 
        OverlayCodeFlowControlForEach.gameObject.SetActive(true);

        OverlayCodePrimitive.gameObject.SetActive(false);
        OverlayCodeObject.gameObject.SetActive(false);
        OverlayCodeFlowControl.gameObject.SetActive(false); // OverlayCodeComment.gameObject.SetActive(true);
        OverlayCodeInput.gameObject.SetActive(true);
    }
    public void OnCodeFlowIf()
    {
        Interactor.InputField.text = "≘ If";
        State = "If";
        OverlayCodeInput.GetComponent<InputField>().text = "if (";
        OverlayCodeFlowControlIf.gameObject.SetActive(false); 
        OverlayCodeFlowControlWhile.gameObject.SetActive(false); 
        OverlayCodeFlowControlFor.gameObject.SetActive(false); 
        OverlayCodeFlowControlForEach.gameObject.SetActive(false);
        OverlayCodePrimitive.gameObject.SetActive(true);
        OverlayCodeObject.gameObject.SetActive(true);
    }
    public void OnCodeFlowWhile()
    {
        Interactor.InputField.text = "≘ While";
        State = "While";
        OverlayCodeInput.GetComponent<InputField>().text = "while (";
        OverlayCodeFlowControlIf.gameObject.SetActive(false); 
        OverlayCodeFlowControlWhile.gameObject.SetActive(false); 
        OverlayCodeFlowControlFor.gameObject.SetActive(false); 
        OverlayCodeFlowControlForEach.gameObject.SetActive(false);

        OverlayCodePrimitive.gameObject.SetActive(true);
        OverlayCodeObject.gameObject.SetActive(true);
    }
    public void OnCodeFlowFor()
    {
        Interactor.InputField.text = "≘ For";
        State = "For";
        OverlayCodeInput.GetComponent<InputField>().text = "for (";
        OverlayCodeFlowControlIf.gameObject.SetActive(false); 
        OverlayCodeFlowControlWhile.gameObject.SetActive(false); 
        OverlayCodeFlowControlFor.gameObject.SetActive(false); 
        OverlayCodeFlowControlForEach.gameObject.SetActive(false);

        
        OverlayCodePrimitive.gameObject.SetActive(true);
        OverlayCodeObject.gameObject.SetActive(true);
    }
    public void OnCodeFlowForEach()
    {
        Interactor.InputField.text = "≘ For Each";
        State = "ForEach";
        OverlayCodeInput.GetComponent<InputField>().text = "foreach (";
        OverlayCodeFlowControlIf.gameObject.SetActive(false); 
        OverlayCodeFlowControlWhile.gameObject.SetActive(false); 
        OverlayCodeFlowControlFor.gameObject.SetActive(false); 
        OverlayCodeFlowControlForEach.gameObject.SetActive(false);

        OverlayCodePrimitive.gameObject.SetActive(true);
        OverlayCodeObject.gameObject.SetActive(true);
    }
    public void OnCodeComment()
    {

    }
    public void OnCodeInput()
    {

    }
    public void OnCodeCustomMethod() {
        OverlayCodeInput.GetComponent<InputField>().text += OverlayCodeCustomMethod.transform.GetChild(0).GetComponent<Text>().text + " (";
        OverlayCodeCustomMethod.gameObject.SetActive(false);
        
        if (function_parameter_type == "double") {
            function_parameter_type = "";
            OnCodePrimitiveDouble();
        }

        // if parameter for function?
    }
    public void OnCodeInY() {
        OverlayCodeInput.GetComponent<InputField>().text += "System.in.Y";
        OverlayCodeSystemInX.SetActive(false);
        OverlayCodeSystemInY.SetActive(false);
        OverlayCodeConstant.SetActive(false);
        OverlayCodeOperator.SetActive(true);
        Interactor.EnableJoystick();
        // if (function_parameter_type == "double") {
        //     function_parameter_type = "";
        //     OnCodePrimitiveDouble();
        // }

        // if parameter for function?
    }
    public void OnCodeInX() {
        OverlayCodeInput.GetComponent<InputField>().text += "System.in.X";
        OverlayCodeSystemInX.SetActive(false);
        OverlayCodeSystemInY.SetActive(false);
        OverlayCodeConstant.SetActive(false);
        OverlayCodeOperator.SetActive(true);
        Interactor.EnableJoystick();
        // if (function_parameter_type == "double") {
        //     function_parameter_type = "";
        //     OnCodePrimitiveDouble();
        // }

        // if parameter for function?
    }

    public void OnCodeOperator() {
        OverlayCodeAdd.SetActive(true);
        OverlayCodeSubtract.SetActive(true);
        OverlayCodeMultiply.SetActive(true);
        OverlayCodeDivide.SetActive(true);
        OverlayCodeModulus.SetActive(true);

        
        // Arithmetic.MULTIPLICATION + TAB + Arithmetic.DIVISION + TAB + Arithmetic.REMAINDER + TAB, 
        // Arithmetic.ADDITION + TAB + Arithmetic.SUBTRACTION + TAB,
    }
    public void OnCodeAdd() {
        OverlayCodeInput.GetComponent<InputField>().text += " + ";
        OverlayCodeAdd.SetActive(false);
        OverlayCodeSubtract.SetActive(false);
        OverlayCodeMultiply.SetActive(false);
        OverlayCodeDivide.SetActive(false);
        OverlayCodeModulus.SetActive(false);

        OverlayCodeConstant.SetActive(true);
        OverlayCodeSystemInX.SetActive(true);
        OverlayCodeSystemInY.SetActive(true);
    }
    public void OnCodeSubtract() {
        OverlayCodeInput.GetComponent<InputField>().text += " - ";
        OverlayCodeAdd.SetActive(false);
        OverlayCodeSubtract.SetActive(false);
        OverlayCodeMultiply.SetActive(false);
        OverlayCodeDivide.SetActive(false);
        OverlayCodeModulus.SetActive(false);
        
        OverlayCodeConstant.SetActive(true);
        OverlayCodeSystemInX.SetActive(true);
        OverlayCodeSystemInY.SetActive(true);
    }
    public void OnCodeMultiply() {
        OverlayCodeInput.GetComponent<InputField>().text += " * ";
        OverlayCodeAdd.SetActive(false);
        OverlayCodeSubtract.SetActive(false);
        OverlayCodeMultiply.SetActive(false);
        OverlayCodeDivide.SetActive(false);
        OverlayCodeModulus.SetActive(false);

        
        OverlayCodeConstant.SetActive(true);
        OverlayCodeSystemInX.SetActive(true);
        OverlayCodeSystemInY.SetActive(true);
    }
    public void OnCodeDivide() {
        OverlayCodeInput.GetComponent<InputField>().text += " / ";
        OverlayCodeAdd.SetActive(false);
        OverlayCodeSubtract.SetActive(false);
        OverlayCodeMultiply.SetActive(false);
        OverlayCodeDivide.SetActive(false);
        OverlayCodeModulus.SetActive(false);

        OverlayCodeConstant.SetActive(true);
        OverlayCodeSystemInX.SetActive(true);
        OverlayCodeSystemInY.SetActive(true);
    }
    public void OnCodeModulus() {
        OverlayCodeInput.GetComponent<InputField>().text += " % ";
        OverlayCodeAdd.SetActive(false);
        OverlayCodeSubtract.SetActive(false);
        OverlayCodeMultiply.SetActive(false);
        OverlayCodeDivide.SetActive(false);
        OverlayCodeModulus.SetActive(false);

        OverlayCodeConstant.SetActive(true);
        OverlayCodeSystemInX.SetActive(true);
        OverlayCodeSystemInY.SetActive(true);
    }
    float constant_value = 0;
    public void OnCodeConstant() {
        OverlayCodeInput.GetComponent<InputField>().text += "0";
        constant_value = 0;
        OverlayCodeConstant.SetActive(false);
        OverlayCodeSystemInX.SetActive(false);
        OverlayCodeSystemInY.SetActive(false);
        OverlayCodeOperator.SetActive(true);
        OverlayCodeConstantAddPointOne.SetActive(true);
        OverlayCodeConstantAddOne.SetActive(true);
        OverlayCodeConstantAddTen.SetActive(true);
        OverlayCodeConstantMinusPointOne.SetActive(true);
        OverlayCodeConstantMinusOne.SetActive(true);
        OverlayCodeConstantMinusTen.SetActive(true);
    }
    public void OnCodeObjectSystem() {
        OverlayCodePrint.SetActive(true);
        OverlayCodePrintLine.SetActive(true);
        OverlayCodeSystem.SetActive(false);
        OverlayCodeObjectString.SetActive(false);
        OverlayCodeObjectComponent.SetActive(false);
    }
    public void OnCodeObjectSystemPrint() {
        State = "Function";
        OverlayCodeInput.GetComponent<InputField>().text += "System.out.print (";
        OverlayCodePrint.SetActive(false);
        OverlayCodePrintLine.SetActive(false);
    }
    public void OnCodeObjectSystemPrintLine() {
        State = "Function";
        OverlayCodeInput.GetComponent<InputField>().text += "System.out.println (";
        OverlayCodePrint.SetActive(false);
        OverlayCodePrintLine.SetActive(false);
    }
    public void OnCodeConstantModify(float delta) {
        
        if (OverlayCodeInput.GetComponent<InputField>().text.EndsWith(constant_value.ToString())) {
            OverlayCodeInput.GetComponent<InputField>().text = OverlayCodeInput.GetComponent<InputField>().text.Substring(0, OverlayCodeInput.GetComponent<InputField>().text.LastIndexOf(constant_value.ToString()));

        }
        else if (OverlayCodeInput.GetComponent<InputField>().text.EndsWith(constant_value.ToString("0.0"))) {
            OverlayCodeInput.GetComponent<InputField>().text = OverlayCodeInput.GetComponent<InputField>().text.Substring(0, OverlayCodeInput.GetComponent<InputField>().text.LastIndexOf(constant_value.ToString("0.0")));

        }
        constant_value += delta;
        OverlayCodeInput.GetComponent<InputField>().text += constant_value.ToString("0.0");
        
    }
}
