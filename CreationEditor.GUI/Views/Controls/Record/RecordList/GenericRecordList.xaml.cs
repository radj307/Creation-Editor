﻿using CreationEditor.GUI.ViewModels.Record.RecordList;
using ReactiveUI;
namespace CreationEditor.GUI.Views.Controls.Record.RecordList; 

public class GenericRecordListViewBase : ReactiveUserControl<IRecordListVM> { }

public partial class GenericRecordList {
    public GenericRecordList(IRecordListVM recordListVM) {
        InitializeComponent();

        DataContext = ViewModel = recordListVM;
    }
}