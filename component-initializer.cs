using System;
using System.IO;
using UnityEngine;

public class ModInitializer : MonoBehaviour
{

	private void Awake()
	{
		ModLoader modLoader = new ModLoader();
		string modsDirectory = Path.Combine(Application.streamingAssetsPath, "Mods");
		modLoader.LoadMods(modsDirectory);
	}
}
