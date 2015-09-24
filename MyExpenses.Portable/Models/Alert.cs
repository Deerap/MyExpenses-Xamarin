using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using SQLite.Net.Attributes;

namespace MyExpenses.Portable.Models
{
  [DataContract]
  public class Alert
  {
    public Alert()
    {
    }

    [DataMember]
    [JsonProperty("details")]
    public string Details { get; set; }

    [DataMember]
    [JsonProperty("alertdate")]
    public DateTime AlertDate { get; set; }

    [Ignore]
    public string AlertDateDisplay
    {
      get { return AlertDate.ToString("D"); }
    }
  }
}
