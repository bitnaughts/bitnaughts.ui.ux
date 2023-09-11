using Mapbox.Geocoding;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Utils;

public class AbstractMapController : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;

    [SerializeField]
    [Geocode]
    string[] _locationStrings;
    Vector2d[] _locations;

    [SerializeField]
    float _spawnScale = 100f;

    [SerializeField]
    GameObject _markerPrefab;

    List<GameObject> _spawnedObjects;

    float zoom;
    public List<string> titles;
    public Interactor Interactor;
    public float min_zoom = 15;

    void Start()
    {
        Interactor = GameObject.Find("ScreenCanvas").GetComponent<Interactor>();
        _locations = new Mapbox.Utils.Vector2d[_locationStrings.Length];
        _spawnedObjects = new List<GameObject>();
        for (int i = 0; i < _locationStrings.Length - 3; i++)//< _locationStrings.Length; i++)
        {
            var locationString = _locationStrings[i];
            _locations[i] = Conversions.StringToLatLon(locationString);
            var instance = Instantiate(_markerPrefab);
            instance.name = i.ToString();
            instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true) + new Vector3(0, 20, 0);
            instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            instance.transform.GetComponentsInChildren<TextMesh>()[0].text = titles[i];
            _spawnedObjects.Add(instance);
        }
    }
    GameObject tint;
    private void Update()
    {
        int count = _spawnedObjects.Count;
        for (int i = 0; i < count; i++)//count; i++)
        {
            var spawnedObject = _spawnedObjects[i];
            var location = _locations[i];
            spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true) + new Vector3(0, 20, 0);
            spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        }
        if (Interactor.Stage == "MapZoom") {
            Camera.main.transform.localPosition = new Vector3(0, 0, -200);
            zoom += Time.deltaTime * 1.5f;
            if (zoom >= 15) {
                // _map.UpdateMap(15f); //new Mapbox.Utils.Vector2d(47.43855f, -122.3071241f), 
                Interactor.MapZoomed();
            }
            else if (Interactor.MarkerIndex != -1) {
                _map.UpdateMap(_locations[Interactor.MarkerIndex], Mathf.Clamp(zoom, 1f, min_zoom));
                Camera.main.orthographicSize = Mathf.Clamp(5*zoom + 10f, 10f, 250f);
            }
        }
        if (Interactor.Stage == "MapUnzoom") {
            // _map.UpdateMap(zoom); //_locations[0], 
            _spawnedObjects[0].SetActive(true);
            _spawnedObjects[0].transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Tutorial";
            GameObject.Find("Example").transform.localPosition = new Vector3(0, 0, 0);
            GameObject.Find("Example").transform.GetChild(0).localRotation = Quaternion.identity;//new Vector3(0, 0, 0);
            // Camera.main.transform.localPosition = new Vector3(0, 0, -200);
            Camera.main.transform.localRotation = Quaternion.identity;
            zoom -= Time.deltaTime * 1.5f;
            Camera.main.transform.localPosition = new Vector3(0, 0, -200);
            if (zoom <= 0.825f) {
                // _map.UpdateMap(15f); //new Mapbox.Utils.Vector2d(47.43855f, -122.3071241f), 
                Interactor.MapUnzoomed();
            }
            else {
                Zoom(zoom);
                // Camera.main.orthographicSize = Mathf.Clamp(5*zoom + 10f, 10f, 250f);
            }
        }
    }
    void Awake()
    {
        _map = FindObjectOfType<AbstractMap>();
    } 
    public void Zoom(float zoom) {
        this.zoom = zoom;
        _map.UpdateMap(Mathf.Clamp(zoom, 1f, min_zoom));
        Camera.main.orthographicSize = Mathf.Clamp(5*zoom + 1f, 15f, 250f);
    }
    public void SetMars()
    {
        tint.SetActive(true);
        min_zoom = 10;
        Zoom(10);
        _map.UpdateMap(new Mapbox.Utils.Vector2d(0, 0));
        _map._imagery.LayerSourceId = "mapbox.mars-satellite";
    }

    public void SetEarth()
    {
        if (tint == null) tint = GameObject.Find("MartianTint");
        tint.SetActive(false);
        min_zoom = 15;
        // _map.UpdateMap(new Mapbox.Utils.Vector2d(44.3812661305678f, -97.9222112121185f));
        _map._imagery.LayerSourceId = "mapbox://styles/mutilar/clc4fwyx7000314pc089vfata/draft";
    }
    public void SetGroversMill() 
    {
        
        var spawnedObject = _spawnedObjects[1];
        _locations[1] = new Mapbox.Utils.Vector2d(40.322441, -74.600610);
        spawnedObject.transform.localPosition = _map.GeoToWorldPosition(new Mapbox.Utils.Vector2d(40.322441, -74.600610), true) + new Vector3(0, 20, 0);
        spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        spawnedObject.SetActive(true);
        // if (GameObject.Find("1") != null) GameObject.Find("1").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Campaign";
        _map.UpdateMap(new Mapbox.Utils.Vector2d(40.322441, -74.600610));
        Camera.main.transform.localPosition = new Vector3(0, 0, -200);
    }
}
