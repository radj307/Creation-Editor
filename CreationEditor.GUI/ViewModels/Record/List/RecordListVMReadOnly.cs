﻿using System.Reactive.Linq;
using CreationEditor.Environment;
using CreationEditor.GUI.Models.Record;
using CreationEditor.GUI.Models.Record.Browser;
using DynamicData;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using MutagenLibrary.References.ReferenceCache;
using Noggog.WPF;
using ReactiveUI;
namespace CreationEditor.GUI.ViewModels.Record;

public class RecordListVMReadOnly : RecordListVM {
    public RecordListVMReadOnly(
        Type type,
        IRecordBrowserSettings recordBrowserSettings,
        IReferenceQuery referenceQuery, 
        IRecordController recordController)
        : base(recordBrowserSettings, referenceQuery, recordController, true) {
        Type = type;

        Records = this.WhenAnyValue(x => x.RecordBrowserSettings.LinkCache, x => x.RecordBrowserSettings.SearchTerm, x => x.Type)
            .Throttle(TimeSpan.FromMilliseconds(300), RxApp.MainThreadScheduler)
            .Do(_ => IsBusy = true)
            .ObserveOn(RxApp.TaskpoolScheduler)
            .Select(x => {
                IsBusy = true;
                return Observable.Create<ReferencedRecord<IMajorRecordIdentifier, IMajorRecordIdentifier>>((obs, cancel) => {
                    foreach (var recordIdentifier in x.Item1.AllIdentifiers(x.Item3)) {
                        if (cancel.IsCancellationRequested) return Task.CompletedTask;

                        //Skip when browser settings don't match
                        if (!RecordBrowserSettings.Filter(recordIdentifier)) continue;

                        var formLinks = ReferenceQuery.GetReferences(recordIdentifier.FormKey, RecordBrowserSettings.LinkCache);
                        var referencedRecord = new ReferencedRecord<IMajorRecordIdentifier, IMajorRecordIdentifier>(recordIdentifier, formLinks);

                        obs.OnNext(referencedRecord);
                    }

                    obs.OnCompleted();
                    return Task.CompletedTask;
                });
            })
            .ObserveOn(RxApp.MainThreadScheduler)
            .Do(x => IsBusy = false)
            .Select(x => x.ToObservableChangeSet())
            .Switch()
            .ToObservableCollection(this);
    }
}
