using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWindMVC.Models
{
    public class RespostaHtml
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object Data { get; set; }
    }
}