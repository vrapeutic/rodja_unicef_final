using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameEvent onEndSuccessfully;
    [SerializeField] GameEvent onEndUnSuccessfully;
    [SerializeField] BoolValue canPlay;
    [SerializeField] StringVariable typeOfAttention;
    [SerializeField] IntVariable sustainedValue;
    int collectedJewels;
    int targetCollectedJewelries;
    private void Start()
    {
        canPlay.Value = true;
        collectedJewels = 0;
        if (typeOfAttention.Value == "sustained")
        {
            if (sustainedValue.Value == 20) targetCollectedJewelries=5;
            else if (sustainedValue.Value == 40) targetCollectedJewelries=10;
            else if (sustainedValue.Value == 60) targetCollectedJewelries=15;
        }
        else targetCollectedJewelries=10;
    }

    public void CollectingJewelry()
    {
        collectedJewels++;
        Debug.Log("collectedJewels= "+collectedJewels);
        if (collectedJewels >= targetCollectedJewelries)
        {
            Debug.Log("collectedJewels= " + collectedJewels+ " targetCollectedJewelries= "+targetCollectedJewelries);
            StartCoroutine(EndingSuccessfullyIEnum());
        }    
    }
    IEnumerator EndingSuccessfullyIEnum()
    {
        yield return new WaitForSeconds(1);
        StopPlaying();
        onEndSuccessfully.Raise();
        StartCoroutine(EndingAttemptInum());
    }

    public void EndUnSeccessfully()
    {
        StopPlaying();
        onEndUnSuccessfully.Raise();
        StartCoroutine(EndingAttemptInum());
    }

    public void StopPlaying()
    {
        canPlay.Value = false;
        Debug.Log("StopPlaying");
    }

    IEnumerator EndingAttemptInum()
    {
        yield return new WaitForSeconds(5);
        if (GetSceneName(0) == "SystemLobby") Application.Quit();
    }

    public void ContinuePlaying()
    {
        canPlay.Value = true;
        Debug.Log("ContinuePlaying");
    }
    
    string GetSceneName(int index)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(index);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

}
