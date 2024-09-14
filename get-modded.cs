using System;
using System.IO;
using System.Reflection;
using UnityEngine;

public class ModLoader
{
	private object harmonyInstance;
	private MethodInfo patchAllMethod;
	
	public ModLoader()
	{
		string harmonyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "0Harmony.dll");
		if (!File.Exists(harmonyPath))
		{
			UnityEngine.Debug.Log("[GET-MODDED] 0Harmony.dll not found. Please ensure it's in the same directory as the game executable.");
			return;
		}
		try
		{
			Assembly assembly = Assembly.LoadFrom(harmonyPath);
			UnityEngine.Debug.Log("[GET-MODDED] 0Harmony.dll loaded successfully.");
      
			Type harmonyType = assembly.GetType("HarmonyLib.Harmony");
			this.harmonyInstance = Activator.CreateInstance(harmonyType, new object[] { "me.vaan.modloader" });
			this.patchAllMethod = harmonyType.GetMethod("PatchAll", new Type[] { typeof(Assembly) });
			this.patchAllMethod.Invoke(this.harmonyInstance, new object[] { Assembly.GetExecutingAssembly() });
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("[GET-MODDED] Error loading or applying Harmony patches: " + ex.Message);
		}
	}

	public void LoadMods(string modsDirectory)
	{
		if (!Directory.Exists(modsDirectory))
		{
			UnityEngine.Debug.Log("[GET-MODDED] Mods directory does not exist.");
			return;
		}
		foreach (string file in Directory.GetFiles(modsDirectory, "*.dll"))
		{
			try
			{
				Assembly modAssembly = Assembly.LoadFrom(file);
				this.patchAllMethod.Invoke(this.harmonyInstance, new object[] { modAssembly });
				UnityEngine.Debug.Log("[GET-MODDED] Loaded and applied patches from " + file);
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log("[GET-MODDED] Error loading mod from " + file + ": " + ex.Message);
			}
		}
	}
}
