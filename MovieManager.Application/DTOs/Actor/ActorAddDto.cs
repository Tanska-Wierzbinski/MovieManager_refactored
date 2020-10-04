using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MovieManager.Application.DTOs.Actor
{
    public class ActorAddDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? DeathDate { get; set; }
        public DateTime BornDate { get; set; }
        public GenderDto Gender { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Country { get; set; }
        public int[] MovieIds { get; set; }

        public List<string> Countries { get; set; }
        public List<SelectListItem> Movies { get; set; }
        //selectList z filmami do wyboru
        //selectList z krajami


        public ActorAddDto()
        {
            Countries = new List<string>();
            CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo getCulture in getCultureInfo)
            {
                RegionInfo getRegionInfo = new RegionInfo(getCulture.LCID);
                if (!(Countries.Contains(getRegionInfo.EnglishName)))
                {
                    Countries.Add(getRegionInfo.EnglishName);
                }
            }
            Countries.Sort();
        }
    }

    public enum GenderDto
    {
        Male=2,
        Female=0
    }
}
