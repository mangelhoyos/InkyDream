using UnityEngine;

public class ActualNodeHandler : MonoBehaviour
{
    public static ActualNodeHandler Instance {private set; get;}
    private int actualNode = 1;
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void NextNode()
    {
        actualNode++;
    }

    public int GetActualNode()
    {
        return actualNode;
    }
}
