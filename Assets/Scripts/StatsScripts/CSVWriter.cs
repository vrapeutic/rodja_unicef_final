﻿
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using UnityEngine.Android;

public class CSVWriter:MonoBehaviour
{
    string path;
    [SerializeField] BoolValue isReqToGenerateCSVFile;
    [SerializeField] BridgePluginInitializer bridge;
    [SerializeField] StringVariable fileName;
    [SerializeField] BoolValue canSavestatsToHeadset;

    public void WriteCSV(string CollectedStats)
    {
        if (!canSavestatsToHeadset.Value) return;
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
        path = CreateDirectory(GetDownloadFolder() + "/VRapeuticSessions/") + fileName.Value + ".csv";
        Debug.Log(path);
        File.AppendAllText(path, CollectedStats);
        if (isReqToGenerateCSVFile) bridge.SendIntent(path);
        // if bool is true BridgePluginInitializer.Instance.SendIntent(path);
        //StartCoroutine( WriteCSVIEnum(CollectedStats));
    }


    public string GetDownloadFolder()
    {
        string[] temp = (Application.persistentDataPath.Replace("Android", "")).Split(new string[] { "//" }, System.StringSplitOptions.None);
        return (temp[0] + "/Download");
        //return (temp[0] + "/Pictures");
    }

    public string CreateDirectory(string dir)
    {
        if (!Directory.Exists(dir))
        {
            var directory = Directory.CreateDirectory(dir);
        }
        if (!SetEveryoneAccess(dir)) Debug.Log("!!!!can`t set Access to everyone");
        return dir;
    }

    string _lastError = "";

    /// <summary>
    /// Set Everyone Full Control permissions for selected directory
    /// </summary>
    /// <param name="dirName"></param>
    /// <returns></returns>
    bool SetEveryoneAccess(string dirName)
    {
        try
        {
            // Make sure directory exists
            if (Directory.Exists(dirName) == false)
                throw new Exception(string.Format("Directory {0} does not exist, so permissions cannot be set.", dirName));

            // Get directory access info
            DirectoryInfo dinfo = new DirectoryInfo(dirName);
            DirectorySecurity dSecurity = dinfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));

            // Set the access control
            dinfo.SetAccessControl(dSecurity);
            _lastError = String.Format("Everyone FullControl Permissions were set for directory {0}", dirName);
            return true;
        }
        catch (Exception ex)
        {
            _lastError = ex.Message;
            return false;
        }
    }
}
