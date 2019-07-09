using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace POSWeb.Entidades
{
    [DataContract]
    [Serializable]
    [KnownType(typeof(CheckBoxListInfo))]
    public class CheckBoxListInfo
    {
        public CheckBoxListInfo(string value, string displayText, bool isChecked, string Id = "", string titulo = "")
        {
            this.Value = value;
            this.DisplayText = displayText;
            this.IsChecked = isChecked;
            this.id = Id;
            this.Titulo = titulo;

        }
        [DataMember]
        public string Value { get; private set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string Titulo { get; set; }
        [DataMember]
        public string DisplayText { get; set; }
        [DataMember]
        public bool IsChecked { get; set; }
    }
}
