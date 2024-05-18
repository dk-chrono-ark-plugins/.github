$ProjectName = $args[0]
copy "$($PSScriptRoot)/PluginTemplate" "$($PSScriptRoot)/$($ProjectName)" -Recurse -Container -Force -ErrorAction:SilentlyContinue -Confirm:$false
$SlnDir = "$($PSScriptRoot)/$($ProjectName)"
copy "$($SlnDir)/PluginTemplate" "$($SlnDir)/$($ProjectName)" -Recurse -Container -Force -ErrorAction:SilentlyContinue
rm "$($SlnDir)/PluginTemplate" -Force -Recurse -ErrorAction:SilentlyContinue -Confirm:$false
$ProjDir = "$($SlnDir)/$($ProjectName)"

gc "$($ProjDir)/PluginTemplateMod.cs" | % { $_ -replace "PluginTemplate", $ProjectName } | Set-Content "$($ProjDir)/$($ProjectName)Mod.cs"
rm "$($ProjDir)/PluginTemplateMod.cs" -Force -Recurse -ErrorAction:SilentlyContinue -Confirm:$false

gc "$($ProjDir)/PluginTemplate.csproj" | % { $_ -replace "PluginTemplate", $ProjectName } | Set-Content "$($ProjDir)/$($ProjectName).csproj"
rm "$($ProjDir)/PluginTemplate.csproj" -Force -Recurse -ErrorAction:SilentlyContinue -Confirm:$false

foreach ($file in @("Api/IConfigurable.cs", "Api/IPatch.cs")) {
    $code = gc "$($ProjDir)/$($file)" | % { $_ -replace "PluginTemplate", $ProjectName }
    rm "$($ProjDir)/$($file)" -Force -Recurse -ErrorAction:SilentlyContinue -Confirm:$false
    Set-Content "$($ProjDir)/$($file)" $code
}

pushd
cd $SlnDir
git init
popd
