using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace LMSServer
{
    [DataContract]
    public class User
    {
        [DataMember]
        [Key] 
        public int Id { get; set; }

        [DataMember]
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [DataMember]
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [DataMember]
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [DataMember]
        [Range(0, 120)]
        public int Age { get; set; }

        [DataMember]
        public bool MarketingConsent { get; set; }
    }
}
