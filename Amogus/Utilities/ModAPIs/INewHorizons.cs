using System.Collections.Generic;
using OWML.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Amogus;

internal interface INewHorizons
{
    void Create(Dictionary<string, object> config, IModBehaviour mod);

    void LoadConfigs(IModBehaviour mod);

    GameObject GetPlanet(string name);

    string GetCurrentStarSystem(); 

    UnityEvent<string> GetChangeStarSystemEvent();

    UnityEvent<string> GetStarSystemLoadedEvent();
}