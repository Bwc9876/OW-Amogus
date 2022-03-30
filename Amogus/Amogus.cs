using Amogus.Utilities.ModAPIs;
using OWML.ModHelper;

namespace Amogus;

public class Amogus : ModBehaviour
{
    public static Amogus Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Patches.Patch();
        INewHorizons newHorizonsAPI = ModHelper.Interaction.GetModApi<INewHorizons>("xen.NewHorizons");
        IQuantumSpaceBuddies quantumSpaceBuddiesAPI = ModHelper.Interaction.GetModApi<IQuantumSpaceBuddies>("Raicuparta.QuantumSpaceBuddies");
        quantumSpaceBuddiesAPI.RegisterAddon(this);
        newHorizonsAPI.LoadConfigs(this);
        Instance.ModHelper.Console.WriteLine("Start of Amogus!");
    }
}