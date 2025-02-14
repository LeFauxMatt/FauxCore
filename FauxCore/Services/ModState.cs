using LeFauxMods.Common.Integrations.FauxCore;

namespace LeFauxMods.Core.Services;

/// <summary>Responsible for managing state.</summary>
internal sealed class ModState
{
    private static ModState? Instance;
    private readonly IModHelper helper;
    private IConfigWithLastSave? configWithLastSave;

    private ModState(IModHelper helper) => this.helper = helper;

    public static IConfigWithLastSave? ConfigWithLastSave
    {
        get => Instance!.configWithLastSave;
        set => Instance!.configWithLastSave = value;
    }

    public static void Init(IModHelper helper) => Instance ??= new ModState(helper);
}