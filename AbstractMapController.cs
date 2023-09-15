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
    public float min_zoom = 10;

    public GameObject Mars;

    public StructureController target;

    void Start()
    {
        Interactor = GameObject.Find("ScreenCanvas").GetComponent<Interactor>();
        _locations = new Mapbox.Utils.Vector2d[_locationStrings.Length];
        _spawnedObjects = new List<GameObject>();
        for (int i = 0; i < _locationStrings.Length; i++)//< _locationStrings.Length; i++)
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
    public GameObject tint, space;
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
        if (target.gameObject.transform.localPosition.y > 1 && target.gameObject.transform.localPosition.y < 215) {
            _map.UpdateMap(Mathf.Clamp(zoom - target.gameObject.transform.localPosition.y / 25f, 1f, min_zoom));
            Camera.main.orthographicSize = Mathf.Clamp(zoom + target.gameObject.transform.localPosition.y / 5f, 10f, 250f);
        }
        if (target.gameObject.transform.localPosition.y > 205 && target.gameObject.transform.localPosition.y < 215) {
            tint.GetComponent<SpriteRenderer>().color = new Color(200f/255f, 125f/255f, 0f/255f, 100f/255f - (target.gameObject.transform.localPosition.y - 205) / 20f);
            space.SetActive(true);
            space.GetComponent<SpriteRenderer>().color = new Color(255f/255f, 255f/255f, 255f/255f, (target.gameObject.transform.localPosition.y - 205) / 10f);
            space.transform.localPosition = new Vector3(target.gameObject.transform.localPosition.x * .9f, 50 + target.gameObject.transform.localPosition.y * .9f, 300);
            Mars.SetActive(true);
        }
        if (target.gameObject.transform.localPosition.y > 215) {
            space.transform.localPosition = new Vector3(target.gameObject.transform.localPosition.x * .9f, 50 + target.gameObject.transform.localPosition.y * .9f, 300);
            Interactor.MapSpace();
            tint.SetActive(false);
            
            foreach (var renderer in GetComponentsInChildren<MeshRenderer>()) {
                renderer.enabled = false;
            }
        }
        if (target.gameObject.transform.localPosition.y > 430) {
            // space.transform.localPosition = new Vector3(target.gameObject.transform.localPosition.x * .9f, 50 + target.gameObject.transform.localPosition.y * .9f, 300);
            // Interactor.MapSpace();
            // tint.SetActive(false);
            SetEarth();
            SetGroversMill();
            foreach (var renderer in GetComponentsInChildren<MeshRenderer>()) {
                renderer.enabled = true;

            }
            target.gameObject.transform.localPosition = new Vector2 (0,0);
            // target.gameObject.SetActive(false);
            Zoom(1);
            Interactor.SetEarth();
        }
        if (Interactor.Stage == "MapZoom") {
            Camera.main.transform.localPosition = new Vector3(0, 0, -200);
            zoom += Time.deltaTime * 1;
            if (zoom >= 10) {
                // _map.UpdateMap(15f); //new Mapbox.Utils.Vector2d(47.43855f, -122.3071241f), 
                Interactor.MapZoomed();
            } else {//if (Interactor.MarkerIndex != -1) {
                _map.UpdateMap(_locations[Interactor.MarkerIndex], Mathf.Clamp(zoom, 1f, min_zoom));
                Camera.main.orthographicSize = Mathf.Clamp(zoom + 10f, 10f, 250f);
            }
        }
        if (Interactor.Stage == "MapUnzoom") {
            // _map.UpdateMap(zoom); //_locations[0], 
            _spawnedObjects[0].SetActive(true);
            // _spawnedObjects[0].transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Navigate";
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
                Camera.main.orthographicSize = Mathf.Clamp(zoom + 10f, 10f, 250f);
            }
        }
        // zoom -= target.translation.magnitude;
        // Zoom(zoom);
    }
    void Awake()
    {
        _map = FindObjectOfType<AbstractMap>();
    } 
    public void Zoom(float zoom) {
        this.zoom = zoom;
        _map.UpdateMap(Mathf.Clamp(zoom, 1f, min_zoom));
        Camera.main.orthographicSize = Mathf.Clamp(zoom + 10f, 10f, 250f);
    }
    public void SetMars()
    {
        if (tint == null) tint = GameObject.Find("MartianTint");
        tint.SetActive(true);
        min_zoom = 10;
        // Zoom(10);
        // _map.UpdateMap(new Mapbox.Utils.Vector2d(0, 0));
        _map._imagery.LayerSourceId = "mapbox.mars-satellite";
    }

    public void SetEarth()
    {
        if (tint == null) tint = GameObject.Find("MartianTint");
        tint.SetActive(false);
        min_zoom = 15;
        if (_map == null) _map = FindObjectOfType<AbstractMap>();
        // _map.UpdateMap(new Mapbox.Utils.Vector2d(44.3812661305678f, -97.9222112121185f));
        _map._imagery.LayerSourceId = "mapbox://styles/mutilar/clc4fwyx7000314pc089vfata/draft";
    }
    public void SetGroversMill() 
    {
        
        var spawnedObject = _spawnedObjects[0];
        _locations[0] = new Mapbox.Utils.Vector2d(40.322441, -74.600610);
        spawnedObject.transform.localPosition = _map.GeoToWorldPosition(new Mapbox.Utils.Vector2d(40.322441, -74.600610), true) + new Vector3(0, 20, 0);
        spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        spawnedObject.SetActive(true);
        // if (GameObject.Find("1") != null) GameObject.Find("1").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Campaign";
        _map.UpdateMap(new Mapbox.Utils.Vector2d(40.322441, -74.600610));
        Camera.main.transform.localPosition = new Vector3(0, 0, -200);
    }
}
