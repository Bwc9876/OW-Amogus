using Amogus.Utilities;
using UnityEngine;

namespace Amogus;

public static class DreamFireHandler
{
    public static readonly DreamArrivalPoint.Location AmogusDreamLocation = (DreamArrivalPoint.Location)500;

    public static void SetupCampfires()
    {
        
        // Enable this because we need DreamWorldController
        DreamWorldController dreamWorldController = Locator.GetDreamWorldController();
        dreamWorldController.gameObject.SetActive(true);
        dreamWorldController._dreamWorldVolume = GameObject.Find("Lobby_Body/Volumes/New Game Object").GetComponent<OWTriggerVolume>();
        dreamWorldController._dreamWorldSector = GameObject.Find("Lobby_Body/Sector").GetComponent<Sector>();
        GameObject.Find("DreamWorld_Body").SetActive(true);

        DreamCampfire fire = GameObject.Find("Lobby_Body/Sector/Prefab_IP_DreamCampfire(Clone)/Controller_Campfire").GetComponent<DreamCampfire>();
        GameObject arrivalLocation = new GameObject("AmogusArrivalPoint", typeof(DreamArrivalPoint));
        // Will update to be the actual game map when we're done testing
        arrivalLocation.transform.SetParent(fire.transform);
        arrivalLocation.transform.localPosition = new Vector3(20, 0, 0);
        arrivalLocation.transform.SetGlobalScale(Vector3.one);
        arrivalLocation.transform.localRotation = Quaternion.identity;

        DreamArrivalPoint arrivalPoint = arrivalLocation.GetComponent<DreamArrivalPoint>();

        arrivalPoint._location = AmogusDreamLocation;
        fire._arrivalPoint = arrivalPoint;
        fire._dreamArrivalLocation = AmogusDreamLocation;
        Locator.RegisterDreamArrivalPoint(arrivalPoint, AmogusDreamLocation);
        
    }
    
}