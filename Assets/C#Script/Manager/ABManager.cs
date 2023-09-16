using System;
using System.Collections;
using System.Collections.Generic;
using C_Script.Common.Model.Singleton;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace C_Script.Manager
{
    public class ABManager : Singleton<ABManager>
    {
        private string MainAbName => "PC";
        private string PathURl => Application.streamingAssetsPath + "/";
        private AssetBundle _mainAb = null;
        private AssetBundleManifest _manifest;

        private Dictionary<string, AssetBundle> _loadedAbDictionary = new Dictionary<string, AssetBundle>();

        private void LoadAb(string abName)
        {
            //加载主包
            if (_mainAb == null)
            {
                try
                {
                    _mainAb = AssetBundle.LoadFromFile( PathURl + MainAbName);
                    _manifest = _mainAb.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e + "mainAB Load ERROR");
                    return;
                }
            }

            if (_loadedAbDictionary.ContainsKey(abName))
            {
                Debug.unityLogger.LogWarning("AssetBundleLoad", "This AssetBundle has already been loaded");
                return;
            }
            
            //加载所需ab包的依赖包
            var dependencies = _manifest.GetAllDependencies(abName);
            foreach (var dependName in dependencies)
            {
                if (!_loadedAbDictionary.ContainsKey(dependName))
                {
                    try
                    {
                        var dependAb = AssetBundle.LoadFromFile(PathURl + dependName);
                        _loadedAbDictionary.Add(dependName, dependAb);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e+ abName +"'s dependAB"+ dependName +"Load ERROR");
                        return;
                    }
                }
            }
            //加载ab包
            try
            {
                _loadedAbDictionary.Add(abName,AssetBundle.LoadFromFile(PathURl + abName));
            }
            catch (Exception e)
            {
                Console.WriteLine(e + abName +"Load ERROR");
            }
        }

        public UnityEngine.Object LoadRes(string abName, string resName)
        {
            LoadAb(abName);
            var res = _loadedAbDictionary[abName].LoadAsset(resName);
            if (res is GameObject) Instantiate(res);
            return res;
        }
        
        public UnityEngine.Object LoadRes(string abName, string resName,Type type)
        {
            LoadAb(abName);
            var res = _loadedAbDictionary[abName].LoadAsset(resName,type);
            if (res is GameObject) Instantiate(res);
            return res;
        }
        
        public UnityEngine.Object LoadRes<T>(string abName, string resName) where T :UnityEngine.Object
        {
            LoadAb(abName);
            var res = _loadedAbDictionary[abName].LoadAsset<T>(resName);
            if (res is GameObject) Instantiate(res);
            return res;
        }

        public void LoadResAsync(string abName, string resName, UnityAction<UnityEngine.Object> callBack)=>
            StartCoroutine(LoadResIEnu(abName,resName,callBack));

        public void LoadResAsync(string abName, string resName,Type type,UnityAction<UnityEngine.Object> callBack) =>
            StartCoroutine(LoadResIEnu(abName,resName,type,callBack));

        public void LoadResAsync<T>(string abName, string resName, UnityAction<UnityEngine.Object> callBack)where T : UnityEngine.Object =>
            StartCoroutine(LoadResIEnu<T>(abName,resName,callBack));
        
        private IEnumerator LoadResIEnu(string abName, string resName,UnityAction<UnityEngine.Object> callBack)
        {
            LoadAb(abName);
            AssetBundleRequest res = _loadedAbDictionary[abName].LoadAssetAsync(resName);
            yield return res;
            if (res.asset is GameObject) callBack(Instantiate(res.asset) as GameObject);
            else callBack(res.asset);
        }
        
        private IEnumerator LoadResIEnu(string abName, string resName,Type type,UnityAction<UnityEngine.Object> callBack)
        {
            LoadAb(abName);
            AssetBundleRequest res = _loadedAbDictionary[abName].LoadAssetAsync(resName,type);
            yield return res;
            if (res.asset is GameObject) callBack(Instantiate(res.asset) as GameObject);
            else callBack(res.asset);
        }
        
        private IEnumerator LoadResIEnu<T>(string abName, string resName,UnityAction<UnityEngine.Object> callBack) where T :UnityEngine.Object
        {
            LoadAb(abName);
            AssetBundleRequest res = _loadedAbDictionary[abName].LoadAssetAsync<T>(resName);
            yield return res;
            if (res.asset is GameObject) callBack(Instantiate(res.asset) as T);
            else callBack(res.asset);
        }
        
    }
}
