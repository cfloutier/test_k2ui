# Test K2UI


test K2UI controls

1. : create the mod with the @munix template builder 
2. : add K2UI as a sub module in the `src/yourMod.Unity/yourMod.Unity/Assets/Runtime` folder
3. : the K2UI needs Newtonsoft.Json, it is included in Kerbal Main Game But should be added in your Unity Project

edit : `src/test_k2ui.Unity/test_k2ui.Unity/Packages/manifest.json`

and add the line in the modules definition
```json
    "com.unity.nuget.newtonsoft-json": "2.0.0"
```

Open the Unity Project : `src/yourMod.Unity/yourMod.Unity` 





Open the packageManager. Add the git pacvkage : `https://github.com/JamesNK/Newtonsoft.Json.git`