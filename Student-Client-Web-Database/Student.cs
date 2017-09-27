using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Student_Client_Web_Database
{
    [DataContract]
    public class Student
    {
        [DataMember] public int ID;
        [DataMember] public string Navn;
        [DataMember] public string Klasse;
    }
}