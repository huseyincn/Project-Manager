using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using static System.Net.Mime.MediaTypeNames;

namespace finalll.Module.BusinessObjects
{
    [DefaultClassOptions]
    [FileAttachment("File")]
    [Persistent("DOKUMAN")]
    public class Dokuman : XPObject
    { 
        public Dokuman(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        [Association]
        public Project Gorev
        {
            get { return GetPropertyValue<Project>("Gorev"); }
            set { SetPropertyValue("Gorev", value); }
        }

        private FileData _file;
        public FileData File
        {
            get { return _file; }
            set { SetPropertyValue(nameof(File), ref _file, value); }
        }

        public void AddFileToGorev(XafApplication xafApplication)
        {
            using (IObjectSpace objectSpace = xafApplication.CreateObjectSpace())
            {
                FileData file = objectSpace.CreateObject<FileData>();
                // ... set properties of file ...
                objectSpace.CommitChanges(); // This will save the FileData object to the database

                this.File = file; // Now you can add the FileData object to the Gorev object
                objectSpace.CommitChanges(); // This will save the Gorev object to the database
            }
        }

    }
}