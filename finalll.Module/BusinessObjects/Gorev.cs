using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Xpo;
using System.ComponentModel;

namespace finalll.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Proje Yönetimi")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Gorev : STreeNode
    {
        private KilometreTasi projectGroup;
        protected override ITreeNode Parent
        {
            get
            {
                return projectGroup;
            }
        }
        protected override IBindingList Children
        {
            get
            {
                return new BindingList<object>(); ;
            }
        }
        public Gorev(Session session) : base(session) { }
        public Gorev(Session session, string name)
            : base(session)
        {
            this.Name = name;
        }
        [Association("ProjectGroup-Projects")]
        public KilometreTasi ProjectGroup
        {
            get
            {
                return projectGroup;
            }
            set
            {
                SetPropertyValue("ProjectGroup", ref projectGroup, value);
            }
        }
    }
}