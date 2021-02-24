using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.ViewModels;

namespace WebStore.ViewModels
{
    public record SelectableSectionViewModel(IEnumerable<SectionViewModel> Sections, int? SectionId, int? ParentSectionId)
    {
        //public IEnumerable<SectionViewModel> Sections { get; set; }

        //public int? SectionId { get; set; }

        //public  int? ParentSectionId { get; set; }

    }
}
