using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (InteractionController))]
public class CanvasController : MonoBehaviour {

    InteractionController interaction_controller;
    List<PanelObject> panels = new List<PanelObject> ();

    void Start () {
        interaction_controller = this.GetComponent<InteractionController> ();

        // panels.Add() //menu?, etc.

        panels.Add (new ShipyardPanel (
            new PointF (0, 0),
            new SizeF (10, 10),
            this.gameObject
        ));
    }
    void Visualize (List<PanelObject> panels) {
        foreach (var panel in panels) {
            Visualize(panel, panel.position);
        }
    }
    void Visualize (PanelObject panel, PointF center) {
        Referencer.prefab_controller.Add(panel, panel.obj.transform);
        foreach (var child_panel in panel.children) {
            Visualize(child_panel, center + child_panel.position);
        }
    }

    void Update () {
        
    }

    // public void AddPanel(PointF position, SizeF size) {
    //     AddPanel(new PanelObject(position, size));
    // }
    // public void AddPanel(PanelObject panel) {
    //     panels.Add(panel);
    // }
}