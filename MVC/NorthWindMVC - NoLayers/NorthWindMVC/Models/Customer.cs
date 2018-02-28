using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWindMVC.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [Display (Name = "Código")]
        [DataType(DataType.CreditCard)]
        public string CustomerID { get; set; }

        [Display(Name = "Empresa")]
        public string CompanyName { get; set; }

        [Display(Name = "Contato")]
        public string ContactName { get; set; }

        [Display(Name = "Titulo Contato")]
        public string ContactTitle { get; set; }

        [Display(Name = "Endereço")]
        public string Address { get; set; }

        [Display(Name = "Cidade")]
        public string City { get; set; }

        [Display(Name = "Regiao")]
        public string Region { get; set; }

        [Display(Name = "Código Postal")]
        public string PostalCode { get; set; }

        [Display(Name = "Pais")]
        public string Country { get; set; }

        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }
    }
}