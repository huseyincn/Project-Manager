using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace finalll.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    [Persistent("PROJE_YURUTUCUSU")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ProjeYurutucusu : XPObject
    { 
        public ProjeYurutucusu(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string _calisanIsmi;
        private string _email;
        private DateTime _dogumGunu;
        private string _adres;
        private string _phoneNumber;

        [XafDisplayName("Çalışan İsmi"), ToolTip("Çalışanın Tam Adı")]
        [RuleRequiredField(DefaultContexts.Save, CustomMessageTemplate = "Bu alan boş olamaz")]
        public string CalisanIsmi
        {
            get { return _calisanIsmi; }
            set { SetPropertyValue(nameof(CalisanIsmi), ref _calisanIsmi, value); }
        }

        [Size(255)]
        [RuleRegularExpression(DefaultContexts.Save,
     @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
     CustomMessageTemplate = "Email düzgün formatta değil.")]
        [RuleRequiredField(DefaultContexts.Save, CustomMessageTemplate = "Bu alan boş olamaz")]
        public string Email
        {
            get { return _email; }
            set { SetPropertyValue(nameof(Email), ref _email, value); }
        }

        
        public DateTime DogumGunu
        {
            get => _dogumGunu;
            set => SetPropertyValue(nameof(DogumGunu), ref _dogumGunu , value);
        }

        public string Adres { get => _adres; set => SetPropertyValue(nameof(Adres), ref _adres, value); }

        [Size(20)]
        [RuleRegularExpression(DefaultContexts.Save,
    @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$",
    CustomMessageTemplate = "Telefon numarası doğru formatta değil")]
        [RuleRequiredField(DefaultContexts.Save, CustomMessageTemplate = "Bu alan boş olamaz")]
        public string PhoneNumber { get => _phoneNumber; set => SetPropertyValue(nameof(PhoneNumber), ref _phoneNumber, value); }

        private Image _image;
      
        public Image Image
        {
            get { return _image; }
            set { SetPropertyValue(nameof(Image), ref _image, value); }
        }

    }
}