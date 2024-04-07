using UnityEngine;

public class GenericUnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T instance = null;

	public static T Instance
	{
		get
		{
			if (instance != null) return instance;
			instance = GameObject.FindObjectOfType<T>();
			if (instance != null) return instance;
			var singletonObj = new GameObject();
			singletonObj.name = typeof(T).ToString();
			instance = singletonObj.AddComponent<T>();
			return instance;
		}
	}

	protected virtual void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			Debug.LogWarning("Deleted duplicate instance " + nameof(T) + " on gameobject" + gameObject.name);
			return;
		}

		instance = GetComponent<T>();
	}
}