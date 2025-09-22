using UnityEngine;
using TMPro;
using System.Collections.Generic;
using NaughtyAttributes;

public class Resources_Container_Managment : MonoBehaviour
{
    public static Resources_Container_Managment Instance;

    private int MaxContainerCount = 8;
    private int CurrentContainerCount = 1;

    private int currentResourceCountAllContainers = 10000;

    [SerializeField] private TextMeshProUGUI resourceScoreText;

    //list with all containers
    private List <Container> containers = new List<Container> ();

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        UpdateResourceScore();
    }

    private void ShowScore()
    {
        foreach (var con in containers)
        {
          con.ReturnResourceCount();
            if (con.GotResources)
            {
               int resourceadd = con.ReturnResourceCount();
                currentResourceCountAllContainers = currentResourceCountAllContainers + resourceadd;
                UpdateResourceScore();
            }
        }
        Debug.Log("resource text should not be new text");
    }

    private void UpdateResourceScore()
    {
        resourceScoreText.text = currentResourceCountAllContainers.ToString("N0");
    }

    
    public void addContainer(Container con)
    {
        if (!containers.Contains(con))
        {
            containers.Add(con);
            CurrentContainerCount++;
            Debug.Log("one container added");

            int resourceadd = 1000;
            addResources(resourceadd);
        }
        else
        {
            Debug.LogWarning("this container is already registerred");
        }
    }

    public void RemoveContainer(Container con)
    {
        containers.Remove(con);
        CurrentContainerCount--;
    }

    
    private void addResources(int resources)
    {
        Debug.Log($"added {resources} resources to container");
    }

    
    private void RemoveResources (int resources)
    {
        Debug.Log($"Removed {resources} resources from container");
    }

}
