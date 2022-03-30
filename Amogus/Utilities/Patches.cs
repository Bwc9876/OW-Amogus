using System;
using System.Collections.Generic;
using System.Linq;
using QSB.WorldSync;
using UnityEngine;

namespace Amogus;

public static class Patches
{

    public static void Patch()
    {
        var WorldSyncOnBuildWorldObjects = typeof(QSBWorldSync).GetMethod(nameof(QSBWorldSync.BuildWorldObjects));
        Amogus.Instance.ModHelper.HarmonyHelper.AddPrefix(WorldSyncOnBuildWorldObjects, typeof(Patches), nameof(OnQSBWorldSyncBuildWorldObjects));
        Amogus.Instance.ModHelper.HarmonyHelper.AddPrefix<SunController>("Update", typeof(Patches), nameof(Disable));
        var TEST = typeof(OWExtensions).GetMethod(nameof(OWExtensions.GetAttachedOWRigidbody), new Type[] {typeof(GameObject), typeof(bool)});
        Amogus.Instance.ModHelper.HarmonyHelper.AddPrefix(TEST, typeof(Patches), nameof(GetAttachedOWRigidBody));
        Amogus.Instance.ModHelper.HarmonyHelper.AddPrefix<SingleLightSensor>("IsIlluminatedByGhostLantern", typeof(Patches), nameof(SingleLightSensorIsIlluminatedByGhostLantern));
    }

    public static bool SingleLightSensorIsIlluminatedByGhostLantern(ref bool __result)
    {
        // This throws a bunch of errors if it returns true so we just return false since this mod never uses ghosts

        __result = false;
        return false;
    }

    public static bool GetAttachedOWRigidBody(this GameObject __0, bool __1, ref OWRigidbody __result)
    {
        // Basically a copy & paste of the original method except there's no activeInHierarchy check

        OWRigidbody owrigidbody = null;
        Transform transform = __0.transform;
        if (__1)
        {
            transform = __0.transform.parent;
        }
        while (owrigidbody == null)
        {
            owrigidbody = transform.GetComponent<OWRigidbody>();
            if ((transform == __0.transform.root && owrigidbody == null) || owrigidbody != null)
            {
                break;
            }
            transform = transform.parent;
        }

        __result = owrigidbody;
        return false;
    }

    // We don't use these managers and they log a bunch of errors, so we disable them
    
    private static readonly string[] ManagersToRemove =
    {
        "OrbManager",
        "SatelliteProjectorManager",
        "MeteorManager",
        "JellyfishManager",
        "AnglerManager",
        "OccasionalManager",
    };

    public static bool Disable()
    {
        return false;
    }

    public static void OnQSBWorldSyncBuildWorldObjects()
    {
        Amogus.Instance.ModHelper.Console.WriteLine("Checking for Lanterns...");
        LanternCreator.SpawnAllLanterns(GameObject.Find("Lobby_Body/Sector").GetComponent<Sector>());
        List<WorldObjectManager> managers = QSBWorldSync.Managers.ToList();
        managers.RemoveAll(m => ManagersToRemove.Contains(m.ToString()));
        QSBWorldSync.Managers = managers.ToArray();
    }
    
}