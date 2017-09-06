using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionFx : MonoBehaviour {

    public void kill()
    {
        Destroy(this.transform.parent.gameObject);
    }

}
