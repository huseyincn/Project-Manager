using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace finalll.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public abstract class STreeNode : BaseObject, ITreeNode
    {
        private string name;
        protected abstract ITreeNode Parent
        {
            get;
        }
        protected abstract IBindingList Children
        {
            get;
        }
        public STreeNode(Session session) : base(session) { }
        [XafDisplayName("Adı")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetPropertyValue("Name", ref name, value);
            }
        }
        #region ITreeNode
        IBindingList ITreeNode.Children
        {
            get
            {
                return Children;
            }
        }
        string ITreeNode.Name
        {
            get
            {
                return Name;
            }
        }
        ITreeNode ITreeNode.Parent
        {
            get
            {
                return Parent;
            }
        }
        #endregion
    }
}