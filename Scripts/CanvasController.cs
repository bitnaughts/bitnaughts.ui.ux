using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (InteractionController))]
public class CanvasController : MonoBehaviour {

    InteractionController interaction_controller;
    List<Panel> panels = new List<Panel> ();

    void Start () {
        interaction_controller = this.GetComponent<InteractionController> ();

        // panels.Add() //menu?, etc.

        panels.Add (new ShipyardPanel (
            new PointF (0, 0),
            new SizeF (10, 10),
            this.gameObject
        ));
    }
    void Visualize (List<Panel> panels) {
        foreach (var panel in panels) {
            Visualize(panel, panel.position);
        }
    }
    void Visualize (Panel panel, PointF center) {
        Referencer.prefab_controller.Add(panel, panel.obj.transform);
        foreach (var child_panel in panel.children) {
            Visualize(child_panel, center + child_panel.position);
        }
    }

    void Update () {
        
    }

    // public void AddPanel(PointF position, SizeF size) {
    //     AddPanel(new Panel(position, size));
    // }
    // public void AddPanel(Panel panel) {
    //     panels.Add(panel);
    // }
}