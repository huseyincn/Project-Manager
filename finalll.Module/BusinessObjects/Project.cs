using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using static finalll.Module.BusinessObjects.Project;

namespace finalll.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    [Persistent("PROJE")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [System.ComponentModel.DisplayName("Projeler")]
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
        private List<Calisan> _users;
        private List<string> _projeDokumanlari;
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
            set { SetPropertyValue(nameof(ProjeYurutucusu), ref _projeYurutucusu, value); }
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