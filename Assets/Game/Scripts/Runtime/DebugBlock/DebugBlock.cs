using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DebugBlock
{
    public string Name { get; set; } = "";
    public Dictionary<string, DataObject> Data { get; private set; } = new Dictionary<string, DataObject>();

    public DebugBlock(string name) {
        Name = name;
    }

    [Conditional("UNITY_EDITOR")]
    public void Clear() {
        Data.Clear();
    }

    #region Public functions - Defining
    [Conditional("UNITY_EDITOR")]
    public void Define(string DataNameAndID) {
        Define(DataNameAndID, DataNameAndID);
    }
    [Conditional("UNITY_EDITOR")]
    public void Define(string dataName, string dataID, string value) {
        if (!Data.ContainsKey(dataID)) {
            Data.Add(dataID, new DataObject(dataName, value));
        }
    }
    [Conditional("UNITY_EDITOR")]
    public void Define(string dataName, string dataID) {


        if (!Data.ContainsKey(dataID)) {
            Data.Add(dataID, new DataObject(dataName, ""));
        }
    }
    [Conditional("UNITY_EDITOR")]
    public void Undefine(string dataID) {
        if (Data.ContainsKey(dataID)) {
            Data.Remove(dataID);
        }
    }
    #endregion
    #region Public functions - Managing
    [Conditional("UNITY_EDITOR")]
    public void Change(string dataID, string value) {
        if (Data.ContainsKey(dataID)) {
            var data = Data[dataID];
            data.Value = value;
            Data[dataID] = data;
        }
    }
    #endregion

}

public struct DataObject
{
    public string Name;
    public string Value;

    public DataObject(string name, string value) {
        Name = name;
        Value = value;
    }
}