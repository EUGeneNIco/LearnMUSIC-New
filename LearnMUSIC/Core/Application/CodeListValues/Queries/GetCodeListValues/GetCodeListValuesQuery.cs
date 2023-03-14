using LearnMUSIC.Core.Application.CodeListValues.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearnMUSIC.Core.Application.CodeListValues.Queries.GetCodeListValues
{
    public class GetCodeListValuesQuery : IRequest<IEnumerable<CodeListValueDto>>
    {
        public string Type { get; set; }
    }
}
