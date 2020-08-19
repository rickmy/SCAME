using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SCAME.Data;
using SCAME.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SCAME.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "i-Provincia")]
    public class PaisProvinciaTH : TagHelper
    {
        private readonly ApplicationDbContext _context;

        public PaisProvinciaTH(ApplicationDbContext context)
        {
            _context = context;
        }
        [HtmlAttributeName("i-Provincia")]

        public int Provincia { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = new List<string>();

            List<Pais> listPais = (from d in _context.Pais
                                   select new Pais {Id= d.Id, NombrePais= d.NombrePais }).ToList();
            List<Provincia> listProvincia = (from e in _context.Provincia
                                   select new Provincia { Id = e.Id, NombreProvincia = e.NombreProvincia, PaisId = e.PaisId }).ToList();

            for (int i = 0; i < listPais.Count; i++)
            {
                if (Provincia == listPais[i].Id)
                {
                    int idPais = listPais[i].Id;

                    foreach (var pro in listProvincia)
                    {
                        if (idPais == pro.PaisId)
                        {
                            names.Add(pro.NombreProvincia);
                        }
                    }

                }
            }
            output.Content.SetContent(names.Count == 0 ? "No hay provincias" : string.Join(", ", names));
        }
    }
}
