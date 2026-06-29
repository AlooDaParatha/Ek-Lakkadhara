using UnityEngine;

public class DisableOnStart : MonoBehaviour
{
    public GameObject objectToDisable;

    void Start()
    {
        objectToDisable.SetActive(false);
    }
}