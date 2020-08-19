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
    [HtmlTargetElement("td", Attributes = "i-Canton")]
    public class PaisCantonTH : TagHelper
    {
        private readonly ApplicationDbContext _context;

        public PaisCantonTH(ApplicationDbContext context)
        {
            _context = context;
        }
        [HtmlAttributeName("i-Canton")]

        public int Canton { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = new List<string>();

            List<Pais> listPais = (from d in _context.Pais
                                   select new Pais { Id = d.Id, NombrePais = d.NombrePais }).ToList();
            List<Provincia> listProvincia = (from e in _context.Provincia
                                             select new Provincia { Id = e.Id, NombreProvincia = e.NombreProvincia, PaisId = e.PaisId }).ToList();
            List<Canton> listCanton = (from e in _context.Canton
                                             select new Canton { Id = e.Id, NombreCanton = e.NombreCanton, ProvinciaId = e.ProvinciaId }).ToList();
            for (int i = 0; i < listPais.Count; i++)
            {
                if (Canton == listPais[i].Id)
                {
                    int idPais = listPais[i].Id;

                    for (int j = 0; j < listProvincia.Count; j++)
                    {
                        if (idPais == listProvincia[j].PaisId)
                        {
                            int idPro = listProvincia[j].Id;
                            foreach (var canton in listCanton)
                            {
                                if (idPro == canton.ProvinciaId)
                                {
                                    names.Add(canton.NombreCanton);
                                }
                            }
                        }
                    }

                }
            }
            output.Content.SetContent(names.Count == 0 ? "No hay ciudades" : string.Join(", ", names));
        }
    }
}
