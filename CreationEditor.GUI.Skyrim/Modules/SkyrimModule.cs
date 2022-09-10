﻿using Autofac;
using CreationEditor.Environment;
using CreationEditor.GUI.Skyrim.ViewModels.Mod;
using CreationEditor.GUI.Skyrim.ViewModels.Record;
using CreationEditor.GUI.ViewModels.Mod;
using CreationEditor.GUI.ViewModels.Record;
using CreationEditor.Skyrim.Environment;
using Mutagen.Bethesda;
using MutagenLibrary.Core.Plugins;
namespace CreationEditor.GUI.Skyrim.Modules; 

public class SkyrimModule : Module {
    protected override void Load(ContainerBuilder builder) {
        builder.RegisterType<SkyrimEditorEnvironment>()
            .As<IEditorEnvironment>()
            .SingleInstance();

        var environmentProvider = SimpleEnvironmentContext.Build(GameRelease.SkyrimSE);
        builder.RegisterInstance(environmentProvider).As<ISimpleEnvironmentContext>();
        
        builder.RegisterType<SkyrimRecordBrowserVM>().As<IRecordBrowserVM>();
        
        builder.RegisterType<SkyrimModGetterVM>().As<IModGetterVM>();
    }
}