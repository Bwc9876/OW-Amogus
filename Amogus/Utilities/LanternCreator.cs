using Amogus.Components;
using Amogus.Utilities;
using OWML.Utils;
using QSB.Utility;
using UnityEngine;

namespace Amogus;

public static class LanternCreator
{
    private static readonly int LanternFlameColorID = Shader.PropertyToID("_Color");
    private static readonly int MainLanternFlameTexID = Shader.PropertyToID("_MainTex");

    public static void SpawnAllLanterns(Sector parentSector)
    {
        // Enable the ringworld so we can yoink the artifacts
        Locator.GetAstroObject(AstroObject.Name.RingWorld).gameObject.SetActive(true);
        
        GameObject sourceLantern = GameObject.Find("RingWorld_Body/Sector_RingInterior/Sector_Zone1/Sector_ArtifactHouse_Zone1/Interactibles_ArtifactHouse_Zone1/Prefab_IP_DreamLanternItem_2 (1)");
        
        Amogus.Instance.ModHelper.Console.WriteLine($"We found {Object.FindObjectsOfType<LanternSpawnPoint>().Length} lantern spawns!");
        
        foreach (LanternSpawnPoint spawnPoint in Object.FindObjectsOfType<LanternSpawnPoint>())
        {
            GameObject newLantern = GetLantern(sourceLantern, spawnPoint.transform, parentSector);
            newLantern.gameObject.name = spawnPoint.gameObject.name.Replace("Spawn", "");
            Delay.RunNextFrame(() =>
            {
                CustomizeLantern(newLantern, spawnPoint.flameColor * spawnPoint.amplifyFlameColor, spawnPoint.ambientColor);
            });
            Amogus.Instance.ModHelper.Console.WriteLine($"New Lantern! {newLantern.transform.position}");
            Object.Destroy(spawnPoint.gameObject);
        }
        
        Locator.GetAstroObject(AstroObject.Name.RingWorld).gameObject.SetActive(false);
    }

    public static void CustomizeLantern(GameObject lantern, Color flameColor, Color ambientColor)
    {
        DreamLanternController controller = lantern.GetComponent<DreamLanternController>();
        controller.enabled = true;
        controller.SetLit(true);
        controller.SetFocus(1);
        controller.UpdateVisuals();
        Texture2D effectImage = Amogus.Instance.ModHelper.Storage.Load<Texture2D>("assets/Greyscale-Lantern-Flame.png");
        OWRenderer[] renderers = {
            lantern.transform.Find("Props_IP_Artifact/Flame").GetComponent<OWRenderer>(),
            lantern.transform.Find("Props_IP_Artifact_ViewModel/Flame_ViewModel").GetComponent<OWRenderer>()
        };
        foreach (OWRenderer flameRenderer in renderers)
        {
            flameRenderer._renderer.material.SetTexture(MainLanternFlameTexID, effectImage);
            flameRenderer._renderer.material.SetColor(LanternFlameColorID, flameColor);
        }
    }

    public static GameObject GetLantern(GameObject sourceLantern, Transform transform, Sector parentSector)
    {
        // TODO: Uh the conceal cover things don't work
        sourceLantern.GetComponent<DreamLanternController>().enabled = true;
        sourceLantern.GetComponent<DreamLanternItem>().enabled = true;
        GameObject newLanternGO = GameObject.Instantiate(sourceLantern, transform.parent);
        newLanternGO.transform.localPosition = transform.localPosition;
        newLanternGO.transform.localRotation = transform.localRotation;
        newLanternGO.transform.SetGlobalScale(Vector3.one);
        DreamLanternItem dreamLanternItem = newLanternGO.GetComponent<DreamLanternItem>();
        dreamLanternItem.SetSector(parentSector);
        DreamLanternController dreamLanternController = newLanternGO.GetComponent<DreamLanternController>();
        dreamLanternController._focuserPetalsBaseEulerAngles = sourceLantern.GetComponent<DreamLanternController>()._focuserPetalsBaseEulerAngles;
        return newLanternGO;
    }
}