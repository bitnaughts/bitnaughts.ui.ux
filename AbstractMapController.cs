using Mapbox.Geocoding;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using UnityEngine;
using System;
using System.Collections;

public class AbstractMapController : MonoBehaviour
{
    AbstractMap _map;
    float zoom;
    void Awake()
    {
        _map = FindObjectOfType<AbstractMap>();
    }    
    public Interactor Interactor;
    void Start() {
        Interactor = GameObject.Find("ScreenCanvas").GetComponent<Interactor>();
        zoom = _map.AbsoluteZoom;
    }

    void Update()
    {
        if (Input.GetKeyDown("r")) 
        {
            int zoom = _map.AbsoluteZoom;
            _map.UpdateMap(new Mapbox.Utils.Vector2d(47.43855f, -122.3071241f), zoom);
        } 
    }
    // int delay = 0;
    void FixedUpdate() {
        if (Interactor.Stage == "MapZoom") {
            Camera.main.transform.localPosition = new Vector3(0, 0, -200);
            // if (delay++ > 5) {  * 5f; delay = 1; }
            zoom += .0275f;
            if (zoom >= 15) {
                _map.UpdateMap(new Mapbox.Utils.Vector2d(47.43855f, -122.3071241f), 15f);

                Interactor.MapZoomed();
            }
            else {
                _map.UpdateMap(new Mapbox.Utils.Vector2d(47.43855f, -122.3071241f), zoom);
                Camera.main.orthographicSize = Mathf.Clamp(5*zoom + 10f, 10f, 250f);
            }
        }
    }
}
