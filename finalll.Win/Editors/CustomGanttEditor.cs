﻿using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.Validation;
using DevExpress.XtraGantt;
using finalll.Module.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace finalll.Win.Editors
{
    [ListEditor(typeof(Gorev), false)]
    public class CustomGanttEditor : ListEditor
    {
        private GanttControl control;
        private object controlDataSource;
        public CustomGanttEditor(IModelListView info) : base(info) { }
        public override SelectionType SelectionType
        {
            get { return SelectionType.Full; }
        }
        public override IList GetSelectedObjects()
        {
            if (control == null)
            {
                return new object[0] { };
            }
            object[] result = new object[1];
            result[0] = control.GetFocusedRow();
            return result;
        }
        private void Control_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectionChanged();
            OnFocusedObjectChanged();
        }
        private void Control_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            OnSelectionChanged();
        }
        public override void Refresh()
        {
            if (control == null)
                return;
            try
            {
                control.BeginUpdate();
                control.DataSource = controlDataSource;
                control.RefreshDataSource();
            }
            finally
            {
                control.EndUpdate();
            }
        }
        private void DataSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            Refresh();
        }
        protected override void AssignDataSourceToControl(object dataSource)
        {
            if (controlDataSource != dataSource)
            {
                IBindingList oldBindable = controlDataSource as IBindingList;
                if (oldBindable != null)
                {
                    oldBindable.ListChanged -= new ListChangedEventHandler(DataSource_ListChanged);
                }
                controlDataSource = dataSource;
                IBindingList bindable = controlDataSource as IBindingList;
                if (bindable != null)
                {
                    bindable.ListChanged += DataSource_ListChanged;
                }
                Refresh();
            }
        }
        protected override object CreateControlsCore()
        {
            control = new GanttControl();
            foreach (IModelColumn column in Model.Columns)
            {
                var ganttColumn = new DevExpress.XtraTreeList.Columns.TreeListColumn();
                ganttColumn.Caption = column.Caption;
                ganttColumn.FieldName = column.PropertyName;
                ganttColumn.Name = column.PropertyName + "Column";
                ganttColumn.Visible = true;
                ganttColumn.SortIndex = column.SortIndex;
                ganttColumn.Format.FormatString = column.DisplayFormat;
                ganttColumn.Format.FormatType = DevExpress.Utils.FormatType.Custom;
                control.Columns.Add(ganttColumn);
            }
            control.KeyFieldName = nameof(Gorev.Oid);
            control.ChartMappings.TextFieldName = nameof(Gorev.Name);
            control.ChartMappings.StartDateFieldName = nameof(Gorev.StartDate);
            control.ChartMappings.FinishDateFieldName = nameof(Gorev.EndDate);
            control.AllowTouchGestures = DevExpress.Utils.DefaultBoolean.True;
            control.OptionsBehavior.ScheduleMode = DevExpress.XtraGantt.Options.ScheduleMode.Auto;
            control.OptionsCustomization.AllowModifyTasks = DevExpress.Utils.DefaultBoolean.True;
            control.OptionsCustomization.AllowModifyDependencies = DevExpress.Utils.DefaultBoolean.True;
            control.OptionsCustomization.AllowModifyProgress = DevExpress.Utils.DefaultBoolean.True;
            control.SelectionChanged += Control_SelectedIndexChanged;
            control.FocusedNodeChanged += Control_FocusedNodeChanged;
            control.MouseDoubleClick += Control_MouseDoubleClick;
            control.KeyDown += Control_KeyDown;
            Refresh();
            return control;
        }
        private void Control_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            OnSelectionChanged();
            OnFocusedObjectChanged();
        }
        private void Control_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GanttControlHitInfo hitInfo = control.CalcHitInfo(e.Location);
                if (hitInfo.ChartHitTest?.ItemInfo != null)
                {
                    OnProcessSelectedItem();
                }
            }
        }
        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnProcessSelectedItem();
            }
        }
        public override object FocusedObject
        {
            get
            {
                return control?.GetFocusedRow();
            }
        }
        public override void Dispose()
        {
            controlDataSource = null;
            base.Dispose();
        }
    }
}