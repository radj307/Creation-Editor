using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
namespace CreationEditor.GUI.Services.Record;

public interface IRecordEditorController {
    public bool IsEditorAvailable<TMajorRecord, TMajorRecordGetter>()
        where TMajorRecord : class, IMajorRecord, TMajorRecordGetter
        where TMajorRecordGetter : class, IMajorRecordGetter;

    public void OpenEditor<TMajorRecord, TMajorRecordGetter>(TMajorRecord record)
        where TMajorRecord : class, IMajorRecord, TMajorRecordGetter
        where TMajorRecordGetter : class, IMajorRecordGetter;

    public void CloseEditor(IMajorRecord record);
}
