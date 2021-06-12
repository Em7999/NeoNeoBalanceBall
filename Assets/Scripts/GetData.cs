using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using System.Text;
using LitJson;

public class GetData : MonoBehaviour
{
   public string DualPlayerMode;

    // Start is called before the first frame update
    public void WhichDual(string whichDual)
    {
        DualPlayerMode = whichDual;
    }

    public void SinglePlayerInput(string PlayerName,int PlayerScore)
    {
        string PlayerResultDate = SingleResultToJson(PlayerName, PlayerScore);
        SaveString(PlayerResultDate,"SinglePlayer");

    }
    public void DualPlayerInput(string GroupName, int GroupScore, int CooperationScore)
    {
        string PlayerResultDate = DualResultToJson(GroupName, GroupScore,CooperationScore);
        SaveString(PlayerResultDate, DualPlayerMode);

    }

    private string GetTime()
    {
        string NowTime;
        int hour = DateTime.Now.Hour;
        int minute = DateTime.Now.Minute;
        int second = DateTime.Now.Second;
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;
        int day = DateTime.Now.Day;

        NowTime = string.Format("{0:D2}:{1:D2}:{2:D2} " + "{3:D4}/{4:D2}/{5:D2}", hour, minute, second, year, month, day);
        return NowTime;
    }

    public string SingleResultToJson(string Name, int NowScore)
    {
        StringBuilder sb = new StringBuilder();
        JsonWriter WriteDate = new JsonWriter(sb);
        WriteDate.WriteObjectStart();
        WriteDate.WritePropertyName("Mode");
        WriteDate.Write("SinglePlayer");
        WriteDate.WritePropertyName("Name");
        WriteDate.Write(Name);
        WriteDate.WritePropertyName("Count");
        WriteDate.Write(NowScore);
        WriteDate.WritePropertyName("Time");
        WriteDate.Write(GetTime());
        WriteDate.WriteObjectEnd();
        return sb.ToString();

    }
    public string DualResultToJson(string Name, int NowScore, int CoScore)
    {
        StringBuilder sb = new StringBuilder();
        JsonWriter WriteDate = new JsonWriter(sb);
        WriteDate.WriteObjectStart();
        WriteDate.WritePropertyName("Mode");
        WriteDate.Write(DualPlayerMode);
        WriteDate.WritePropertyName("GroupName");
        WriteDate.Write(Name);
        WriteDate.WritePropertyName("Count");
        WriteDate.Write(NowScore);
        WriteDate.WritePropertyName("CoScore");
        WriteDate.Write(CoScore);
        WriteDate.WritePropertyName("Time");
        WriteDate.Write(GetTime());
        WriteDate.WriteObjectEnd();
        return sb.ToString();

    }

    private void SaveString(string str, string _mode)
    {
        FileInfo fi = new FileInfo(Application.dataPath + "/Resources/"+_mode+".txt");

        StreamWriter sw = null;
        StreamWriter dw;
        if (fi.Exists)
        {

            sw = fi.AppendText();
        }
        else
        {
            sw = fi.CreateText();
        }

        sw.WriteLine(str);
        sw.Close();
    }


}
