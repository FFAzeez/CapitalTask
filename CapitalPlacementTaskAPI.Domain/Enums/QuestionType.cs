using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.Enums
{
    public enum QuestionType
    {
        Paragraph, 
        ShortAnswer, 
        YesNo, 
        Dropdown, 
        MultipleChoice, 
        Date, 
        Number, 
        FileUpload,
        VideoQuestion
    }
}
