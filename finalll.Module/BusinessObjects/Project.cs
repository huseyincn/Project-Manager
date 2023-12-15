using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;

namespace finalll.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Proje Yönetimi")]
    [ImageName("BO_Contact")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    [Persistent("PROJE")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [System.ComponentModel.DisplayName("Projeler")]
    [DefaultProperty("ProjeAdi")]
    public class Project : XPObject
    { 
        public Project(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        public enum ProjeDurumu
        {
            ONAY_BEKLIYOR,
            DEVAM_EDIYOR,
            TAMAMLANDI
        }

        public enum ParasalGetiriTipi
        {
            GUNLUK,
            AYLIK,
            YILLIK
        }

        public enum ProjeTipi
        {
            YURTDISI,
            TUBITAK,
            KOBI,
            ODEV
        }

        private string _projeAdi;
        private int _projeNo;
        private ProjeYurutucusu _projeYurutucusu;
        private string _projeAmaci;
        private DateTime _kayitTarihi;
        private DateTime _projeBaslangici;
        private DateTime _projeBitisi;
        private DateTime _tahminiBaslangic;
        private DateTime _bitis;
        private ProjeDurumu _projeDurumu;
        private int _projeGetirisi;
        private ParasalGetiriTipi _parasalGetiriTipi;
        private ProjeTipi _projeTipi;



        [Association("Project-Milestones")]
        public XPCollection<KilometreTasi> Milestones
        {
            get { return GetCollection<KilometreTasi>(nameof(Milestones)); }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ProjeAdi
        {
            get { return _projeAdi; }
            set { SetPropertyValue(nameof(ProjeAdi), ref _projeAdi, value); }
        }

        public int ProjeNo
        {
            get { return _projeNo; }
            set { SetPropertyValue(nameof(ProjeNo), ref _projeNo, value); }
        }

        public ProjeYurutucusu ProjeYurutucusu
        {
            get { return _projeYurutucusu; }
            set
            {
                if (_projeYurutucusu == value) return;
                var prevProjeYurutucusu = _projeYurutucusu;
                _projeYurutucusu = value;
                if (!IsLoading)
                {
                    if (prevProjeYurutucusu != null && prevProjeYurutucusu.Project == this)
                        prevProjeYurutucusu.Project = null;
                    if (_projeYurutucusu != null)
                        _projeYurutucusu.Project = this;
                }
                OnChanged(nameof(ProjeYurutucusu));
            }
        }

        public string ProjeAmaci
        {
            get { return _projeAmaci; }
            set { SetPropertyValue(nameof(ProjeAmaci), ref _projeAmaci, value); }
        }

        public DateTime KayitTarihi
        {
            get => _kayitTarihi;
            set => SetPropertyValue(nameof(KayitTarihi), ref _kayitTarihi, value);
        }

        public DateTime ProjeBaslangici
        {
            get => _projeBaslangici;
            set => SetPropertyValue(nameof(ProjeBaslangici), ref _projeBaslangici, value);
        }

        public DateTime ProjeBitisi
        {
            get => _projeBitisi;
            set => SetPropertyValue(nameof(ProjeBitisi), ref _projeBitisi, value);
        }

        public DateTime TahminiBaslangic
        {
            get => _tahminiBaslangic;
            set => SetPropertyValue(nameof(TahminiBaslangic), ref _tahminiBaslangic, value);
        }

        public DateTime Bitis
        {
            get => _bitis;
            set => SetPropertyValue(nameof(Bitis), ref _bitis, value);
        }

        public ProjeDurumu ProjeDurum
        {
            get => _projeDurumu;
            set => SetPropertyValue(nameof(ProjeDurum), ref _projeDurumu, value);
        }

        public int Getiri
        {
            get => _projeGetirisi;
            set => SetPropertyValue(nameof(Getiri), ref _projeGetirisi, value);
        }

        public ParasalGetiriTipi GetiriTip
        {
            get => _parasalGetiriTipi;
            set => SetPropertyValue(nameof(GetiriTip), ref _parasalGetiriTipi, value);
        }


        [Association("Project-Calisanlar",typeof(Calisan))]
        public XPCollection<Calisan> Calisanlar
        {
            get
            {
                return GetCollection<Calisan>(nameof(Calisanlar));
            }
        }


        [DevExpress.Xpo.Aggregated, DevExpress.Xpo.Association("ProjectFiles")]
        public XPCollection<Dokuman> Files
        {
            get { return GetCollection<Dokuman>(nameof(Files)); }
        }

        public ProjeTipi projetipi
        {
            get => _projeTipi;
            set => SetPropertyValue(nameof(projetipi), ref _projeTipi, value);
        }

        //public void AddFile(FileData file)
        //{
        //    Dokuman myFile = new Dokuman(Session);
        //    myFile.File = file;
        //    myFile.Project = this;
        //    Files.Add(myFile);
        //}

        //public void AddFileToProject(Project project, string filePath, string fileName)
        //{
        //    var fileData = new FileData(project.Session);
        //    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        //    {
        //        fileData.LoadFromStream(fileName, stream);
        //    }
        //    project.Files.Add(fileData);
        //}

        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue(nameof(PersistentProperty), ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
    }
}