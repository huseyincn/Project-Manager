using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace finalll.Module.BusinessObjects
{
    [DefaultClassOptions]
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
        [Association("ProjectFiles")]
        public Project ProjectToAttach
        {
            get { return _project; }
            set { SetPropertyValue(nameof(ProjectToAttach), ref _project, value); }
        }
    }
}