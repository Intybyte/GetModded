An harmony patch loader to inject using dnSpy or similar means, for Unity games where the developer didn't add modding support. </br>
Once you added these classes and built the module each time you need to add this:

```cs
// add in the Awake/Start/Enable of something that gets executed on startup like a main menu
new GameObject("ModInitializer").AddComponent<ModInitializer>();
```

This is for .NET version 4.7.1 / 4.7.2. Once you added the ModInitializer you will need to throw the Fat Harmony 472 inside the folder where there is the game `.exe`, now the game will check the `/StreamingAssets/Mods` folder for patches.

![get modded](./93h72r.jpg)
