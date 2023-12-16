using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace finalll.Module.BusinessObjects
{
    [DefaultClassOptions]
    [FileAttachment("File")]
    public class Dokuman : FileData
    { 
        public Dokuman(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private Project _project;
        [Association("Dokumanlar")]
        public Project Project
        {
            get { return _project; }
            set { SetPropertyValue(nameof(Project), ref _project, value); }
        }
    }
}