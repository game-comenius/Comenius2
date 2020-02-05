#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StudentInverter : MonoBehaviour
{

}

[InitializeOnLoad]
[CustomEditor(typeof(StudentInverter))]
class StudentInverterEditor : Editor
{
    StudentInverter serialObj = null;

    private void OnEnable()
    {
        serialObj = (StudentInverter)serializedObject.targetObject;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Invert"))
        {
            Invert();
        }

        if (GUILayout.Button("Remove All Inverters"))
        {
            RemoveAll();
        }
    }

    private void Invert()
    {
        serialObj.transform.localRotation *= Quaternion.Euler(0, 180, 0);

        Queue<Transform> qOfParents = new Queue<Transform>();
        qOfParents.Enqueue(serialObj.transform);

        while (qOfParents.Count != 0)
        {
            Transform transform = qOfParents.Dequeue();
            Vector3 pos = transform.localPosition;

            transform.localPosition = new Vector3(pos.x, pos.y, -pos.z);

            for (int i = 0; i < transform.childCount; i++)
            {
                qOfParents.Enqueue(transform.GetChild(i));
            }
        }
    }

    private void RemoveAll()
    {
        List<StudentInverter> inverters = new List<StudentInverter>();

        inverters.AddRange(GameObject.FindObjectsOfType<StudentInverter>());

        while (inverters.Count != 0)
        {
            DestroyImmediate(inverters[0]);
            inverters.RemoveAt(0);
        }
    }
}
#endif