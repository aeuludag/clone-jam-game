using UnityEngine;

public class NumpadNumberButton : MonoBehaviour
{
    public Numpad numpad;
    void Start()
    {
        numpad = transform.parent.GetComponent<Numpad>();
    }
    public void Send()
    {
        numpad.Send(gameObject.name);
    }
}
