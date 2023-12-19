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
    [NavigationItem("Proje Yönetimi")]
    [ImageName("BO_Contact")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    [Persistent("PROJE")]
    [XafDisplayName("Proje")]
    [Appearance("RedIfDue", TargetItems = "*", Criteria = "Bitis < LocalDateTimeToday() AND ProjeDurum != 'TAMAMLANDI'", BackColor = "Red")]
    [Appearance("GreenIfDone", TargetItems = "*", Criteria = "ProjeDurum = 'TAMAMLANDI'", BackColor = "Green")]
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

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("Projenin Adı")]
        public string ProjeAdi
        {
            get { return _projeAdi; }
            set { SetPropertyValue(nameof(ProjeAdi), ref _projeAdi, value); }
        }

        [XafDisplayName("Proje No")]
        public int ProjeNo
        {
            get { return _projeNo; }
            set { SetPropertyValue(nameof(ProjeNo), ref _projeNo, value); }
        }
        [XafDisplayName("Proje Yürütücüsü")]
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

        [XafDisplayName("Projenin Amacı")]
        public string ProjeAmaci
        {
            get { return _projeAmaci; }
            set { SetPropertyValue(nameof(ProjeAmaci), ref _projeAmaci, value); }
        }

        [XafDisplayName("Kayıt Tarihi")]
        public DateTime KayitTarihi
        {
            get => _kayitTarihi;
            set => SetPropertyValue(nameof(KayitTarihi), ref _kayitTarihi, value);
        }

        [Browsable(true)]
        [XafDisplayName("Proje Başlangıcı")]
        public DateTime ProjeBaslangici
        {
            get => _projeBaslangici;
            set => SetPropertyValue(nameof(ProjeBaslangici), ref _projeBaslangici, value);
        }

        [Browsable(true)]
        [XafDisplayName("Projenin Bitişi")]
        public DateTime ProjeBitisi
        {
            get => _projeBitisi;
            set => SetPropertyValue(nameof(ProjeBitisi), ref _projeBitisi, value);
        }

        [XafDisplayName("Tahmini Başlangıç")]
        public DateTime TahminiBaslangic
        {
            get => _tahminiBaslangic;
            set => SetPropertyValue(nameof(TahminiBaslangic), ref _tahminiBaslangic, value);
        }

        [XafDisplayName("Bitiş Tarihi")]
        public DateTime Bitis
        {
            get => _bitis;
            set => SetPropertyValue(nameof(Bitis), ref _bitis, value);
        }

        [XafDisplayName("Proje Durumu")]
        public ProjeDurumu ProjeDurum
        {
            get => _projeDurumu;
            set => SetPropertyValue(nameof(ProjeDurum), ref _projeDurumu, value);
        }

        [XafDisplayName("Projenin Getirisi")]
        public int Getiri
        {
            get => _projeGetirisi;
            set => SetPropertyValue(nameof(Getiri), ref _projeGetirisi, value);
        }

        [XafDisplayName("Getiri Tipi")]
        public ParasalGetiriTipi GetiriTip
        {
            get => _parasalGetiriTipi;
            set => SetPropertyValue(nameof(GetiriTip), ref _parasalGetiriTipi, value);
        }


        [Association("Project-Calisanlar",typeof(Calisan))]
        [XafDisplayName("Çalışanlar")]
        public XPCollection<Calisan> Calisanlar
        {
            get
            {
                return GetCollection<Calisan>(nameof(Calisanlar));
            }
        }

        //[Association("Dokumanlar"), Aggregated]
        //public XPCollection<Dokuman> Files
        //{
        //    get { return GetCollection<Dokuman>(nameof(Files)); }
        //}


        [Association, DevExpress.Xpo.Aggregated]
        [XafDisplayName("Dosyalar")]
        public XPCollection<Dokuman> Files
        {
            get { return GetCollection<Dokuman>("Files"); }
        }

        [XafDisplayName("Projenin Tipi")]
        public ProjeTipi projetipi
        {
            get => _projeTipi;
            set => SetPropertyValue(nameof(projetipi), ref _projeTipi, value);
        }

        [Association("Project-KMTaslari", typeof(KilometreTasi)), DevExpress.Xpo.Aggregated]
        [XafDisplayName("Kilometre Taşı")]
        public XPCollection<KilometreTasi> KMTaslari
        {
            get
            {
                return GetCollection<KilometreTasi>(nameof(KMTaslari));
            }
        }

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