using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTile : MonoBehaviour
{
    public enum State { Empty, Sown, Watered}
    public State state;



    private void Start()
    {
        state = State.Empty;
    }

    public void Sow()
    {
        state = State.Sown;

        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.position = transform.position;
        go.transform.localScale = Vector3.one / 2;
    }

    public bool IsEmpty ()
    {
        return state == State.Empty;
    }
}
