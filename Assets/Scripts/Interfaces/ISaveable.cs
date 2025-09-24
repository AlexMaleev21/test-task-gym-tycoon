using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*interface for saving and loading methods*/
public interface ISaveable<TSaveData>
{
    TSaveData GetSaveData();
    void LoadFromSaveData(TSaveData saveData);

    //void LoadFromSaveData(TSaveData saveData, List<TData> data);
}
