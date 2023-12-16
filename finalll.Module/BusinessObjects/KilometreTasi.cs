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
    public class KilometreTasi : STreeNode
    {
        protected override ITreeNode Parent
        {
            get
            {
                return null;
            }
        }
        protected override IBindingList Children
        {
            get
            {
                return Projects;
            }
        }
        public KilometreTasi(Session session) : base(session) { }
        public KilometreTasi(Session session, string name)
            : base(session)
        {
            this.Name = name;
        }
        [Association("ProjectGroup-Projects"), Aggregated]
        public XPCollection<Gorev> Projects
        {
            get
            {
                return GetCollection<Gorev>("Projects");
            }
        }

        Project projeninIsmi;
        [Association("Project-KMTaslari", typeof(KilometreTasi))]
        public Project ProjeninIsmi
        {
            get => projeninIsmi;
            set => SetPropertyValue(nameof(ProjeninIsmi), ref projeninIsmi, value);
        }
    }
}