﻿using UnityEngine;

namespace TweakableLiftingSurface
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class Controller : MonoBehaviour
	{
        public static void HookModule(string targetModule, string attachingModule)
        {
            for (int indexParts = 0; indexParts < PartLoader.LoadedPartsList.Count; indexParts++)
            {
                AvailablePart currentAP = PartLoader.LoadedPartsList[indexParts];
                Part currentPart = currentAP.partPrefab;

                for (int indexModules = 0; indexModules < currentPart.Modules.Count; indexModules++)
                {
                    if (targetModule == currentPart.Modules[indexModules].moduleName)
                    {
                        if (!ModuleAttached(currentPart, attachingModule))
                        {
                            Debug.Log(targetModule + " found. Adding tweakability.");
                            PartModule newModule = currentPart.AddModule(attachingModule);
                            if (null == newModule)
                            {
                                Debug.LogError(attachingModule + " attachment failed.");
                            }
                            newModule.moduleName = attachingModule;
                        }
                        break;
                    }
                }
            }
        }

        private static bool ModuleAttached (Part part, string moduleName)
        {
            for (int indexModules = 0; indexModules < part.Modules.Count; indexModules++)
            {
                if (moduleName == part.Modules[indexModules].moduleName)
                {
                    return (true);
                }
            }
            return (false);
        }
    }
}