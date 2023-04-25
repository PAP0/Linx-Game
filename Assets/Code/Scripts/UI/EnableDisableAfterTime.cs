using UnityEngine;

[System.Serializable]
public class ObjectActivation
{
    public GameObject[] objects;
    public float activationTime;
}

public class EnableDisableAfterTime : MonoBehaviour
{
    public ObjectActivation[] activationList;

    void Update()
    {
        float currentTime = Time.time;

        foreach (ObjectActivation activation in activationList)
        {
            if (currentTime >= activation.activationTime)
            {
                foreach (GameObject obj in activation.objects)
                {
                    obj.SetActive(true);
                }
            }
            else
            {
                foreach (GameObject obj in activation.objects)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
