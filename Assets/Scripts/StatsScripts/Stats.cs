using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] IntVariable level;
    [SerializeField] BoolValue canPlay;
    //times when we can start aiming or end for one target
    List<System.DateTime> startingTimes = new List<System.DateTime>();
    List<System.DateTime> endingTimes = new List<System.DateTime>();
    //ending time -starting time
    List<double> interruptionDurations = new List<double>();
    //for following Distractors
    List<string> DistractorsName = new List<string>();
    List<double> TimeFollowingDistractors = new List<double>();
    System.DateTime registerDistractorTime;
    [SerializeField] FloatVariable timeFollowingSelectiveDistractor;
    TextWriter tw;
    string collectedData = "";

    private void Start()
    {

    }

    public void RegisteringStartTimeForTargeting()
    {
        if (!canPlay.Value) return;
        startingTimes.Add(System.DateTime.Now);
        //Debug.Log("StartTimeForTargeting :" + startingTimes[startingTimes.Count - 1]);
    }

    public void RegisteringEndTimeForTargeting()
    {
        endingTimes.Add(System.DateTime.Now);
        //Debug.Log("EndTimeForTargeting :" + endingTimes[endingTimes.Count - 1]);
    }

    public void RegisteringInterraptions()
    {
        for (int i = 0; i < endingTimes.Count; i++)
        {
            interruptionDurations.Add((endingTimes[i] - startingTimes[i]).TotalSeconds);
            //Debug.Log("Interraptions " + (interruptionDurations.Count - 1) + ":" + interruptionDurations[interruptionDurations.Count - 1]);
        }
        //Debug.Log("Interaptions registered");
    }

    //if we are selective we will optain the distracting time from camera hitted the distractor
    //if we are adaptive we will optain the distracting time from difference between raise distractor event and complation of distractor
    public void RegisteringDistractorName(string name)
    {
        if ((level.Value == 4 || level.Value == 5 || level.Value == 6) && (name == "Visitors_waving_adaptive")) return;
        DistractorsName.Add(name);
        Debug.Log("RegisteringDistractorName :"+ name);
        registerDistractorTime = System.DateTime.Now;
        if (name == "Bird_passing" || name == "Visitors_waving_selective" || name == "Tractor_passing")
        {
            StartCoroutine(RegisteringDistractorFollowingTimeIenum());
        }
    }
    IEnumerator RegisteringDistractorFollowingTimeIenum()
    {
        if (!canPlay.Value) yield return null;
        yield return new WaitForSeconds(20);
        TimeFollowingDistractors.Add(timeFollowingSelectiveDistractor.Value);
        Debug.Log("DistractorFollowingTime " + DistractorsName[TimeFollowingDistractors.Count - 1] + ":" + TimeFollowingDistractors[TimeFollowingDistractors.Count - 1]);
    }

    public void RegisteringDistractorFollowingTime()
    {
        Debug.Log("RegisteringDistractorFollowingTime");
        //if (!canPlay.Value) return;
        TimeFollowingDistractors.Add((System.DateTime.Now - registerDistractorTime).TotalSeconds);
        Debug.Log("DistractorFollowingTime " + DistractorsName[TimeFollowingDistractors.Count - 1] + ":" + TimeFollowingDistractors[TimeFollowingDistractors.Count - 1]);
    }

    public void RegisteringLastDistractor()
    {
        Debug.Log("@@DistractorsName.Count " + DistractorsName.Count + "," + "imeFollowingDistractors.Count " + TimeFollowingDistractors.Count);
        if (DistractorsName.Count> TimeFollowingDistractors.Count&& DistractorsName.Count>0)
        {
            Debug.Log("DistractorsName.Count " + DistractorsName.Count + "," + "imeFollowingDistractors.Count "+ TimeFollowingDistractors.Count);
            TimeFollowingDistractors.Add((System.DateTime.Now - registerDistractorTime).TotalSeconds);
        }
    }

    public void WriteCSV1()
    {
        //tw = new StreamWriter(FileName, true);
        tw.WriteLine("Archeeko"+", "+ level.Value);
        tw.WriteLine("Target Starting Time" + ", " + "Target Hitting Time "+", "+ "Interruption Durations");
        for (int i = 0; i < endingTimes.Count; i++)
        {
            tw.WriteLine(startingTimes[i].ToString() + ", " + endingTimes[i].ToString()+", " + interruptionDurations[i].ToString());
        }
        tw.WriteLine("Distractor Name          " + ", " + "Time Following It");
        for (int i = 0; i < TimeFollowingDistractors.Count; i++)
        {
            tw.WriteLine(DistractorsName[i].ToFixedString(25,' ')+", "+ TimeFollowingDistractors[i].ToString());
        }
        tw.Close();
    }

    public void WriteCSV()
    {
        collectedData += "Rodja" + ", " + level.Value + Environment.NewLine;
        Debug.Log("!!!collectedData1 :" + collectedData);
        collectedData += "Target Starting Time" + ", " + "Target Hitting Time " + ", " + "Interruption Durations" + ", " +
            "Distractor Name          " + ", " + "Time Following It" + Environment.NewLine;
        Debug.Log("!!!collectedData2 :" + collectedData);
        int arrLength = endingTimes.Count > DistractorsName.Count ? endingTimes.Count : DistractorsName.Count;
        Debug.Log("!!!arrLength: " + arrLength + " DistractorsName.Count " + DistractorsName.Count + " endingTimes.Count " + endingTimes.Count);
        for (int i = 0; i < arrLength; i++)
        {
            if (i < endingTimes.Count && i < TimeFollowingDistractors.Count)
                collectedData += startingTimes[i].ToString() + ", " + endingTimes[i].ToString() + ", " + interruptionDurations[i].ToString() + ", " +
                    DistractorsName[i].ToFixedString(25, ' ') + ", " + TimeFollowingDistractors[i].ToString() + Environment.NewLine;
            else if (i < endingTimes.Count) collectedData += startingTimes[i].ToString() + ", " + endingTimes[i].ToString() + ", " + interruptionDurations[i].ToString() + Environment.NewLine;
            else if (i < TimeFollowingDistractors.Count) collectedData += " , , , " + DistractorsName[i].ToFixedString(25, ' ') + ", " + TimeFollowingDistractors[i].ToString() + Environment.NewLine;
        }
        Debug.Log("!!!collectedData3 :" + collectedData);
        CSVWriter csv = new CSVWriter();
        GetComponent<CSVWriter>().WriteCSV(collectedData);
        Debug.Log("!!WriteCSV");
    }
}
