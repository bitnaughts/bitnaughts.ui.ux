using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class ClickableTextInteractor : MonoBehaviour
{   
    public string initialized_text;
    public void Initialize(string text, int line, int pos) {
        this.GetComponent<RectTransform>().position = new Vector2(25f + (pos - (text.Length-1)/2f) * 25f, -25f + line * -50f);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(text.Length * 25f, 50f);
        if (IsValidURL(text)) initialized_text = "<a>" + text + "</a>";
        else initialized_text = text;
        this.transform.GetChild(0).GetComponent<Text>().text = initialized_text;
    }
    public void OnClick() {
        if (initialized_text.Contains("<a>") && initialized_text.Contains("</a>")) Application.OpenURL(initialized_text.Substring(3, initialized_text.Length - 7));
    }

    public static bool IsValidURL(string url) {
        return url.StartsWith("https://");
        // return System.Uri.IsWellFormedUriString(url, System.UriKind.Absolute); // not accurate
    }

    void Start()
    {
    }

    void Update()
    {
        
    }
}
