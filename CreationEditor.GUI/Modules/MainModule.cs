﻿using System.IO.Abstractions;
using Autofac;
using CreationEditor.Environment;
using CreationEditor.GUI.Models.Record.Browser;
using CreationEditor.GUI.Services;
using CreationEditor.GUI.Services.Startup;
using CreationEditor.GUI.ViewModels;
using CreationEditor.GUI.ViewModels.Mod;
using CreationEditor.GUI.ViewModels.Record;
using MutagenLibrary.References.ReferenceCache;
namespace CreationEditor.GUI.Modules; 

public class MainModule : Module {
    protected override void Load(ContainerBuilder builder) {
        builder.RegisterType<Startup>()
            .As<IStartup>();
        
        builder.RegisterType<MainVM>()
            .SingleInstance();
        
        
        builder.RegisterInstance(new FileSystem())
            .As<IFileSystem>()
            .SingleInstance();
        
        builder.RegisterType<BackgroundTaskManager>()
            .As<IBackgroundTaskManager>()
            .SingleInstance();
        
        builder.RegisterType<BackgroundReferenceQuery>()
            .As<IReferenceQuery>()
            .SingleInstance();

        builder.RegisterType<BusyService>()
            .As<IBusyService>()
            .SingleInstance();

        builder.RegisterType<RecordBrowserSettings>()
            .As<IRecordBrowserSettings>();

        builder.RegisterType<RecordListVMReadOnly>()
            .AsSelf();
        
        //VMs
        builder.RegisterType<ModSelectionVM>();
    }
}
