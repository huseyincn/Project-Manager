using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;

namespace finalll.Module.BusinessObjects
{
    [DefaultClassOptions]
    [FileAttachment("Files")]
    [NavigationItem("Proje Yönetimi")]
    [XafDisplayName("Görev")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [Appearance("RedIfDue", TargetItems = "*", Criteria = "EndDate < LocalDateTimeToday() AND TaskDurumu != 'BITMIS'", BackColor = "Red")]
    [Appearance("GreenIfDone", TargetItems = "*", Criteria = "TaskDurumu = 'BITMIS'", BackColor = "Green")]
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
        [XafDisplayName("Kilometre Taşı")]
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

        private DateTime startDate;
        [XafDisplayName("Başlangıç Tarihi")]
        public DateTime StartDate
        {
            get { return startDate; }
            set { SetPropertyValue("StartDate", ref startDate, value); }
        }

        private DateTime _endDate;
        [XafDisplayName("Bitiş Tarihi")]
        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetPropertyValue("EndDate", ref _endDate, value); }
        }

        public enum TaskDurum
        {
            BASLAMAMIS,
            DEVAM_EDIYOR,
            BITMIS
        }

        private TaskDurum _taskDurum;
        [XafDisplayName("Görev Durumu")]
        public TaskDurum TaskDurumu
        {
            get { return _taskDurum; }
            set { SetPropertyValue("TaskDurumu", ref _taskDurum, value); }
        }
    }
}