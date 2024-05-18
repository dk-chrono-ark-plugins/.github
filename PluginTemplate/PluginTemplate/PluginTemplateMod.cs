using ChronoArkMod.Plugin;
using PluginTemplate.Api;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace PluginTemplate;

#nullable enable

public class PluginTemplateMod : ChronoArkPlugin
{
    private static PluginTemplateMod? _instance;
    private readonly List<IPatch> _patches = [];

    public static PluginTemplateMod? Instance => _instance;
    internal Harmony? _harmony;

    public override void Dispose()
    {
        _instance = null;
    }

    public override void Initialize()
    {
        _instance = this;
        _harmony = new(GetGuid());

        foreach (var patch in _patches)
        {
            if (patch.Mandatory)
            {
                Debug.Log($"patching {patch.Name}");
                patch.Commit();
                Debug.Log("success!");
            }
        }
    }
}
