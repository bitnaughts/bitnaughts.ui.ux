using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelObject {

    int id;

    PointF position; 
    SizeF size;

    List<InteractionObject> interactivity = new List<InteractionObject> ();

    public PanelObject (PointF position, SizeF size,) {
        this.position = position;
        this.size = size;
    }

}
public class ShipyardPanel : PanelObject {

    
}

// public class ModularPanelObject : PanelObject {

// }