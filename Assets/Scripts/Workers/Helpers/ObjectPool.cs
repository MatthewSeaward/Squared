using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class ObjectPool 
{
    private static Dictionary<string, List<GameObject>> _pool = new Dictionary<string, List<GameObject>>();

    private static Dictionary<string, List<GameObject>> Pool
    {
        get
        {
            if (_pool == null) _pool = new Dictionary<string, List<GameObject>>();
            return _pool;
        }
        set
        {
            _pool = value;
        }
    }

    public static void Clear()
    {
        Pool.Clear();
    }

    public static void PreLoad(GameObject objectToSpawn, int count)
    {
        PreLoad(objectToSpawn.name, objectToSpawn, count);
    }

    public static void PreLoad(string key, GameObject objectToSpawn, int count)
    {
        for (int i = 0; i < count; i++)
        {

            List<GameObject> list = null;
            if (Pool.ContainsKey(key))
            {
                list = Pool[key];
            }
            else
            {
                list = new List<GameObject>();
                Pool.Add(key, list);
            }

            var obj = GameObject.Instantiate(objectToSpawn);
            list.Add(obj);

            obj.SetActive(false);
        }
    }

    public static GameObject Instantiate(GameObject objectToSpawn, Vector3 position)
    {
       return Instantiate(objectToSpawn, position, new Quaternion(0, 0, 0, 0));
    }

    public static GameObject Instantiate(GameObject objectToSpawn, Vector3 position, Quaternion rotation)
    {
        return Instantiate(objectToSpawn.name, objectToSpawn, position, rotation);
    }

    public static GameObject Instantiate(string key, GameObject objectToSpawn, Vector3 position, Quaternion rotation)
    {
        try
        {
            if (!Pool.ContainsKey(key)) Pool.Add(key, new List<GameObject>());

            GameObject firstInactive = GetFirstInactiveObject(objectToSpawn, Pool[key]);

            firstInactive.transform.position = position;
            firstInactive.transform.rotation = rotation;
            firstInactive.SetActive(true);
            return firstInactive;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static GameObject GetGameObject(GameObject gameObject)
    {
        return GetGameObject(gameObject, gameObject.name);
    }

    public static GameObject GetGameObject(GameObject gameObject, string key)
    {
        try
        {
            if (!Pool.ContainsKey(key)) Pool.Add(key, new List<GameObject>());

            var selectedList = Pool[key];
            try
            {
                var obj = selectedList.First(x => x != null && !x.activeInHierarchy);
                if (obj != null)
                {
                    return obj;
                }
            }
            catch
            { }

            selectedList.Add(gameObject);

            return gameObject;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static GameObject GetFirstInactiveObject(GameObject objectToSpawn, List<GameObject> selectedList)
    {
        GameObject firstInactive = null;
        try
        {
            if (selectedList == null) selectedList = new List<GameObject>();

            try
            {
                firstInactive = selectedList.FirstOrDefault(x => x != null && !x.activeInHierarchy);
            }
            catch (MissingReferenceException)
            {
                selectedList.Clear();
            }
            catch (NullReferenceException)
            {
                selectedList.Clear();
            }

            if (firstInactive == null)
            {
                firstInactive = GameObject.Instantiate(objectToSpawn);
                selectedList.Add(firstInactive);
            }
        }
        catch (Exception)
        {
        }

        return firstInactive;
    }

    public static void DeactivatePool(string key)
    {
        try
        {
            if (!Pool.ContainsKey(key)) return;
            foreach (GameObject go in Pool[key])
            {
                try
                {
                    go.SetActive(false);
                }
                catch (Exception)
                {
                }
            }
        }
        catch (Exception)
        {
        }
    }

    public static IEnumerable<GameObject> GetActivePool(GameObject obj)
    {
        return GetActivePool(obj.name);
    }

    public static IEnumerable<GameObject> GetActivePool(string key)
    {
        if (Pool == null || !Pool.ContainsKey(key))
        {
            return new GameObject[0];
        }

        return Pool[key].Where(x => x != null && x.activeInHierarchy);
    }
}