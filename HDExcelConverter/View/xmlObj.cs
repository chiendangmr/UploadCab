using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HDExcelConverter.View
{
    [DefaultValue(null)]
    [XmlRoot(ElementName = "Media")]    
    public class xmlObj
    { 

        [DefaultValue(null)]
        [XmlElement(ElementName = "Channel")]
        public string Channel { get; set; }

        [DefaultValue(null)]
        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }      

        public string ProgramName { get; set; }

        [DefaultValue(null)]
        public string ExternalProgramName { get; set; }

        [DefaultValue(null)]
        public string Season { get; set; }

        [DefaultValue(null)]
        public string Episode { get; set; }

        [DefaultValue(null)]
        public string Content { get; set; }

        [DefaultValue(null)]
        [XmlElement(ElementName = "BroadcastDate")]
        public string BroadcastDate { get; set; }   
       

        [DefaultValue(null)]
        public string TimeToLive { get; set; }

        [DefaultValue(null)]
        public string Copyrighted { get; set; }

        [DefaultValue(null)]
        [XmlElement(ElementName = "CopyrightedStart")]
        public string CopyrightedStart { get; set; }        

        [DefaultValue(null)]
        [XmlElement(ElementName = "CopyrightedEnd")]
        public string CopyrightedEnd { get; set; }       

        [DefaultValue(null)]
        public string StartTimeCode { get; set; }

        [DefaultValue(null)]
        public string EndTimeCode { get; set; }

        [DefaultValue(null)]
        public string WorkflowID { get; set; }

        [DefaultValue(null)]
        public string Director { get; set; }

        [DefaultValue(null)]
        public string Actor { get; set; }

        [DefaultValue(null)]
        public string Year { get; set; }

        [DefaultValue(null)]
        [XmlElement(ElementName = "CopyrightsScale")]
        public string CopyrightsScale { get; set; }

        [DefaultValue(null)]
        [XmlElement(ElementName = "ProductionUnit")]
        public string ProductionUnit { get; set; }
        [DefaultValue(null)]
        [XmlElement(ElementName = "ProductionCountry")]
        public string ProductionCountry { get; set; }
        [DefaultValue(null)]
        public string Language { get; set; }
        [DefaultValue(null)]
        public string Awards { get; set; }
        [DefaultValue(null)]
        public string Keyword { get; set; }

        [DefaultValue(null)]
        [XmlElement(ElementName = "DistributorUnit")]
        public string DistributorUnit { get; set; }

        [DefaultValue(null)]
        public string ParentalRating { get; set; }

        [DefaultValue(null)]
        public string StarRating { get; set; }
    }
}
