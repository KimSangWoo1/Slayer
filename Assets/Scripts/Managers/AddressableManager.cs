using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.ResourceLocations;
using Cysharp.Threading.Tasks;

public class AddressableManager : MonoSingleton<AddressableManager>
{
    AsyncOperationHandle<IList<UnityEngine.Object>> loadHandle;

    public void Download(IList<string> keys, Action onEvent = null)
    {
        AsyncOperationHandle<long> opSizeHandle = Addressables.GetDownloadSizeAsync(keys);
        opSizeHandle.Completed += (opSize) =>
        {
            if (opSize.Status == AsyncOperationStatus.Succeeded)
            {
                long updateLabelSize = opSize.Result;
                if (updateLabelSize > 0)
                {
                    Debug.Log("[�ٿ�ε�] Size : " + updateLabelSize);
                    StartCoroutine(coDownload(keys));
                }
                else
                {
                    Debug.Log("[�ٿ�ε�] �� �׸� ����");
                }
            }
        };
        Addressables.Release(opSizeHandle);
        onEvent?.Invoke();
    }

    //�ٿ�ε�
    private IEnumerator coDownload(IList<string> keys)
    {
        var opDownloadHandle = Addressables.DownloadDependenciesAsync(keys, false);
        while (!opDownloadHandle.IsDone || opDownloadHandle.Status == AsyncOperationStatus.None)
        {
            float percent = opDownloadHandle.GetDownloadStatus().Percent;
            Debug.Log($"{percent * 100} / {100} %");
            yield return new WaitForEndOfFrame();
        }
        Debug.Log($"[�ٿ�ε�]  ���� : {opDownloadHandle.IsDone} , ��� : {opDownloadHandle.Status}");

        Addressables.Release(opDownloadHandle);
        Debug.Log("[�ٿ�ε�] Proccess End");
    }

    public void LoadObject(IList<string> keys, Addressables.MergeMode mergeMode)
    {
        Debug.Log($"[Load] ����, ������� : {mergeMode}");

        loadHandle = Addressables.LoadAssetsAsync<UnityEngine.Object>(keys,
                addressables =>
                {
                   if(typeof(GameObject) == addressables.GetType())
                    {

                    }
                }, mergeMode, true);

        Debug.Log("[Load] End");
    }

    public async UniTask<TextAsset> LoadData(string key)
    {
        Debug.Log($"[Load] Data ����");
        TextAsset DataAsset = null;

        AsyncOperationHandle<TextAsset> loadDataHandle =  Addressables.LoadAssetAsync<TextAsset>(key);
        loadDataHandle.Completed += (op) =>
        {
            Debug.Log($"[Load] op.Status : {op.Status}");
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                DataAsset = op.Result;
                if (DataAsset != null)
                {
                    Debug.Log(DataAsset.name);
                }
                else
                {
                    Debug.Log("[Load] Data NULL");
                }
                Debug.Log("[Load] Data End");
            }
        };
        await loadDataHandle.Task;
        //Addressables.Release(loadDataHandle);
        //return await loadDataHandle.Task; 
        return DataAsset;
    }

    public void CheckUpdate()
    {
        StartCoroutine(coUpdate());
    }
    public IEnumerator coUpdate()
    {
        Debug.Log("[������Ʈ] ����");

        List<string> catalogsToUpdate = new List<string>();
        AsyncOperationHandle<List<string>> checkForUpdateHandle = Addressables.CheckForCatalogUpdates();
        checkForUpdateHandle.Completed += op =>
        {
            catalogsToUpdate.AddRange(op.Result);
        };
        yield return checkForUpdateHandle;
        if (catalogsToUpdate.Count > 0)
        {
            Debug.Log("[������Ʈ] �׸� ����");

        }
        else
        {
            Debug.Log("[������Ʈ] �׸� ����");
        }
    }
}
