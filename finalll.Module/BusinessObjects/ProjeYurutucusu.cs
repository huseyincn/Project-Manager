using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace finalll.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Proje Yönetimi")]
    [Persistent("PROJE_YURUTUCUSU")]
    public class ProjeYurutucusu : Person
    { 
        public ProjeYurutucusu(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private Project _project;
        public Project Project
        {
            get { return _project; }
            set
            {
                if (_project == value) return;
                var prevProject = _project;
                _project = value;
                if (!IsLoading)
                {
                    if (prevProject != null && prevProject.ProjeYurutucusu == this)
                        prevProject.ProjeYurutucusu = null;
                    if (_project != null)
                        _project.ProjeYurutucusu = this;
                }
                OnChanged(nameof(Project));
            }
        }

    }
}