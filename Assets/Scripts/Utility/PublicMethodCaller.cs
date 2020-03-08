using UnityEngine;
using UnityEngine.Events;

public class PublicMethodCaller : MonoBehaviour {

    public UnityEvent PublicMethodsToBeCalledOnStart;

    void Start()
    {
        PublicMethodsToBeCalledOnStart.Invoke();
    }
}
